using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.Relatorios
{
    public class PLMarginAnalysisController : Controller
    {
        //
        // GET: /PLMarginAnalysis/
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            PLMarginAnalysisViewModel viewmodel = new Models.PLMarginAnalysisViewModel();
            viewmodel.PLMarginAnalysis = new List<PLMarginAnalysis>();
            return View(viewmodel);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            PLMarginAnalysisViewModel viewmodel = new Models.PLMarginAnalysisViewModel();
           
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                string ano = collection["ano"];
                if (ano == "00")
                    ViewBag.Ano = "- Todos -";
                else
                    ViewBag.Ano = "20" + ano;
                viewmodel.PLMarginAnalysis = provider.SLT_MARGIN_ANALYSIS(Convert.ToInt32(ano));

                Session["PLMARGINANALYSIS"] = viewmodel.PLMarginAnalysis;

                //viewmodel.DistinctRelease = viewmodel.PLMarginAnalysis.Select(x => x).Distinct().ToList();

                viewmodel.DistinctRelease = viewmodel.PLMarginAnalysis.GroupBy(p => p.ID_TIPO_RELEASE).Select(grp => grp.First()).OrderBy(x => x.ID_TIPO_RELEASE).ToList();
            }
            catch (Exception ex)
            {
            viewmodel.PLMarginAnalysis = new List<PLMarginAnalysis>();
                ViewBag.Error = ex.Message;
            }


            return View(viewmodel);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult ExportReports(FormCollection collection)
        {
            string log = "";
            try
            {
                string docType = collection["doctype"].ToString();

                string reportName = collection["reportName"].ToString();
                string printName = collection["printName"].ToString();
                string reportModelPath = string.Concat(Server.MapPath("~/Reports/"), reportName);
                string reportSavePath = Server.MapPath("~/Reports/EXCEL/");
                log += reportModelPath + ";;";
                log += reportSavePath + ";;";
                string contentType = "application/excel";
                Helpers.Reports export = new Helpers.Reports();
                List<PLMarginAnalysis> auxPL = (List<PLMarginAnalysis>)Session["PLMARGINANALYSIS"];
                byte[] rpt = null;
                log += docType + ";;";
                switch (docType)
                {
                    case "xls":
                        rpt = export.ExportToEXCEL(reportModelPath, reportSavePath, printName, auxPL, new List<Helpers.ReportParamValues>() { }, new List<System.Data.DataTable>() { });
                        return File(rpt, contentType, string.Concat(printName, ".", docType));
                    case "pdf":
                        reportName = collection["reportName"].ToString();
                        printName = collection["printName"].ToString();
                        reportModelPath = string.Concat(Server.MapPath("~/Reports/"), reportName);
                        reportSavePath = Server.MapPath("~/Reports/PDF/");
                        contentType = "application/pdf";
                        rpt = export.ExportToPDF(reportModelPath, reportSavePath, printName, auxPL, new List<Helpers.ReportParamValues>() { }, new List<System.Data.DataTable>() { });
                        return File(rpt, contentType, string.Concat(printName, ".", docType));
                }
            }
            catch (Exception ex)
            {
                log += ex.Message;
            }
            return Content(log);
        }
    }
}
