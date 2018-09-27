using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using SEDOGv2.Helpers;
using System.Text;

namespace SEDOGv2.Models.Context
{
    /// <summary>
    /// Classe criada para poder ter mais de um desenvolvedor trabalhando no sistema ao mesmo tempo.
    /// </summary>
    public class PLProjetoProvider_ext : Conn
    {

        public List<ResponsavelDepartamento> SEL_RESPONSAVEL_POR_DEPARTAMENTO()
        {
            List<ResponsavelDepartamento> listRespDepartamento = new List<ResponsavelDepartamento>();
            try
            {
                DataTable dt = new DataTable();
                string procedure = AddScheme("SEL_RESPONSAVEL_POR_DEPARTAMENTO");

                dt = GetTable(procedure);

                listRespDepartamento = dt.DataTableToList<ResponsavelDepartamento>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listRespDepartamento;
        }

        public List<DespDepartamento> SLT_DESP_DEPARTAMENTOS(int mes, int ano, int departamento, string financeiro)
        {
            List<DespDepartamento> listDespDepartamento = new List<DespDepartamento>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_MES", mes));
                parameters.Add(AddParameter("P_ANO", ano));
                parameters.Add(AddParameter("P_IDDEPARTAMENTOJDE", departamento));
                parameters.Add(AddParameter("P_FINANCEIRO", financeiro));
                string procedure = AddScheme("SLT_DESP_DEPARTAMENTOS");

                dt = GetTable(procedure, parameters.ToArray());

                listDespDepartamento = dt.DataTableToList<DespDepartamento>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listDespDepartamento;
        }

        public List<Departamento> SEL_BU_POR_DEPARTAMENTO_OVERHEAD()
        {
            List<Departamento> listDepartamento = new List<Departamento>();
            try
            {
                DataTable dt = new DataTable();
                string procedure = AddScheme("SEL_BU_POR_DEPARTAMENTO_OVERHEAD");

                dt = GetTable(procedure);

                listDepartamento = dt.DataTableToList<Departamento>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listDepartamento;
        }

        public List<Departamento> SEL_DEPARTAMENTOSJDE()
        {
            List<Departamento> listDepartamento = new List<Departamento>();
            try
            {
                DataTable dt = new DataTable();
                string procedure = AddScheme("SEL_DEPARTAMENTOSJDE");

                dt = GetTable(procedure);

                listDepartamento = dt.DataTableToList<Departamento>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listDepartamento;
        }

        public List<Departamento> SEL_DEPARTAMENTOSJDE_POR_USUARIOS(string P_LOGIN)
        {
            List<Departamento> listDepartamento = new List<Departamento>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_LOGIN", P_LOGIN));
                string procedure = AddScheme("SEL_DEPARTAMENTOSJDE_POR_USUARIOS");

                dt = GetTable(procedure, parameters.ToArray());

                listDepartamento = dt.DataTableToList<Departamento>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listDepartamento;
        }
        //ParametrosDireitosProduto
        public ParametrosDireitosProduto SEL_PARAMETROS_DIREITOS_PRODUTO(int IDProj_SEDOG, string CodProduto, string Packing)
        {
            ParametrosDireitosProduto _parametrosDireitosProduto = new ParametrosDireitosProduto();
            try
            {
                DataTable dt = new DataTable();

                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", IDProj_SEDOG));
                parameters.Add(AddParameter("P_COD_PRODUTO", CodProduto));
                parameters.Add(AddParameter("P_PACKING", Packing));

                string procedure = AddScheme("SEL_PARAMETROS_DIREITOS_PRODUTO");

                dt = GetTable(procedure, parameters.ToArray());

                _parametrosDireitosProduto = dt.DataTableToObject<ParametrosDireitosProduto>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _parametrosDireitosProduto;
        }

        public void INS_PARAMETROS_DIREITOS_PRODUTO(int IDProj_SEDOG, string CodProduto, string Packing, decimal artistico, decimal autoral)
        {
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", IDProj_SEDOG));
                parameters.Add(AddParameter("P_CODPROD", CodProduto));
                parameters.Add(AddParameter("P_PACKING", Packing));
                parameters.Add(AddParameter("P_ARTISTICO", artistico));
                parameters.Add(AddParameter("P_AUTORAL", autoral));
                string procedure = AddScheme("INS_PARAMETROS_DIREITOS_PRODUTO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UPD_PARAMETROS_DIREITOS_PRODUTO(int IDProj_SEDOG, string CodProduto, string Packing, decimal artistico, decimal autoral)
        {
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", IDProj_SEDOG));
                parameters.Add(AddParameter("P_CODPROD", CodProduto));
                parameters.Add(AddParameter("P_PACKING", Packing));
                parameters.Add(AddParameter("P_ARTISTICO", artistico));
                parameters.Add(AddParameter("P_AUTORAL", autoral));
                string procedure = AddScheme("UPD_PARAMETROS_DIREITOS_PRODUTO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> SEL_USUARIOS_POR_DEPTO(string departamento)
        {
            List<Usuario> usuarios = new List<Models.Usuario>();
            try
            {
                DataTable dt = new DataTable();

                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_DEPARTAMENTO", departamento));

                string procedure = AddScheme("SEL_USUARIOS_POR_DEPTO");

                dt = GetTable(procedure, parameters.ToArray());

                usuarios = dt.DataTableToList<Usuario>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return usuarios;
        }

        public List<Overhead> SLT_COMPARATIVO_MENSAL_ANTERIOR(int visao, int mes, int ano)
        {
            List<Overhead> ret = new List<Overhead>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_VISAO", visao));
                parameters.Add(AddParameter("P_MES", mes));
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = AddScheme("SLT_COMPARATIVO_MENSAL_ANTERIOR");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<Overhead>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<Overhead> SLT_COMPARATIVO_MENSAL_OVERHEAD(int visao, int mes, int ano)
        {
            List<Overhead> ret = new List<Overhead>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_VISAO", visao));
                parameters.Add(AddParameter("P_MES", mes));
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = AddScheme("SLT_COMPARATIVO_MENSAL_OVERHEAD");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<Overhead>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<Overhead> SLT_COMPARATIVO_MENSAL_FORCAST(int visao, int mes, int ano)
        {
            List<Overhead> ret = new List<Overhead>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_VISAO", visao));
                parameters.Add(AddParameter("P_MES", mes));
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = AddScheme("SLT_COMPARATIVO_MENSAL_FORCAST");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<Overhead>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<Genero_Musical> SEL_GENERO_MUSICAL()
        {
            List<Genero_Musical> ret = new List<Genero_Musical>();
            try
            {
                DataTable dt = new DataTable();

                string procedure = AddScheme("SEL_GENERO_MUSICAL");

                dt = GetTable(procedure);

                ret = dt.DataTableToList<Genero_Musical>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public long UPD_PL_PROJETOS_SEDOG(long P_IDPROJ_SEDOG, string P_PROJETO, string P_IDARTISTA, string P_ARTISTA, string P_OBS, int P_ID_TIPO_CONTRATO, int P_ID_TIPO_PROCESSO, int P_ID_TIPO_RELEASE, string P_DATA_LANCAMENTO, string P_NACIONALIDADE, decimal P_EBITIDA_PROJETADO, decimal P_RECEITA_PROJETADA, string P_LOGIN_RESPONSAVEL, int P_ID_GENERO_MUSICAL, int P_ID_MEDIDOR_KPI)
        {
            long ret = 0;
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_PROJETO", P_PROJETO));
                parameters.Add(AddParameter("P_IDARTISTA", P_IDARTISTA));
                parameters.Add(AddParameter("P_ARTISTA", P_ARTISTA));
                parameters.Add(AddParameter("P_OBS", P_OBS));
                parameters.Add(AddParameter("P_ID_TIPO_CONTRATO", P_ID_TIPO_CONTRATO));
                parameters.Add(AddParameter("P_ID_TIPO_PROCESSO", P_ID_TIPO_PROCESSO));
                parameters.Add(AddParameter("P_ID_TIPO_RELEASE", P_ID_TIPO_RELEASE));
                parameters.Add(AddParameter("P_DATA_LANCAMENTO", P_DATA_LANCAMENTO));
                parameters.Add(AddParameter("P_NACIONALIDADE", P_NACIONALIDADE));
                parameters.Add(AddParameter("P_EBITIDA_PROJETADO", P_EBITIDA_PROJETADO));
                parameters.Add(AddParameter("P_RECEITA_PROJETADA", P_RECEITA_PROJETADA));
                parameters.Add(AddParameter("P_LOGIN_RESPONSAVEL", P_LOGIN_RESPONSAVEL));
                parameters.Add(AddParameter("P_ID_GENERO_MUSICAL", P_ID_GENERO_MUSICAL));
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                parameters.Add(AddParameter("P_ID_MEDIDOR_KPI", P_ID_MEDIDOR_KPI));

                string procedure = AddScheme("UPD_PL_PROJETOS_SEDOG");

                OleDbParameter[] parametersClone = parameters.ToArray();

                ExecutaProcedureNoQuery(procedure, parametersClone);

                ret = long.Parse(parametersClone[12].Value.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<VendasDigitaisJVModel> SEL_VENDAS_DIGITAIS_JV_POR_USAGETYPE(int anomes)
        {
            List<VendasDigitaisJVModel> ret = new List<VendasDigitaisJVModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ANOMES", anomes));
                string procedure = AddScheme("SEL_VENDAS_DIGITAIS_JV_POR_USAGETYPE");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<VendasDigitaisJVModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<VendasDigitaisPorFaixa> SEL_VENDAS_DIGITAIS_POR_FAIXA(string isrc, int anomes)
        {
            List<VendasDigitaisPorFaixa> ret = new List<VendasDigitaisPorFaixa>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ISRC", isrc));
                parameters.Add(AddParameter("P_ANOMES", anomes));
                string procedure = AddScheme("SEL_VENDAS_DIGITAIS_POR_FAIXA");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<VendasDigitaisPorFaixa>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<DireitosMarginAnalysisViewModel> SLT_DIREITOS_LUCROS_E_PERDAS(int _tipoContrato)
        {
            List<DireitosMarginAnalysisViewModel> direitos = new List<Models.DireitosMarginAnalysisViewModel>();
            DireitosMarginAnalysisViewModel direito =  new DireitosMarginAnalysisViewModel();

            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_TIPO_CONTRATO", _tipoContrato));
                string procedure = AddScheme("SLT_DIREITOS_LUCROS_E_PERDAS");

                

                dt = GetTable(procedure, parameters.ToArray());

                foreach(DataRow dr in dt.Rows)
                {
                    if(direito.IdProjeto != Convert.ToInt32(dr["IDPROJ_SEDOG"].ToString()))
                    {
                        if (direito.IdProjeto != 0)
                            direitos.Add(direito);

                        direito = new DireitosMarginAnalysisViewModel();
                        direito.IdProjeto = Convert.ToInt32(dr["IDPROJ_SEDOG"].ToString());
                        direito.IdArtista = Convert.ToInt64(dr["IDARTISTA"].ToString());
                        direito.Artista = dr["ARTISTA"].ToString();
                        direito.Projeto = dr["PROJETO"].ToString();
                        direito.Release = Convert.ToDateTime(dr["DATA_LANCAMENTO"].ToString());
                    }

                    switch(Convert.ToInt32(dr["IDPL"].ToString()))
                    {
                        case 10://Venda Líquida Física 
                            direito.FisicaVendaLiquida = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 20://Custo de Fabricação
                            direito.FisicaCustodeFabricacao = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 30://Direitos Autorais
                            direito.FisicaDireitosAutorais = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 40://Distribuição
                            direito.FisicaDistribuicao = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 50://Direitos Artísticos
                            direito.FisicaDireitosArtisticos = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 60://Margem Venda Física    
                            direito.FisicaMargemVenda = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 70://Receita Venda Digital
                            direito.DigitalReceitaVenda = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 100://Custos de Distribuição
                            direito.DigitalCustosdeDistribuicao = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 101://Custos de Digitalização
                            direito.DigitalCustosdeDigitalizacao = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 80://Direitos Artísticos
                            direito.DigitalDireitosArtisticos = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 90://Direitos Autorais
                            direito.DigitalDireitosAutorais = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 102://Outros Custos Diretos
                            direito.DigitalOutrosCustosDireitos = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 110://Margem Venda Digital 
                            direito.DigitalMargemVenda = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 120://Receita de Licenciamento  
                            direito.ReceitadeLicenciamento = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 130://Direitos  
                            direito.Direitos = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 140://Margem Licenciamento
                            direito.MargemLicenciamento = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 150://Receita Novos Negócios
                            direito.NovosNegociosReceita = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 151://Custos de Fabricação 
                            direito.NovosNegociosCustosdeFabricacao = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 152://Custos de Distribuição 
                            direito.NovosNegociosCustosdeDistribuicao = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 160://Direitos Artísticos 
                            direito.NovosNegociosDireitosArtisticos = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 161://Direitos Autorais 
                            direito.NovosNegociosDireitosAutorais = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 162://Outros Custos Diretos 
                            direito.NovosNegociosOutrosCustosDireitos = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 170://Margem Novos Negócios   
                            direito.NovosNegociosMargem = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 220://Custo Inicial 
                            direito.ResultadoCustoInicial = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 240://Marketing
                            direito.ResultadoMarketing = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 230://Custo de Gravação
                            direito.ResultadoCustodeGravacao = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 190://Provisão Devedores Duvidosos
                            direito.ResultadoProvisaoDevedoresDuvidosos = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 200://Provisão de Obsolescência  
                            direito.ResultadoProvisaodeObsolescencia = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 210://Provisão de Devolução 
                            direito.ResultadoProvisaodeDevolução = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 180://Comissão de Vendas 
                            direito.ResultadoComissaodeVendas = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 245://Valor a ser pago para parceiro JV
                            direito.ResultadoValorParceiroJV = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        case 250://Resultado Líquido 
                            direito.ResultadoLiquido = Convert.ToDecimal(dr["VALOR"].ToString());
                            break;
                        default:
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return direitos;
        }

        public List<PLProjeto> SEL_LOOKUP_PL_PROJETO(string term)
        {
            List<PLProjeto> ret = new List<PLProjeto>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_TERM", term));
                string procedure = AddScheme("SEL_LOOKUP_PL_PROJETO");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<PLProjeto>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<Plataforma> SLT_DIREITOS_PROJETO_PLATAFORMA_DIGITAL(long idProjeto)
        {
            List<Plataforma> ret = new List<Plataforma>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", idProjeto));
                string procedure = AddScheme("SLT_DIREITOS_PROJETO_PLATAFORMA_DIGITAL");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<Plataforma>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<CanalDeVendasDigital> SLT_DIREITOS_PROJETO_GRUPO_DIGITAL(long idProjeto)
        {
            List<CanalDeVendasDigital> ret = new List<CanalDeVendasDigital>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", idProjeto));
                string procedure = AddScheme("SLT_DIREITOS_PROJETO_GRUPO_DIGITAL");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<CanalDeVendasDigital>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<VendasDireitos> SLT_DIREITOS_PROJETO(long idProjeto)
        {
            List<VendasDireitos> ret = new List<VendasDireitos>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", idProjeto));
                string procedure = AddScheme("SLT_DIREITOS_PROJETO");

                dt = GetTable(procedure, parameters.ToArray());

               foreach(DataRow row in dt.Rows)
                {
                    VendasDireitos vendasDigital = new Models.VendasDireitos();
                    VendasDireitos vendasFisica = new Models.VendasDireitos();

                    vendasDigital.Vendas = "Digitais";
                    vendasDigital.Qtde = (!string.IsNullOrEmpty(row["QTDDIGITAL"].ToString())) ? Convert.ToInt32(row["QTDDIGITAL"].ToString()) : 0 ;
                    vendasDigital.Valor = (!string.IsNullOrEmpty(row["RECEITA_DIGITAL"].ToString())) ? Convert.ToDecimal(row["RECEITA_DIGITAL"].ToString()) : 0 ;


                    vendasFisica.Vendas = "Físicas";
                    vendasFisica.Qtde = (!string.IsNullOrEmpty(row["QTDFISICA"].ToString())) ? Convert.ToInt32(row["QTDFISICA"].ToString()) : 0;
                    vendasFisica.Valor = (!string.IsNullOrEmpty(row["RECEITA_FISICA"].ToString())) ? Convert.ToDecimal(row["RECEITA_FISICA"].ToString()) : 0;

                    decimal total = vendasFisica.Valor + vendasDigital.Valor;

                    //contagem das porcentagens
                    vendasDigital.Porcentagem = (vendasDigital.Valor / total) * 100;
                    vendasFisica.Porcentagem = (vendasFisica.Valor/ total) * 100;

                    if(vendasDigital.Valor > vendasFisica.Valor)
                    {
                        ret.Add(vendasDigital);
                        ret.Add(vendasFisica);
                    }
                    else
                    {
                        ret.Add(vendasFisica);
                        ret.Add(vendasDigital);
                    }
                    

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<ISRCs> SLT_ISRC_RECEITA_TITULO_POR_PROJETO(long idLote)
        {
            List<ISRCs> ret = new List<ISRCs>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PLLOTE", idLote));
                string procedure = AddScheme("SLT_ISRC_RECEITA_TITULO_POR_PROJETO");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<ISRCs>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public void ExecuteSQL(string _sql)
        {
            try
            {
                ExecuteCommand(_sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Procedures GTS


        public List<ArtistaProjeto> SLT_ARTISTAS_GTS(string artista)
        {
            List<ArtistaProjeto> ret = new List<ArtistaProjeto>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ARTISTA", artista));
                string procedure = AddScheme("SLT_ARTISTAS_GTS");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<ArtistaProjeto>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<GTSMarginCachePartc> SEL_GTS_MARGEN_CACHE_PARTC(string IDArtista, int ano)
        {
            List<GTSMarginCachePartc> ret = new List<GTSMarginCachePartc>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDARTISTA", IDArtista));
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = AddScheme("SEL_GTS_MARGEN_CACHE_PARTC");


                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<GTSMarginCachePartc>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<GTSForecast> SEL_GTS_FCAST_PLAN(string IDArtista, int ano)
        {
            List<GTSForecast> ret = new List<GTSForecast>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDARTISTA", IDArtista));
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = AddScheme("SEL_GTS_FCAST_PLAN");


                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<GTSForecast>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<GTSRegiaoReceitaShows> SEL_GTS_RECEITA_SHOWS(string IDArtista, int ano)
        {
            List<GTSReceitaShows> ret = new List<GTSReceitaShows>();
            List<GTSRegiaoReceitaShows> regioes = new List<GTSRegiaoReceitaShows>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDARTISTA", IDArtista));
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = AddScheme("SEL_GTS_RECEITA_SHOWS");


                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<GTSReceitaShows>();

                var distincts = ret.Select(x => new { x.IDARTISTA, x.ANO, x.MES }).Distinct();

                foreach(var dis in distincts)
                {
                    var selecionados = ret.Where(x => x.IDARTISTA == dis.IDARTISTA && x.ANO == dis.ANO && x.MES == dis.MES );
                    GTSRegiaoReceitaShows regiao = new GTSRegiaoReceitaShows();
                    regiao.IDARTISTA = dis.IDARTISTA;
                    regiao.ANO = dis.ANO;
                    regiao.MES = dis.MES;

                    foreach (var sel in selecionados)
                    {
                        switch(sel.REGIAO.ToUpper())
                        {
                            case "SUL":
                                regiao.QTDSul = sel.QUANTIDADE;
                                regiao.VLSul = sel.VALOR;
                                break;
                            case "SUDESTE":
                                regiao.QTDSudeste = sel.QUANTIDADE;
                                regiao.VLSudeste = sel.VALOR;
                                break;
                            case "NORTE":
                                regiao.QTDNorte = sel.QUANTIDADE;
                                regiao.VLNorte = sel.VALOR;
                                break;
                            case "NORDESTE":
                                regiao.QTDNordeste = sel.QUANTIDADE;
                                regiao.VLNordeste = sel.VALOR;
                                break;
                            case "CENTRO OESTE":
                                regiao.QTDCentroOeste = sel.QUANTIDADE;
                                regiao.VLCentroOeste = sel.VALOR;
                                break;
                            case "EXTERIOR":
                                regiao.QTDExterior = sel.QUANTIDADE;
                                regiao.VLExterior = sel.VALOR;
                                break;
                            default:
                                break;
                        }
                    }
                    regioes.Add(regiao);

                }

                /*foreach(GTSReceitaShows aux in ret)
                {
                    GTSRegiaoReceitaShows regiao = new GTSRegiaoReceitaShows();
                    regiao.IDARTISTA = aux.IDARTISTA;
                    regiao.ANO = aux.ANO;
                    regiao.MES = aux.MES;
                }*/



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return regioes;
        }

        public List<GTSSaldos> SEL_GTS_SALDOS(string IDArtista)
        {
            List<GTSSaldos> ret = new List<GTSSaldos>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDARTISTA", IDArtista));
                string procedure = AddScheme("SEL_GTS_SALDOS");


                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<GTSSaldos>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }


        #endregion

        #region excel
        /// <summary>
        /// Cria conexão OLEDB para ler/escrever no excel
        /// </summary>
        /// <param name="path">Caminho do arquivo excel que será trabalhado</param>
        /// <returns></returns>
        private string GetExcelConnectionString(string path)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            // XLSX - Excel 2007, 2010, 2012, 2013
            props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            props["Extended Properties"] = "Excel 12.0 XML";
            //props["Data Source"] = "C:\\temp\\uploadORC.xlsx";
            props["Data Source"] = path;

            // XLS - Excel 2003 and Older
            //props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
            //props["Extended Properties"] = "Excel 8.0";
            //props["Data Source"] = "C:\\MyExcel.xls";

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }

            return sb.ToString();
        }

        /// <summary>
        /// Metódo para ler um arquivo excel e retornar um dataset
        /// </summary>
        /// <param name="path">caminho do arquivo excel que será lido</param>
        /// <returns></returns>
        public DataSet ReadExcelFile(string path)
        {
            DataSet ds = new DataSet();

            string connectionString = GetExcelConnectionString(path);

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;

                // Get all Sheets in Excel File
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                // Loop through all Sheets to get data
                foreach (DataRow dr in dtSheet.Rows)
                {
                    string sheetName = dr["TABLE_NAME"].ToString();

                    if (!sheetName.EndsWith("$"))
                        continue;

                    // Get all rows from the Sheet
                    cmd.CommandText = "SELECT * FROM [" + sheetName + "]";

                    DataTable dt = new DataTable();
                    dt.TableName = sheetName;

                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);

                    ds.Tables.Add(dt);
                }

                cmd = null;
                conn.Close();
            }

            return ds;
        }
        #endregion
    }
}