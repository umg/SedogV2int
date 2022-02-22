using SEDOGv2.Helpers;
using SEDOGv2.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace SEDOGv2.Controllers.AtualizacaoTabelas
{
    public class AtualizaTabMensalController : Controller
    {
        public static bool TarefaEmAndamento { get; set; } = false;

        // GET: AtualizaTabMensal
        public ActionResult Index()
        {
            ViewBag.TarefaEmAndamento = TarefaEmAndamento;
            ViewBag.AlertaAviso = false;
            if (TempData["AlertaAviso"] != null)
            {
                ViewBag.AlertaAviso = bool.Parse(TempData["AlertaAviso"].ToString());
                ViewBag.Mensagem = TempData["Mensagem"].ToString();
            }
            else if (TarefaEmAndamento && TempData["AlertaSucesso"] == null)
            {
                ViewBag.AlertaAviso = true;
                ViewBag.Mensagem = "Existe uma tarefa em andamento.";
            }
            else if (TempData["AlertaSucesso"] != null)
            {
                ViewBag.AlertaSucesso = bool.Parse(TempData["AlertaSucesso"].ToString());
                ViewBag.Mensagem = TempData["Mensagem"].ToString();
            }
            return View();
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult AtualizaTabMensal(FormCollection collection)
        {
            var dataReferencia = new DateTime();
            if (DateTime.TryParse(collection["dataReferencia"], out dataReferencia))
            {
                try
                {
                    var context = System.Web.HttpContext.Current;
                    TarefaEmAndamento = true;
                    HostingEnvironment.QueueBackgroundWorkItem(
                        clt => StartProcessing(context, dataReferencia, clt)
                    );
                }
                catch (Exception)
                {
                    TempData["AlertaAviso"] = true;
                    TempData["Mensagem"] = "Ops. Ocorreu um erro interno!";
                }

                TempData["AlertaSucesso"] = TarefaEmAndamento ? true : false;
                TempData["Mensagem"] = "Tarefa iniciada com sucesso";
                Thread.Sleep(10000);
            }
            else
            {
                TempData["AlertaAviso"] = true;
                TempData["Mensagem"] = "Preencha a data corretamente";
            }

            return RedirectToAction("Index");
        }
        public void StartProcessing(HttpContext context, DateTime dataReferencia, CancellationToken cancellationToken = default(CancellationToken))
        {
            var provider = new PLProjetoProvider_ext(context);
            if (appSettings.Ambiente == "PTSEDOG") { provider.PL_PROCESSO_DIGITAL_MENSAL(dataReferencia, "PTDIGITAL");  }
            if (appSettings.Ambiente == "MXSEDOG") { provider.PL_PROCESSO_DIGITAL_MENSAL(dataReferencia, "MXDIGITAL"); }
            if (appSettings.Ambiente == "ESSEDOG") { provider.PL_PROCESSO_DIGITAL_MENSAL(dataReferencia, "ESDIGITAL"); }
            //provider.PL_PROCESSO_DIGITAL_MENSAL(dataReferencia);
            //Helpers.functions.SendEmail(Helpers.appSettings.DestinatarioEmailTabMensal, Helpers.appSettings.CopiaEmailTabMensal, new string[] { }, "Atualização Tab Mensal", $"Prezados,<br><br>A Atualização \"Tab Mensal\" foi finalizada com sucesso para o mês {dataReferencia.Month}.<br><br>Atenciosamente,<br>SEDOG", null);
            TarefaEmAndamento = false;
        }

        private Task LongRunningFunc(DateTime dataReferencia, CancellationToken clt)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(60000);
                TarefaEmAndamento = false;
            });
        }
    }
}