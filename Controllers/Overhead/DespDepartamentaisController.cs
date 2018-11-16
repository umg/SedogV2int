using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using System.IO;
using System.Configuration;

namespace SEDOGv2.Controllers.Overhead
{
    public class DespDepartamentaisController : Controller
    {
        // GET: DespDepartamentais
        public ActionResult Index()
        {
            //viewModel._Departamentos.AddRange();
            DespDepartamentaisViewModel viewModel = new DespDepartamentaisViewModel();
            viewModel = Load();
            viewModel._Listado = false;
            viewModel._selectedMes = DateTime.Now.Month;
            viewModel._selectedAno = int.Parse(DateTime.Now.ToString("yy"));
            return View(viewModel);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            DespDepartamentaisViewModel viewModel = new DespDepartamentaisViewModel();
            try
            {
                PLProjetoProvider provider = new Models.Context.PLProjetoProvider();
                viewModel = Load();

                viewModel._Listado = true;

              

                viewModel._selectedAno = int.Parse(collection["ano"]);
                viewModel._selectedMes = int.Parse(collection["mes"]);
                viewModel._selectedDepartamento = int.Parse(collection["departamento"]);

                int mes = viewModel._selectedMes;
                int ano = viewModel._selectedAno;
                int departamento = viewModel._selectedDepartamento;

                viewModel._hasDepartamento = (departamento != 0);

                //string financeiro = "1";
                string financeiro = "0";
                //Helpers.appSettings._User.Departamento == "FINANCEIRO")
                if (ConfigurationManager.AppSettings["contasSalarios"].Contains(Helpers.appSettings._User.Login))
                    financeiro = "1";

                viewModel._DespDepartamento = new List<DespDepartamento>();
                viewModel._DespDepartamento.AddRange(provider.SLT_DESP_DEPARTAMENTOS(mes, ano, departamento, financeiro));

                ViewBag.MesAtual = new DateTime(ano, mes, 1).ToString("MMM", System.Globalization.CultureInfo.GetCultureInfo(Helpers.appSettings._User.Culture)).ToUpper();
                if (viewModel._DespDepartamento.Count > 0)
                {
                    ViewBag.Forecast = viewModel._DespDepartamento[0].FCAST;
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(new List<OverheadViewModel>());
            }
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult SendEmail(FormCollection collection)
        {
            //viewModel._Departamentos.AddRange();
            DespDepartamentaisViewModel viewModel = new DespDepartamentaisViewModel();

            try
            {
                viewModel = Load();

                viewModel._Listado = false;

                PLProjetoProvider provider = new Models.Context.PLProjetoProvider();
                List<Departamento> Listadepartamentos = provider.SEL_DEPARTAMENTOSJDE();

                string[] ids = collection["emails"].Split(',');
                int ano = Convert.ToInt32(collection["emailano"]);
                int mes = Convert.ToInt32(collection["emailmes"]);
                string financeiro = "0";
                string mesatual = new DateTime(ano, mes, 1).ToString("MMM", System.Globalization.CultureInfo.GetCultureInfo(Helpers.appSettings._User.Culture)).ToUpper();

                string css = "";
                StreamReader strR = new StreamReader(Server.MapPath("~/Content/report.css"));
                css = strR.ReadToEnd();
                strR.Close();

                string body = "Attached is the departmental expense files. <br> For the departments that have overflow in the month, please send explanations to the Budget department within 10 days of this date. <br> Regards, SEDOG";

                foreach (string id in ids)
                {
                    ResponsavelDepartamento _responsavel = viewModel._ResponsavelDepartamento.Where(x => x.ID == Convert.ToInt32(id)).FirstOrDefault();
                    List<System.Net.Mail.Attachment> atts = new List<System.Net.Mail.Attachment>();
                    string[] departamentos = _responsavel.IDDEPARTAMENTO.Split(';');




                    foreach (string _departamento in departamentos)
                    {
                        //List<DespDepartamento> _despDepartamento = provider.SLT_DESP_DEPARTAMENTOS(mes, ano, _departamento, financeiro);
                        int depInt = Convert.ToInt32(_departamento);
                        List<DespDepartamento> _despDepartamento = provider.SLT_DESP_DEPARTAMENTOS(mes, ano, depInt, financeiro);
                        string nomeDepartamento = Listadepartamentos.Where(x => x.ID == depInt).Select(y => y.MCDL01).FirstOrDefault();

                        decimal MES_ACTION = 0;
                        decimal MES_FCAST = 0;
                        decimal MES_PLAN = 0;
                        decimal DIF_ACTION_FCAST = 0;
                        decimal DIF_ACTION_PLAN = 0;
                        decimal AC_ACTION = 0;
                        decimal AC_FCAST = 0;
                        decimal AC_PLAN = 0;
                        decimal DIF_YTD_ACTION_FCAST = 0;
                        decimal DIF_YTD_ACTION_PLAN = 0;


                        if (_despDepartamento.Count > 0)
                        {
                            string table = "<table style='width:100%; white-space:nowrap;'>";
                            table = string.Concat(table, "<tr>");
                            table = string.Concat(table, "<td colspan='13' class='left'><img src='", SEDOGv2.Controllers.ContentToPrint.ResolveServerUrl("/Content/themes/base/images/logomarca_bw.jpg", false), "' width='141' height='84' style='width:141px; height: 84px' /></td>");
                            table = string.Concat(table, "</tr><tr>");
                            table = string.Concat(table, "<td colspan='13' style='text-align:center'><h2 style='text-align:center' class='bold underline'>Overhead</h2></td>");
                            table = string.Concat(table, "</tr><tr>");
                            table = string.Concat(table, "<td colspan='13' style='text-align:center'><h2 style='text-align:center' class='bold underline'>Departmental Expenses: ", nomeDepartamento, "</h2></td>");
                            table = string.Concat(table, "</tr><tr>");
                            table = string.Concat(table, "<td colspan='5'>&nbsp;</td>");
                            table = string.Concat(table, "<td>", DateTime.Now.ToString("MMMM").ToUpper(), "/", DateTime.Now.Year, "</td>");
                            table = string.Concat(table, "<td>", DateTime.Now.ToString("dd/MM/yyyy"), " às ", DateTime.Now.ToString("HH:mm:ss"), "</td>");
                            table = string.Concat(table, "</tr><tr>");
                            table = string.Concat(table, "<td>Account</td>");
                            table = string.Concat(table, "<td>Sub</td>");
                            table = string.Concat(table, "<td>Desrciption</td>");
                            table = string.Concat(table, "<td>Act. ", mesatual, "</td>");
                            table = string.Concat(table, "<td>", _despDepartamento[0].FCAST, "</td>");
                            table = string.Concat(table, "<td>Plan ", mesatual, "</td>");
                            table = string.Concat(table, "<td>Var. Act/Fcast ", mesatual, "</td>");
                            table = string.Concat(table, "<td>Var. Act/Plan ", mesatual, "</td>");
                            table = string.Concat(table, "<td>Act YTD</td>");
                            table = string.Concat(table, "<td>Plan. YTD</td>");
                            table = string.Concat(table, "<td>Fcast YTD</td>");
                            table = string.Concat(table, "<td>Var. Action/Fcast YTD</td>");
                            table = string.Concat(table, "<td>Var. Action/Plan YTD</td>");
                            table = string.Concat(table, "</tr>");

                            foreach (DespDepartamento item in _despDepartamento)
                            {
                                table = string.Concat(table, "<tr>");
                                table = string.Concat(table, "<td>", item.GBOBJ, "</td>");
                                table = string.Concat(table, "<td>", item.GBSUB, "</td>");
                                table = string.Concat(table, "<td>", item.GMDL01, "</td>");
                                table = string.Concat(table, "<td>", string.Format("{0:C2}", item.MES_ACTION), "</td>");
                                table = string.Concat(table, "<td>", string.Format("{0:C2}", item.MES_FCAST), "</td>");
                                table = string.Concat(table, "<td>", string.Format("{0:C2}", item.MES_PLAN), "</td>");
                                table = string.Concat(table, "<td>", string.Format("{0:C2}", item.DIF_ACTION_FCAST), "</td>");
                                table = string.Concat(table, "<td>", string.Format("{0:C2}", item.DIF_ACTION_PLAN), "</td>");
                                table = string.Concat(table, "<td>", string.Format("{0:C2}", item.AC_ACTION), "</td>");
                                table = string.Concat(table, "<td>", string.Format("{0:C2}", item.AC_FCAST), "</td>");
                                table = string.Concat(table, "<td>", string.Format("{0:C2}", item.AC_PLAN), "</td>");
                                table = string.Concat(table, "<td>", string.Format("{0:C2}", item.DIF_YTD_ACTION_FCAST), "</td>");
                                table = string.Concat(table, "<td>", string.Format("{0:C2}", item.DIF_YTD_ACTION_PLAN), "</td>");
                                table = string.Concat(table, "</tr>");

                                MES_ACTION += item.MES_ACTION;
                                MES_FCAST += item.MES_FCAST;
                                MES_PLAN += item.MES_PLAN;
                                DIF_ACTION_FCAST += item.DIF_ACTION_FCAST;
                                DIF_ACTION_PLAN += item.DIF_ACTION_PLAN;
                                AC_ACTION += item.AC_ACTION;
                                AC_FCAST += item.AC_FCAST;
                                AC_PLAN += item.AC_PLAN;
                                DIF_YTD_ACTION_FCAST += item.DIF_YTD_ACTION_FCAST;
                                DIF_YTD_ACTION_PLAN += item.DIF_YTD_ACTION_PLAN;
                            }

                            table = string.Concat(table, "<tr class='trTotal'>");
                            table = string.Concat(table, "<td colspan='3'>Total</td>");
                            table = string.Concat(table, "<td>", string.Format("{0:C2}", MES_ACTION), "</td>");
                            table = string.Concat(table, "<td>", string.Format("{0:C2}", MES_FCAST), "</td>");
                            table = string.Concat(table, "<td>", string.Format("{0:C2}", MES_PLAN), "</td>");
                            table = string.Concat(table, "<td>", string.Format("{0:C2}", DIF_ACTION_FCAST), "</td>");
                            table = string.Concat(table, "<td>", string.Format("{0:C2}", DIF_ACTION_PLAN), "</td>");
                            table = string.Concat(table, "<td>", string.Format("{0:C2}", AC_ACTION), "</td>");
                            table = string.Concat(table, "<td>", string.Format("{0:C2}", AC_FCAST), "</td>");
                            table = string.Concat(table, "<td>", string.Format("{0:C2}", AC_PLAN), "</td>");
                            table = string.Concat(table, "<td>", string.Format("{0:C2}", DIF_YTD_ACTION_FCAST), "</td>");
                            table = string.Concat(table, "<td>", string.Format("{0:C2}", DIF_YTD_ACTION_PLAN), "</td>");

                            table = string.Concat(table, "</tr>");
                            table = string.Concat(table, "</table>");


                            Byte[] bytes = null;
                            using (var ms = new MemoryStream())
                            {
                                string final = "<html><head><style type='text/css'>";
                                final += css;
                                final += "</style></head><body>";
                                final += table;
                                final += "</body></html>";
                                StreamWriter writer = new StreamWriter(ms, System.Text.Encoding.UTF8);
                                writer.Write(final);
                                writer.Flush();
                                bytes = ms.ToArray();
                            }

                            atts.Add(new System.Net.Mail.Attachment(new System.IO.MemoryStream(bytes), string.Concat(nomeDepartamento, ".xls"), "application/vnd.ms-excel"));

                            //Helpers.functions.SendEmail(_responsavel.EMAIL.Split(';'), _responsavel.COPIAS.Split(';'), _responsavel.COPIASOCULTA.Split(';'), "Despesas Departamentais Diretoria", body, atts);
                            //Erik.Jordan@umusic.com
                        }
                    }
                    string auxBody = string.Concat("E-mails: ", _responsavel.EMAIL, "<br>", body);
                    Helpers.functions.SendEmail(new string[] { "felipe.santanna@umusic.com" }, new string[] { "felipe.santanna@umusic.com" }, _responsavel.COPIASOCULTA.Split(';'), "Despesas Departamentais Diretoria", auxBody, atts.ToArray());
                }
                ViewBag.Message = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.SUCCESS, "E-mail sent");
            }
            catch(Exception ex)
            {
                ViewBag.Message = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, ex.Message);
            }
            return RedirectToAction("Index", viewModel);
        }

        public DespDepartamentaisViewModel Load()
        {
            PLProjetoProvider provider = new Models.Context.PLProjetoProvider();

            DespDepartamentaisViewModel viewModel = new DespDepartamentaisViewModel();
            viewModel._ResponsavelDepartamento = new List<ResponsavelDepartamento>();
            viewModel._ResponsavelDepartamento.AddRange(provider.SEL_RESPONSAVEL_POR_DEPARTAMENTO());

            if (Helpers.appSettings._ResponsavelDepartamento != null)
                Helpers.appSettings._ResponsavelDepartamento = viewModel._ResponsavelDepartamento;

            viewModel._Departamentos = provider.SEL_DEPARTAMENTOSJDE_POR_USUARIOS(Helpers.appSettings._User.Login);

            viewModel._Departamentos = viewModel._Departamentos.Where(d => d.LOGIN == Helpers.appSettings._User.Login).ToList();

            return viewModel;
        }

    }
}