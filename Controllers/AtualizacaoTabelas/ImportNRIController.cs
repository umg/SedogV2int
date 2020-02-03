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
            List<ImportNRIAnc> nriList = new List<ImportNRIAnc>();

            DataTable dt = new DataTable();
            try
            {
                Models.Context.Conn db = new Models.Context.Conn();

                string selRetorno = "SELECT NRI.IDPROJ_SEDOG, YEAR, RELEASE_DATE, RELEASE_DATE_YTD, ARTISTNAME, " +
                                    "LICENSE_INCOME, LICENSE_ROYALTIES, LICENSE_MARGIN, LICENSE_PERCENT, " +
                                    "PREMIUM_INCOME, PREMIUM_MARGIN, PREMIUM_PERCENT, PREMIUM_DIFF, " +
                                    "SPONSORSHIP_INCOME, SPONSORSHIP_MARGIN, SPONSORSHIP_PERCENT, SPONSORSHIP_DIFF ," +
                                    "OTHERADV_INCOME,OTHERADV_MARGIN , OTHERADV_PERCENT,OTHERADV_DIFF ," +
                                    "MANAGMNT_COMISSION_INCOME,MANAGMNT_COMISSION_MARGIN,MANAGMNT_COMISSION_PERCENT,MANAGMNT_COMISSION_DIFF," +
                                    "LIVEEVENT_INCOME,LIVEEVENT_MARGIN,LIVEEVENT_PERCENT,LIVEEVENT_DIFF, " +
                                    "LIVEAGENCY_INCOME,LIVEAGENCY_MARGIN,LIVEAGENCY_PERCENT,LIVEAGENCY_DIFF, " +
                                    "PASSIVETOURING_INCOME,PASSIVETOURING_MARGIN,PASSIVETOURING_PERCENT,PASSIVETOURING_DIFF," +
                                    "PASSIVEPUBLISHING_INCOME,PASSIVEPUBLISHING_MARGIN,PASSIVEPUBLISHING_PERCENT,PASSIVEPUBLISHING_DIFF," +
                                    "ALLNRI_INCOME,ALLNRI_MARGIN,ALLNRI_PERCENT,ALLNRI_DIFF " +
                                    "FROM MXSEDOG . NRI NRI INNER JOIN MXSEDOG . PL_PROJETO_SEDOG PRJ ON NRI.IDPROJ_SEDOG = PRJ.IDPROJ_SEDOG";

                dt = db.GetTableFromSQLString(selRetorno);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ImportNRIAnc ret = new ImportNRIAnc();

                    ret.IdProjetoSedog = dt.Rows[i]["IDPROJ_SEDOG"].ToString();
                    ret.Year = dt.Rows[i]["YEAR"].ToString();
                    ret.ReleaseDate = dt.Rows[i]["RELEASE_DATE"].ToString();
                    ret.YTD = dt.Rows[i]["RELEASE_DATE_YTD"].ToString();
                    ret.ArtistName = dt.Rows[i]["ARTISTNAME"].ToString();
                    ret.LicenseIncome = dt.Rows[i]["LICENSE_INCOME"].ToString();
                    ret.LicenseRoyalties = dt.Rows[i]["LICENSE_ROYALTIES"].ToString();
                    ret.LicenceMargin = dt.Rows[i]["LICENSE_MARGIN"].ToString();
                    ret.Premium = dt.Rows[i]["PREMIUM_INCOME"].ToString();
                    ret.SponsorShip = dt.Rows[i]["SPONSORSHIP_INCOME"].ToString();
                    ret.OtherAdv = dt.Rows[i]["OTHERADV_INCOME"].ToString();
                    ret.ManagmntComiission = dt.Rows[i]["MANAGMNT_COMISSION_INCOME"].ToString();
                    ret.LiveEvent = dt.Rows[i]["LIVEEVENT_INCOME"].ToString();
                    ret.LiveAgency = dt.Rows[i]["LIVEAGENCY_INCOME"].ToString();
                    ret.PassiveTouring = dt.Rows[i]["PASSIVETOURING_INCOME"].ToString();
                    ret.PassivePublishing = dt.Rows[i]["PASSIVEPUBLISHING_INCOME"].ToString();
                    ret.AllNRI = dt.Rows[i]["ALLNRI_INCOME"].ToString();

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
        //return View();
    

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
                            ret.LicencePercent = dt.Rows[i]["LICENSE_PERCENT"].ToString();
                            ret.Premium = dt.Rows[i]["PREMIUM_INCOME"].ToString();
                            ret.PremiumMargin = dt.Rows[i]["PREMIUM_MARGIN"].ToString();
                            ret.PremiumPercent = dt.Rows[i]["PREMIUM_PERCENT"].ToString();
                            ret.PremiumDIF = dt.Rows[i]["PREMIUM_DIFF"].ToString();
                            ret.SponsorShip = dt.Rows[i]["SPONSORSHIP_INCOME"].ToString();
                            ret.SponsorShipMargin = dt.Rows[i]["SPONSORSHIP_MARGIN"].ToString();
                            ret.SponsorShipPercent = dt.Rows[i]["SPONSORSHIP_PERCENT"].ToString();
                            ret.SponsorShipDIF = dt.Rows[i]["SPONSORSHIP_DIFF"].ToString();
                            ret.OtherAdv = dt.Rows[i]["OTHERADV_INCOME"].ToString();
                            ret.OtherAdvMargin = dt.Rows[i]["OTHERADV_MARGIN"].ToString();
                            ret.OtherAdvPercent = dt.Rows[i]["OTHERADV_PERCENT"].ToString();
                            ret.OtherAdvDIF = dt.Rows[i]["OTHERADV_DIFF"].ToString();
                            ret.ManagmntComiission = dt.Rows[i]["MANAGMNT_COMISSION_INCOME"].ToString();
                            ret.ManagmntComiissionMargin = dt.Rows[i]["MANAGMNT_COMISSION_MARGIN"].ToString();
                            ret.ManagmntComiissionPercent = dt.Rows[i]["MANAGMNT_COMISSION_PERCENT"].ToString();
                            ret.ManagmntComiissionDIF = dt.Rows[i]["MANAGMNT_COMISSION_DIFF"].ToString();
                            ret.LiveEvent = dt.Rows[i]["LIVEEVENT_INCOME"].ToString();
                            ret.LiveEventMargin = dt.Rows[i]["LIVEEVENT_MARGIN"].ToString();
                            ret.LiveEventPercent = dt.Rows[i]["LIVEEVENT_PERCENT"].ToString();
                            ret.LiveEventDIF = dt.Rows[i]["LIVEEVENT_DIFF"].ToString();
                            ret.LiveAgency = dt.Rows[i]["LIVEAGENCY_INCOME"].ToString();
                            ret.LiveAgencyMargin = dt.Rows[i]["LIVEAGENCY_MARGIN"].ToString();
                            ret.LiveAgencyPercent = dt.Rows[i]["LIVEAGENCY_PERCENT"].ToString();
                            ret.LiveAgencyDIF = dt.Rows[i]["LIVEAGENCY_DIFF"].ToString();
                            ret.PassiveTouring = dt.Rows[i]["PASSIVETOURING_INCOME"].ToString();
                            ret.PassiveTouringMargin = dt.Rows[i]["PASSIVETOURING_MARGIN"].ToString();
                            ret.PassiveTouringPercent = dt.Rows[i]["PASSIVETOURING_PERCENT"].ToString();
                            ret.PassiveTouringDIF = dt.Rows[i]["PASSIVETOURING_DIFF"].ToString();
                            ret.PassivePublishing = dt.Rows[i]["PASSIVEPUBLISHING_INCOME"].ToString();
                            ret.PassivePublishingMargin = dt.Rows[i]["PASSIVEPUBLISHING_MARGIN"].ToString();
                            ret.PassivePublishingPercent = dt.Rows[i]["PASSIVEPUBLISHING_PERCENT"].ToString();
                            ret.PassivePublishingDIF = dt.Rows[i]["PASSIVEPUBLISHING_DIFF"].ToString();
                            ret.AllNRI = dt.Rows[i]["ALLNRI_INCOME"].ToString();
                            ret.AllNRIMargin = dt.Rows[i]["ALLNRI_MARGIN"].ToString();
                            ret.AllNRIPercent = dt.Rows[i]["ALLNRI_PERCENT"].ToString();
                            ret.AllNRIDIF = dt.Rows[i]["ALLNRI_DIFF"].ToString();

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