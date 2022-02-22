using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using SEDOGv2.Helpers;


namespace SEDOGv2.Controllers.Cadastros
{
    public class ParametrosVendaFisicaController : Controller
    {
        // GET: Usuario
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            List<ParametrosVendaFisicaViewModel> model = new List<ParametrosVendaFisicaViewModel>();
            try
            {
                    
                
                PLProjetoProvider provider = new PLProjetoProvider();

                model = provider.SLT_PARAMETROS_VENDAS_FISICAS(0);
                
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
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                if (
                    collection["MIDIA"] != "" &&
                    collection["AVERAGE_MANUFACTURING_PRICE"] != "" &&
                    collection["OBSOLENCENCE_PROVISION_PERCENT"] != "" &&
                    collection["RETURNS_PROVISION_PERCENT"] != "" &&
                    collection["BAD_DEBTS_PROVISION_PERCENT"] != "" &&
                    collection["COPYRIGHT_PERCENT"] != "" &&
                    collection["ARTIST_RIGHT_PERCENT"] != "" &&
                    collection["OTHER_ROYALTY_PERCENT"] != "" &&
                    collection["PRODUCER_ROYALTY_PERCENT"] != "" &&
                    collection["DISTRIBUITION_COST_PERCENT"] != "" &&
                    collection["MANUFACTURING_COST_PERCENT"] != "" &&
                    collection["SALES_COMISSION_PERCENT"] != "" &&
                    collection["TAX_PERCENT"] != "")

                    provider.INS_PARAMETROS_VENDAS_FISICAS(
                        collection["MIDIA"],
                        Convert.ToDecimal(collection["AVERAGE_MANUFACTURING_PRICE"]),
                        Convert.ToDecimal(collection["OBSOLENCENCE_PROVISION_PERCENT"]),
                        Convert.ToDecimal(collection["RETURNS_PROVISION_PERCENT"]),
                        Convert.ToDecimal(collection["BAD_DEBTS_PROVISION_PERCENT"]),
                        Convert.ToDecimal(collection["COPYRIGHT_PERCENT"]),
                        Convert.ToDecimal(collection["ARTIST_RIGHT_PERCENT"]),
                        Convert.ToDecimal(collection["OTHER_ROYALTY_PERCENT"]),
                        Convert.ToDecimal(collection["PRODUCER_ROYALTY_PERCENT"]),
                        Convert.ToDecimal(collection["DISTRIBUITION_COST_PERCENT"]),
                        Convert.ToDecimal(collection["MANUFACTURING_COST_PERCENT"]),
                        Convert.ToDecimal(collection["SALES_COMISSION_PERCENT"]),
                        Convert.ToDecimal(collection["TAX_PERCENT"])
                        );
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [ActionFilter_CheckLogin]
        public ActionResult Edit(int id)
        {
            ParametrosVendaFisicaViewModel model = new ParametrosVendaFisicaViewModel();
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();

                model = provider.SLT_PARAMETROS_VENDAS_FISICAS(id).First();
                

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                if (collection["ID"] != "" && 
                    collection["MIDIA"] != "" &&
                    collection["AVERAGE_MANUFACTURING_PRICE"] != "" &&
                    collection["OBSOLENCENCE_PROVISION_PERCENT"] != "" &&
                    collection["RETURNS_PROVISION_PERCENT"] != "" &&
                    collection["BAD_DEBTS_PROVISION_PERCENT"] != "" &&
                    collection["COPYRIGHT_PERCENT"] != "" &&
                    collection["ARTIST_RIGHT_PERCENT"] != "" &&
                    collection["OTHER_ROYALTY_PERCENT"] != "" &&
                    collection["PRODUCER_ROYALTY_PERCENT"] != "" &&
                    collection["DISTRIBUITION_COST_PERCENT"] != "" &&
                    collection["MANUFACTURING_COST_PERCENT"] != "" &&
                    collection["SALES_COMISSION_PERCENT"] != "" &&
                    collection["TAX_PERCENT"] != "")
                {
                    provider.UPD_PARAMETROS_VENDA_FISICA(Convert.ToInt32(collection["ID"]),collection["MIDIA"],
                        Convert.ToDecimal(collection["AVERAGE_MANUFACTURING_PRICE"]),
                        Convert.ToDecimal(collection["OBSOLENCENCE_PROVISION_PERCENT"]),
                        Convert.ToDecimal(collection["RETURNS_PROVISION_PERCENT"]),
                        Convert.ToDecimal(collection["BAD_DEBTS_PROVISION_PERCENT"]),
                        Convert.ToDecimal(collection["COPYRIGHT_PERCENT"]),
                        Convert.ToDecimal(collection["ARTIST_RIGHT_PERCENT"]),
                        Convert.ToDecimal(collection["OTHER_ROYALTY_PERCENT"]),
                        Convert.ToDecimal(collection["PRODUCER_ROYALTY_PERCENT"]),
                        Convert.ToDecimal(collection["DISTRIBUITION_COST_PERCENT"]),
                        Convert.ToDecimal(collection["MANUFACTURING_COST_PERCENT"]),
                        Convert.ToDecimal(collection["SALES_COMISSION_PERCENT"]),
                        Convert.ToDecimal(collection["TAX_PERCENT"]));
                    

                    
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [ActionFilter_CheckLogin]
        public ActionResult Delete(int id)
        {
            ParametrosVendaFisicaViewModel model = new ParametrosVendaFisicaViewModel();
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();

                model = provider.SLT_PARAMETROS_VENDAS_FISICAS(id).First();

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }

        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                PLProjetoProvider provider = new PLProjetoProvider();
                if (collection["ID"] != "")
                {
                    provider.DEL_PARAMETROS_VENDA_FISICA(Convert.ToInt32(collection["ID"]));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Index");
        }

       
    }
}