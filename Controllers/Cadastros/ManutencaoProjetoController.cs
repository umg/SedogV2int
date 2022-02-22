using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.Cadastros
{
    public class ManutencaoProjetoController : Controller
    {
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            return View(provider.SEL_PLPROJETOS(""));
        }
        [ActionFilter_CheckLogin]
        public ActionResult Edit(long id)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            ManutencaoProjetoViewModel ret = new ManutencaoProjetoViewModel();
            ret.plProjetoSedog = provider.SEL_PL_PROJETO_SEDOG(id);

            foreach (var plprj in ret.plProjetoSedog)
            {
                ret.idProjSedog = id;   
                ret.Artista = plprj.ARTISTA;
                ret.Projeto = plprj.PROJETO;
                ret.IdArtista = plprj.IDARTISTA;
                ret.LancamentoProduto = plprj.DATA_LANCAMENTO;
                ret.IdTipoContrato = plprj.ID_TIPO_CONTRATO;
                ret.IdTipoRelease = plprj.ID_TIPO_RELEASE;
                ret.IdTipoProcesso = plprj.ID_TIPO_PROCESSO;
                ret.Obs = plprj.OBS;
                ret.Nacionalidade = plprj.NACIONALIDADE;
                ret.Responsavel = plprj.LOGIN_RESPONSAVEL;
                ret.EBITDAProjetado = plprj.EBITIDA_PROJETADO;
                ret.RECEITAProjetado = plprj.RECEITA_PROJETADA;
                ret.IdGeneroMusical = plprj.ID_GENERO_MUSICAL;
                ret.IdMedidorKPI = plprj.ID_MEDIDOR_KPI;
            }

            ret._TipoContrato = provider.SEL_PL_TIPO_CONTRATO();
            ret._TipoProcesso = provider.SEL_PL_TIPO_PROCESSO();
            ret._TipoRelease = provider.SEL_PL_TIPO_RELEASE();
            ret._TipoReceita = provider.SEL_PL_TIPO_RECEITA();
            ret._TipoGeneroMusical = provider.SEL_GENERO_MUSICAL();

            ret.plProjeto = provider.SEL_PL_PROJETO(id);
            ret.plProduto = provider.SEL_PL_PRODUTO(id);
            ret.plProdutoBus = provider.SEL_PL_PRODUTO_BUS(id);
            ret.plReceitasExtras = provider.SEL_PL_RECEITAS_EXTRAS(id);
            ret.plParametrosDireitos = provider.SEL_PL_PARAMETROS_DIREITOS(id);
            ret._Usuarios = provider.SEL_USUARIOS_POR_DEPTO("#IT##FINANCE#");
            ret._MedidorKPI = provider.SEL_PL_MEDIDOR_KPI();

            return View(ret);
        }
        [ActionFilter_CheckLogin]
        public ActionResult Delete(long id)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            provider.DEL_PRJ_SEDOG(id);
            return RedirectToAction("Index");
        }
        [ActionFilter_CheckLogin]
        public ActionResult RemoveProjetoDigital(long id, string idProjDigital)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            provider.DEL_PL_PROJETO(id, idProjDigital, "");
            return RedirectToAction("Edit", new { @id = id });
        }
        [ActionFilter_CheckLogin]
        public ActionResult AddProjetoDigital(long id, string idArt)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            List<ProjetosBrdigitalPorArtistCode> ret = provider.SLT_PROJETOS_POR_ARTISTA(idArt);
            //return View(ret.Where(d => d.ASSOCIADO == "").ToList());
            return View(ret);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult AddProjetoDigital(FormCollection collection)
        {
            if (collection["chkSelProjetos[]"] != null)
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                long idProjSedog = long.Parse(collection["idProjSedog"]);
                foreach (string prjs in collection["chkSelProjetos[]"].Split(','))
                {
                    var prj = prjs.Split('|');
                    if (prj.Length > 1)
                        provider.INS_PL_PROJETO(idProjSedog, prj[0].Trim(), prj[1].Trim());
                }

                // atualiza titulos dos fonogramas dos projetos
                Helpers.functions.ISRCSummaryUpdate(idProjSedog);

            }
            return RedirectToAction("Edit", new { id = collection["idProjSedog"] });
        }
        [ActionFilter_CheckLogin]
        public ActionResult RemoveProdutoFisico(long id, string codprod, string packing)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            provider.DEL_PRODUTO(id, codprod, packing);
            return RedirectToAction("Edit", new { id = id });
        }

        [ActionFilter_CheckLogin]
        public ActionResult EditProdutoFisico(int id, string codprod, string packing)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            ParametrosDireitosProduto _parametros = new ParametrosDireitosProduto();
            _parametros = provider.SEL_PARAMETROS_DIREITOS_PRODUTO(id, codprod, packing);
            return View(_parametros);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult EditProdutoFisico(FormCollection collection)
        {
            PLProjetoProvider provider = new PLProjetoProvider();

            int IDProj_SEDOG = Convert.ToInt32(collection["idProjSedog"]);
            string CodProduto = collection["codprod"];
            string Packing = collection["packing"];
            
            decimal artistico = 0;
            decimal autoral = 0;
            decimal producer = 0;
            decimal other = 0;

            bool tryartistico = decimal.TryParse(collection["artistico"], out artistico);
            bool tryautoral = decimal.TryParse(collection["autoral"], out autoral);
            bool tryproducer = decimal.TryParse(collection["producer"], out producer);
            bool tryother = decimal.TryParse(collection["other"], out other);

            if (tryartistico && tryautoral)
            {
                provider.INS_PARAMETROS_DIREITOS_PRODUTO(IDProj_SEDOG, CodProduto, Packing, artistico, autoral, producer, other);
                ViewBag.Error = "Parâmetros salvos com sucesso!";
            }
            else
            {
                ViewBag.Error = "Ocorreu um erro na hora de salvar os valores";
            }

            return RedirectToAction("Edit", new { id = collection["idProjSedog"] });
        }


        [ActionFilter_CheckLogin]
        public ActionResult AddProdutoFisico(long id, string artista)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            List<ProdutoFisico> ret = provider.SEL_PRODUTO_FISICO_POR_ARTISTA(artista);
            return View(ret.Where(d => d.ASSOCIADO == "").ToList());
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult AddProdutoFisico(FormCollection collection)
        {
            if (collection["chkSelProdutos[]"] != null)
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                long idProjSedog = long.Parse(collection["idProjSedog"]);
                foreach (string prjs in collection["chkSelProdutos[]"].Split(','))
                {
                    var prj = prjs.Split('|');
                    if (prj.Length > 1)
                        provider.INS_PRODUTO(idProjSedog, prj[0].Trim(), prj[1].Trim());
                }
            }
            return RedirectToAction("Edit", new { id = collection["idProjSedog"] });
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult AddBU(FormCollection collection)
        {
            if (collection["BU"] != "")
            {
                string codprod = collection["cod_prod"];
                string packing = collection["packing"];
                if (codprod == "") codprod = "0";
                if (packing == "") packing = "00";
                //if (Convert.ToInt32(packing) <= 9) packing = "0" + packing;
                if (packing.Length < 2) packing = packing.Trim().PadLeft(2, '0');

                PLProjetoProvider provider = new PLProjetoProvider();
                long idProjSedog = long.Parse(collection["idProjSedog"]);
                provider.INS_PRODUTO_BUS(idProjSedog, codprod, packing, collection["BU"].Trim().PadLeft(12, ' '));
            }
            return RedirectToAction("Edit", new { id = collection["idProjSedog"] });
        }
        [ActionFilter_CheckLogin]
        public ActionResult DelBU(long id, string codprod, string packing, string BU)
        {
            if (BU != "")
            {
                if (codprod == "") codprod = "0";
                if (packing == "") packing = "0";
                PLProjetoProvider provider = new PLProjetoProvider();
                provider.DEL_PRODUTO_BUS(id, codprod, packing, BU.Trim().PadLeft(12, ' '));
            }
            return RedirectToAction("Edit", new { @id = id });
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult EditDetalhesProjeto(FormCollection collection)
        {
            if (collection["idProjSedog"] != "")
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                long idProjetoSedog = long.Parse(collection["idProjSedog"]);
                string nomeDoProjeto = collection["nomeDoProjeto"];
                string nomeDoArtista = collection["nomeDoArtista"];
                string idArtista = collection["idArtista"];
                string obs = collection["obs[]"].Replace(",", " + ");
                string gos = collection["GOS"];

                string responsavel = collection["selResponsavel"];
                int idGeneroMusical = int.Parse(collection["selGeneroMusical"]);
                int tipoContrato = int.Parse(collection["selTipoContrato"]);
                int tipoRelease = int.Parse(collection["selTipoRelease"]);
                int tipoProcesso = int.Parse(collection["selTipoProcesso"]);
                DateTime dtAtual = DateTime.Now;
                DateTime.TryParse(collection["lancamento"], out dtAtual);
                string lancamento = dtAtual.ToString("yyyy-MM-dd");
                string origem = collection["selOrigem"];
                if (origem == "NAT") origem = "NAC";

                if (gos == "GOS") origem = "GOS";

                string percAut = collection["percAut"];
                string percArt = collection["percArt"];
                string percOther = collection["percOther"];
                string percProdu = collection["percProdu"];

                int idMedidorKPI = int.Parse(collection["selIdMedidorKPI"]);

                decimal dpercAut = 0;
                decimal dpercArt = 0;
                decimal dpercOther = 0;
                decimal dpercProdu = 0;
                decimal ebtidaProjetada = 0;
                decimal receitaProjetada = 0;

                decimal.TryParse(percAut, out dpercAut);
                decimal.TryParse(percArt, out dpercArt);
                decimal.TryParse(percOther, out dpercOther);
                decimal.TryParse(percProdu, out dpercProdu);

                decimal.TryParse(collection["ebtidaProjetada"], out ebtidaProjetada);
                decimal.TryParse(collection["receitaProjetada"], out receitaProjetada);

                if (string.IsNullOrEmpty(responsavel)) responsavel = "";

                provider.UPD_PL_PROJETOS_SEDOG(idProjetoSedog, nomeDoProjeto, idArtista, nomeDoArtista, obs, tipoContrato, tipoProcesso, tipoRelease, lancamento, origem, ebtidaProjetada, receitaProjetada, responsavel, idGeneroMusical , idMedidorKPI);

                provider.UPD_PL_PARAMETROS_DIREITOS(idProjetoSedog, dpercArt, dpercAut, dpercOther, dpercProdu);

                // atualiza titulos dos fonogramas dos projetos
                Helpers.functions.ISRCSummaryUpdate(idProjetoSedog);
            }
            return RedirectToAction("Edit", new { id = collection["idProjSedog"] });
        }
        [ActionFilter_CheckLogin]
        public ActionResult AddReceitaExtra(FormCollection collection)
        {
            if (collection["idProjSedog"] != "")
            {
                long idProjSedog = long.Parse(collection["idProjSedog"]);
                int idTipoReceita = int.Parse(collection["selTipoReceita"]);

                decimal valor = 0;
                decimal.TryParse(collection["valor"], out valor);

                decimal despesa = 0;
                decimal.TryParse(collection["despesa"], out despesa);

                DateTime data = DateTime.Parse(collection["data"]);

                PLProjetoProvider provider = new PLProjetoProvider();
                provider.INS_PL_OUTRAS_RECEITAS(idProjSedog, idTipoReceita, valor, (data.Year * 100) + data.Month, despesa);
            }
            return RedirectToAction("Edit", new { id = collection["idProjSedog"] });
        }
        [ActionFilter_CheckLogin]
        public ActionResult RemoveReceitaExtra(long id, int idTipoReceita, decimal valor)
        {
            if (id != 0)
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                provider.DEL_PL_OUTRAS_RECEITAS(id, idTipoReceita, valor);
            }
            return RedirectToAction("Edit", new { @id = id });
        }
    }
}