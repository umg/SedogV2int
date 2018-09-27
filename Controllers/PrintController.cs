using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml;
using System.Reflection;
using ClosedXML.Excel;

namespace SEDOGv2.Controllers
{
    public class PrintController : Controller
    {

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(ContentToPrint content)
        {
            Byte[] bytes = null;
            
            string css = "";
            StreamReader strR = new StreamReader(Server.MapPath("~/Content/report.css"));
            css = strR.ReadToEnd();
            strR.Close();
            /*strR = new StreamReader(Server.MapPath("~/Content/font-awesome.css"));
            css += strR.ReadToEnd();
            strR.Close();
            strR = new StreamReader(Server.MapPath("~/Content/site.css"));
            css += strR.ReadToEnd();
            strR.Close();*/

            string htmlLimpo = ContentToPrint.AcertaStringIMG(content.PrintHtmlContent);

            if (content.PrintHtmlType == "PDF")
            {
                using (var ms = new MemoryStream())
                {
                    using (var doc = new Document())
                    {
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {
                            doc.Open();
                            using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                            {
                                using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlLimpo)))
                                {
                                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                                }
                            }
                            doc.Close();
                        }
                    }
                    bytes = ms.ToArray();
                }
            }
            else
            {
                using (var ms = new MemoryStream())
                {
                    string final = "<html><head><style type='text/css'>";
                    final += css;
                    final += "</style><head>";
                    final += htmlLimpo;
                    final += "</html>";
                    StreamWriter writer = new StreamWriter(ms, System.Text.Encoding.UTF8);
                    writer.Write(final);
                    writer.Flush();
                    bytes = ms.ToArray();
                }
            }

            //var testFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.pdf");
            //System.IO.File.WriteAllBytes(testFile, bytes);

            if (content.PrintHtmlType == "PDF")
                return File(bytes, "application/pdf", "Report.pdf");
            else
                return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xls");

        }


    }
    public class ContentToPrint
    {
        [AllowHtml]
        public string PrintHtmlContent { get; set; }
        public string PrintHtmlType { get; set; }

        public static string AcertaStringIMG(string input)
        {
            string ret = "";
            int carAtual = 0;
            while (carAtual <= input.Length)
            {
                carAtual = input.IndexOf("<img", carAtual);
                if (carAtual > -1) carAtual = input.IndexOf(">", carAtual + 1);
                if (carAtual == -1) carAtual = input.Length + 1;
                else
                {

                    input = input.Insert(carAtual, "/");
                }
                carAtual++;
            }
            ret = input;
            return ret;
        }

        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            if (serverUrl.IndexOf("://") > -1)
                return serverUrl;

            string newUrl = serverUrl;
            Uri originalUri = System.Web.HttpContext.Current.Request.Url;
            newUrl = (forceHttps ? "https" : originalUri.Scheme) +
#if DEBUG            
                "://" + originalUri.Authority + newUrl;
#else
                "://" + originalUri.Authority +"/SEDOGv2INT/" +  newUrl;
#endif
            return newUrl;
        }
    }

}