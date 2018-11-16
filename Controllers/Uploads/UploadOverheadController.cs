using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Helpers;

namespace SEDOGv2.Controllers.Uploads
{
    public class UploadOverheadController : Controller
    {
        // GET: UploadOverhead
        public ActionResult Index()
        {
            if(TempData["ForcastMessage"] != null)
                ViewBag.ForcastMessage = TempData["ForcastMessage"].ToString();

            return View();
        }

        [HttpPost]
        public ActionResult UploadForcast(FormCollection collection, HttpPostedFileBase file)
        {
            try
            {


                if (file.ContentLength > 0)
                {
                    int _mes = Convert.ToInt32(collection["mes"]);
                    if (_mes > 0)
                    {
                        Models.Context.PLProjetoProvider_ext conn = new Models.Context.PLProjetoProvider_ext();

                        string path = Server.MapPath("~/temp/forcast.xls");
                        file.SaveAs(path);

                        DataSet ds = conn.ReadExcelFile(path);

                        int _contador = 0;


                        string _INTO = "INSERT INTO " + appSettings.Ambiente + ".FORCAST (ORMCU, OROBJ, ORSUB, FC_MES1, FC_MES2, FC_MES3, FC_MES4, FC_MES5, FC_MES6, FC_MES7, FC_MES8, FC_MES9, FC_MES10, FC_MES11, FC_MES12, MES, ANO) VALUES ";
                        string _insert = string.Empty;
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            _contador++;
                            _insert = string.Concat(_insert, " ( ");
                            _insert = string.Concat(_insert, "'", row[0].ToString(), "' , "); //ORMCU 
                            _insert = string.Concat(_insert, "'", row[1].ToString(), "' , "); //OROBJ
                            _insert = string.Concat(_insert, "'", row[2].ToString().PadLeft(2, '0'), "' , "); //ORSUB
                            _insert = string.Concat(_insert, validaDecimal(row[3].ToString()), " , "); //FC_MES1 
                            _insert = string.Concat(_insert, validaDecimal(row[4].ToString()), " , "); //FC_MES2 
                            _insert = string.Concat(_insert, validaDecimal(row[5].ToString()), " , "); //FC_MES3 
                            _insert = string.Concat(_insert, validaDecimal(row[6].ToString()), " , "); //FC_MES4 
                            _insert = string.Concat(_insert, validaDecimal(row[7].ToString()), " , "); //FC_MES5 
                            _insert = string.Concat(_insert, validaDecimal(row[8].ToString()), " , "); //FC_MES6 
                            _insert = string.Concat(_insert, validaDecimal(row[9].ToString()), " , "); //FC_MES7 
                            _insert = string.Concat(_insert, validaDecimal(row[10].ToString()), " , "); //FC_MES8 
                            _insert = string.Concat(_insert, validaDecimal(row[11].ToString()), " , "); //FC_MES9 
                            _insert = string.Concat(_insert, validaDecimal(row[12].ToString()), " , "); //FC_MES10 
                            _insert = string.Concat(_insert, validaDecimal(row[13].ToString()), " , "); //FC_MES11 
                            _insert = string.Concat(_insert, validaDecimal(row[14].ToString()), " , "); //FC_MES12 
                            _insert = string.Concat(_insert, _mes, " , "); //FC_MES1 
                            _insert = string.Concat(_insert, DateTime.Now.ToString("yy")); //FC_MES1 
                            _insert = string.Concat(_insert, ") , ");

                            if (_contador == 900)
                            {
                                _insert = _insert.Remove(_insert.Length - 2, 1);
                                _insert = string.Concat(_INTO, _insert);
                                conn.ExecuteSQL(_insert);
                                _insert = string.Empty;
                                _contador = 0;
                            }

                        }

                        /*RESTO DO FOREACH NAO INSERIDO*/
                        if (_insert.Length > 0)
                        {
                            _insert = _insert.Remove(_insert.Length - 2, 1);
                            _insert = string.Concat(_INTO, _insert);
                            conn.ExecuteSQL(_insert);
                            _insert = string.Empty;
                            _contador = 0;
                        }

                        TempData["ForcastMessage"] = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.SUCCESS, "File successfully processed!");

                    }
                    else
                    {
                        TempData["ForcastMessage"] = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, "You need to choose the forecast month");
                    }
                }
                else
                {
                    TempData["ForcastMessage"] = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, "No files found for upload");
                }
            }
            catch(Exception ex)
            {
                TempData["ForcastMessage"] = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, string.Concat("There was an error processing the request: ", ex.Message));
            }
            return RedirectToAction("Index");
        }

        public string validaDecimal(string _value)
        {
            decimal decValido = 0;
            bool validada = decimal.TryParse(_value, out decValido);
            return decValido.ToString();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
           
            if (file.ContentLength > 0)
            {

                Models.Context.PLProjetoProvider_ext conn = new Models.Context.PLProjetoProvider_ext();

                string path = Server.MapPath("~/temp/generico.xls");
                file.SaveAs(path);

                DataSet ds = conn.ReadExcelFile(path);

                try
                {
                    int ordem = 0;
                    int _contador = 1;
                    string _resposta = "";
                    string _head = "INSERT INTO " + appSettings.Ambiente + ".ORCAMENTO_OVERHEAD(ORMCU, OROBJ, ORSUB, OR_MES1, OR_MES2, OR_MES3, OR_MES4, OR_MES5, OR_MES6, OR_MES7, OR_MES8, OR_MES9, OR_MES10, OR_MES11, OR_MES12, ANO) VALUES";
                    string _insert = "";
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        _insert = string.Concat(_insert, " ( ");
                        _insert = string.Concat(_insert, "'", row[0].ToString(), "', ");//ORMCU
                        _insert = string.Concat(_insert, "'", row[1].ToString(), "', ");//OROBJ
                        _insert = string.Concat(_insert, "'", row[2].ToString().PadLeft(2, '0'), "', ");//ORSUB

                        _insert = string.Concat(_insert, (row[3].ToString() == "") ? "0" : row[3].ToString(), " , ");//OR_MES1
                        _insert = string.Concat(_insert, (row[4].ToString() == "") ? "0" : row[4].ToString(), " , ");//OR_MES2
                        _insert = string.Concat(_insert, (row[5].ToString() == "") ? "0" : row[5].ToString(), " , ");//OR_MES3
                        _insert = string.Concat(_insert, (row[6].ToString() == "") ? "0" : row[6].ToString(), " , ");//OR_MES4
                        _insert = string.Concat(_insert, (row[7].ToString() == "") ? "0" : row[7].ToString(), " , ");//OR_MES5
                        _insert = string.Concat(_insert, (row[8].ToString() == "") ? "0" : row[8].ToString(), " , ");//OR_MES6
                        _insert = string.Concat(_insert, (row[9].ToString() == "") ? "0" : row[9].ToString(), " , ");//OR_MES7
                        _insert = string.Concat(_insert, (row[10].ToString() == "") ? "0" : row[10].ToString(), " , ");//OR_MES8
                        _insert = string.Concat(_insert, (row[11].ToString() == "") ? "0" : row[11].ToString(), " , ");//OR_MES9
                        _insert = string.Concat(_insert, (row[12].ToString() == "") ? "0" : row[12].ToString(), " , ");//OR_MES10
                        _insert = string.Concat(_insert, (row[13].ToString() == "") ? "0" : row[13].ToString(), " , ");//OR_MES11
                        _insert = string.Concat(_insert, (row[14].ToString() == "") ? "0" : row[14].ToString(), " , ");//OR_MES12

                        _insert = string.Concat(_insert, 18, " ");//ANO

                        _insert = string.Concat(_insert, ") , ");
                        ordem++;

                        if(ordem == 100)
                        {
                            if (_insert.Length > 0)
                                _insert = _insert.Remove(_insert.Length - 2, 1);

                            _insert = string.Concat(_head, _insert);
                            conn.ExecuteCommandSQL(_insert);

                            _resposta = string.Concat(_contador.ToString(), " - ", ordem.ToString(), "/n");
                            _contador++;
                            
                            _insert = "";
                            ordem = 0;
                        }

                    }

                    if (ordem > 0)
                    {
                        if (_insert.Length > 0)
                            _insert = _insert.Remove(_insert.Length - 2, 1);

                        _insert = string.Concat(_head, _insert);
                        conn.ExecuteCommandSQL(_insert);

                        _resposta = string.Concat(_contador.ToString(), " - ", ordem.ToString(), "/n");
                        _contador++;

                        _insert = "";
                        ordem = 0;
                    }
                   
                }
                catch (Exception ex)
                {
                }

                /*Removendo arquivo*/
                /*if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);*/

            }

            return View();
        }
    }
}