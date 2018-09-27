using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEDOGv2.Controllers
{
    public class KeepMeAliveController : Controller
    {
        // GET: KeepMeAlive
        [ActionFilter_CheckLogin]
        [HttpPost]
        public DefaultHttpHandler Index()
        {
            return new DefaultHttpHandler();
        }
    }
}