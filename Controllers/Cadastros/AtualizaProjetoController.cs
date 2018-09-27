using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using SEDOGv2.Helpers;

namespace SEDOGv2.Controllers.Adm
{
    public class AtualizaProjetoController : Controller
    {
        // GET: AtualizaProjeto
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            var model = provider.SEL_PLPROJETOS("");
            var date = provider.SEL_DATAREFERENCIA();
            ViewBag.DataRef = date.ToString("yyyy-MM-dd");
            if (Session["resposta"] != null)
            {
                model.Message = Session["resposta"].ToString();
            }
            return View(model);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            Session["resposta"] = "";
            PLProjetoProvider provider = new PLProjetoProvider();
            return View(provider.SEL_PLPROJETOS(collection["pesquisa"]));
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult AtualizaProjetos(FormCollection collection)
        {
            Session["resposta"] = "";
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                if (collection["selProdutos"] != null)
                {
                    foreach (string projeto in collection["selProdutos"].Split(','))
                    {
                        provider.PL_RODA_NEW_SEDOG(long.Parse(projeto));
                    }
                }
                else
                    provider.PL_RODA_NEW_SEDOG(0);

                Session["resposta"] = "Atualizado";
            }
            catch (Exception ex)
            {
                Session["resposta"] = "Erro: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult AtualizaDataReferencia(FormCollection collection)
        {
            Session["resposta"] = "";
            try
            {
                DateTime date = new DateTime();
                PLProjetoProvider provider = new PLProjetoProvider();
                if(DateTime.TryParse(collection["data"], out date))
                {
                    provider.UPD_PL_DADOS_DATA_REFERENCIA(collection["data"], functions.ConvertDateToCordis(date));
                }

                Session["resposta"] = "Atualizado";
            }
            catch (Exception ex)
            {
                Session["resposta"] = "Erro: " + ex.Message;
            }
            return RedirectToAction("Index");
        }

    }
}