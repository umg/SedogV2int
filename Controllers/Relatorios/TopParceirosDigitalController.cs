using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.Relatorios
{
    public class TopParceirosDigitalController : Controller
    {
        // GET: TopParceirosDigital
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            List<TopParceirosDigitalViewModel> viewModel = new List<TopParceirosDigitalViewModel>();
            ViewBag.Message = "";
            ViewBag.Mes = "";
            ViewBag.Ano = "";
            return View(viewModel);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            List<TopParceirosDigitalViewModel> viewModel = new List<TopParceirosDigitalViewModel>();
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                ViewBag.Message = "";
                ViewBag.Mes = collection["mes"] == "0" ? "Todos" : collection["mes"];
                ViewBag.Ano = collection["ano"] == "0" ? "Todos" : collection["ano"];
                viewModel = provider.SLT_TOP_PARCEIROS_DIGITAL(int.Parse(collection["ano"]), int.Parse(collection["mes"]));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(viewModel);
        }

    }
}