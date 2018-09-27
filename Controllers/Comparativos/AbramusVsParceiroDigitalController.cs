using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEDOGv2.Controllers.Comparativos
{
    public class AbramusVsParceiroDigitalController : Controller
    {
        // GET: AbramusVsParceiroDigital
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            return View();
        }
    }
}