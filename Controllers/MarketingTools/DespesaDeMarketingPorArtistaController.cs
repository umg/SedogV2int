using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using SEDOGv2.Helpers;

namespace SEDOGv2.Controllers.MarketingTools
{
    public class DespesaDeMarketingPorArtistaController : Controller
    {
        // GET: DespesaDeMarketingPorArtista
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            DespesasDeMarketingPorArtistaViewModel model = new DespesasDeMarketingPorArtistaViewModel();
            model.PLProjetos = new List<PLProjeto>();
            model.DespesaDeMarketingPorArtistaReport = new List<DespesasDeMarketingPorArtista>();

            return View(model);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            DespesasDeMarketingPorArtistaViewModel model = new DespesasDeMarketingPorArtistaViewModel();
            model.PLProjetos = new List<PLProjeto>();
            model.DespesaDeMarketingPorArtistaReport = new List<DespesasDeMarketingPorArtista>();
            PLProjetoProvider provider = new PLProjetoProvider();
            try
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(appSettings.Culture);

                string artista = collection["txtArtistaProduto"];
                int mes = int.Parse(collection["selMes"]);
                string tipo = collection["selTipo"];
                model.PLProjetos = new List<PLProjeto>();
                model.DespesaDeMarketingPorArtistaReport = provider.SLT_DESPPESAS_MARKETING_POR_ARTISTA(tipo, mes, artista);

                ViewBag.Artista = "Artista: " + artista.Trim() + " - Período: " + (tipo == "ANO" ? "Ano Corrente" : "Desde o Lançamento") + " até " + culture.DateTimeFormat.GetMonthName(mes);

                decimal TotCUSTO_INICIAL = 0;
                decimal TotTV = 0;
                decimal TotRADIO = 0;
                decimal TotMIDIA_ON_LINE = 0;
                decimal TotIMPRENSA = 0;
                decimal TotMERCHANDISING = 0;
                decimal TotOUTROS = 0;
                decimal TotAMOSTRAS = 0;
                decimal TotACAO_PROMO_OUTROS = 0;
                decimal TotVIDEOCLIPS = 0;
                decimal TotFREELANCERS = 0;
                decimal TotCLIPPING = 0;
                decimal TotNBD = 0;
                decimal TotDIGITAL = 0;
                decimal TotPUBLICIDADE_ON_LINE = 0;

                foreach (var despesas in model.DespesaDeMarketingPorArtistaReport)
                {
                    TotCUSTO_INICIAL += despesas.CUSTO_INICIAL;
                    TotTV += despesas.TV;
                    TotRADIO += despesas.RADIO;
                    TotMIDIA_ON_LINE += despesas.MIDIA_ON_LINE;
                    TotIMPRENSA += despesas.IMPRENSA;
                    TotMERCHANDISING += despesas.MERCHANDISING;
                    TotOUTROS += despesas.OUTROS;
                    TotAMOSTRAS += despesas.AMOSTRAS;
                    TotACAO_PROMO_OUTROS += despesas.ACAO_PROMO_OUTROS;
                    TotVIDEOCLIPS += despesas.VIDEOCLIPS;
                    TotFREELANCERS += despesas.FREELANCERS;
                    TotCLIPPING += despesas.CLIPPING;
                    TotNBD += despesas.NBD;
                    TotDIGITAL += despesas.DIGITAL;
                    TotPUBLICIDADE_ON_LINE += despesas.PUBLICIDADE_ON_LINE;
                }

                ViewBag.TotCUSTO_INICIAL = TotCUSTO_INICIAL;
                ViewBag.TotTV = TotTV;
                ViewBag.TotRADIO = TotRADIO;
                ViewBag.TotMIDIA_ON_LINE = TotMIDIA_ON_LINE;
                ViewBag.TotIMPRENSA = TotIMPRENSA;
                ViewBag.TotMERCHANDISING = TotMERCHANDISING;
                ViewBag.TotOUTROS = TotOUTROS;
                ViewBag.TotAMOSTRAS = TotAMOSTRAS;
                ViewBag.TotACAO_PROMO_OUTROS = TotACAO_PROMO_OUTROS;
                ViewBag.TotVIDEOCLIPS = TotVIDEOCLIPS;
                ViewBag.TotFREELANCERS = TotFREELANCERS;
                ViewBag.TotCLIPPING = TotCLIPPING;
                ViewBag.TotNBD = TotNBD;
                ViewBag.TotDIGITAL = TotDIGITAL;
                ViewBag.TotPUBLICIDADE_ON_LINE = TotPUBLICIDADE_ON_LINE;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }
    }
}