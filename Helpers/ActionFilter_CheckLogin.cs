using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEDOGv2
{
    public class ActionFilter_CheckLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userSession = filterContext.HttpContext.Session["SEDOGv2.USUARIOS"];
            if (userSession == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Logout");
                return;
            }
        }
    }
}