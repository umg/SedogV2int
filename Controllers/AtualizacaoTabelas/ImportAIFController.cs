using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Helpers;
using SEDOGv2.Models;
using System.IO;


namespace SEDOGv2.Controllers.AtualizacaoTabelas
{
    public class ImportAIFController : Controller
    {
        // GET: ImportAIF
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            ImportAIF ret = new Models.ImportAIF();
            try
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Files/"), fileName);
                        file.SaveAs(path);

                        ProcessAIF psc = new ProcessAIF();
                        ret = psc.ProcessAif(path);

                        ViewBag.FileName = fileName;
                    }
                }
            }
                catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(ret);
        }

    }
}