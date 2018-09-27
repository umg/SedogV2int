using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Helpers;


namespace SEDOGv2.Controllers.Adm
{
    public class AtualizaDashBoardController : Controller
    {
        // GET: AtualizaDashBoard
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            
            return View();
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(System.Web.Mvc.FormCollection collection)
        {
            try
            {
                DashBoard_Helper dh = new DashBoard_Helper();
                dh.path = Server.MapPath("~");

                dh.LoadDataBaseDashboard(int.Parse(collection["txtAno"]), int.Parse(collection["txtMes"]));
                dh.LoadDataToXml();
                ViewBag.Error = "Atualizado";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro: " + ex.Message;
            }
            return View();
        }
    }
}