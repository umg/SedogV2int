using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.Relatorios
{
    public class VendasDigitaisJVController : Controller
    {
        // GET: VendasDigitaisJV
        public ActionResult Index()
        {
            return View();
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {

            List<VendasDigitaisJVModel> _viewModel = new List<VendasDigitaisJVModel>();
            PLProjetoProvider _content = new PLProjetoProvider();

            try
            {
                int anomes = int.Parse(string.Concat(collection["ano"], collection["mes"]));

                _viewModel = _content.SEL_VENDAS_DIGITAIS_JV_POR_USAGETYPE(anomes);
                ViewBag.Mes = collection["mes"];
                ViewBag.Ano = collection["ano"];
            }
            catch(Exception ex)
            {
                ViewBag.Message = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, string.Concat("There was an error processing the request: ", ex.Message));
            }

            return View(_viewModel);
        }

    }
}