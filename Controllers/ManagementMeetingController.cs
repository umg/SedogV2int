using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SEDOGv2.Controllers.Financeiro
{
    public class ManagementMeetingController : Controller
    {

        // GET: ManagementMeeting
        public ActionResult Index()
        {
            var managementMeetings = new List<ManagementMeeting>();
            try
            {
                var provider = new PLProjetoProvider();
                managementMeetings = provider.SEL_MANAGEMENT_MEETING();
            }
            catch (Exception e)
            {
                ViewBag.MessageError = "ERRO:" + e.Message.ToString();
            }

            if (TempData["MessageSuccess"] != null)
            {
                ViewBag.MessageSuccess += TempData["MessageSuccess"].ToString();
            }
            if (TempData["MessageError"] != null)
            {
                ViewBag.MessageError += "ERRO:" + TempData["MessageError"].ToString();
            }
            return View(managementMeetings);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var file = Request.Files.Count > 0 ? Request.Files.Get(0) : null;
            var managementMeetings = new List<ManagementMeeting>();
            var provider = new PLProjetoProvider();
            var directory = Server.MapPath("~/Files/ManagementMeeting");

            var managementMeeting = new ManagementMeeting()
            {
                Nome = collection["nome"].ToString(),
                Descricao = collection["descricao"].ToString(),
                DataCadastro = DateTime.Now,
                FilePath = file.ContentLength != 0 ? Path.Combine(directory, Path.GetFileName(file.FileName)) : null,
                FileExtension = file.ContentLength != 0 ? Path.GetExtension(file.FileName) : null
            };

            if (managementMeeting.IsValid())
            {
                try
                {
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    file.SaveAs(managementMeeting.FilePath);

                    provider.INS_MANAGEMENT_MEETING(managementMeeting);

                    ViewBag.MessageSuccess = "Success! File saved.";

                }
                catch (Exception ex)
                {
                    ViewBag.MessageError = "ERROR:" + ex.Message.ToString();
                }
            }
            else
            {
                managementMeeting.ModelErros.ForEach(x => ModelState.AddModelError(string.Empty, x));
                ViewBag.MessageInfo = "The file can't be saved";
            }

            try
            {
                managementMeetings = provider.SEL_MANAGEMENT_MEETING();
            }
            catch (Exception ex)
            {
                ViewBag.MessageError += "ERROR:" + ex.Message.ToString();
            }

            return View(managementMeetings);
        }

        [HttpGet]
        public FileResult Download(string nomeArquivo, string caminhoArquivo)
        {
            var fileBytes = System.IO.File.ReadAllBytes(caminhoArquivo);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, nomeArquivo);
        }

        [ActionFilter_CheckLogin]
        [HttpDelete]
        public ActionResult DeleteArquivo(int id)
        {
            var managementMeetings = new List<ManagementMeeting>();
            var provider = new PLProjetoProvider();

            try
            {
                managementMeetings = provider.SEL_MANAGEMENT_MEETING();
            }
            catch (Exception e)
            {
                TempData["MessageError"] = "ERRO:" + e.Message.ToString();
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                provider.DEL_MANAGEMENT_MEETING(id);

                var pathArquivo = managementMeetings.Where(x => x.ManagementMeetingId == id).Select(x => x.FilePath).FirstOrDefault();

                if (System.IO.File.Exists(pathArquivo))
                    System.IO.File.Delete(pathArquivo);

                managementMeetings.Remove(managementMeetings.FirstOrDefault(x => x.ManagementMeetingId == id));

                TempData["MessageSuccess"] = "Success! File deleted.";

            }
            catch (Exception e)
            {
                TempData["MessageError"] = "ERROR:" + e.Message.ToString();
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}