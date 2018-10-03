using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SEDOGv2.Controllers.AtualizacaoTabelas
{
    public class AtualizaAssinantesController : Controller
    {
        // GET: AtualizaAssinantes
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            return View(new List<AtualizaAssinantesViewModel>());
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            List<AtualizaAssinantesViewModel> model = new List<AtualizaAssinantesViewModel>();

            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                model = provider.SLT_ASSINANTES(int.Parse(collection["ano"]));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }
        [ActionFilter_CheckLogin]
        public ActionResult UpdateAssinantes(AtualizaAssinantesViewModel[] valores)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                string updateString = "UPDATE " + provider.AddScheme("ASSINANTES") + " SET (INDIVID , FAMILIA, PROMO) = ";
                foreach (AtualizaAssinantesViewModel val in valores)
                {
                    string s = updateString + "(" +
                        val.INDIVID.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")) + " , " +
                        val.FAMILIA.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")) + " , " +
                        val.PROMO.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")) +
                        ") WHERE ANOMES = " + val.ANOMES;
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