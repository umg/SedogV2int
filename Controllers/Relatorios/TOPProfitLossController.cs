using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.Relatorios
{
    public class TOPProfitLossController : Controller
    {
        //
        // GET: /TOPProfitLoss/
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            TOPProfitLossViewModel viewModel = new TOPProfitLossViewModel();
            viewModel.TOPProfitLoss = new List<TOPProfitLoss>();
            return View(viewModel);
        }


        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            TOPProfitLossViewModel viewModel = new TOPProfitLossViewModel();

            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                string ano = collection["ano"];
                if (ano == "00")
                    ViewBag.Ano = "- Todos -";
                else
                    ViewBag.Ano = "20" + collection["ano"];
                viewModel.TOPProfitLoss = provider.SLT_TOP_PROFIT_LOSS(Convert.ToInt32(ano));
                Session["TOPPROFITLOSS"] = viewModel.TOPProfitLoss;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(viewModel);

        }

        public ActionResult ExportReports(FormCollection collection)
        {
            string docType = collection["doctype"].ToString();
            string reportName = collection["reportName"].ToString();
            string printName = collection["printName"].ToString();
            string reportModelPath = string.Concat(Server.MapPath("~/Reports/"), reportName);
            string reportSavePath = Server.MapPath("~/Reports/EXCEL/");
            string contentType = "application/Excel";
            Helpers.Reports export = new Helpers.Reports();
            List<TOPProfitLoss> auxPL = (List<TOPProfitLoss>)Session["TOPPROFITLOSS"];
            byte[] rpt = null;
            switch (docType)
            {
                case "xls":
                    rpt = export.ExportToEXCEL(reportModelPath, reportSavePath, printName, auxPL, new List<Helpers.ReportParamValues>() { }, new List<System.Data.DataTable>() { });
                    return File(rpt, contentType, string.Concat(printName, ".", docType));
                    break;
                case "pdf":
                    reportName = collection["reportName"].ToString();
                    printName = collection["printName"].ToString();
                    reportModelPath = string.Concat(Server.MapPath("~/Reports/"), reportName);
                    reportSavePath = Server.MapPath("~/Reports/PDF/");
                    contentType = "application/pdf";
                    rpt = export.ExportToPDF(reportModelPath, reportSavePath, printName, auxPL, new List<Helpers.ReportParamValues>() { }, new List<System.Data.DataTable>() { });
                    return File(rpt, contentType, string.Concat(printName, ".", docType));
                    break;
                default:
                    return RedirectToAction("Index");
                    break;
            }

        }
    }
}
