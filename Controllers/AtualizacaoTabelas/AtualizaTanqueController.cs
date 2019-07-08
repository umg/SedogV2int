using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Helpers;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.AtualizacaoTabelas
{
    public class AtualizaTanqueController : Controller
    {
        // GET: AtualizaTanque
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            List<CamposTanqueViewModel> model = new List<CamposTanqueViewModel>();
            ViewBag.ValoresTanque = new List<ValoresTanqueViewModel>();
            ViewBag.FORE = 0;
            ViewBag.REAL = 0;
            ViewBag.PLAN = 0;
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();

                model = provider.SEL_CAMPOS_TANQUE("");
                TempData["CAMPOS_TANQUE"] = model;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            List<CamposTanqueViewModel> model = new List<CamposTanqueViewModel>();
            long FORE = 0, PLAN = 0, REAL = 0;
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();

                model = provider.SEL_CAMPOS_TANQUE("");
                ViewBag.TipoTanque = collection["nome"];

                List<CamposTanqueViewModel> colunas = new List<CamposTanqueViewModel>();
                colunas = provider.SEL_CAMPOS_TANQUE(collection["nome"]);

                string sql = "SELECT " + colunas[0].REAL + " REAL , " + colunas[0].FORE + " FORE , " + colunas[0].PLAN + " PLAN , ANO , DESCMES , '" + collection["nome"] + "' TANQUE FROM " + provider.AddScheme("TANQUE") + " WHERE ANO = " + collection["ano"];
                List<ValoresTanqueViewModel> ret = provider.SEL_VALORES_TANQUE(sql);
                ViewBag.ValoresTanque = ret;

                foreach (ValoresTanqueViewModel valor in ret)
                {
                    PLAN += valor.PLAN;
                    REAL += valor.REAL;
                    FORE += valor.FORE;
                }

                ViewBag.FORE = FORE;
                ViewBag.REAL = REAL;
                ViewBag.PLAN = PLAN;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(model);
        }

        [ActionFilter_CheckLogin]
        public ActionResult UpdateTanque(ValoresTanqueViewModel[] valores)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                List<CamposTanqueViewModel> campos = provider.SEL_CAMPOS_TANQUE(HttpUtility.HtmlDecode(valores[0].TANQUE));
                string updateString = "UPDATE " + provider.AddScheme("TANQUE") + " SET (" + campos[0].FORE + " , " + campos[0].PLAN + " , " + campos[0].REAL + ") = ";

                foreach (ValoresTanqueViewModel val in valores)
                {
                    string s = updateString + "(" + val.FORE + " , " + val.PLAN + " , " + val.REAL + ") WHERE DESCMES = '" + val.DESCMES + "' AND ANO = " + val.ANO;
                    provider.ExecuteCommandSQL(s);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, responseText = "Atualizado!" }, JsonRequestBehavior.AllowGet);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult UpdateTanqueAll(FormCollection collection)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                provider.ATUALIZA_TANQUE_COMBUSTIVEL(int.Parse(collection["anoALL"]) - 2000, int.Parse(collection["mesALL"]));
            }
            catch { }
            return RedirectToAction("Index");
        }


        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult UpdateForcast(FormCollection collection)
        {
            #region oldversion  

            //try
            //{
            //    PLProjetoProvider provider = new PLProjetoProvider();


            //    string _sqlUpdate = string.Empty;
            //    string[] valueMesForcast = collection["mesForcast"].Split('#');
            //    int mesAtual = Convert.ToInt32(valueMesForcast[0]);
            //    int mesAnterior = Convert.ToInt32(valueMesForcast[1]);
            //    int anoAtual = DateTime.Now.Year;
            //    List<CamposTanqueViewModel> camposTanque = new List<CamposTanqueViewModel>();
            //    string err = string.Empty;
            //    string success = string.Empty;

            //    if (TempData["CAMPOS_TANQUE"] != null)
            //    {
            //        camposTanque = (string.IsNullOrEmpty(collection["nomeForecast"])) ? (List<CamposTanqueViewModel>)TempData["CAMPOS_TANQUE"] : ((List<CamposTanqueViewModel>)TempData["CAMPOS_TANQUE"]).Where(x => x.NOME == collection["nomeForecast"]).ToList();
            //    }
            //    else
            //    {
            //        camposTanque = provider.SEL_CAMPOS_TANQUE(collection["nomeForecast"]);
            //    }


            //    foreach (CamposTanqueViewModel campos in camposTanque)
            //    {
            //        for(int i = mesAnterior; i<mesAtual; i++)
            //        {
            //            try
            //            {
            //                string _sql = string.Concat("UPDATE BRSEDOG.TANQUE SET ", campos.FORE, " = ", campos.REAL, "WHERE ANO = ", anoAtual, " AND NUMMES = ", i.ToString(), " AND NOME = '", campos.NOME, "'");
            //                //executa; 
            //                success += string.Concat(campos.NOME, "(Mês , ", i.ToString(), " ): ", "Atualização realizada com sucesso \n!");
            //            }
            //            catch(Exception ex)
            //            {
            //                err += string.Concat(campos.NOME, "(Mês , ", i.ToString(), " ): ", ex.Message, "\n");
            //            }
            //        }
            //    }

            //    if(string.IsNullOrEmpty(err))
            //    {
            //        ViewBag.Error = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.SUCCESS, "Atualização efetuada com sucesso!");
            //    }
            //    else if(string.IsNullOrEmpty(success))
            //    {
            //        ViewBag.Error = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, "Atualização efetuada com sucesso!");
            //    }
            //    else
            //    {
            //        ViewBag.Error = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.NOTYPE, string.Concat(success, err));
            //    }


            //}
            //catch (Exception ex)
            //{
            //    ViewBag.Error = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, string.Concat("Ocorreu um erro na hora de processar a solicitação: ", ex.Message));
            //}
            #endregion

            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();


                string _sqlUpdate = string.Empty;
                string valueMesForcast = collection["mesForcast"];
                int anoAtual = DateTime.Now.Year;

                List<CamposTanqueViewModel> camposTanque = new List<CamposTanqueViewModel>();
                
                if (TempData["CAMPOS_TANQUE"] != null)
                {
                    camposTanque = (string.IsNullOrEmpty(collection["nomeForcast"])) ? (List<CamposTanqueViewModel>)TempData["CAMPOS_TANQUE"] : ((List<CamposTanqueViewModel>)TempData["CAMPOS_TANQUE"]).Where(x => x.NOME == collection["nomeForcast"]).ToList();
                }
                else
                {
                    camposTanque = provider.SEL_CAMPOS_TANQUE(collection["nomeForcast"]);
                }

                if (camposTanque.Count > 0)
                {

                    string _sql = "UPDATE " + appSettings.Ambiente + ".TANQUE ";
                    string _set = "SET ";

                    foreach (CamposTanqueViewModel campos in camposTanque)
                    {
                        _set += string.Concat(campos.FORE, " = ", campos.REAL, ", ");
                    }

                    //if (!string.IsNullOrEmpty(_set))
                    _set = _set.Remove(_set.Length - 2, 2);
                    
                    _sql += string.Concat(_set, " WHERE ANO = ", anoAtual, " AND NUMMES < ", valueMesForcast);
                    provider.ExecuteCommandSQL(_sql);

                }

                ViewBag.Error = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.SUCCESS, "Atualização realizada com sucesso!");

            }
            catch (Exception ex)
            {
                ViewBag.Error = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, string.Concat("Ocorreu um erro na hora de processar a solicitação: ", ex.Message));
            }


            return RedirectToAction("Index");
        }
    }
}