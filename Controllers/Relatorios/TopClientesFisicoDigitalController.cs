using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using System;
using System.Web.Mvc;

namespace SEDOGv2.Controllers.Relatorios
{
    public class TopClientesFisicoDigitalController : Controller
    {
        // GET: TopClientesFisicoDigital
        public ActionResult Index()
        {
            return View(new TopClientesFisicoDigitalViewModel());
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection filtros)
        {
            var topVendas = new TopClientesFisicoDigitalViewModel();
            var provider = new PLProjetoProvider();

            topVendas.TopClientesFisicoDigitalTotal = provider.SLT_TOP_VENDAS(filtros["mesDe"], filtros["anoDe"], filtros["mesAte"], filtros["anoAte"], DateTime.DaysInMonth(int.Parse(filtros["anoAte"]), int.Parse(filtros["mesAte"])).ToString());
            topVendas.TopClientesFisicoDigital = provider.SLT_TOP10_CLIENTE(filtros["mesDe"], filtros["anoDe"], filtros["mesAte"], filtros["anoAte"], int.Parse(filtros["top"]));

            return View(topVendas);
        }
    }
}