using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;

namespace SEDOGv2.Controllers.MarketingTools
{
    public class DespesaDeMarketingPorArtistaProjetoController : Controller
    {
        // GET: DespesaDeMarketingPorArtistaProjeto
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            DespesasDeMarketingTotalPorArtistaViewModel model = new DespesasDeMarketingTotalPorArtistaViewModel();
            model.PLProjetos = new List<PLProjeto>();
            model.DespesasDeMarketingTotalPorArtistaReport = new List<DespesasDeMarketingTotalPorArtista>();

            return View(model);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            DespesasDeMarketingTotalPorArtistaViewModel model = new DespesasDeMarketingTotalPorArtistaViewModel();
            model.PLProjetos = new List<PLProjeto>();
            model.DespesasDeMarketingTotalPorArtistaReport = new List<DespesasDeMarketingTotalPorArtista>();
            PLProjetoProvider provider = new PLProjetoProvider();
            decimal valor=0;
            long qtd=0;
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
            else if (Request.Form["selTipo"] != null)
            {
                ViewBag.Ano = collection["selTipo"];
                try
                {
                    long P_IDPROJ_SEDOG = long.Parse(collection["selProduto"].Split('|')[0]);

                    ViewBag.IDProjetoSedog = collection["selProduto"];
                    ViewBag.Artista = collection["selProduto"].Split('|')[1];
                    ViewBag.Projeto = collection["selProduto"].Split('|')[2];

                    model.PLProjetos = new List<PLProjeto>();
                    ViewBag.Hide = true;
                    ViewBag.Columns = 7;
                    if (collection["selTipo"] == "MENSAL")
                    {
                        ViewBag.Hide = false;
                        ViewBag.Columns = 9;
                        model.DespesasDeMarketingTotalPorArtistaReport = provider.SLT_DESPESAS_MARKETING_TOTAL_POR_ARTISTA_MES_ANO(P_IDPROJ_SEDOG);
                    }
                    else
                    {
                        model.DespesasDeMarketingTotalPorArtistaReport = provider.SLT_DESPESAS_MARKETING_TOTAL_POR_ARTISTA(P_IDPROJ_SEDOG);
                    }
                    foreach(var r in model.DespesasDeMarketingTotalPorArtistaReport)
                    {
                        valor += r.VALOR;
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