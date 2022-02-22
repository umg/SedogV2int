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
            var dataIni = collection["anoInicial"] + "-" + collection["mesInicial"] + "-01";
            var dataFim = collection["anoFim"] + "-" + collection["mesFim"] + "-01";

            DateTime dataInicial;
            DateTime dataFinal;
            List<TopClientesFisicoViewModel> model = null;
            if (DateTime.TryParse(dataIni, out dataInicial) && DateTime.TryParse(dataFim, out dataFinal))
            {
                ViewBag.filtro = dataInicial.ToShortDateString() + " - " + dataFinal.ToShortDateString();

                var provider = new PLProjetoProvider();
                model = provider.SLT_TOP_CLIENTES_DETALHADO(dataInicial, dataFinal);
            }

            return View(model);
        }
    }
}