using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using SEDOGv2.Helpers;

namespace SEDOGv2.Controllers.AtualizacaoTabelas
{
    public class AtualizaGTSController : Controller
    {
        // GET: AtualizaGTS
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
                TempData.Remove("Message");
            }

            ViewBag.anoALL = DateTime.Now.Year;
            PLProjetoProvider provider = new PLProjetoProvider();
            AtualizaGTSViewModel _model = new Models.AtualizaGTSViewModel();
            _model.Artista = new List<Models.ArtistaProjeto>();
            _model.Artista = provider.SLT_ARTISTAS_GTS("");

            return View(_model);
        }

        [ActionFilter_CheckLogin]
        public JsonResult ArtistaLookup(string term)
        {

            PLProjetoProvider provider = new PLProjetoProvider();
            List<ArtistaProjeto> ret = provider.SLT_ARTISTA_PROJETO(term);

            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public string RemoverArtista(string term)
        {
            
            string retorno = "";

            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                retorno = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.SUCCESS, string.Concat("Artista(", term, ") removido com sucesso!"));

            }
            catch(Exception ex)
            {
                retorno = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, ex.Message);
            }

            return retorno;
        }


        [ActionFilter_CheckLogin]
        [HttpPost]
        public string UploadImage()
        {

            string retorno = "";

            try
            {
                HttpPostedFileBase file = Request.Files["fileUpload"];
                string idartista = Request["idartista"];
                string artista = Request["artista"];

                string _path = HttpContext.Server.MapPath(string.Concat(ConfigurationManager.AppSettings["IMAGEPATH"], idartista, ".png")).ToString();
                if (System.IO.File.Exists(_path))
                    System.IO.File.Delete(_path);

                Bitmap b = (Bitmap)Bitmap.FromStream(file.InputStream);

                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(_path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        b.Save(memory, ImageFormat.Png);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
                retorno = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.SUCCESS, string.Concat("Imagem do Artista ( ", artista, " ) salva com sucesso!"));


                /* PLProjetoProvider provider = new PLProjetoProvider();

                 //criar metodo de salvar imagem
                 string _path = string.Concat( ConfigurationManager.AppSettings["IMAGEPATH"], term, ".png");

                 StreamReader sr = new StreamReader(file);


                 Bitmap b = (Bitmap)Bitmap.FromStream(sr.BaseStream);
                 using (MemoryStream ms = new MemoryStream())
                 {
                     b.Save(ms, ImageFormat.Png);

                     // use the memory stream to base64 encode..
                 }

                 /*System.IO.FileInfo fn = new FileInfo(_path);
                 Response.Write(fn.FullName);


                 if (!File.Exists(_path))
                     return new StreamWriter(File.Create(this.Path));
                 else
                     return File.AppendText(this.Path);*/




                //retorno = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.SUCCESS, string.Concat("Imagem do Artista(", term, ") salva com sucesso!"));

            }
            catch (Exception ex)
            {
                retorno = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, ex.Message);
            }

            return retorno;
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            AtualizaGTSViewModel model = new Models.AtualizaGTSViewModel();
            PLProjetoProvider provider = new PLProjetoProvider();

            if (!string.IsNullOrEmpty(collection["nomeArtista"]))
            {
            
                string[] filterPesquisaNome = collection["nomeArtista"].Split('-');
                string nomeartista = filterPesquisaNome[1].Trim();
                int ano = Convert.ToInt32(collection["anoALL"]);

                model.Infos = new InfoGTSViewModel();
                model.Infos.Nome = filterPesquisaNome[0];
                model.Infos.IDArtista = nomeartista;
                model.Infos.Ano = collection["anoALL"];
                model.Infos.Pesquisa = true;
                model.Infos.IDTipo = collection["selTipo"];
                ViewBag.IDTipo = collection["selTipo"];
                ViewBag.anoALL = collection["anoALL"];
                ViewBag.consultaNomeCodArtista = collection["nomeArtista"];

                model.Artista = new List<Models.ArtistaProjeto>();
                model.Artista = provider.SLT_ARTISTAS_GTS("");

                switch (collection["selTipo"])
                {
                    case "1":
                        model._GTSForecast = new List<Models.GTSForecast>();
                        model._GTSForecast = provider.SEL_GTS_FCAST_PLAN(nomeartista, ano);
                        model.Infos.Tipo = "Forecast";

                        if (model._GTSForecast == null || model._GTSForecast.Count == 0)
                        {
                            ExecuteGTSForecast(nomeartista, ano.ToString());
                            model._GTSForecast = provider.SEL_GTS_FCAST_PLAN(nomeartista, ano);
                        }

                        break;
                    case "2":
                        //RECEITA
                        model._GTSReceitaShows = new List<GTSReceitaShows>();
                        model._GTSRegiaoReceitaShows = provider.SEL_GTS_RECEITA_SHOWS(nomeartista, ano);
                        model.GTSIsReceita = true;
                        model.Infos.Tipo = "Receita";

                        if (model._GTSRegiaoReceitaShows == null || model._GTSRegiaoReceitaShows.Count == 0)
                        {
                            ExecuteGTSReceitaShows(nomeartista, ano.ToString());
                            model._GTSRegiaoReceitaShows = provider.SEL_GTS_RECEITA_SHOWS(nomeartista, ano);
                        }

                        break;
                    case "3":
                        //SHOW
                        model._GTSReceitaShows = new List<GTSReceitaShows>();
                        model._GTSRegiaoReceitaShows = provider.SEL_GTS_RECEITA_SHOWS(nomeartista, ano);
                        model.GTSIsShows = true;
                        model.Infos.Tipo = "Shows";

                        if (model._GTSRegiaoReceitaShows == null || model._GTSRegiaoReceitaShows.Count == 0)
                        {
                            ExecuteGTSReceitaShows(nomeartista, ano.ToString());
                            model._GTSRegiaoReceitaShows = provider.SEL_GTS_RECEITA_SHOWS(nomeartista, ano);
                        }

                        break;
                    case "4":
                        model._GTSSaldos = new List<GTSSaldos>();
                        model._GTSSaldos = provider.SEL_GTS_SALDOS(nomeartista);
                        model.Infos.Tipo = "GTS Saldo";

                        if (model._GTSSaldos == null || model._GTSSaldos.Count == 0)
                        {
                            ExecuteGTSSaldos(nomeartista);
                            model._GTSSaldos = provider.SEL_GTS_SALDOS(nomeartista);
                        }

                        break;
                    case "5":
                        model._GTSMarginCachePartc = new List<GTSMarginCachePartc>();
                        model._GTSMarginCachePartc = provider.SEL_GTS_MARGEN_CACHE_PARTC(nomeartista, ano);
                        model.Infos.Tipo = "Margin, Cachê e Participação";

                        if (model._GTSMarginCachePartc == null || model._GTSMarginCachePartc.Count == 0)
                        {
                            ExecuteGTSMarginCachePartc(nomeartista, ano.ToString());
                            model._GTSMarginCachePartc = provider.SEL_GTS_MARGEN_CACHE_PARTC(nomeartista, ano);
                        }

                        break;
                    default:
                        break;
                }

            }
            else
            {
                TempData["Message"] = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, "Você precisa escolher um artista");
                return RedirectToAction("Index");
            }
            
            return View(model);
        }

        [ActionFilter_CheckLogin]
        public ActionResult UpdateGTSForecast(GTSForecast[] valores)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                string updateString = "UPDATE " + provider.AddScheme("GTS_FCAST_PLAN") + " SET (VALOR_FCAST_RECEITA , VALOR_PLAN_RECEITA , VALOR_FCAST_MARGIN , VALOR_PLAN_MARGIN) = ";

                foreach (GTSForecast val in valores)
                {
                    string s = updateString + "(" + val.VALOR_FCAST_RECEITA + " , " + val.VALOR_PLAN_RECEITA + " , " + val.VALOR_FCAST_MARGIN + " , " + val.VALOR_PLAN_MARGIN + ") WHERE IDARTISTA = '" + val.IDARTISTA + "' AND MES = " + val.MES + " AND ANO = " + val.ANO;
                    provider.ExecuteCommandSQL(s);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, responseText = "Updated!" }, JsonRequestBehavior.AllowGet);
        }

        [ActionFilter_CheckLogin]
        public ActionResult UpdateGTSReceita(GTSRegiaoReceitaShows[] valores)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                string updateString = "UPDATE " + provider.AddScheme("GTS_RECEITA_SHOWS") + " SET (VALOR) = ";

                foreach (GTSRegiaoReceitaShows val in valores)
                {
                    foreach (string regiao in val.Regioes)
                    {
                        string s = updateString + "(" + val.GetType().GetProperty(string.Concat("VL", regiao.Replace(" ", ""))).GetValue(val, null) + ") WHERE IDARTISTA = '" + val.IDARTISTA + "' AND  REGIAO = '" + regiao + "' AND MES = " + val.MES + " AND ANO = " + val.ANO;
                        provider.ExecuteCommandSQL(s);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, responseText = "Updated!" }, JsonRequestBehavior.AllowGet);
        }

        [ActionFilter_CheckLogin]
        public ActionResult UpdateGTSShows(GTSRegiaoReceitaShows[] valores)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                string updateString = "UPDATE " + provider.AddScheme("GTS_RECEITA_SHOWS") + " SET (QUANTIDADE) = ";

                foreach (GTSRegiaoReceitaShows val in valores)
                {
                    foreach (string regiao in val.Regioes)
                    {
                        string s = updateString + "(" + val.GetType().GetProperty(string.Concat("QTD", regiao.Replace(" ", ""))).GetValue(val, null) + ") WHERE IDARTISTA = '" + val.IDARTISTA + "' AND  REGIAO = '" + regiao + "' AND MES = " + val.MES + " AND ANO = " + val.ANO;
                        provider.ExecuteCommandSQL(s);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, responseText = "Updated!" }, JsonRequestBehavior.AllowGet);
        }

        [ActionFilter_CheckLogin]
        public ActionResult UpdateGTSSaldos(GTSSaldos[] valores)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                string updateString = "UPDATE " + provider.AddScheme("GTS_SALDOS") + " SET (VALOR) = ";

                foreach (GTSSaldos val in valores)
                {
                    string s = updateString + "(" + val.VALOR + ") WHERE IDARTISTA = " + val.IDARTISTA;
                    provider.ExecuteCommandSQL(s);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, responseText = "Updated!" }, JsonRequestBehavior.AllowGet);
        }

        [ActionFilter_CheckLogin]
        public ActionResult UpdateGTSMargenCachePartc(GTSMarginCachePartc[] valores)
          {
              try
              {
                  PLProjetoProvider provider = new PLProjetoProvider();
                  string updateString = "UPDATE " + provider.AddScheme("GTS_MARGEN_CACHE_PARTC") + " SET (VALOR_ARTISTA , VALOR_LIVE , VALOR_GTS , VALOR_CACHE , VALOR_MARGEM) = ";

                  foreach (GTSMarginCachePartc val in valores)
                  {
                      string s = updateString + "(" + val.VALOR_ARTISTA + " , " + val.VALOR_LIVE + " , " + val.VALOR_GTS + " , " + val.VALOR_CACHE + " , " + val.VALOR_MARGEM + ") WHERE IDARTISTA = '" + val.IDARTISTA + "' AND MES = '" + val.MES + "' AND ANO = " + val.ANO;
                      provider.ExecuteCommandSQL(s);
                  }
              }
              catch (Exception ex)
              {
                  return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
              }
            return Json(new { success = true, responseText = "Updated!" }, JsonRequestBehavior.AllowGet);
        }

        private void ExecuteGTSForecast(string nomeartista, string ano)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            string s = "INSERT INTO " + appSettings.Ambiente + ".GTS_FCAST_PLAN (IDARTISTA, ANO, MES, VALOR_FCAST_RECEITA , VALOR_PLAN_RECEITA , VALOR_FCAST_MARGIN , VALOR_PLAN_MARGIN) VALUES ";
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 1 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 2 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 3 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 4 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 5 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 6 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 7 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 8 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 9 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 10 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 11 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 12 , 0 , 0 , 0 , 0) ");
            provider.ExecuteCommandSQL(s);
        }

        private void ExecuteGTSReceitaShows(string nomeartista, string ano)
        {
            PLProjetoProvider provider = new PLProjetoProvider();

            string s = "INSERT INTO  " + appSettings.Ambiente + ".GTS_RECEITA_SHOWS  (IDARTISTA, REGIAO, ANO, MES, VALOR, QUANTIDADE  ) VALUES ";
            s = string.Concat(s, "(", nomeartista, " , 'Sul', ", ano, " , 1 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Sudeste', ", ano, " , 1 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Norte', ", ano, " , 1 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Nordeste', ", ano, " , 1 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Centro Oeste', ", ano, " , 1 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Exterior', ", ano, " , 1 , 0 , 0 ), ");

            s = string.Concat(s, "(", nomeartista, " , 'Sul', ", ano, " , 2 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Sudeste', ", ano, " , 2 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Norte', ", ano, " , 2 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Nordeste', ", ano, " , 2 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Centro Oeste', ", ano, " , 2 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Exterior', ", ano, " , 2 , 0 , 0 ), ");

            s = string.Concat(s, "(", nomeartista, " , 'Sul', ", ano, " , 3 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Sudeste', ", ano, " , 3 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Norte', ", ano, " , 3 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Nordeste', ", ano, " , 3 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Centro Oeste', ", ano, " , 3 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Exterior', ", ano, " , 3 , 0 , 0 ), ");

            s = string.Concat(s, "(", nomeartista, " , 'Sul', ", ano, " , 4 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Sudeste', ", ano, " , 4 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Norte', ", ano, " , 4 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Nordeste', ", ano, " , 4 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Centro Oeste', ", ano, " , 4 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Exterior', ", ano, " , 4 , 0 , 0 ), ");

            s = string.Concat(s, "(", nomeartista, " , 'Sul', ", ano, " , 5 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Sudeste', ", ano, " , 5 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Norte', ", ano, " , 5 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Nordeste', ", ano, " , 5 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Centro Oeste', ", ano, " , 5 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Exterior', ", ano, " , 5 , 0 , 0 ), ");

            s = string.Concat(s, "(", nomeartista, " , 'Sul', ", ano, " , 6 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Sudeste', ", ano, " , 6 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Norte', ", ano, " , 6 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Nordeste', ", ano, " , 6 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Centro Oeste', ", ano, " , 6 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Exterior', ", ano, " , 6  , 0 , 0 ), ");

            s = string.Concat(s, "(", nomeartista, " , 'Sul', ", ano, " , 7 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Sudeste', ", ano, " , 7 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Norte', ", ano, " , 7 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Nordeste', ", ano, " , 7 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Centro Oeste', ", ano, " , 7 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Exterior', ", ano, " , 7 , 0 , 0 ), ");

            s = string.Concat(s, "(", nomeartista, " , 'Sul', ", ano, " , 8 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Sudeste', ", ano, " , 8 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Norte', ", ano, " , 8 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Nordeste', ", ano, " , 8 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Centro Oeste', ", ano, " , 8 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Exterior', ", ano, " , 8 , 0 , 0 ), ");

            s = string.Concat(s, "(", nomeartista, " , 'Sul', ", ano, " , 9 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Sudeste', ", ano, " , 9 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Norte', ", ano, " , 9 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Nordeste', ", ano, " , 9 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Centro Oeste', ", ano, " , 9 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Exterior', ", ano, " , 9 , 0 , 0 ), ");

            s = string.Concat(s, "(", nomeartista, " , 'Sul', ", ano, " , 10 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Sudeste', ", ano, " , 10, 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Norte', ", ano, " , 10 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Nordeste', ", ano, " , 10 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Centro Oeste', ", ano, " , 10 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Exterior', ", ano, " , 10 , 0 , 0 ), ");

            s = string.Concat(s, "(", nomeartista, " , 'Sul', ", ano, " , 11 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Sudeste', ", ano, " , 11 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Norte', ", ano, " , 11 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Nordeste', ", ano, " , 11 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Centro Oeste', ", ano, " , 11 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Exterior', ", ano, " , 11 , 0 , 0 ), ");

            s = string.Concat(s, "(", nomeartista, " , 'Sul', ", ano, " , 12 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Sudeste', ", ano, " , 12 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Norte', ", ano, " , 12 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Nordeste', ", ano, " , 12 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Centro Oeste', ", ano, " , 12 , 0 , 0 ), ");
            s = string.Concat(s, "(", nomeartista, " , 'Exterior', ", ano, " , 12 , 0 , 0 ) ");

            provider.ExecuteCommandSQL(s);
        }

        private void ExecuteGTSSaldos(string nomeartista)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            string s = string.Concat("INSERT INTO " + appSettings.Ambiente + ".GTS_SALDOS (IDARTISTA, VALOR) VALUES (", nomeartista, " , 0 )");
            provider.ExecuteCommandSQL(s);
        }

        private void ExecuteGTSMarginCachePartc(string nomeartista, string ano)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            string s = "INSERT INTO " + appSettings.Ambiente + ".GTS_MARGEN_CACHE_PARTC (IDARTISTA, ANO, MES, VALOR_ARTISTA, VALOR_LIVE, VALOR_GTS, VALOR_CACHE, VALOR_MARGEM) VALUES ";
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 1 , 0 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 2 , 0 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 3 , 0 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 4 , 0 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 5 , 0 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 6 , 0 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 7 , 0 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 8 , 0 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 9 , 0 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 10 , 0 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 11 , 0 , 0 , 0 , 0 , 0), ");
            s = string.Concat(s, "(", nomeartista, " , ", ano, " , 12 , 0 , 0 , 0 , 0 , 0) ");
            provider.ExecuteCommandSQL(s);
        }

    }
}