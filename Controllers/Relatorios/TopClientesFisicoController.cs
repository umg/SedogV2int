using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SEDOGv2.Controllers.Relatorios
{
    public class TopClientesFisicoController : Controller
    {
        // GET: TopClientesFisico
        public ActionResult Index()
        {
            return View();
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            DateTime dataInicial;
            DateTime dataFinal;
            List<TopClientesFisicoViewModel> model = null;
            if (DateTime.TryParse(collection["dataInicial"], out dataInicial) && DateTime.TryParse(collection["dataFinal"], out dataFinal))
            {
                ViewBag.filtro = $"{collection["dataInicial"]} - {collection["dataFinal"]}";

                var provider = new PLProjetoProvider();
                model = provider.SLT_TOP_CLIENTES_DETALHADO(dataInicial, dataFinal);
            }

            return View(model);
        }
    }
}