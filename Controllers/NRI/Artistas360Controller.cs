using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using System.IO;

namespace SEDOGv2.Controllers.NRI
{
    public class Artistas360Controller : Controller
    {
        // GET: Artistas360
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            List<NRIProjetos360ViewModel> model = new List<NRIProjetos360ViewModel>();
            try
            {
                model = provider.SEL_NRI_PROJETOS();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }
        [ActionFilter_CheckLogin]
        public ActionResult Edit(long id)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            NRIProjetos360ViewModel model = new NRIProjetos360ViewModel();
            try
            {
                model = provider.SEL_NRI_PROJETO(id);
                model.CONTRATOS = provider.SEL_NRI_PROJETO_CONTRATO(id);
                model.RECEITAS = provider.SEL_NRI_PROJETO_RECEITA(id);
                model.ANEXOS = provider.SEL_NRI_PROJETOS_ANEXOS(id);

                ViewBag.NRITipoReceita = provider.SEL_NRI_TIPO_RECEITA();
                ViewBag.NRITipoContrato = provider.SEL_NRI_TIPO_CONTRATO();
                ViewBag.NRIResponsavel = provider.SEL_NRI_RESPONSAVEL();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            NRIProjetos360ViewModel model = new NRIProjetos360ViewModel();
            try
            {
                long idProjetoNRI = long.Parse(collection["idProjNri"]);
                model = provider.SEL_NRI_PROJETO(idProjetoNRI);
                model.CONTRATOS = provider.SEL_NRI_PROJETO_CONTRATO(idProjetoNRI);
                model.RECEITAS = provider.SEL_NRI_PROJETO_RECEITA(idProjetoNRI);

                ViewBag.NRITipoReceita = provider.SEL_NRI_TIPO_RECEITA();
                ViewBag.NRITipoContrato = provider.SEL_NRI_TIPO_CONTRATO();
                ViewBag.NRIResponsavel = provider.SEL_NRI_RESPONSAVEL();                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult EditDetalhes360(FormCollection collection)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            long idProjNri = long.Parse(collection["idProjNri"]);
            string idArtista = collection["idArtista"];
            
            provider.UPD_NRI_PROJETOS(idProjNri, idArtista, collection["nomeDoArtista"], collection["nomeDoProjeto"], collection["buProjeto"], collection["lancamento"], collection["obs"], collection["expiracao"]);

            return RedirectToAction("Edit", new { @id = idProjNri });
        }
        [ActionFilter_CheckLogin]
        public ActionResult RemoveContrato(long id, long idTipoContrato)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            provider.DEL_NRI_CONTRATO(id, idTipoContrato);
            return RedirectToAction("Edit", new { @id = id });
        }
        [ActionFilter_CheckLogin]
        public ActionResult RemoveReceita(long id, long idTipoReceita, long idseq)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            provider.DEL_NRI_RECEITA(id, idTipoReceita, idseq);
            return RedirectToAction("Edit", new { @id = id });
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult AddReceita(FormCollection collection)
        {
            int intOut;
            decimal decOut;

            PLProjetoProvider provider = new PLProjetoProvider();
            long idProjNri = long.Parse(collection["idProjNri"]);
            long idReceita = long.Parse(collection["receita"]);
            long idResponsavel = long.Parse(collection["responsavel"]);
            int emailCobranca = collection["emailCobranca"] == null ? 0 : 1;

            decOut = 0;
            decimal.TryParse(collection["percent"], out decOut);
            decimal percent = decOut;

            intOut = 0;
            int.TryParse(collection["quantidade"], out intOut);
            int quantidade = intOut;
            intOut = 0;
            int.TryParse(collection["saldo"], out intOut);
            int saldo = intOut;

            provider.INS_NRI_PROJETOS_RECEITA(idProjNri, idReceita, idResponsavel, percent, collection["acordo"], quantidade, saldo, collection["obs"], emailCobranca);

            return RedirectToAction("Edit", new { @id = idProjNri });
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult AddContrato(FormCollection collection)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            long idProjNri = long.Parse(collection["idProjNri"]);
            long idContrato = long.Parse(collection["contrato"]);
            

            provider.INS_NRI_PROJETOS_CONTRATO(idProjNri, idContrato, collection["descricao"]);

            return RedirectToAction("Edit", new { @id = idProjNri });
        }
        [ActionFilter_CheckLogin]
        public ActionResult Delete(long id)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            provider.DEL_NRI_PROJETO(id);
            return RedirectToAction("Index");
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult AddProjeto(FormCollection collection)
        {
            try
            {
                DateTime dt = DateTime.Now;

                PLProjetoProvider provider = new PLProjetoProvider();
                string artista = collection["nomeDoArtista"];
                if (artista == string.Empty)
                    artista = collection["typeAheadArtista"];
                string projeto = collection["project"];
                string obs = collection["obs"];
                string bu = collection["bu"];

                DateTime.TryParse(collection["lancamento"], out dt);
                DateTime lancamento = dt;

                long idArtista = 0;
                long.TryParse(collection["idArtista"], out idArtista);

                provider.INS_NRI_PROJETOS(idArtista, artista, projeto, bu, lancamento, obs);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Index");

        }
        [ActionFilter_CheckLogin]
        public ActionResult HistoricoNRICobranca(long id)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            List<NRIHsitoricoCobrancaViewModel> model = new List<NRIHsitoricoCobrancaViewModel>();
            try
            {
                model = provider.SEL_NRI_HISTORICO_COBRANCAS(id);
                ViewBag.Receita = provider.SEL_NRI_PROJETO_RECEITA_POR_SEQ(id);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult HistoricoNRICobranca(FormCollection collection)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            List<NRIHsitoricoCobrancaViewModel> model = new List<NRIHsitoricoCobrancaViewModel>();

            long id_seq= long.Parse(collection["ID_SEQ"]);
            string contato =collection["CONTATO"];
            string comentarios =collection["COMENTARIOS"];
            decimal valor = decimal.Parse(collection["VALOR"]);
            string data = collection["DATA"];

            try
            {
                provider.INS_NRI_HISTORICO_COBRANCAS(id_seq, contato, comentarios, valor, data);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("HistoricoNRICobranca", new { @id = id_seq });
        }
        [ActionFilter_CheckLogin]
        public ActionResult HistoricoNRICobrancaFechar(long id, long id_seq)
        {
            PLProjetoProvider provider = new PLProjetoProvider();

            try
            {
                provider.UPD_NRI_HISTORICO_COBRANCAS(id);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("HistoricoNRICobranca", new { @id = id_seq });
        }
        [ActionFilter_CheckLogin]
        public ActionResult HistoricoNRICobrancaApagar(long id, long id_seq)
        {
            PLProjetoProvider provider = new PLProjetoProvider();

            try
            {
                provider.DEL_NRI_HISTORICO_COBRANCAS(id);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("HistoricoNRICobranca", new { @id = id_seq });
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult AddAnexoProjeto(FormCollection collection)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            long idProjNri = long.Parse(collection["idProjNri"]);
            string obs = collection["obs"];

            var file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                
                var fileName = Path.GetFileName(file.FileName);
                var dir = Server.MapPath("~/Anexos/" + idProjNri.ToString() + "/");
                var path = Path.Combine(dir, fileName);

                if (!System.IO.Directory.Exists(dir))
                    System.IO.Directory.CreateDirectory(dir);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                file.SaveAs(path);
            }

            provider.INS_NRI_PROJETOS_ANEXOS(idProjNri, file.FileName, collection["obs"]);

            return RedirectToAction("Edit", new { @id = idProjNri });
        }
        [ActionFilter_CheckLogin]
        public ActionResult RemoveAnexoProjeto(long id, string fileName)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            long idProjNri = id;
            var dir = Server.MapPath("~/Anexos/" + idProjNri.ToString() + "/");
            var path = Path.Combine(dir, fileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            provider.DEL_NRI_PROJETOS_ANEXOS(idProjNri, fileName);

            return RedirectToAction("Edit", new { @id = idProjNri });
        }
        [ActionFilter_CheckLogin]
        public ActionResult HistoricoNRIShows(long id)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            List<NRIHsitoricoShowViewModel> model = new List<NRIHsitoricoShowViewModel>();
            try
            {
                model = provider.SEL_NRI_HISTORICO_SHOWS(id);
                ViewBag.Receita = provider.SEL_NRI_PROJETO_RECEITA_POR_SEQ(id);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult HistoricoNRIShows(FormCollection collection)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            List<NRIHsitoricoShowViewModel> model = new List<NRIHsitoricoShowViewModel>();

            long id_seq = long.Parse(collection["ID_SEQ"]);
            string obs = collection["OBS"];
            decimal valor = decimal.Parse(collection["VALOR"]);
            string data = collection["DATA"];

            try
            {
                provider.INS_NRI_HISTORICO_SHOWS(id_seq, valor, obs, data);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("HistoricoNRIShows", new { @id = id_seq });
        }
        public ActionResult HistoricoNRIShowsApagar(long id, long id_seq)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            try
            {
                provider.DEL_NRI_HISTORICO_SHOWS(id);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("HistoricoNRIShows", new { @id = id_seq });
        }
        [ActionFilter_CheckLogin]
        public ActionResult HistoricoNRICobrancaReceita(long id)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            List<NRIHsitoricoCobrancaViewModel> model = new List<NRIHsitoricoCobrancaViewModel>();
            try
            {
                model = provider.SEL_NRI_HISTORICO_COBRANCAS(id);
                ViewBag.Shows = provider.SEL_NRI_HISTORICO_SHOWS(id);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }
        public ActionResult MudaEmailAlertaCobranca(long id, long ID_SEQ)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            try
            {
                provider.UPD_NRI_ALERTA_EMAIL(ID_SEQ);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Edit", new { @id = id });
        }
    }
}