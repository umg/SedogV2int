using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.Relatorios
{
    public class DigitalCustomerUsageTypeController : Controller
    {
        // GET: DigitalCustomerUsageType
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            DigitalCustomerUsageTypeReportViewModel model = new DigitalCustomerUsageTypeReportViewModel();
            model.PLProjetos = new List<PLProjeto>();
            model.DigitalReport = new List<DigitalCustomerUsageType>();
            return View(model);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            DigitalCustomerUsageTypeReportViewModel model = new DigitalCustomerUsageTypeReportViewModel();
            model.PLProjetos = new List<PLProjeto>();
            model.DigitalReport = new List<DigitalCustomerUsageType>();
            PLProjetoProvider provider = new PLProjetoProvider();
            decimal valor=0;
            long qtd=0;
            ViewBag.Ano = "- Todos -";
            if (Request.Form["Pesquisar"] != null)
            {
                try
                {
                    Resposta<List<PLProjeto>> resp = new Resposta<List<PLProjeto>>();

                    resp = provider.SEL_PLPROJETOS(collection["txtArtistaProduto"]);

                    model.Message = resp.Message;
                    model.PLProjetos = resp.Dados;
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            else if (Request.Form["btnGerarRelatorios"] != null)
            {
                try
                {
                    long P_IDPROJ_SEDOG = long.Parse(collection["selProduto"]);
                    ViewBag.IDProjetoSedog = P_IDPROJ_SEDOG;

                    model.PLProjetos = new List<PLProjeto>();
                    model.DigitalReport = provider.SLT_DIGITAL_CUSTOMER_USAGETYPE(P_IDPROJ_SEDOG);
                    foreach(var r in model.DigitalReport)
                    {
                        valor += r.VALOR;
                        qtd += r.QTDE;
                    }
                    ViewBag.QTDE = qtd;
                    ViewBag.VALOR = valor;
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            else if (Request.Form["selAno"] != null)
            {
                try
                {
                    string varano = collection["selAno"];
                    if (varano == "0") {
                        ViewBag.Ano = "- Todos -";
                    }
                    else
                    {
                        varano = "20" + collection["selAno"];
                        ViewBag.Ano = "20" + collection["selAno"];
                    }
                    long P_IDPROJ_SEDOG = long.Parse(collection["idProjeto"]);
                    ViewBag.IDProjetoSedog = P_IDPROJ_SEDOG;

                    model.PLProjetos = new List<PLProjeto>();
                    model.DigitalReport = provider.SLT_DIGITAL_CUSTOMER_USAGETYPE(P_IDPROJ_SEDOG).Where(d => ((int)d.ACCOUNTINGYEARMONTH / 100) == int.Parse(varano)).ToList();
                    foreach (var r in model.DigitalReport)
                    {
                        valor += r.VALOR;
                        qtd += r.QTDE;
                    }
                    ViewBag.QTDE = qtd;
                    ViewBag.VALOR = valor;
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return View(model);
        }
    }
}