using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using System.Web.Mvc;

namespace SEDOGv2.Controllers.Relatorios
{
    public class TopClientesVendasAnualController : Controller
    {
        // GET: TopClientesVendasAnual
        public ActionResult Index()
        {
            return View(new TopClientesVendasAnualViewModel());
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection filtros)
        {
            var topClientesVendasAnual = new TopClientesVendasAnualViewModel();
            var provider = new PLProjetoProvider();

            topClientesVendasAnual.Total = provider.SLT_TOP_CLIENTES_VENDAS_COMPARATIVO_ANUAL(filtros["tipoCliente"].ToString().Substring(0,1), int.Parse(filtros["mes1"]), int.Parse(filtros["ano1"]), int.Parse(filtros["mes2"]), int.Parse(filtros["ano2"]), 'T');
            topClientesVendasAnual.Fracionado = provider.SLT_TOP_CLIENTES_VENDAS_COMPARATIVO_ANUAL(filtros["tipoCliente"].ToString().Substring(0, 1), int.Parse(filtros["mes1"]), int.Parse(filtros["ano1"]), int.Parse(filtros["mes2"]), int.Parse(filtros["ano2"]), 'F');

            return View(topClientesVendasAnual);
        }
    }
}