using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.Adm
{
    public class PaginasAdmController : Controller
    {
        // GET: PaginasAdm
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            ViewBag.PagesHeaders = provider.SLT_PAGEHEADERS();
            return View(provider.SLT_PAGES(0));
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                long idSubTitle = 0;
                if (collection["txtPage"] != "" && collection["txtPath"] != "" && collection["txtDescricao"] != "")
                    if (long.TryParse(collection["selMenu"], out idSubTitle))
                        provider.INS_PAGE(idSubTitle, collection["txtPage"], collection["txtPath"], collection["txtDescricao"]);
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
            }

            return RedirectToAction("Index");
        }
        [ActionFilter_CheckLogin]
        public ActionResult Edit(long id)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            ViewBag.PagesHeaders = provider.SLT_PAGEHEADERS();
            return View(provider.SLT_PAGES(id).First());
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                long idSubTitle = 0;
                long id = 0;
                if (collection["PAGE"] != "" && collection["PATH"] != "" && collection["DESCRICAO"] != "")
                    if (long.TryParse(collection["IDSUBTITLE"], out idSubTitle) && long.TryParse(collection["ID"], out id))
                        provider.UPD_PAGE(id, idSubTitle, collection["PAGE"], collection["PATH"], collection["DESCRICAO"]);
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [ActionFilter_CheckLogin]
        public ActionResult Delete(long id)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            ViewBag.PagesHeaders = provider.SLT_PAGEHEADERS();
            return View(provider.SLT_PAGES(id).First());
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                long id = 0;
                    if (long.TryParse(collection["ID"], out id))
                        provider.DEL_PAGE(id);
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
            }

            return RedirectToAction("Index");
        }

    }
}