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

            List<ImportNRIAnc> nriList = new List<ImportNRIAnc>();

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

                        ProcessNRIAnc psc = new ProcessNRIAnc();
                        dt = psc.ProcessaNRIAnc(path);


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ImportNRIAnc ret = new ImportNRIAnc();

                            ret.IdProjetoSedog = dt.Rows[i]["IDPROJ_SEDOG"].ToString();                            
                            ret.Year = dt.Rows[i]["YEAR"].ToString();
                            ret.ReleaseDate = dt.Rows[i]["RELEASE_DATE"].ToString();
                            ret.YTD = dt.Rows[i]["RELEASE_DATE_YTD"].ToString();
                            ret.ArtistName= dt.Rows[i]["ARTISTNAME"].ToString();
                            ret.LicenseIncome = dt.Rows[i]["LICENSE_INCOME"].ToString();
                            ret.LicenseRoyalties = dt.Rows[i]["LICENSE_ROYALTIES"].ToString();
                            ret.LicenceMargin = dt.Rows[i]["LICENSE_MARGIN"].ToString();
                            ret.Premium = dt.Rows[i]["PREMIUM"].ToString();
                            ret.SponsorShip = dt.Rows[i]["SPONSORSHIP"].ToString();
                            ret.OtherAdv = dt.Rows[i]["OTHER_ADV"].ToString();
                            ret.ManagmntComiission = dt.Rows[i]["MANAGMNT_COMISSION"].ToString();
                            ret.LiveEvent = dt.Rows[i]["LIVE_EVENT_THEATRE"].ToString();
                            ret.LiveAgency = dt.Rows[i]["LIVE_AGENCY_TICKETING"].ToString();
                            ret.PassiveTouring = dt.Rows[i]["PASSIVE_TOURING_INCOME"].ToString();
                            ret.AllNRI = dt.Rows[i]["ALL_NRI"].ToString();

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