using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using SEDOGv2.Helpers;

namespace SEDOGv2.Controllers.Relatorios
{
    public class PLPresidenciaFinanceiroController : Controller
    {
        //
        // GET: /PLPresidenciaFinanceiro/
        [ActionFilter_CheckLogin]
        public ActionResult Index()
        {
            PLPresidenciaFinanceiroViewModel model = new PLPresidenciaFinanceiroViewModel();
            model.PLProjetos = new List<PLProjeto>();
            model.LucrosEPerdas = new List<LucrosEPerdas>();
            model.DetalhesDosProdutos = new List<DetalhesDeProdutos>();
            model.DetalhesDosProdutosBU = new List<DetalhesDeProdutosBU>();
            model.RecDespTot = new ReceitaDespesaTotal();
            return View(model);
        }
        [ActionFilter_CheckLogin]
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            PLPresidenciaFinanceiroViewModel model = new PLPresidenciaFinanceiroViewModel();
            model.PLProjetos = new List<PLProjeto>();
            model.LucrosEPerdas = new List<LucrosEPerdas>();
            model.RecDespTot = new ReceitaDespesaTotal();
            ViewBag.IDLote = 0;

            //Código para pesquisar os projetos
            if (Request.Form["Pesquisar"] != null)
            {
                try
                {
                    PLProjetoProvider provider = new PLProjetoProvider();
                    Resposta<List<PLProjeto>> resp = new Resposta<List<PLProjeto>>();

                    resp = provider.SEL_PLPROJETOS(collection["txtArtistaProduto"]);

                    model.Error = resp.Error;
                    model.Message = resp.Message;
                    model.PLProjetos = resp.Dados;
                }
                catch (Exception ex)
                {
                    model.Error = ex.Message;
                    ViewBag.Error = ex.Message;
                }
            }
            //Código para gerar os relatórios
            else if (Request.Form["btnGerarRelatorios"] != null)
            {

                PLProjetoProvider provider = new PLProjetoProvider();
                string varprojetos = collection["selProdutos"];
                string varano = collection["hdnDate"];
                ViewBag.Ano = "- Todos -";
                //Inserir tabela de lotes
                long IDLote = provider.INS_PL_LOTE("PL Presidencia");
                int ano = 0;
                bool tryANo = Int32.TryParse(varano, out ano);

                ViewBag.IDLote = IDLote;

                string[] projetos = varprojetos.Split(',');
                string sql = "INSERT INTO " + appSettings.Ambiente + ".PL_LOTE_PLPROJETO( ID_PLLOTE , IDPROJ_SEDOG ) VALUES ";
                foreach (string s in projetos)
                    sql = string.Concat(sql, " ( ", IDLote.ToString(), " , ", s, " ), ");

                if (!string.IsNullOrEmpty(sql))
                    sql = sql.Remove(sql.Length - 2, 2);

                provider.ExecuteCommandSQL(sql);

                //gerar relatórios
                model.LucrosEPerdas = provider.SLT_LUCROS_E_PERDAS(0, 0, IDLote, ano);
                //deletar lotes

                model.DetalhesHeaderDosProdutos = provider.SLT_PRODUTOS_HEADER_POR_PROJETO(IDLote);
                model.DetalhesDosProdutos = provider.SLT_PRODUTOS_POR_PROJETO(IDLote);
                model.DetalhesDosProdutosBU = provider.SLT_PRODUTOS_POR_PROJETO_BU(IDLote);
                model.isrc = provider.SLT_ISRC_RECEITA_TITULO_POR_PROJETO(IDLote);
                
                //model.RecDespTot = provider.SEL_RECEITAS_DESPESAS_EBITDA(IDLote);

                decimal despesa = 0;
                decimal receita = 0;
                decimal resultado = 0;

                foreach (LucrosEPerdas lucros in model.LucrosEPerdas)
                {
                    if (lucros.IDPL == 10) ViewBag.VendaFisicaLiquida = lucros.Valor;
                    if (lucros.IDPL == 150) ViewBag.ReceitasNovosNegocios = lucros.Valor;


                    //if (lucros.Tipo == "RD" && lucros.IDPL != 250)
                    //    receita = receita + lucros.Valor;
                    //else if (lucros.Tipo == "DD")
                    //    despesa = despesa + lucros.Valor;
                    if (lucros.IDPL == 320)
                    {
                        if (lucros.Valor < 0)
                        {
                            lucros.Valor = (lucros.Valor * -1);
                            //lucros.Tipo = "RR";
                        } else
                        {
                            lucros.Valor = (lucros.Valor * -1);
                            //lucros.Tipo = "DD";
                        }

                    }

                    if (lucros.Tipo == "R" || lucros.Tipo == "RR")
                         receita = receita + lucros.Valor;
                    if (lucros.IDPL == 320)
                    {
                        lucros.Valor = (lucros.Valor * -1);
                    }


                    else if (lucros.Tipo == "D" || lucros.Tipo == "DD")
                      despesa = despesa + lucros.Valor;                    

                }

                ViewBag.ValorTotalReceita = receita;

                resultado = receita - despesa;
                ViewBag.desprec = resultado >= 0 ? "receita" : "despesa";
                ViewBag.updown = resultado >= 0 ? "fa-arrow-up" : "fa-arrow-down";
                ViewBag.despesa = string.Format("{0:c2}", despesa);
                ViewBag.receita = string.Format("{0:c2}", receita);
                ViewBag.resultado = string.Format("{0:c2}", resultado);
                ViewBag.fundoResultado = resultado >= 0 ? "bgBlue" : "bgRed";

            }
            else if (Request.Form["selAno"] != null)
            {
                
                PLProjetoProvider provider = new PLProjetoProvider();
                string idProjeto = collection["idProjeto"];
                string varano = collection["selAno"];
                if (varano == "0")
                    ViewBag.Ano = "- Todos -";
                else
                    ViewBag.Ano = "20" + collection["selAno"];

                //Inserir tabela de lotes
                long IDLote = long.Parse(idProjeto);

                ViewBag.IDLote = IDLote;
                
                int ano = 0;
                bool tryANo = Int32.TryParse(varano, out ano);

                //gerar relatórios
                model.LucrosEPerdas = provider.SLT_LUCROS_E_PERDAS(0, 0, IDLote, ano);
                //deletar lotes

                model.DetalhesHeaderDosProdutos = provider.SLT_PRODUTOS_HEADER_POR_PROJETO(IDLote);
                model.DetalhesDosProdutos = provider.SLT_PRODUTOS_POR_PROJETO(IDLote);
                model.DetalhesDosProdutosBU = provider.SLT_PRODUTOS_POR_PROJETO_BU(IDLote);
                model.isrc = provider.SLT_ISRC_RECEITA_TITULO_POR_PROJETO(IDLote);

                decimal despesa = 0;
                decimal receita = 0;
                decimal resultado = 0;

                foreach (LucrosEPerdas lucros in model.LucrosEPerdas)
                {
                    if (lucros.IDPL == 10) ViewBag.VendaFisicaLiquida = lucros.Valor;
                    if (lucros.IDPL == 150) ViewBag.ReceitasNovosNegocios = lucros.Valor;


                    //if (lucros.Tipo == "RD" && lucros.IDPL != 250)
                    //    receita = receita + lucros.Valor;
                    //else if (lucros.Tipo == "DD")
                    //    despesa = despesa + lucros.Valor;
                    if (lucros.IDPL == 320)
                    {
                        if (lucros.Valor < 0)
                        {
                            lucros.Valor = (lucros.Valor * -1);
                            //lucros.Tipo = "RR";
                        }
                        else
                        {
                            lucros.Valor = (lucros.Valor * -1);
                            //lucros.Tipo = "DD";
                        }

                    }
                    if (lucros.Tipo == "R")
                        receita = receita + lucros.Valor;
                    if (lucros.IDPL == 320)
                    {
                        lucros.Valor = (lucros.Valor * -1);
                    }
                    else if (lucros.Tipo == "D" || lucros.Tipo == "DD")
                        despesa = despesa + lucros.Valor;
                }

                ViewBag.ValorTotalReceita = receita;

                resultado = receita - despesa;
                ViewBag.desprec = resultado >= 0 ? "receita" : "despesa";
                ViewBag.updown = resultado >= 0 ? "fa-arrow-up" : "fa-arrow-down";
                ViewBag.despesa = string.Format("{0:c2}", despesa);
                ViewBag.receita = string.Format("{0:c2}", receita);
                ViewBag.resultado = string.Format("{0:c2}", resultado);
                ViewBag.fundoResultado = resultado >= 0 ? "bgBlue" : "bgRed";
            }
            return View("Index", model);
        }
        
    }
}

