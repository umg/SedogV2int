using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using SEDOGv2.Helpers;

namespace SEDOGv2.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(System.Web.Mvc.FormCollection collection)
        {
            string login = collection["txtLogin"];
            string pass = collection["txtPass"];
            string servidor = collection["selServidor"].Split(':')[0];
            string ambiente = collection["selServidor"].Split(':')[1];
            string culture = collection["selServidor"].Split(':')[2];

            appSettings.Servidor= servidor;
            appSettings.Ambiente = ambiente;

            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(pass))
            {
                LoginProvider provider = new LoginProvider();
                Models.Resposta<Usuario> resp = new Models.Resposta<Usuario>();
                
                resp = provider.Login(login, pass, servidor, ambiente, culture);

                if (string.IsNullOrEmpty(resp.Error))
                {
                    Helpers.appSettings._User = resp.Dados;

                    return Redirect("Home");
                }
                else
                {
                    ViewBag.Message = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, resp.Error);
                }
            }
            else
            {
                ViewBag.Message = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, "You need to type the login and password");
            }

            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}
