using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Helpers;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace SEDOGv2.Controllers.Relatorios
{
    public class DireitosProfitLossController : Controller
    {
        // GET: DireitosProfitLoss
        public ActionResult Index()
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            DireitosProfitLoss _model = new Models.DireitosProfitLoss();

            _model._PLProjetos = provider.SEL_PL_PROJETO_SEDOG(0);
            ViewBag.contrato = "3";

            return View(_model);
        }


        [ActionFilter_CheckLogin]
        public JsonResult ProjetoLookup(string term)
        {

            PLProjetoProvider provider = new PLProjetoProvider();
            List<PLProjeto> ret = provider.SEL_LOOKUP_PL_PROJETO(term);

            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string[] auxSplit = collection["contrato"].Split('|');
            ViewBag.contrato = auxSplit[0];
            ViewBag.contratoextenso = string.Concat(auxSplit[1], " - ", DateTime.Now.ToString("dd/MM/yyyy"));

            PLProjetoProvider provider = new PLProjetoProvider();
            DireitosProfitLoss _model = new Models.DireitosProfitLoss();
            string log = "";

            if (ViewBag.contrato == "3")
            {
                string[] s = collection["nomeArtista"].Split('-');
                s[0] = s[0].Trim();
                ViewBag.contratoextenso = string.Concat(s[1], " - ", DateTime.Now.ToString("dd/MM/yyyy"));
                s[1] = s[1].TrimStart().TrimEnd().cleanString().Replace(' ', '-');


                _model._parceiros = provider.SLT_DIREITOS_PROJETO_PLATAFORMA_DIGITAL(Convert.ToInt32(s[0]));

                _model._vendasDireito = provider.SLT_DIREITOS_PROJETO(Convert.ToInt32(s[0]));
                _model._canalDeVendas = provider.SLT_DIREITOS_PROJETO_GRUPO_DIGITAL(Convert.ToInt32(s[0]));
                List<System.Collections.IEnumerable> numerables = new List<System.Collections.IEnumerable>();
                numerables.Add(_model._canalDeVendas);
                numerables.Add(_model._vendasDireito);


                string reportModelPath = Server.MapPath("~/Reports/DireitosProfitLoss.rpt");
                string reportSavePath = Server.MapPath("~/Reports/PDF/");

                log += reportModelPath + ";;";
                log += reportSavePath + ";;";
                log += "pdf;;";
                byte[] rpt = null;
                Helpers.Reports export = new Helpers.Reports();


                ReportParamValues param = new ReportParamValues();
                param.paraName = "ProjetoAnoMes";
                param.paraValor = ViewBag.contratoextenso;

                //List<ReportParamValues> reportParams = new List<ReportParamValues>();
                //reportParams.Add()

                string contentType = "application/pdf";
                rpt = export.ExportToPDF(reportModelPath, reportSavePath, s[1], _model._parceiros, new List<Helpers.ReportParamValues>() { param }, numerables);
                ViewBag.enviando = "true";
                return File(rpt, contentType, string.Concat(s[1], ".pdf"));
            }
            else
            {
                string reportModelPath = Server.MapPath("~/Reports/DireitosProfitLoss.rpt");
                string reportSavePath = Server.MapPath("~/Reports/PDF/");
                MemoryStream outputMemStream = new MemoryStream();
                ZipOutputStream zipStream = new ZipOutputStream(outputMemStream);
                zipStream.SetLevel(9); //0-9, 9 being the highest level of compression
                int _contadores = 0;
                string zipFilename = "IDs";

                foreach (string projeto in collection["chkprojetos"].Split(','))
                {
                    string[] s = projeto.Split('|');

                    s[0] = s[0].Trim();
                    ViewBag.contratoextenso = string.Concat(s[1], " - ", DateTime.Now.ToString("dd/MM/yyyy"));
                    s[1] = s[1].Replace("[$1]", ",").TrimStart().TrimEnd().cleanString().Replace(' ', '-'); //desfaz o código inventado para as vírgulas[$1]
                    zipFilename = string.Concat(zipFilename, "-", s[0]);
                    _model._parceiros = provider.SLT_DIREITOS_PROJETO_PLATAFORMA_DIGITAL(Convert.ToInt32(s[0]));

                    _model._vendasDireito = provider.SLT_DIREITOS_PROJETO(Convert.ToInt32(s[0]));
                    _model._canalDeVendas = provider.SLT_DIREITOS_PROJETO_GRUPO_DIGITAL(Convert.ToInt32(s[0]));

                    if (_model._parceiros.Count > 0 && _model._vendasDireito.Count > 0 && _model._canalDeVendas.Count > 0)
                    {
                        
                        List<System.Collections.IEnumerable> numerables = new List<System.Collections.IEnumerable>();
                        numerables.Add(_model._canalDeVendas);
                        numerables.Add(_model._vendasDireito);

                        log += reportModelPath + ";;";
                        log += reportSavePath + ";;";
                        log += "pdf;;";
                        byte[] rpt = null;
                        Helpers.Reports export = new Helpers.Reports();

                        ReportParamValues param = new ReportParamValues();
                        param.paraName = "ProjetoAnoMes";
                        param.paraValor = ViewBag.contratoextenso;

                        //List<ReportParamValues> reportParams = new List<ReportParamValues>();
                        //reportParams.Add()

                        rpt = export.ExportToPDF(reportModelPath, reportSavePath, s[1], _model._parceiros, new List<Helpers.ReportParamValues>() { param }, numerables);

                        byte[] buffer = new byte[4096];
                        ZipEntry entry = new ZipEntry(string.Concat(s[1], ".pdf"));
                        entry.DateTime = DateTime.Now;
                        zipStream.PutNextEntry(entry);

                        zipStream.Write(rpt, 0, rpt.Length);
                        _contadores++;
                    }
                    else
                    {
                        ViewBag.Message = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, string.Concat( ViewBag.contratoextenso, " está zerado! /n" ));
                    }

                }

                
                    
                if (_contadores > 0)
                {

                    zipStream.CloseEntry();
                    zipStream.IsStreamOwner = false;
                    zipStream.Close();

                    outputMemStream.Position = 0;

                    zipFilename = string.Concat(zipFilename, "-date-", DateTime.Now.ToString("ddMMyyyy"));

                    return File(outputMemStream, "application/zip", string.Concat(zipFilename, ".zip"));
                }
                else
                {
                    _model._PLProjetos = provider.SEL_PL_PROJETO_SEDOG(0);
                    //ViewBag.contrato = "3";

                    return View(_model);
                }
                
            }
            //FIM!
        }

    }
}