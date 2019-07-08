using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using SEDOGv2.Helpers;

namespace SEDOGv2.Controllers.GTS
{
    public class PerformanceController : Controller
    {
        // GET: Performance
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
                TempData.Remove("Message");
            }

            PLProjetoProvider provider = new PLProjetoProvider();
            PerformanceGTSViewModel _model = new Models.PerformanceGTSViewModel();
            _model.Artista = new List<Models.ArtistaProjeto>();
            _model.Artista = provider.SLT_ARTISTAS_GTS("");

            return View(_model);
        }

        [ActionFilter_CheckLogin]
        public ActionResult Display(FormCollection form)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            PerformanceGTSViewModel _model = new Models.PerformanceGTSViewModel();
            try
            {
                _model.Infos = new InfosPerformanceGTS();
                _model.Infos.Ano = Convert.ToInt32( (!string.IsNullOrEmpty(form["anoALL"]) ? form["anoALL"] : "0") );
                _model.Infos.Mes = Convert.ToInt32((!string.IsNullOrEmpty(form["mesALL"]) ? form["mesALL"] : "0")); 

                if (form["chbunico[]"] != null)
                {
                    string[] dados = form["chbunico[]"].ToString().Split(',');
                    string auxDuplicidade = "";
                    foreach (string dado in dados)
                    {
                        if (!auxDuplicidade.Contains(dado))
                        {
                            auxDuplicidade = string.Concat(auxDuplicidade, dado, "#");
                            string[] artista = dado.Split('|');
                            _model.Infos.IDArtista = string.Concat(_model.Infos.IDArtista, artista[0], " | ");
                            _model.Infos.Artista = string.Concat(_model.Infos.Artista, artista[1], " | ");
                        }
                    }
                }

                if (!string.IsNullOrEmpty(_model.Infos.IDArtista))
                    _model.Infos.IDArtista = _model.Infos.IDArtista.Remove(_model.Infos.IDArtista.Length - 2, 2);

                if (!string.IsNullOrEmpty(_model.Infos.Artista))
                    _model.Infos.Artista = _model.Infos.Artista.Remove(_model.Infos.Artista.Length - 2, 2);

                // 1. Últimos 12 meses
                // BRSEDOG.SLT_GTS_RECEITA_ULT12MESES_PAINEL
                List<System.Data.OleDb.OleDbParameter> lsOleDbParameter = new List<System.Data.OleDb.OleDbParameter>();
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = (!string.IsNullOrEmpty(_model.Infos.IDArtista) ? _model.Infos.IDArtista : null), ParameterName = "P_IDARTISTA" });
                System.Data.DataTable dtReceitaUltimos12Meses = provider.GetTable(provider.AddScheme("SLT_GTS_RECEITA_ULT12MESES_PAINEL"), lsOleDbParameter.ToArray());

                /*if (!string.IsNullOrEmpty(_model.Infos.IDArtista))
                {
                    lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = (!string.IsNullOrEmpty(_model.Infos.IDArtista) ? _model.Infos.IDArtista : null), ParameterName = "P_IDARTISTA" });
                     System.Data.DataTable dtReceitaUltimos12Meses = provider.GetTable(provider.AddScheme("SLT_GTS_RECEITA_ULT12MESES_PAINEL"), lsOleDbParameter.ToArray());
                }
                else
                {
                    dtReceitaUltimos12Meses = provider.GetTable(provider.AddScheme("SLT_GTS_RECEITA_ULT12MESES_PAINEL"));
                }*/


                foreach (System.Data.DataRow row in dtReceitaUltimos12Meses.Rows)
                {
                    _model.dataGTSReceita12Meses = string.Concat(_model.dataGTSReceita12Meses, Convert.ToInt32(row["DATA"]).ToString(), ", ");
                    _model.labelGTSReceita12Meses = string.Concat(_model.labelGTSReceita12Meses, "'", row["ABREV"].ToString(), "', ");
                }

                if (!string.IsNullOrEmpty(_model.dataGTSReceita12Meses))
                    _model.dataGTSReceita12Meses = _model.dataGTSReceita12Meses.Remove(_model.dataGTSReceita12Meses.Length - 2, 2);

                if (!string.IsNullOrEmpty(_model.labelGTSReceita12Meses))
                    _model.labelGTSReceita12Meses = _model.labelGTSReceita12Meses.Remove(_model.labelGTSReceita12Meses.Length - 2, 2);

                // 2. Mapa da região 
                // BRSEDOG.SLT_GTS_SHOWS_REGIAO_PAINEL
                lsOleDbParameter = new List<System.Data.OleDb.OleDbParameter>();
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Ano, ParameterName = "P_ANO" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Mes, ParameterName = "P_MES" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = (!string.IsNullOrEmpty(_model.Infos.IDArtista) ? _model.Infos.IDArtista : null), ParameterName = "P_IDARTISTA" });
                System.Data.DataTable dtShowsRegiao = provider.GetTable(provider.AddScheme("SLT_GTS_SHOWS_REGIAO_PAINEL"), lsOleDbParameter.ToArray());
                //System.Data.DataTable dtShowsRegiao = provider.GetTable(provider.AddScheme("SLT_GTS_SHOWS_REGIAO_PAINEL"));

                foreach (System.Data.DataRow row in dtShowsRegiao.Rows)
                {
                    switch (row["REGIAO"].ToString())
                    {
                        case "Sudeste":
                            _model.regiaoSudesteQTDShow = row["QTD_SHOWS"].ToString();
                            break;
                        case "Sul":
                            _model.regiaoSulQTDShow = row["QTD_SHOWS"].ToString();
                            break;
                        case "Centro Oeste":
                            _model.regiaoCentroOesteQTDShow = row["QTD_SHOWS"].ToString();
                            break;
                        case "Exterior":
                            _model.regiaoExteriorQTDShow = row["QTD_SHOWS"].ToString();
                            break;
                        case "Norte":
                            _model.regiaoNorteQTDShow = row["QTD_SHOWS"].ToString();
                            break;
                        case "Nordeste":
                            _model.regiaoNordesteQTDShow = row["QTD_SHOWS"].ToString();
                            break;
                        default:
                            break;
                    }
                }

                // 3. Tanque
                // SLT_GTS_TANQUE_RECEITA_PAINEL
                //  SLT_GTS_TANQUE_MARGEN_PAINEL

                lsOleDbParameter = new List<System.Data.OleDb.OleDbParameter>();
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Mes, ParameterName = "P_MES" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Ano, ParameterName = "P_ANO" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = (!string.IsNullOrEmpty(_model.Infos.IDArtista) ? _model.Infos.IDArtista : null), ParameterName = "P_IDARTISTA" });
                System.Data.DataTable dtTanqueReceita = provider.GetTable(provider.AddScheme("SLT_GTS_TANQUE_RECEITA_PAINEL"), lsOleDbParameter.ToArray());

                if (dtTanqueReceita != null)
                {
                    //YTD_REAL, YTD_PLAN, TOTAL_ANO_PLAN, YTD, TOTAL_ANO
                    _model.tanqueReceitadataTotal = Convert.ToInt32( (dtTanqueReceita.Rows[0]["TOTAL_ANO"] is DBNull) ? "0" : dtTanqueReceita.Rows[0]["TOTAL_ANO"] ).ToString();
                    _model.tanqueReceitadataAtual = Convert.ToInt32((dtTanqueReceita.Rows[0]["YTD_REAL"] is DBNull) ? "0" : dtTanqueReceita.Rows[0]["YTD_REAL"]).ToString();
                    _model.tanqueReceitadataProvisao = Convert.ToInt32((dtTanqueReceita.Rows[0]["YTD"] is DBNull) ? "0" : dtTanqueReceita.Rows[0]["YTD"]).ToString();
                    _model.tanqueReceitadataTotalPlan = Convert.ToInt32((dtTanqueReceita.Rows[0]["TOTAL_ANO_PLAN"] is DBNull) ? "0" : dtTanqueReceita.Rows[0]["TOTAL_ANO_PLAN"]).ToString();
                    _model.tanqueReceitadataProvisaoPlan = Convert.ToInt32((dtTanqueReceita.Rows[0]["YTD_PLAN"] is DBNull) ? "0" : dtTanqueReceita.Rows[0]["YTD_PLAN"]).ToString();
                }

                lsOleDbParameter = new List<System.Data.OleDb.OleDbParameter>();
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Mes, ParameterName = "P_MES" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Ano, ParameterName = "P_ANO" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = (!string.IsNullOrEmpty(_model.Infos.IDArtista) ? _model.Infos.IDArtista : null), ParameterName = "P_IDARTISTA" });
                System.Data.DataTable dtTanqueMargen = provider.GetTable(provider.AddScheme("SLT_GTS_TANQUE_MARGEN_PAINEL"), lsOleDbParameter.ToArray());
                if (dtTanqueMargen != null)
                {
                    //YTD_REAL, YTD_PLAN, TOTAL_ANO_PLAN, YTD, TOTAL_ANO
                    _model.tanqueMargendataTotal = Convert.ToInt32((dtTanqueMargen.Rows[0]["TOTAL_ANO"] is DBNull) ? "0" : dtTanqueMargen.Rows[0]["TOTAL_ANO"]).ToString();
                    _model.tanqueMargendataAtual = Convert.ToInt32((dtTanqueMargen.Rows[0]["YTD_REAL"] is DBNull) ? "0" : dtTanqueMargen.Rows[0]["YTD_REAL"]).ToString();
                    _model.tanqueMargendataProvisao = Convert.ToInt32((dtTanqueMargen.Rows[0]["YTD"] is DBNull) ? "0" : dtTanqueMargen.Rows[0]["YTD"]).ToString();
                    _model.tanqueMargendataTotalPlan = Convert.ToInt32((dtTanqueMargen.Rows[0]["TOTAL_ANO_PLAN"] is DBNull) ? "0" : dtTanqueMargen.Rows[0]["TOTAL_ANO_PLAN"]).ToString();
                    _model.tanqueMargendataProvisaoPlan = Convert.ToInt32((dtTanqueMargen.Rows[0]["YTD_PLAN"] is DBNull) ? "0" : dtTanqueMargen.Rows[0]["YTD_PLAN"]).ToString();
                }


                // 4. Tanque
                //SLT_GTS_CACHE_PAINEL
                //SLT_GTS_SHOWS_PAINEL
                //SLT_GTS_RECEITA_PAINEL
                //SLT_GTS_MARGIN_PAINEL
                //SLT_GTS_PARTC_PAINEL


                lsOleDbParameter = new List<System.Data.OleDb.OleDbParameter>();
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Ano, ParameterName = "P_ANO" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Mes, ParameterName = "P_MES" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = (!string.IsNullOrEmpty(_model.Infos.IDArtista) ? _model.Infos.IDArtista : null), ParameterName = "P_IDARTISTA" });
                System.Data.DataTable dtGTSCache = provider.GetTable(provider.AddScheme("SLT_GTS_CACHE_PAINEL"), lsOleDbParameter.ToArray());
                if (dtGTSCache != null)
                    _model.detalhesCacheMedioMes = Convert.ToDecimal(dtGTSCache.Rows[0][1].ToString());


                lsOleDbParameter = new List<System.Data.OleDb.OleDbParameter>();
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Ano, ParameterName = "P_ANO" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Mes, ParameterName = "P_MES" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = (!string.IsNullOrEmpty(_model.Infos.IDArtista) ? _model.Infos.IDArtista : null), ParameterName = "P_IDARTISTA" });
                System.Data.DataTable dtGTSShows = provider.GetTable(provider.AddScheme("SLT_GTS_SHOWS_PAINEL"), lsOleDbParameter.ToArray());
                if (dtGTSShows != null)
                    _model.detalhesNumeroShowsMes = Convert.ToDecimal(dtGTSShows.Rows[0]["VALOR"].ToString());


                lsOleDbParameter = new List<System.Data.OleDb.OleDbParameter>();
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Ano, ParameterName = "P_ANO" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Mes, ParameterName = "P_MES" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = (!string.IsNullOrEmpty(_model.Infos.IDArtista) ? _model.Infos.IDArtista : null), ParameterName = "P_IDARTISTA" });
                System.Data.DataTable dtGTSReceita = provider.GetTable(provider.AddScheme("SLT_GTS_RECEITA_PAINEL"), lsOleDbParameter.ToArray());
                if (dtGTSReceita != null)
                    _model.detalhesTotalReceitaMes = Convert.ToDecimal(dtGTSReceita.Rows[0]["VALOR"].ToString());


                lsOleDbParameter = new List<System.Data.OleDb.OleDbParameter>();
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Ano, ParameterName = "P_ANO" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Mes, ParameterName = "P_MES" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = (!string.IsNullOrEmpty(_model.Infos.IDArtista) ? _model.Infos.IDArtista : null), ParameterName = "P_IDARTISTA" });
                System.Data.DataTable dtGTSMargin = provider.GetTable(provider.AddScheme("SLT_GTS_MARGIN_PAINEL"), lsOleDbParameter.ToArray());
                if (dtGTSMargin != null)
                    _model.detalhesMarginMes = Convert.ToDecimal(dtGTSMargin.Rows[0]["VALOR"].ToString());


                lsOleDbParameter = new List<System.Data.OleDb.OleDbParameter>();
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Ano, ParameterName = "P_ANO" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Mes, ParameterName = "P_MES" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = (!string.IsNullOrEmpty(_model.Infos.IDArtista) ? _model.Infos.IDArtista : null), ParameterName = "P_IDARTISTA" });
                System.Data.DataTable dtGTSPartc = provider.GetTable(provider.AddScheme("SLT_GTS_PARTC_PAINEL"), lsOleDbParameter.ToArray());

                foreach (System.Data.DataRow row in dtGTSPartc.Rows)
                {
                    switch (row["TIPO"].ToString())
                    {
                        case "LIVE":
                            _model.detalhesParticipacaoLIVE = Convert.ToDecimal(row["VALOR"].ToString());
                            break;
                        case "ARTISTA":
                            _model.detalhesParticipacaoArtista = Convert.ToDecimal(row["VALOR"].ToString());
                            break;
                        case "GTS":
                            _model.detalhesParticipacaoGTS = Convert.ToDecimal(row["VALOR"].ToString());
                            break;
                        default:
                            break;
                    }
                }

                lsOleDbParameter = new List<System.Data.OleDb.OleDbParameter>();
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Ano, ParameterName = "P_ANO" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = _model.Infos.Mes, ParameterName = "P_MES" });
                lsOleDbParameter.Add(new System.Data.OleDb.OleDbParameter { Value = (!string.IsNullOrEmpty(_model.Infos.IDArtista) ? _model.Infos.IDArtista : null), ParameterName = "P_IDARTISTA" });
                System.Data.DataTable dtGTSSaldo = provider.GetTable(provider.AddScheme("SLT_GTS_SALDO_PAINEL"), lsOleDbParameter.ToArray());
                if (dtGTSMargin != null)
                    _model.detalhesSaldoAberto = Convert.ToDecimal(dtGTSSaldo.Rows[0]["VALOR"].ToString());


                /*}
                else
                {
                    TempData["Message"] = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, "Você não selecionou nenhum artista");
                    return RedirectToAction("Index"); ;

                }*/
            }
            catch (Exception ex)
            {
                TempData["Message"] = Helpers.Erros.ShowMessage(Helpers.Erros.MessageType.ERROR, string.Concat("Ocorreu um erro na hora de processar a solicitação: ", ex.Message));
                return RedirectToAction("Index");
            }



            return View(_model);
        }
    }
}