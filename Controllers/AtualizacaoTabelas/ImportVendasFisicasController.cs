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
    public class ImportVendasFisicasController : Controller
    {
        // GET: ImportAIF
        public ActionResult Index()
        {
            //List<ImportAIF> nriList = new List<ImportAIF>();

            //DataTable dt = new DataTable();
            //try
            //{
            //    Models.Context.Conn db = new Models.Context.Conn();

            //    string selRetorno = "SELECT AIF.IDPROJ_SEDOG, PRJ.PROJETO, R2_PROJECT, YEAR,FOREIGN_INCOME, ARTIST_ROYALTIES, PRODUCER_ROYALTY, OTHER_ROYALTY, ALL_ROYALTIES, FOREIGN_MARGIN, PERC_AIF_MARGIN FROM MXSEDOG . AIF_INCOMING AIF INNER JOIN MXSEDOG . PL_PROJETO_SEDOG PRJ ON AIF.IDPROJ_SEDOG = PRJ.IDPROJ_SEDOG";

            //    dt = db.GetTableFromSQLString(selRetorno);

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        ImportAIF ret = new ImportAIF();

            //        ret.IdProjetoSedog = dt.Rows[i]["IDPROJ_SEDOG"].ToString();
            //        ret.Projeto = dt.Rows[i]["PROJETO"].ToString();
            //        ret.R2Projects = dt.Rows[i]["R2_PROJECT"].ToString();
            //        ret.Year = dt.Rows[i]["Year"].ToString();
            //        ret.ForeignIncome = dt.Rows[i]["FOREIGN_INCOME"].ToString();
            //        ret.ArtistRoyalties = dt.Rows[i]["ARTIST_ROYALTIES"].ToString();
            //        ret.ProducerRoyalties = dt.Rows[i]["PRODUCER_ROYALTY"].ToString();
            //        ret.OtherRoyalty = dt.Rows[i]["OTHER_ROYALTY"].ToString();
            //        ret.AllRoyalty = dt.Rows[i]["ALL_ROYALTIES"].ToString();
            //        ret.ForeignMargin = dt.Rows[i]["FOREIGN_MARGIN"].ToString();
            //        ret.PercAIFMargin = dt.Rows[i]["PERC_AIF_MARGIN"].ToString();


            //        nriList.Add(ret);
            //    }

            //    ViewBag.FileName = "Last file imported content";
            //    //ViewBag.processedNRIList = nriList;


            //}
            //catch (Exception ex)
            //{
            //    ViewBag.Error = ex.Message;
            //}
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            //List<ImportAIF> aifList = new List<ImportAIF>();

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

                        ProcessVendasFisicas psc = new ProcessVendasFisicas();
                        dt = psc.ProcessVendasFisica(path);


                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    ImportAIF ret = new ImportAIF();

                        //    ret.IdProjetoSedog = dt.Rows[i]["IDPROJ_SEDOG"].ToString() ;
                        //    ret.Projeto = dt.Rows[i]["PROJETO"].ToString();
                        //    ret.R2Projects = dt.Rows[i]["R2_PROJECT"].ToString();
                        //    ret.Year = dt.Rows[i]["Year"].ToString();
                        //    ret.ForeignIncome = dt.Rows[i]["FOREIGN_INCOME"].ToString();
                        //    ret.ArtistRoyalties = dt.Rows[i]["ARTIST_ROYALTIES"].ToString();
                        //    ret.ProducerRoyalties = dt.Rows[i]["PRODUCER_ROYALTY"].ToString();
                        //    ret.OtherRoyalty = dt.Rows[i]["OTHER_ROYALTY"].ToString();
                        //    ret.AllRoyalty = dt.Rows[i]["ALL_ROYALTIES"].ToString();
                        //    ret.ForeignMargin = dt.Rows[i]["FOREIGN_MARGIN"].ToString();
                        //    ret.PercAIFMargin = dt.Rows[i]["PERC_AIF_MARGIN"].ToString();

                        //    aifList.Add(ret);
                        //}

                        //ViewBag.FileName = fileName;
                        //ViewBag.processedList = aifList;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

    }
}