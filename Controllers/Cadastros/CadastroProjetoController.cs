using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.Adm
{
    public class CadastroProjetoController : Controller
    {
        //
        // GET: /CadastroProjeto/
        
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            CadastroProjetoViewModel model = new CadastroProjetoViewModel();

            model._TipoContrato = provider.SEL_PL_TIPO_CONTRATO();
            model._TipoProcesso= provider.SEL_PL_TIPO_PROCESSO();
            model._TipoRelease = provider.SEL_PL_TIPO_RELEASE();

            return View(model);
        }
        [ActionFilter_CheckLogin]
        public JsonResult ArtistaLookup(string term)
        {
#if DEBUG
            //àrea de teste para diminuir tempo de consulta ao banco.
            List<ArtistaProjeto> ret = new List<ArtistaProjeto>
            {
                new ArtistaProjeto() {ARTISTNAME="PauloDiniz" , IDARTISTA="20000087011"},
                new ArtistaProjeto() {ARTISTNAME="GiorgosZampetas" , IDARTISTA="20000360358"},
                new ArtistaProjeto() {ARTISTNAME="Rihanna" , IDARTISTA="30023286567"},
                new ArtistaProjeto() {ARTISTNAME="Various Artists" , IDARTISTA="20000023370"},
                new ArtistaProjeto() {ARTISTNAME="DanielBalavoine" , IDARTISTA="20000049584"},
                new ArtistaProjeto() {ARTISTNAME="FreddieMcGregor" , IDARTISTA="20000101626"},
                new ArtistaProjeto() {ARTISTNAME="KarelGott" , IDARTISTA="20000108200"},
                new ArtistaProjeto() {ARTISTNAME="The Rolling Stones" , IDARTISTA="20000155169"},
                new ArtistaProjeto() {ARTISTNAME="Zed" , IDARTISTA="20000163124"},
                new ArtistaProjeto() {ARTISTNAME="ReinhardMey" , IDARTISTA="20000142809"},
                new ArtistaProjeto() {ARTISTNAME="S.T.S" , IDARTISTA="20000030356"},
                new ArtistaProjeto() {ARTISTNAME="Boyzone" , IDARTISTA="20000060814"},
                new ArtistaProjeto() {ARTISTNAME="Tennessee ErnieFord" , IDARTISTA="20000094957"},
                new ArtistaProjeto() {ARTISTNAME="EddieHolman" , IDARTISTA="20000182867"},
                new ArtistaProjeto() {ARTISTNAME="Sérgio Mendes Trio" , IDARTISTA="20000232011"},
                new ArtistaProjeto() {ARTISTNAME="The Melos Ensemble Of London" , IDARTISTA="20000094265"},
                new ArtistaProjeto() {ARTISTNAME="MarcAntoine" , IDARTISTA="20000251189"},
                new ArtistaProjeto() {ARTISTNAME="FerryCorsten" , IDARTISTA="20000260130"},
                new ArtistaProjeto() {ARTISTNAME="birdpaula" , IDARTISTA="30358752740"},
                new ArtistaProjeto() {ARTISTNAME="mewithoutYou" , IDARTISTA="30735667790"},
                new ArtistaProjeto() {ARTISTNAME="MollyJohnson" , IDARTISTA="40054321951"},
                new ArtistaProjeto() {ARTISTNAME="StevieWonder" , IDARTISTA="20000159471"},
                new ArtistaProjeto() {ARTISTNAME="Bliss" , IDARTISTA="31503131574"},
                new ArtistaProjeto() {ARTISTNAME="Telegram" , IDARTISTA="40038019154"},
                new ArtistaProjeto() {ARTISTNAME="Musica Antiqua Köln" , IDARTISTA="20000024761"},
                new ArtistaProjeto() {ARTISTNAME="NaraLeão" , IDARTISTA="20000027780"},
                new ArtistaProjeto() {ARTISTNAME="AmyGrant" , IDARTISTA="20000040941"},
                new ArtistaProjeto() {ARTISTNAME="WillieBobo" , IDARTISTA="20000058882"},
                new ArtistaProjeto() {ARTISTNAME="The Master Drummers of Dagbon" , IDARTISTA="31692335291"},
                new ArtistaProjeto() {ARTISTNAME="Vinícius deMoraes" , IDARTISTA="20000024835"},
                new ArtistaProjeto() {ARTISTNAME="MaurizioPollini" , IDARTISTA="20000141524"},
                new ArtistaProjeto() {ARTISTNAME="MichaelTilson Thomas" , IDARTISTA="20000143125"},
                new ArtistaProjeto() {ARTISTNAME="Pixinguinha" , IDARTISTA="20000151521"},
                new ArtistaProjeto() {ARTISTNAME="Alamir", IDARTISTA="30023394095" } ,
                new ArtistaProjeto() {ARTISTNAME="Simone & Simaria", IDARTISTA="31799642302" },
                new ArtistaProjeto() {ARTISTNAME="JonasVilar", IDARTISTA="31498692873" },
                new ArtistaProjeto() {ARTISTNAME="J. Balvin", IDARTISTA="30493972162" },
                new ArtistaProjeto() {ARTISTNAME="Molotov", IDARTISTA="20000296965" }
            };

            ret = ret.Where(d => d.ARTISTNAME.ToUpper().Contains(term.ToUpper())).ToList();
#else
            PLProjetoProvider provider = new PLProjetoProvider();
            List<ArtistaProjeto> ret = provider.SLT_ARTISTA_PROJETO(term);            
#endif
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        [ActionFilter_CheckLogin]
        public JsonResult ProjectsByArtistCodeBrDigital(string artistcode)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            List<ProjetosBrdigitalPorArtistCode> ret = provider.SLT_PROJETOS_POR_ARTISTA(artistcode, 1);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        [ActionFilter_CheckLogin]
        public JsonResult ProductsByArtistBrDigital(string artist)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            List<ProdutoFisico> ret = provider.SEL_PRODUTO_FISICO_POR_ARTISTA(artist);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult CreateProject(FormCollection collection)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            RespostaCadastroProjetoViewModel model = new RespostaCadastroProjetoViewModel();
            model.Includes = new List<Resposta<string>>();
            string StepMessage = "Início";
            try
            {
                CadastroProjetoViewModel cadastroVM = new CadastroProjetoViewModel();
                StepMessage = "Seleciona tipo contrato";
                cadastroVM._TipoContrato = provider.SEL_PL_TIPO_CONTRATO();
                StepMessage = "Seleciona tipo processo";
                cadastroVM._TipoProcesso = provider.SEL_PL_TIPO_PROCESSO();
                StepMessage = "Seleciona tipo release";
                cadastroVM._TipoRelease = provider.SEL_PL_TIPO_RELEASE();


                StepMessage = "Carrega idArtista";
                string idArtista = collection["idArtista"];

                StepMessage = "Carrega projetos";
                string[] _selectedProjects = collection["selectedProjects"].Split('|');
                StepMessage = "Carrega produtos";
                string[] _selectedProducts = collection["selectedProducts"].Split('|');
                StepMessage = "Carrega bus extras";
                string[] _busExtras = collection["busExtras"].Split('|');
                StepMessage = "Carrega NomeProjeto";
                string nomeDoProjeto = collection["nomeDoProjeto"];
                StepMessage = "Carrega NomeArtista";
                string nomeDoArtista = collection["nomeDoArtista"];
                StepMessage = "Carrega Bus Adicionais";
                string[] _busAdicionaisProjeto = collection["busAdicionaisProjeto"].Split(',');
                StepMessage = "Carrega Obs";
                string obs = collection["obs[]"].Replace(",", " + ");
                StepMessage = "Carrega Origem";
                string origem = collection["selOrigem"];
                StepMessage = "Carrega TipoContrato";
                int tipoContrato = int.Parse(collection["selTipoContrato"]);
                StepMessage = "Carrega TipoRelease";
                int tipoRelease = int.Parse(collection["selTipoRelease"]);
                StepMessage = "Carrega TipoProcesso";
                int tipoProcesso = int.Parse(collection["selTipoProcesso"]);

                StepMessage = "Carrega PercentualAutoral";
                string percAut = collection["percAut"];
                StepMessage = "Carrega PercentualArtistico";
                string percArt = collection["percArt"];

                StepMessage = "Carrega Gospel";
                string gos = collection["GOS"];

                decimal dpercAut = 0;
                decimal dpercArt = 0;

                StepMessage = "Verifica PercentualAutoral";
                decimal.TryParse(percAut, out dpercAut);
                StepMessage = "Verifica PercentualArtistico";
                decimal.TryParse(percArt, out dpercArt);

                if (origem == "NAT") origem = "NAC";
                if (gos == "GOS") origem = "GOS";

                List<string> selectedProjects = new List<string>();
                List<string> selectedProducts = new List<string>();
                List<string> busAdicionaisProjeto = new List<string>();
                List<string> busExtras = new List<string>();

                if (_selectedProjects.Length > 0)
                    selectedProjects = _selectedProjects.ToList();
                if (_selectedProducts.Length > 0)
                    selectedProducts = _selectedProducts.ToList();
                if (_busAdicionaisProjeto.Length > 0)
                    busAdicionaisProjeto = _busAdicionaisProjeto.ToList();
                if (_busExtras.Length > 0)
                    busExtras = _busExtras.ToList();

                long idProjetoSedog = 0;
                DateTime dtAtual = DateTime.Now;

                StepMessage = "Verifica DataLançamento";
                DateTime.TryParse(collection["lancamento"], out dtAtual);

                StepMessage = "DataLançamento Para Texto";
                string lancamento = dtAtual.ToString("yyyy-MM-dd");

                StepMessage = "INS_PL_PROJETOS_SEDOG";
                idProjetoSedog = provider.INS_PL_PROJETOS_SEDOG(idProjetoSedog, nomeDoProjeto, idArtista, nomeDoArtista, obs, tipoContrato, tipoProcesso, tipoRelease, lancamento, origem);

                model.IdProjetoSedog = idProjetoSedog.ToString();
                model.NomeDoProjeto = nomeDoProjeto;
                model.IdArtista = idArtista;
                model.NomeDoArtista = nomeDoArtista;
                model.Obs = obs; 
                model.TipoContrato = cadastroVM._TipoContrato.Where(d => d.ID_TIPO_CONTRATO == tipoContrato.ToString()).First().TIPO_CONTRATO;
                model.TipoProcesso = cadastroVM._TipoProcesso.Where(d => d.ID_TIPO_PROCESSO== tipoContrato.ToString()).First().TIPO_PROCESSO;
                model.TipoRelease = cadastroVM._TipoRelease.Where(d => d.ID_TIPO_RELEASE== tipoContrato.ToString()).First().TIPO_RELEASE;
                model.Lancamento = lancamento;
                model.Origem = origem;


                if (idProjetoSedog != 0)
                {
                    foreach (string project in selectedProjects)
                    {
                        if (project != "")
                        {
                            string[] prj = project.Split(';');
                            StepMessage = "INS_PL_PROJETO: " + project;
                            model.Includes.Add(provider.INS_PL_PROJETO(idProjetoSedog, prj[0].Trim(), prj[1].Trim()));
                        }
                    }

                    foreach (string product in selectedProducts)
                    {
                        if (product != "")
                        {
                            string[] prd = product.Split(';');
                            StepMessage = "INS_PRODUTO: " + product;
                            model.Includes.Add(provider.INS_PRODUTO(idProjetoSedog, prd[0].Trim(), prd[1].Trim()));
                        }
                    }
                    foreach (string prdbu in busExtras)
                    {
                        if (prdbu != "")
                        {
                            string[] prd = prdbu.Split(';');
                            if (prd.Length > 1)
                            {
                                if (prd[2] != "")
                                {
                                    string[] bus = prd[2].Split(',');
                                    foreach (string bu in bus)
                                    {
                                        if (bu != "")
                                        {
                                            StepMessage = "INS_PRODUTO_BUS: " + prdbu;
                                            model.Includes.Add(provider.INS_PRODUTO_BUS(idProjetoSedog, prd[0].Trim(), prd[1].Trim(), bu.Trim().PadLeft(12, ' ')));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    foreach (string buextra in busAdicionaisProjeto)
                    {
                        if (buextra != "")
                        {
                            StepMessage = "INS_PRODUTO_BUS: " + buextra;
                            model.Includes.Add(provider.INS_PRODUTO_BUS(idProjetoSedog, "0", "0", buextra.Trim().PadLeft(12, ' ')));
                        }
                    }
                    StepMessage = "INS_PL_PARAMETROS_DIREITOS: " + dpercArt.ToString() + " | " + dpercAut.ToString();
                    model.Includes.Add(provider.INS_PL_PARAMETROS_DIREITOS(idProjetoSedog, dpercArt, dpercAut));

                    // atualiza titulos dos fonogramas dos projetos
                    Helpers.functions.ISRCSummaryUpdate(idProjetoSedog);
                }
            }
            catch (Exception ex){
                Resposta<string> err = new Resposta<string>();
                err.Dados = "Erro ao processar iformação: " + StepMessage;
                err.Error = ex.Source;
                err.Message = ex.Message;
                model.Includes.Add(err);
            }
            return View(model);
        }

        [ActionFilter_CheckLogin]
        public ActionResult ProcuraArtistaPorProduto()
        {
            return View();
        }
        [ActionFilter_CheckLogin]
        public ActionResult ProcuraPorProduto()
        {
            return View();
        }
        [ActionFilter_CheckLogin]
        public ActionResult ProcuraPorProjetoPorISRC()
        {
            return View();
        }
        [ActionFilter_CheckLogin]
        public JsonResult ListaProjetoPorISRC(string term)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            List<ProjetoTotalViewModel> dados = provider.SEL_PROJETO_POR_ISRCS(term);
            JsonDataHeader data = new JsonDataHeader();
            data.data = dados;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [ActionFilter_CheckLogin]
        public JsonResult ListaArtistaPorProduto(string term)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            List<ArtistaProjeto> dados = provider.SLT_PROJETO_ARTISTA(term);
            JsonDataHeader data = new JsonDataHeader();
            data.data = dados;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [ActionFilter_CheckLogin]
        public JsonResult ListaProduto(string term)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            List<ProdutoFisico> dados = provider.SEL_PRODUTO_FISICO_BY_ANY(term);
            JsonDataHeader data = new JsonDataHeader();
            data.data = dados.Where(d => d.ASSOCIADO == "").ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
    
}
