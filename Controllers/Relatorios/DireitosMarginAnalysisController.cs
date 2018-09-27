using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEDOGv2.Controllers.Relatorios
{
    public class DireitosMarginAnalysisController : Controller
    {
        // GET: DireitosMarginAnalysis
        public ActionResult Index()
        {
            /*PLProjetoProvider provider = new PLProjetoProvider();
            DireitosMarginAnalysisViewModel _model = new DireitosMarginAnalysisViewModel();
            _model._TipoContrato = provider.SEL_PL_TIPO_CONTRATO();
            return View(_model);*/
            return View();
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string[] auxSplit = collection["contrato"].Split('|');
            ViewBag.contrato = auxSplit[0];
            ViewBag.contratoextenso = string.Concat(auxSplit[1] , " - ", DateTime.Now.ToString("dd/MM/yyyy"));
            PLProjetoProvider provider = new PLProjetoProvider();
            List<DireitosMarginAnalysisViewModel> _model = provider.SLT_DIREITOS_LUCROS_E_PERDAS(Convert.ToInt32(auxSplit[0]));
                        
            return View(_model);
        }
    }
}