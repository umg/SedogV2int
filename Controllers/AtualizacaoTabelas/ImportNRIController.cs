using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Helpers;
using SEDOGv2.Models;
using System.IO;
using System.Data;

namespace SEDOGv2.Controllers.AtualizacaoTabelas
{
    public class ImportNRIController : Controller
    {
        // GET: ImportNI
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {

            List<ImportNRI> nriList = new List<ImportNRI>();

            DataTable dt = new DataTable();
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

                        ProcessNRI psc = new ProcessNRI();
                        dt = psc.ProcessaNRI(path);


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ImportNRI ret = new ImportNRI();

                            ret.IdProjetoSedog = dt.Rows[i]["IDPROJ_SEDOG"].ToString();                            
                            ret.R2Projects = dt.Rows[i]["R2_PROJECT"].ToString();
                            ret.Advance = dt.Rows[i]["Advance"].ToString();
                            ret.Recoupable = dt.Rows[i]["Recoupable"].ToString();                            

                            nriList.Add(ret);
                        }

                        ViewBag.FileName = fileName;
                        //ViewBag.processedNRIList = nriList;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(nriList);
        }
    }
}