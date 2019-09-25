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
    public class ImportAdvancesController : Controller
    {
        // GET: ImportNI
        public ActionResult Index()
        {
            List<ImportNRI> nriList = new List<ImportNRI>();

            DataTable dt = new DataTable();
            try
            {
                Models.Context.Conn db = new Models.Context.Conn();

                string selRetorno = "SELECT NRI.IDPROJ_SEDOG, R2_PROJECT, YEAR, TOTAL, ADVANCE, AUDIO, VIDEO FROM MXSEDOG . ADVANCES_RECOUPABLE NRI INNER JOIN MXSEDOG . PL_PROJETO_SEDOG PRJ ON NRI.IDPROJ_SEDOG = PRJ.IDPROJ_SEDOG";

                dt = db.GetTableFromSQLString(selRetorno);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ImportNRI ret = new ImportNRI();

                    ret.IdProjetoSedog = dt.Rows[i]["IDPROJ_SEDOG"].ToString();
                    ret.R2Projects = dt.Rows[i]["R2_PROJECT"].ToString();
                    ret.Year = dt.Rows[i]["YEAR"].ToString();
                    ret.Total = dt.Rows[i]["TOTAL"].ToString();
                    ret.Advance = dt.Rows[i]["ADVANCE"].ToString();
                    ret.Audio = dt.Rows[i]["AUDIO"].ToString();
                    ret.Video = dt.Rows[i]["VIDEO"].ToString();

                    nriList.Add(ret);
                }

                ViewBag.FileName = "Last file imported content";
                //ViewBag.processedNRIList = nriList;


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(nriList);
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
                            ret.Year = dt.Rows[i]["YEAR"].ToString();
                            ret.Total = dt.Rows[i]["TOTAL"].ToString();
                            ret.Advance = dt.Rows[i]["ADVANCE"].ToString();
                            ret.Audio = dt.Rows[i]["AUDIO"].ToString();
                            ret.Video = dt.Rows[i]["VIDEO"].ToString();

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