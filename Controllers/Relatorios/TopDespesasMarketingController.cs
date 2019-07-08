using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.Relatorios
{
    public class TopDespesasMarketingController : Controller
    {
        // GET: TopDespesasMarketing
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            List<TopDespesasMarketingViewModel> model = new List<TopDespesasMarketingViewModel>();
            ViewBag.TituloReport = "";
            return View(model);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            int ano = int.Parse(collection["ano"]) % 2000;
            string tipo = collection["tipo"];

            ViewBag.TituloReport = tipo + " - " + collection["ano"];

            if (ano == 0)
                ViewBag.TituloReport = tipo + " - Todos";

            List<TopDespesasMarketingViewModel> model = provider.SLT_TOP_DESPESAS_MARKETING(ano, tipo);
            return View(model);
        }
    }
}