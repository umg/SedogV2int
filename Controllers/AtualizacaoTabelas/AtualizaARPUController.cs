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
    public class AtualizaARPUController : Controller
    {
        // GET: AtualizaARPU
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            return View(new List<ARPUViewModel>());
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            List<ARPUViewModel> model = new List<ARPUViewModel>();

            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                model = provider.SLT_ARPU_ANO(int.Parse(collection["ano"]));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }
        [ActionFilter_CheckLogin]
        public ActionResult UpdateARPU(ARPUViewModel[] valores)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                string updateString = "UPDATE " + provider.AddScheme("ARPU") + " SET (PREMIUM , GRATUITO ) = ";
                foreach (ARPUViewModel val in valores)
                {                    
                    string s = updateString + "(" + val.PREMIUM.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")) + " , " + val.GRATUITO.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")) + ") WHERE MESES = '" + val.MES + "' AND ANO = " + val.ANO;
                    provider.ExecuteCommandSQL(s);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, responseText = "Updated!" }, JsonRequestBehavior.AllowGet);
        }
        
    }
}