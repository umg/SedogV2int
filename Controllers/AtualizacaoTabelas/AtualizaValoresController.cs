using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEDOGv2.Controllers.AtualizacaoTabelas
{
    public class AtualizaValoresController : Controller
    {
        // GET: AtualizaValores
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            return View();
        }
    }
}