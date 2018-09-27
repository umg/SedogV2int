using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEDOGv2.Controllers.Relatorios
{
    public class VendasDigitaisPorFaixaController : Controller
    {
        // GET: VendasDigitaisPorFaixa
        public ActionResult Index()
        {
            ViewBag.Mes = DateTime.Now.Month.ToString().PadLeft(2, '0');
            ViewBag.Ano = DateTime.Now.Year.ToString().PadLeft(2, '0');
            return View();
        }

        [ActionFilter_CheckLogin]
        public JsonResult ISRCLookup(string term)
        {

            PLProjetoProvider provider = new PLProjetoProvider();
            List<ArtistaProjeto> ret = provider.SLT_ARTISTA_PROJETO(term);

            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {

            List<VendasDigitaisPorFaixa> _viewModel = new List<VendasDigitaisPorFaixa>();
            PLProjetoProvider _content = new PLProjetoProvider();

            try
            {
                string isrc = collection["nomeISRC"];
                int anomes = int.Parse(string.Concat(collection["ano"], collection["mes"]));

                _viewModel = _content.SEL_VENDAS_DIGITAIS_POR_FAIXA(isrc, anomes);
                ViewBag.Mes = collection["mes"];
                ViewBag.Ano = collection["ano"];
            }
            catch (Exception ex)
            {
                ViewBag.Message = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, string.Concat("Ocorreu um erro na hora de processar a solicitação: ", ex.Message));
            }

            return View(_viewModel);
        }
    }
}