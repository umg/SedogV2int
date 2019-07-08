using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using SEDOGv2.Helpers;


namespace SEDOGv2.Controllers.Cadastros
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            List<UsuarioViewModel> model = new List<UsuarioViewModel>();
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();

                model = provider.SLT_USUARIOS("");
                ViewBag.Departamentos = provider.SEL_DEPARTAMENTOS();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                if (collection["txtLogin"] != "" && collection["txtNome"] != "" && collection["txtEmail"] != "" && collection["selDepto"] != "")
                    provider.INS_USUARIO(collection["txtLogin"], collection["txtNome"], collection["txtEmail"], collection["selDepto"]);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [ActionFilter_CheckLogin]
        public ActionResult Edit(string login)
        {
            UsuarioViewModel model = new UsuarioViewModel();
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();

                model = provider.SLT_USUARIOS(login).First();
                model.Paginas = provider.SLT_PAGES_POR_USUARIO(login);
                ViewBag.Departamentos = provider.SEL_DEPARTAMENTOS();

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                if (collection["LOGIN"] != "" && collection["NOME"] != "" && collection["EMAIL"] != "" && collection["DEPTO"] != "")
                {
                    provider.UPD_USUARIO(collection["LOGIN"], collection["NOME"], collection["EMAIL"], collection["DEPTO"]);
                    provider.DEL_PAGINAS_USUARIO(collection["LOGIN"]);
                    foreach (var idPag in collection["chkPaginas"].Split(','))
                    {
                        provider.INS_PAGINAS_USUARIO(collection["LOGIN"], long.Parse(idPag));
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [ActionFilter_CheckLogin]
        public ActionResult Delete(string login)
        {
            UsuarioViewModel model = new UsuarioViewModel();
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();

                model = provider.SLT_USUARIOS(login).First();

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                if (collection["LOGIN"] != "")
                {
                    provider.DEL_PAGINAS_USUARIO(collection["LOGIN"]);
                    provider.DEL_USUARIO(collection["LOGIN"]);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [ActionFilter_CheckLogin]
        public ActionResult AddDepto(string login)
        {
            UsuarioViewModel model = new UsuarioViewModel();
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();

                model = provider.SLT_USUARIOS(login).First();
                model._Departamentos = new List<Models.Departamento>();
                model._Departamentos = provider.SEL_DEPARTAMENTOSJDE_POR_USUARIOS(login);

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult AddDepto(FormCollection collection)
        {
            try
            {
                string _execute = "";
                string _delete = "";

                PLProjetoProvider provider = new PLProjetoProvider();
                if (collection["LOGIN"] != "")
                {
                    _delete = string.Concat(_execute, "DELETE FROM " + appSettings.Ambiente + " . DEPARTAMENTOSJDE_POR_USUARIO WHERE LOGIN = '", collection["LOGIN"], "' ");
                    _execute = string.Concat(_execute, " INSERT INTO " + appSettings.Ambiente + " . DEPARTAMENTOSJDE_POR_USUARIO VALUES ");

                    foreach (var depto in collection["chkDeptoJDE"].Split(','))
                        _execute = string.Concat(_execute, "('", collection["LOGIN"], "', ", depto, ") , ");

                    if (!string.IsNullOrEmpty(collection["chkDeptoJDE"]))
                        _execute = _execute.Remove(_execute.Length - 2, 2);

                    //_execute = string.Concat(_execute, ";");

                    provider.ExecuteSQL(_delete);
                    provider.ExecuteSQL(_execute);

                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}