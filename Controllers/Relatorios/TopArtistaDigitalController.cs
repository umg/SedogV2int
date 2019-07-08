using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.Relatorios
{
    public class TopArtistaDigitalController : Controller
    {
        // GET: TopParceirosDigital
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            List<TopArtistaDigitalViewModel> viewModel = new List<TopArtistaDigitalViewModel>();
            ViewBag.Message = "";
            ViewBag.Mes = "";
            ViewBag.Ano = "";
            return View(viewModel);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            List<TopArtistaDigitalViewModel> viewModel = new List<TopArtistaDigitalViewModel>();
            try
            {
                ViewBag.Message = "";
                ViewBag.Mes = collection["mes"] == "0" ? "Todos" : collection["mes"];
                ViewBag.Ano = collection["ano"] == "0" ? "Todos" : collection["ano"];
                PLProjetoProvider provider = new PLProjetoProvider();
                viewModel = provider.SLT_TOP_ARTISTA_DIGITAL(int.Parse(collection["ano"]), int.Parse(collection["mes"]));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View(viewModel);
        }
    }
}