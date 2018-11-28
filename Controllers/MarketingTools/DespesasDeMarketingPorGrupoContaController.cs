using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.MarketingTools
{
    public class DespesasDeMarketingPorGrupoContaController : Controller
    {
        // GET: DespesasDeMarketingPorGrupoConta
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            ViewBag.TValor = 0;
            return View(new List<DespesasDeMarketingCTBRepertorioPorGrupoContaViewModel>());
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            int mes = int.Parse(collection["mes"]);
            int ano = int.Parse(collection["ano"]);
            string origem = "";
            switch (collection["origem"])
            {
                case "NAC":
                    origem = "Nacional";
                    break;
                case "INT":
                    origem = "Internacional";
                    break;
                case "SPE":
                    origem = "Special Marketing";
                    break;
                default:
                    origem = "Todos";
                    break;
            }
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(Helpers.appSettings._User.Culture);

            ViewBag.Header = culture.DateTimeFormat.GetMonthName(mes) + "/" + collection["ano"] + " - Repertoire: " + origem;

            PLProjetoProvider provider = new PLProjetoProvider();
            ViewBag.TValor = 0;
            decimal total = 0;
            List <DespesasDeMarketingCTBRepertorioPorGrupoContaViewModel> lst = provider.RODA_DESPESAS_MARKETING_CTB_MCS_REPERTORIO_GRUPOCONTAS(mes, ano, collection["origem"]);
            foreach (var item in lst)
            {
                total += item.VALOR;
            }
            ViewBag.TValor = total;
            return View(lst);
        }
    }
}