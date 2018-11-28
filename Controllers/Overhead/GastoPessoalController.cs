using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.Overhead
{
    public class GastoPessoalController : Controller
    {
        // GET: GastoPessoal
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            OverheadViewModel _viewModel = new Models.OverheadViewModel();
            _viewModel._selectedAno = DateTime.Now.Year;
            _viewModel._selectedMes = DateTime.Now.Month;
            _viewModel._selectedVisualizacao = "";
            _viewModel.Overhead = new List<Models.Overhead>();

            return View(_viewModel);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            OverheadViewModel _viewModel = new OverheadViewModel();
            _viewModel.Overhead = new List<Models.Overhead>();

            try
            {
                _viewModel._selectedAno = int.Parse(collection["ano"]);
                _viewModel._selectedMes = int.Parse(collection["mes"]);
                _viewModel._selectedVisualizacao = collection["visualizacao"];


                int mes = _viewModel._selectedMes;
                int ano = _viewModel._selectedAno;


                switch (_viewModel._selectedVisualizacao)
                {
                    case "mes":
                        ViewBag.MesAtual = new DateTime(ano, mes, 1).ToString("MMMM", System.Globalization.CultureInfo.GetCultureInfo(Helpers.appSettings._User.Culture)).ToUpper();
                        ViewBag.MesAnterior = new DateTime(ano, mes, 1).AddMonths(-1).ToString("MMMM", System.Globalization.CultureInfo.GetCultureInfo(Helpers.appSettings._User.Culture)).ToUpper();

                        _viewModel.Overhead = provider.SLT_COMPARATIVO_MENSAL_ANTERIOR(3, mes, ano);
                        break;
                    case "plan":
                        ViewBag.MesAtual = string.Concat("AC ", new DateTime(ano, mes, 1).ToString("MMMM", System.Globalization.CultureInfo.GetCultureInfo(Helpers.appSettings._User.Culture)).ToUpper());
                        ViewBag.MesAnterior = string.Concat("PLAN ", new DateTime(ano, mes, 1).ToString("MMMM", System.Globalization.CultureInfo.GetCultureInfo(Helpers.appSettings._User.Culture)).ToUpper());

                        _viewModel.Overhead = provider.SLT_COMPARATIVO_MENSAL_OVERHEAD(3, mes, ano);
                        break;
                    case "forcast":
                        ViewBag.MesAtual = new DateTime(ano, mes, 1).ToString("MMMM", System.Globalization.CultureInfo.GetCultureInfo(Helpers.appSettings._User.Culture)).ToUpper();
                        _viewModel.Overhead = provider.SLT_COMPARATIVO_MENSAL_FORCAST(3, mes, ano);

                        if (_viewModel.Overhead.Count > 0)
                        {
                            ViewBag.MesAnterior = _viewModel.Overhead[0].FCAST;
                        }
                        break;
                    default:
                        break;
                }

                //return View(provider.SLT_OVERHEAD_UMB(4, mes, ano));

                return View(_viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, string.Concat("Ocorreu um erro na hora de processar a solicitação: ", ex.Message));
                return View(_viewModel);
            }
        }
    }
}