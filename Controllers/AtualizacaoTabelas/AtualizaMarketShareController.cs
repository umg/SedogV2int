using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Helpers;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.AtualizacaoTabelas
{
    public class AtualizaMarketShareController : Controller
    {
        // GET: AtualizaMarketShare
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            return View(new List<MarketShareViewModel>());
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            List<MarketShareViewModel> model = new List<MarketShareViewModel>();

            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                model = provider.SLT_MARKET_SHARE_ANO(int.Parse(collection["ano"]));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }
        [ActionFilter_CheckLogin]
        public ActionResult UpdateMarketShare(MarketShareViewModel[] valores)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                string updateString = "UPDATE " + provider.AddScheme("MKTSHARE") + " SET (PERCENT) = ";
                foreach (MarketShareViewModel val in valores)
                {
                    string s = updateString + "(" + val.PERCENT.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")) + ") WHERE MESES = '" + val.MES + "' AND ANO = " + val.ANO;
                    provider.ExecuteCommandSQL(s);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, responseText = "Atualizado!" }, JsonRequestBehavior.AllowGet);
        }
    }
}