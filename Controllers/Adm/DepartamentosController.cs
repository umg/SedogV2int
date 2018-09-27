using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.Cadastros
{
    public class DepartamentosController : Controller
    {
        // GET: Departamentos
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            List<DepartamentosViewModel> model = new List<DepartamentosViewModel>();
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();

                model = provider.SEL_DEPARTAMENTOS();
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            List<DepartamentosViewModel> model = new List<DepartamentosViewModel>();
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();

                if (collection["txtDepartamento"] != string.Empty)
                    provider.INS_DEPARTAMENTOS(collection["txtDepartamento"].ToUpper());

                model = provider.SEL_DEPARTAMENTOS();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }
        public ActionResult Apagar(string nome)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                provider.DEL_DEPARTAMENTOS(nome);

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Index");
        }

    }
}