using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEDOGv2.Controllers.AtualizacaoTabelas
{
    public class ImportALLController : Controller
    {
        // GET: ImportAll
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p.StartInfo.FileName = ConfigurationManager.AppSettings["pathProcessamento"];
                p.Start();

            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            ViewBag.message = "Process started... please wait for e-mail confirmation response!";

            return View();
        }
    }
}