using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Helpers;
using SEDOGv2.Models.Context;
using System.Xml;
using System.Text;

namespace SEDOGv2.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            DashBoard_Helper dh = new DashBoard_Helper();
            dh.path = Server.MapPath("~");                        
            dh.LoadMyDashBoard(appSettings._User);
            ViewBag.Dashboard = dh.SHtml;
            ViewBag.ScriptData = dh.SScriptArea;
            ViewBag.Nav = dh.SNav;
            return View();
        }

        [HttpPost]
        public void ExportToExcel(FormCollection collection)
        {
            string vsl = HttpUtility.UrlDecode(collection["dadosTable"]);

            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment;filename="+collection["xlsName"].ToString());
            Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            Response.Write("<head><META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");

            FileInfo fi = new FileInfo(Server.MapPath("~/Content/exportexcel.css"));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StreamReader sr = fi.OpenText();
            while (sr.Peek() >= 0)
                sb.Append(sr.ReadLine());

            Response.Write(string.Concat("<style type=\"text/css\">", sb.ToString(), "</style>"));
            Response.Write("</head>");
            Response.Write(vsl);
            Response.Write("</html>");
            Response.Flush();
            Response.End();
        }

    }

}
