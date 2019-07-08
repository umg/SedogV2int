using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.MarketingTools
{
    public class DespesasDeMarketingPorGrupoController : Controller
    {
        // GET: DespesasDeMarketingPorGrupo
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            ViewBag.TGTOTAL = 0;
            ViewBag.TORCAMENTO_MES = 0;
            ViewBag.TG2 = 0;
            ViewBag.TG440 = 0;
            ViewBag.TG441 = 0;
            ViewBag.TG447 = 0;
            ViewBag.TG442 = 0;
            ViewBag.TG443 = 0;
            ViewBag.TG445 = 0;
            ViewBag.TG446 = 0;
            ViewBag.TG448 = 0;
            ViewBag.TG449 = 0;
            ViewBag.TG450 = 0;
            ViewBag.TNBD = 0;
            ViewBag.TDIGITAL = 0;
            ViewBag.TG444 = 0;
            ViewBag.TG453 = 0;
            return View(new List<DespesasDeMarketingCTBRepertorioPorGrupoViewModel>());
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            int mes = int.Parse(collection["mes"]);
            int ano = int.Parse(collection["ano"]);
            string origem = "";

            ViewBag.TGTOTAL = 0;
            ViewBag.TORCAMENTO_MES = 0;
            ViewBag.TG2 = 0;
            ViewBag.TG440 = 0;
            ViewBag.TG441 = 0;
            ViewBag.TG447 = 0;
            ViewBag.TG442 = 0;
            ViewBag.TG443 = 0;
            ViewBag.TG445 = 0;
            ViewBag.TG446 = 0;
            ViewBag.TG448 = 0;
            ViewBag.TG449 = 0;
            ViewBag.TG450 = 0;
            ViewBag.TNBD = 0;
            ViewBag.TDIGITAL = 0;
            ViewBag.TG444 = 0;
            ViewBag.TG453 = 0;

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
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("pt-br");

            ViewBag.Header = culture.DateTimeFormat.GetMonthName(mes) + "/" + collection["ano"] + " - Repertório: " + origem;

            PLProjetoProvider provider = new PLProjetoProvider();
            List<DespesasDeMarketingCTBRepertorioPorGrupoViewModel> lst = provider.RODA_DESPESAS_MARKETING_CTB_MCS_REPERTORIO_GRUPO(mes, ano, collection["origem"]);
            foreach (var item in lst)
            {
                ViewBag.TGTOTAL += item.GTOTAL;
                ViewBag.TORCAMENTO_MES += item.ORCAMENTO_MES;
                ViewBag.TG2 += item.G2;
                ViewBag.TG440 += item.G440;
                ViewBag.TG441 += item.G441;
                ViewBag.TG447 += item.G447;
                ViewBag.TG442 += item.G442;
                ViewBag.TG443 += item.G443;
                ViewBag.TG445 += item.G445;
                ViewBag.TG446 += item.G446;
                ViewBag.TG448 += item.G448;
                ViewBag.TG449 += item.G449;
                ViewBag.TG450 += item.G450;
                ViewBag.TNBD += item.NBD;
                ViewBag.TDIGITAL += item.DIGITAL;
                ViewBag.TG444 += item.G444;
                ViewBag.TG453 += item.G453;
            }

            return View(lst);
        }
    }
}