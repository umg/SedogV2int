using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using SEDOGv2.Helpers;


namespace SEDOGv2.Models.Context
{
    public class PLProjetoProvider : PLProjetoProvider_ext
    {
        
        public List<AtualizaAssinantesViewModel> SLT_ASSINANTES(int ano)
        {
            List<AtualizaAssinantesViewModel> ret = new List<AtualizaAssinantesViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = AddScheme("SLT_ASSINANTES");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<AtualizaAssinantesViewModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public Resposta<List<ProjetoBU>> SEL_BU_PROJETO(string bu)
        {
            Resposta<List<ProjetoBU>> resp = new Resposta<List<ProjetoBU>>();
            List<ProjetoBU> pls = new List<ProjetoBU>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();

                parameters.Add(AddParameter("P_BU", bu));
                string procedure = AddScheme("SEL_BU_PROJETO");

                dt = GetTable(procedure, parameters.ToArray());

                resp.Dados = dt.DataTableToList<ProjetoBU>();


            }
            catch (Exception ex)
            {
                resp.Error = ex.StackTrace;
                resp.Message = ex.Message;
            }

            return resp;
        }

        public Resposta<List<PLProjeto>> SEL_PLPROJETOS(string pesquisa)
        {
            Resposta<List<PLProjeto>> resp = new Resposta<List<PLProjeto>>();
            List<PLProjeto> pls = new List<PLProjeto>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_VALOR", pesquisa));
                string procedure = AddScheme("SEL_PLPROJETOS");

                dt = GetTable(procedure, parameters.ToArray());

                resp.Dados = dt.DataTableToList<PLProjeto>();


            }
            catch (Exception ex)
            {
                resp.Error = ex.StackTrace;
                resp.Message = ex.Message;
            }

            return resp;
        }
        public long INS_PL_LOTE(string chamada)
        {

            OleDbParameter[] param = new OleDbParameter[2];

            param[0] = new OleDbParameter();
            param[0].ParameterName = "P_CHAMADA";
            param[0].OleDbType = OleDbType.VarChar;
            param[0].Size = 20;
            param[0].Value = chamada; // Envio de arquivo.

            param[1] = new OleDbParameter();
            param[1].ParameterName = "P_RETURN";
            param[1].OleDbType = OleDbType.BigInt;
            param[1].Direction = System.Data.ParameterDirection.Output;
            param[1].Value = 0; // Envio de arquivo.


            string procedure = AddScheme("INS_PL_LOTE");
            ExecutaProcedureNoQuery(procedure, param);

            return Convert.ToInt64(param[1].Value);
        }
        public List<LucrosEPerdas> SLT_LUCROS_E_PERDAS(int IDArtista, long IDProjetoSEDOG, long IDlote, int ano)
        {
            List<LucrosEPerdas> listLucros = new List<LucrosEPerdas>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDARTISTA", IDArtista));
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", IDProjetoSEDOG));
                parameters.Add(AddParameter("P_IDLOTE", IDlote));
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = AddScheme("SLT_LUCROS_E_PERDAS");

                dt = GetTable(procedure, parameters.ToArray());

                listLucros = dt.DataTableToList<LucrosEPerdas>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listLucros;
        }
        public List<PLMarginAnalysis> SLT_MARGIN_ANALYSIS(int ano)
        {
            List<PLMarginAnalysis> listLucros = new List<PLMarginAnalysis>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = AddScheme("SLT_MARGIN_ANALYSIS");

                dt = GetTable(procedure, parameters.ToArray());

                listLucros = dt.DataTableToList<PLMarginAnalysis>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listLucros;
        }
        public List<TOPProfitLoss> SLT_TOP_PROFIT_LOSS(int ano)
        {
            List<TOPProfitLoss> listLucros = new List<TOPProfitLoss>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = AddScheme("SLT_TOP_PROFIT_LOSS ");

                dt = GetTable(procedure, parameters.ToArray());

                listLucros = dt.DataTableToList<TOPProfitLoss>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listLucros;
        }
        public List<DetalhesDeProdutos> SLT_PRODUTOS_POR_PROJETO(long idLote)
        {
            List<DetalhesDeProdutos> ret = new List<DetalhesDeProdutos>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PLLOTE", idLote));
                string procedure = AddScheme("SLT_PRODUTOS_POR_PROJETO");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<DetalhesDeProdutos>();

                foreach (DetalhesDeProdutos det in ret)
                {
                    //det.URL = GetCapa.GetCoverUrlString(det.COD_BARRAS);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<DetalhesDeProdutosBU> SLT_PRODUTOS_POR_PROJETO_BU(long idLote)
        {
            List<DetalhesDeProdutosBU> ret = new List<DetalhesDeProdutosBU>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PLLOTE", idLote));
                string procedure = AddScheme("SLT_PRODUTOS_POR_PROJETO_BU");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<DetalhesDeProdutosBU>();

                foreach (DetalhesDeProdutosBU det in ret)
                {
                    //det.URL = GetCapa.GetCoverUrlString(det.COD_BARRAS);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }


        public List<DetalhesDeProdutos> SLT_PRODUTOS_HEADER_POR_PROJETO(long idLote)
        {
            List<DetalhesDeProdutos> ret = new List<DetalhesDeProdutos>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PLLOTE", idLote));
                string procedure = AddScheme("SLT_PRODUTOS_HEADER_POR_PROJETO");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<DetalhesDeProdutos>();

                foreach (DetalhesDeProdutos det in ret)
                {
                    det.URL = GetCapa.GetCoverUrlString(det.COD_BARRAS, det.COD_BARRAS_DIG);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<ArtistaProjeto> SLT_ARTISTA_PROJETO(string artista)
        {
            List<ArtistaProjeto> ret = new List<ArtistaProjeto>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ARTISTA", artista));
                string procedure = AddScheme("SLT_ARTISTA_PROJETO ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<ArtistaProjeto>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<ProjetosBrdigitalPorArtistCode> SLT_PROJETOS_POR_ARTISTA(string artistcode, int p_optional = 0)
        {
            List<ProjetosBrdigitalPorArtistCode> ret = new List<ProjetosBrdigitalPorArtistCode>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ARTISTCODE", artistcode));
                parameters.Add(AddParameter("P_OPTIONAL", p_optional));

                string procedure = AddScheme("SLT_PROJETOS_POR_ARTISTA ");


                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<ProjetosBrdigitalPorArtistCode>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<ProdutoFisico> SEL_PRODUTO_FISICO_POR_ARTISTA(string artist)
        {
            List<ProdutoFisico> ret = new List<ProdutoFisico>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ARTISTA", artist));
                string procedure = AddScheme("SEL_PRODUTO_FISICO_POR_ARTISTA");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<ProdutoFisico>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<ProdutoFisico> SEL_PRODUTO_FISICO_BY_ANY(string pesquisa)
        {
            List<ProdutoFisico> ret = new List<ProdutoFisico>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_PESQUISA", pesquisa));
                string procedure = AddScheme("SEL_PRODUTO_FISICO_BY_ANY");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<ProdutoFisico>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<TipoContrato> SEL_PL_TIPO_CONTRATO()
        {
            List<TipoContrato> ret = new List<TipoContrato>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                string procedure = AddScheme("SEL_PL_TIPO_CONTRATO");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TipoContrato>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<TipoRelease> SEL_PL_TIPO_RELEASE()
        {
            List<TipoRelease> ret = new List<TipoRelease>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                string procedure = AddScheme("SEL_PL_TIPO_RELEASE");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TipoRelease>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<TipoProcesso> SEL_PL_TIPO_PROCESSO()
        {
            List<TipoProcesso> ret = new List<TipoProcesso>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                string procedure = AddScheme("SEL_PL_TIPO_PROCESSO");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TipoProcesso>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<TipoReceita> SEL_PL_TIPO_RECEITA()
        {
            List<TipoReceita> ret = new List<TipoReceita>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                string procedure = AddScheme("SEL_PL_TIPO_RECEITA");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TipoReceita>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public long INS_PL_PROJETOS_SEDOG(long P_IDPROJ_SEDOG, string P_PROJETO, string P_IDARTISTA, string P_ARTISTA, string P_OBS, int P_ID_TIPO_CONTRATO, int P_ID_TIPO_PROCESSO, int P_ID_TIPO_RELEASE, string P_DATA_LANCAMENTO, string P_NACIONALIDADE, decimal P_EBITIDA_PROJETADO, string P_LOGIN_REPONSAVEL, int P_ID_GENERO_MUSICAL, decimal P_RECEITA_PROJETADA, int P_ID_MEDIDOR_KPI)
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
                parameters.Add(AddParameter("P_LOGIN_REPONSAVEL", P_LOGIN_REPONSAVEL));
                parameters.Add(AddParameter("P_ID_GENERO_MUSICAL", P_ID_GENERO_MUSICAL));
                parameters.Add(AddParameter("P_RECEITA_PROJETADA", P_RECEITA_PROJETADA));
                parameters.Add(AddParameter("P_ID_MEDIDOR_KPI", P_ID_MEDIDOR_KPI));
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG, ParameterDirection.Output));

                string procedure = AddScheme("INS_PL_PROJETOS_SEDOG");

                OleDbParameter[] parametersClone = parameters.ToArray();

                ExecutaProcedureNoQuery(procedure, parametersClone);

                ret = long.Parse(parametersClone[14].Value.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public long UPD_PL_PROJETOS_SEDOG(long P_IDPROJ_SEDOG, string P_PROJETO, string P_IDARTISTA, string P_ARTISTA, string P_OBS, int P_ID_TIPO_CONTRATO, int P_ID_TIPO_PROCESSO, int P_ID_TIPO_RELEASE, string P_DATA_LANCAMENTO, string P_NACIONALIDADE)
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
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));

                string procedure = AddScheme("UPD_PL_PROJETOS_SEDOG");

                OleDbParameter[] parametersClone = parameters.ToArray();

                ExecutaProcedureNoQuery(procedure, parametersClone);

                ret = long.Parse(parametersClone[9].Value.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public Resposta<string> DEL_PL_PROJETO(long P_IDPROJ_SEDOG, string P_IDPROJ, string P_PROJECTNAME)
        {
            Resposta<string> ret = new Resposta<string>();
            ret.Dados = "Projeto: " + P_IDPROJ + " - " + P_PROJECTNAME;
            ret.Error = "";
            ret.Message = "";
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                parameters.Add(AddParameter("P_IDPROJ", P_IDPROJ));
                //parameters.Add(AddParameter("P_PROJECTNAME", P_PROJECTNAME));

                string procedure = AddScheme("DEL_PL_PROJETO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                ret.Error = ex.Source;
                ret.Message = ex.Message;
            }
            return ret;
        }
        public Resposta<string> INS_PL_PROJETO(long P_IDPROJ_SEDOG, string P_IDPROJ, string P_PROJECTNAME)
        {
            Resposta<string> ret = new Resposta<string>();
            ret.Dados = "Projeto: " + P_IDPROJ + " - " + P_PROJECTNAME;
            ret.Error = "";
            ret.Message = "";
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                parameters.Add(AddParameter("P_IDPROJ", P_IDPROJ));
                parameters.Add(AddParameter("P_PROJECTNAME", P_PROJECTNAME));

                string procedure = AddScheme("INS_PL_PROJETO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                ret.Error = ex.Source;
                ret.Message = ex.Message;
            }
            return ret;
        }
        public Resposta<string> INS_PRODUTO(long P_IDPROJ_SEDOG, string P_COD_PRODUTO, string P_PACKING)
        {
            Resposta<string> ret = new Resposta<string>();
            ret.Dados = "Produto: " + P_COD_PRODUTO + " - " + P_PACKING;
            ret.Error = "";
            ret.Message = "";
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                parameters.Add(AddParameter("P_COD_PRODUTO", P_COD_PRODUTO));
                parameters.Add(AddParameter("P_PACKING", P_PACKING));

                string procedure = AddScheme("INS_PRODUTO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                ret.Error = ex.Source;
                ret.Message = ex.Message;
            }
            return ret;
        }
        public Resposta<string> DEL_PRODUTO(long P_IDPROJ_SEDOG, string P_COD_PRODUTO, string P_PACKING)
        {
            Resposta<string> ret = new Resposta<string>();
            ret.Dados = "Produto: " + P_COD_PRODUTO + " - " + P_PACKING;
            ret.Error = "";
            ret.Message = "";
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                parameters.Add(AddParameter("P_COD_PRODUTO", P_COD_PRODUTO));
                parameters.Add(AddParameter("P_PACKING", P_PACKING));

                string procedure = AddScheme("DEL_PRODUTO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                ret.Error = ex.Source;
                ret.Message = ex.Message;
            }
            return ret;
        }
        public void PL_RODA_NEW_SEDOG(long P_IDPROJ_SEDOG)
        {
            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));

            string procedure = AddScheme("PL_RODA_NEW_SEDOG");

            ExecutaProcedureNoQuery(procedure, parameters.ToArray());
        }
        public Resposta<string> INS_PRODUTO_BUS(long P_IDPROJ_SEDOG, string P_COD_PRODUTO, string P_PACKING, string P_BU)
        {
            Resposta<string> ret = new Resposta<string>();
            if (P_COD_PRODUTO == "0")
                ret.Dados = "BU Projeto: " + P_BU;
            else
                ret.Dados = "BU Produto: " + P_COD_PRODUTO + " - " + P_PACKING + " / BU: " + P_BU;
            ret.Error = "";
            ret.Message = "";
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                parameters.Add(AddParameter("P_COD_PRODUTO", P_COD_PRODUTO));
                parameters.Add(AddParameter("P_PACKING", P_PACKING));
                parameters.Add(AddParameter("P_BU", P_BU));

                string procedure = AddScheme("INS_PRODUTO_BUS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                ret.Error = ex.Source;
                ret.Message = ex.Message;
            }
            return ret;
        }
        public Resposta<string> DEL_PRODUTO_BUS(long P_IDPROJ_SEDOG, string P_COD_PRODUTO, string P_PACKING, string P_BU)
        {
            Resposta<string> ret = new Resposta<string>();
            if (P_COD_PRODUTO == "0")
                ret.Dados = "BU Projeto: " + P_BU;
            else
                ret.Dados = "BU Produto: " + P_COD_PRODUTO + " - " + P_PACKING + " / BU: " + P_BU;
            ret.Error = "";
            ret.Message = "";
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                parameters.Add(AddParameter("P_COD_PRODUTO", P_COD_PRODUTO));
                parameters.Add(AddParameter("P_PACKING", P_PACKING));
                parameters.Add(AddParameter("P_BU", P_BU));

                string procedure = AddScheme("DEL_PRODUTO_BUS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                ret.Error = ex.Source;
                ret.Message = ex.Message;
            }
            return ret;
        }
        public Resposta<string> INS_PL_PARAMETROS_DIREITOS(long P_IDPROJ_SEDOG, decimal P_PERCENT_ARTISTICO_DIG, decimal P_PERCENT_AUTORAL_DIG)
        {
            Resposta<string> ret = new Resposta<string>();
            ret.Dados = "Percentual Artistico/Autoral: " + P_PERCENT_ARTISTICO_DIG.ToString("#.00") + " - " + P_PERCENT_AUTORAL_DIG.ToString("#.00");
            ret.Error = "";
            ret.Message = "";
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                parameters.Add(AddParameter("P_PERCENT_ARTISTICO_DIG", P_PERCENT_ARTISTICO_DIG));
                parameters.Add(AddParameter("P_PERCENT_AUTORAL_DIG", P_PERCENT_AUTORAL_DIG));

                string procedure = AddScheme("INS_PL_PARAMETROS_DIREITOS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                ret.Error = ex.Source;
                ret.Message = ex.Message;
            }
            return ret;
        }
        public Resposta<string> UPD_PL_PARAMETROS_DIREITOS(long P_IDPROJ_SEDOG, decimal P_PERCENT_ARTISTICO_DIG, decimal P_PERCENT_AUTORAL_DIG, decimal P_PERCENT_ROYALTY_OTHER_DIG, decimal P_PERCENT_ROYALTY_PRODUCER_DIG)
        {
            Resposta<string> ret = new Resposta<string>();
            ret.Dados = "Percentual Artistico/Autoral: " + P_PERCENT_ARTISTICO_DIG.ToString("#.00") + " - " + P_PERCENT_AUTORAL_DIG.ToString("#.00");
            ret.Error = "";
            ret.Message = "";
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                parameters.Add(AddParameter("P_PERCENT_ARTISTICO_DIG", P_PERCENT_ARTISTICO_DIG));
                parameters.Add(AddParameter("P_PERCENT_AUTORAL_DIG", P_PERCENT_AUTORAL_DIG));
                parameters.Add(AddParameter("P_PERCENT_ROYALTY_OTHER_DIG", P_PERCENT_ROYALTY_OTHER_DIG));
                parameters.Add(AddParameter("P_PERCENT_ROYALTY_PRODUCER_DIG", P_PERCENT_ROYALTY_PRODUCER_DIG));

                string procedure = AddScheme("UPD_PL_PARAMETROS_DIREITOS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                ret.Error = ex.Source;
                ret.Message = ex.Message;
            }
            return ret;
        }
        public List<TopArtistaDigitalViewModel> SLT_TOP_ARTISTA_DIGITAL(int ano, int mes)
        {
            List<TopArtistaDigitalViewModel> ret = new List<TopArtistaDigitalViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ANO", ano));
                parameters.Add(AddParameter("P_MES", mes));
                string procedure = AddScheme("SLT_TOP_ARTISTA_DIGITAL ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TopArtistaDigitalViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<TopParceirosDigitalViewModel> SLT_TOP_PARCEIROS_DIGITAL(int ano, int mes)
        {
            List<TopParceirosDigitalViewModel> ret = new List<TopParceirosDigitalViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ANO", ano));
                parameters.Add(AddParameter("P_MES", mes));
                string procedure = AddScheme("SLT_TOP_PARCEIROS_DIGITAL ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TopParceirosDigitalViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<DepartamentosViewModel> SEL_DEPARTAMENTOS()
        {
            List<DepartamentosViewModel> ret = new List<DepartamentosViewModel>();
            try
            {
                DataTable dt = new DataTable();
                string procedure = AddScheme("SEL_DEPARTAMENTOS");

                dt = GetTable(procedure);

                ret = dt.DataTableToList<DepartamentosViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public void INS_DEPARTAMENTOS(string depto)
        {
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_DEPTO", depto));
                string procedure = AddScheme("INS_DEPARTAMENTOS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DEL_DEPARTAMENTOS(string depto)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_DEPTO", depto));
                string procedure = AddScheme("DEL_DEPARTAMENTOS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<UsuarioViewModel> SLT_USUARIOS(string login)
        {
            List<UsuarioViewModel> ret = new List<UsuarioViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_LOGIN", login));
                string procedure = AddScheme("SLT_USUARIOS");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<UsuarioViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<ParametrosVendaFisicaViewModel> SLT_PARAMETROS_VENDAS_FISICAS(int ID)
        {
            List<ParametrosVendaFisicaViewModel> ret = new List<ParametrosVendaFisicaViewModel>();      
            try
            {
                DataTable dt = new DataTable();
                
                string procedure = AddScheme("SLT_PARAMETROS_VENDAS_FISICAS");
                List<OleDbParameter> parameters = new List<OleDbParameter>();

                parameters.Add(AddParameter("P_ID", ID));


                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<ParametrosVendaFisicaViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public void INS_USUARIO(string login, string nome, string email, string departamento)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_LOGIN", login.ToUpper()));
                parameters.Add(AddParameter("P_NOME", nome.ToUpper()));
                parameters.Add(AddParameter("P_EMAIL", email.ToUpper()));
                parameters.Add(AddParameter("P_DEPTO", departamento.ToUpper()));
                string procedure = AddScheme("INS_USUARIO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void INS_PARAMETROS_VENDAS_FISICAS(  string  P_MIDIA,
                                                    decimal P_AVERAGE_MANUFACTURING_PRICE,
                                                    decimal P_OBSOLENCENCE_PROVISION_PERCENT,
                                                    decimal P_RETURNS_PROVISION_PERCENT,
                                                    decimal P_BAD_DEBTS_PROVISION_PERCENT,
                                                    decimal P_COPYRIGHT_PERCENT,
                                                    decimal P_ARTIST_RIGHT_PERCENT,
                                                    decimal P_OTHER_ROYALTY_PERCENT,
                                                    decimal P_PRODUCER_ROYALTY_PERCENT,
                                                    decimal P_DISTRIBUITION_COST_PERCENT,
                                                    decimal P_MANUFACTURING_COST_PERCENT,
                                                    decimal P_SALES_COMISSION_PERCENT,
                                                    decimal P_TAX_PERCENT)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();

                parameters.Add(AddParameter("P_MIDIA", P_MIDIA.ToUpper()));
                parameters.Add(AddParameter("P_AVERAGE_MANUFACTURING_PRICE", P_AVERAGE_MANUFACTURING_PRICE));
                parameters.Add(AddParameter("P_OBSOLENCENCE_PROVISION_PERCENT", P_OBSOLENCENCE_PROVISION_PERCENT));
                parameters.Add(AddParameter("P_RETURNS_PROVISION_PERCENT", P_RETURNS_PROVISION_PERCENT));
                parameters.Add(AddParameter("P_BAD_DEBTS_PROVISION_PERCENT", P_BAD_DEBTS_PROVISION_PERCENT));
                parameters.Add(AddParameter("P_COPYRIGHT_PERCENT", P_COPYRIGHT_PERCENT));
                parameters.Add(AddParameter("P_ARTIST_RIGHT_PERCENT", P_ARTIST_RIGHT_PERCENT));
                parameters.Add(AddParameter("P_OTHER_ROYALTY_PERCENT", P_OTHER_ROYALTY_PERCENT));
                parameters.Add(AddParameter("P_PRODUCER_ROYALTY_PERCENT", P_PRODUCER_ROYALTY_PERCENT));
                parameters.Add(AddParameter("P_DISTRIBUITION_COST_PERCENT", P_DISTRIBUITION_COST_PERCENT));
                parameters.Add(AddParameter("P_MANUFACTURING_COST_PERCENT", P_MANUFACTURING_COST_PERCENT));
                parameters.Add(AddParameter("P_SALES_COMISSION_PERCENT", P_SALES_COMISSION_PERCENT));
                parameters.Add(AddParameter("P_TAX_PERCENT", P_TAX_PERCENT));

                string procedure = AddScheme("INS_PARAMETROS_VENDAS_FISICAS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UPD_USUARIO(string login, string nome, string email, string departamento)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_LOGIN", login.ToUpper()));
                parameters.Add(AddParameter("P_NOME", nome.ToUpper()));
                parameters.Add(AddParameter("P_EMAIL", email.ToUpper()));
                parameters.Add(AddParameter("P_DEPTO", departamento.ToUpper()));
                string procedure = AddScheme("UPD_USUARIO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UPD_PARAMETROS_VENDA_FISICA(int P_ID, string P_MIDIA,
                                                    decimal P_AVERAGE_MANUFACTURING_PRICE,
                                                    decimal P_OBSOLENCENCE_PROVISION_PERCENT,
                                                    decimal P_RETURNS_PROVISION_PERCENT,
                                                    decimal P_BAD_DEBTS_PROVISION_PERCENT,
                                                    decimal P_COPYRIGHT_PERCENT,
                                                    decimal P_ARTIST_RIGHT_PERCENT,
                                                    decimal P_OTHER_ROYALTY_PERCENT,
                                                    decimal P_PRODUCER_ROYALTY_PERCENT,
                                                    decimal P_DISTRIBUITION_COST_PERCENT,
                                                    decimal P_MANUFACTURING_COST_PERCENT,
                                                    decimal P_SALES_COMISSION_PERCENT,
                                                    decimal P_TAX_PERCENT)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID", P_ID));

                parameters.Add(AddParameter("P_MIDIA", P_MIDIA.ToUpper()));
                parameters.Add(AddParameter("P_AVERAGE_MANUFACTURING_PRICE", P_AVERAGE_MANUFACTURING_PRICE));
                parameters.Add(AddParameter("P_OBSOLENCENCE_PROVISION_PERCENT", P_OBSOLENCENCE_PROVISION_PERCENT));
                parameters.Add(AddParameter("P_RETURNS_PROVISION_PERCENT", P_RETURNS_PROVISION_PERCENT));
                parameters.Add(AddParameter("P_BAD_DEBTS_PROVISION_PERCENT", P_BAD_DEBTS_PROVISION_PERCENT));
                parameters.Add(AddParameter("P_COPYRIGHT_PERCENT", P_COPYRIGHT_PERCENT));
                parameters.Add(AddParameter("P_ARTIST_RIGHT_PERCENT", P_ARTIST_RIGHT_PERCENT));
                parameters.Add(AddParameter("P_OTHER_ROYALTY_PERCENT", P_OTHER_ROYALTY_PERCENT));
                parameters.Add(AddParameter("P_PRODUCER_ROYALTY_PERCENT", P_PRODUCER_ROYALTY_PERCENT));
                parameters.Add(AddParameter("P_DISTRIBUITION_COST_PERCENT", P_DISTRIBUITION_COST_PERCENT));
                parameters.Add(AddParameter("P_MANUFACTURING_COST_PERCENT", P_MANUFACTURING_COST_PERCENT));
                parameters.Add(AddParameter("P_SALES_COMISSION_PERCENT", P_SALES_COMISSION_PERCENT));
                parameters.Add(AddParameter("P_TAX_PERCENT", P_TAX_PERCENT));
                string procedure = AddScheme("UPD_PARAMETROS_VENDA_FISICA");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DashBoardsPorUsuarioViewModel> SLT_LOAD_DASHBOARD_INFO(string login)
        {
            var ret = new List<DashBoardsPorUsuarioViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_LOGIN", login));
                string procedure = AddScheme("SLT_LOAD_DASHBOARD_INFO");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<DashBoardsPorUsuarioViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<PagesPorUsuarioViewModel> SLT_PAGES_POR_USUARIO(string login)
        {
            List<PagesPorUsuarioViewModel> ret = new List<PagesPorUsuarioViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_LOGIN", login));
                string procedure = AddScheme("SLT_PAGES_POR_USUARIO");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<PagesPorUsuarioViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<DashBoard> SLT_OBJETOS()
        {
            try
            {
                var procedure = AddScheme("SLT_OBJETOS");

                var dt = GetTable(procedure);

                return dt.DataTableToList<DashBoard>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void INS_OBJETOS_USUARIO(string login, string idpage, string label)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_LOGIN", login.ToUpper()));
                parameters.Add(AddParameter("P_IDPAGE", idpage));
                parameters.Add(AddParameter("P_LABEL", label));
                string procedure = AddScheme("INS_OBJETOS_USUARIO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void DEL_OBJETO_USUARIO(string login)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_LOGIN", login.ToUpper()));
                string procedure = AddScheme("DEL_OBJETO_USUARIO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DEL_PAGINAS_USUARIO(string login)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_LOGIN", login.ToUpper()));
                string procedure = AddScheme("DEL_PAGINAS_USUARIO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void INS_PAGINAS_USUARIO(string login, long idpage)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_LOGIN", login.ToUpper()));
                parameters.Add(AddParameter("P_IDPAGE", idpage));
                string procedure = AddScheme("INS_PAGINAS_USUARIO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DEL_USUARIO(string login)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_LOGIN", login.ToUpper()));
                string procedure = AddScheme("DEL_USUARIO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DEL_PARAMETROS_VENDA_FISICA(int id)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID", id));
                string procedure = AddScheme("DEL_PARAMETROS_VENDA_FISICA");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void UPD_PL_DADOS_DATA_REFERENCIA(string data, int dataCordis)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_DATA_REFERENCIA", data));
                parameters.Add(AddParameter("P_DATA_REFERENCIA_CORDIS", dataCordis));
                string procedure = AddScheme("UPD_PL_DADOS_DATA_REFERENCIA");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PagesViewModel> SLT_PAGES(long id)
        {
            List<PagesViewModel> ret = new List<PagesViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID", id));
                string procedure = AddScheme("SLT_PAGES");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<PagesViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<PageHeadersViewModel> SLT_PAGEHEADERS()
        {
            List<PageHeadersViewModel> ret = new List<PageHeadersViewModel>();
            try
            {
                DataTable dt = new DataTable();
                string procedure = AddScheme("SLT_PAGEHEADERS");

                dt = GetTable(procedure);

                ret = dt.DataTableToList<PageHeadersViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public void INS_PAGE(long idSubtitle, string page, string path, string descricao)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDSUBTITLE", idSubtitle));
                parameters.Add(AddParameter("P_PAGE", page));
                parameters.Add(AddParameter("P_PATH", path));
                parameters.Add(AddParameter("P_DESCRICAO", descricao));
                string procedure = AddScheme("INS_PAGE");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UPD_PAGE(long id, long idSubtitle, string page, string path, string descricao)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID", id));
                parameters.Add(AddParameter("P_IDSUBTITLE", idSubtitle));
                parameters.Add(AddParameter("P_PAGE", page));
                parameters.Add(AddParameter("P_PATH", path));
                parameters.Add(AddParameter("P_DESCRICAO", descricao));
                string procedure = AddScheme("UPD_PAGE");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DEL_PAGE(long id)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID", id));
                string procedure = AddScheme("DEL_PAGE");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MenuViewModel> SLT_MENUS(long id)
        {
            List<MenuViewModel> ret = new List<MenuViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID", id));
                string procedure = AddScheme("SLT_MENUS");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<MenuViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public void INS_MENUS(string titulo, string subTitulo, string icone)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_TITULO", titulo));
                parameters.Add(AddParameter("P_SUBTITULO", subTitulo));
                parameters.Add(AddParameter("P_ICONE", icone));
                string procedure = AddScheme("INS_MENUS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TopParceirosGraficoViewModel> SLT_TOPGRAFICO_PARCEIROS_DIGITAL(int anomesatual, int anomesinicial)
        {
            List<TopParceirosGraficoViewModel> ret = new List<TopParceirosGraficoViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ATUAL", anomesatual));
                parameters.Add(AddParameter("P_INICIAL", anomesinicial));
                string procedure = AddScheme("SLT_TOPGRAFICO_PARCEIROS_DIGITAL");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TopParceirosGraficoViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<TopArtistasGraficoViewModel> SLT_TOPGRAFICO_ARTISTAS_DIGITAL(int anomesatual, int anomesinicial)
        {
            List<TopArtistasGraficoViewModel> ret = new List<TopArtistasGraficoViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ATUAL", anomesatual));
                parameters.Add(AddParameter("P_INICIAL", anomesinicial));
                string procedure = AddScheme("SLT_TOPGRAFICO_ARTISTAS_DIGITAL");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TopArtistasGraficoViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public DateTime SEL_DATAREFERENCIA()
        {
            DateTime ret = new DateTime();
            try
            {
                DataTable dt = new DataTable();
                string procedure = AddScheme("SEL_DATAREFERENCIA");

                dt = GetTable(procedure);
                if (dt.Rows.Count > 0)
                    ret = DateTime.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<TotalDigitalMensalViewModel> SLT_TOTAL_PARCEIROS_DIGITAL(int anomesatual, int anomesinicial)
        {
            List<TotalDigitalMensalViewModel> ret = new List<TotalDigitalMensalViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ATUAL", anomesatual));
                parameters.Add(AddParameter("P_INICIAL", anomesinicial));
                string procedure = AddScheme("SLT_TOTAL_PARCEIROS_DIGITAL");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TotalDigitalMensalViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<TotalByTypeDigitalViewModel> SLT_TOTAL_TIPO_ACTUAL(int anomesatual)
        {
            List<TotalByTypeDigitalViewModel> ret = new List<TotalByTypeDigitalViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ATUAL", anomesatual));
                string procedure = AddScheme("SLT_TOTAL_TIPO_ACTUAL");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TotalByTypeDigitalViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<TotalByTypeDigitalViewModel> SLT_TOTAL_TIPO_YTD(int anomesatual, int anomesinicial)
        {
            List<TotalByTypeDigitalViewModel> ret = new List<TotalByTypeDigitalViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ATUAL", anomesatual));
                parameters.Add(AddParameter("P_INICIAL", anomesinicial));
                string procedure = AddScheme("SLT_TOTAL_TIPO_YTD");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TotalByTypeDigitalViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<TotalByTypeDigitalViewModel> SLT_TOTAL_USAGETYPE_ACTUAL(int anomesatual)
        {
            List<TotalByTypeDigitalViewModel> ret = new List<TotalByTypeDigitalViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ATUAL", anomesatual));
                string procedure = AddScheme("SLT_TOTAL_USAGETYPE_ACTUAL");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TotalByTypeDigitalViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<TotalByTypeDigitalViewModel> SLT_TOTAL_SALETYPE_ACTUAL(int anomesatual)
        {
            List<TotalByTypeDigitalViewModel> ret = new List<TotalByTypeDigitalViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ATUAL", anomesatual));
                string procedure = AddScheme("SLT_TOTAL_SALETYPE_ACTUAL");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TotalByTypeDigitalViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<PL_Projeto_Sedog> SEL_PL_PROJETO_SEDOG(long idProjetoSedog)
        {
            List<PL_Projeto_Sedog> ret = new List<PL_Projeto_Sedog>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", idProjetoSedog));
                string procedure = AddScheme("SEL_PL_PROJETO_SEDOG");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<PL_Projeto_Sedog>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<PL_Projeto> SEL_PL_PROJETO(long idProjetoSedog)
        {
            List<PL_Projeto> ret = new List<PL_Projeto>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", idProjetoSedog));
                string procedure = AddScheme("SEL_PL_PROJETO");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<PL_Projeto>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<PL_Produto_BUS> SEL_PL_PRODUTO_BUS(long idProjetoSedog)
        {
            List<PL_Produto_BUS> ret = new List<PL_Produto_BUS>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", idProjetoSedog));
                string procedure = AddScheme("SEL_PL_PRODUTO_BUS");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<PL_Produto_BUS>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<PL_Produto> SEL_PL_PRODUTO(long idProjetoSedog)
        {
            List<PL_Produto> ret = new List<PL_Produto>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", idProjetoSedog));
                string procedure = AddScheme("SEL_PL_PRODUTO");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<PL_Produto>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public ReceitaDespesaTotal SEL_RECEITAS_DESPESAS_EBITDA(long idProjetoSedog)
        {
            ReceitaDespesaTotal ret = new ReceitaDespesaTotal();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", idProjetoSedog));
                string procedure = AddScheme("SEL_RECEITAS_DESPESAS_EBITDA");

                dt = GetTable(procedure, parameters.ToArray());
                var p = dt.DataTableToList<ReceitaDespesaTotal>();
                if (p.Count > 0)
                    ret = p[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public PL_Parametros_Direitos SEL_PL_PARAMETROS_DIREITOS(long idProjetoSedog)
        {
            PL_Parametros_Direitos ret = new PL_Parametros_Direitos();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", idProjetoSedog));
                string procedure = AddScheme("SEL_PL_PARAMETROS_DIREITOS");

                dt = GetTable(procedure, parameters.ToArray());
                var p = dt.DataTableToList<PL_Parametros_Direitos>();
                if (p.Count > 0)
                    ret = p[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<PL_Receitas_Extras> SEL_PL_RECEITAS_EXTRAS(long idProjetoSedog)
        {
            List<PL_Receitas_Extras> ret = new List<PL_Receitas_Extras>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", idProjetoSedog));
                string procedure = AddScheme("SEL_PL_RECEITAS_EXTRAS");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<PL_Receitas_Extras>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public Resposta<string> INS_PL_OUTRAS_RECEITAS(long P_IDPROJ_SEDOG, int P_ID_TIPO_RECEITA, decimal P_VALOR, int P_ANOMES, decimal P_DESPESA)
        {
            Resposta<string> ret = new Resposta<string>();
            ret.Error = "";
            ret.Message = "";
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                parameters.Add(AddParameter("P_ID_TIPO_RECEITA", P_ID_TIPO_RECEITA));
                parameters.Add(AddParameter("P_VALOR", P_VALOR));
                parameters.Add(AddParameter("P_ANOMES", P_ANOMES));
                parameters.Add(AddParameter("P_DESPESA", P_DESPESA));

                string procedure = AddScheme("INS_PL_OUTRAS_RECEITAS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                ret.Error = ex.Source;
                ret.Message = ex.Message;
            }
            return ret;
        }
        public Resposta<string> DEL_PL_OUTRAS_RECEITAS(long P_IDPROJ_SEDOG, int P_ID_TIPO_RECEITA, decimal P_VALOR)
        {
            Resposta<string> ret = new Resposta<string>();
            ret.Error = "";
            ret.Message = "";
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                parameters.Add(AddParameter("P_ID_TIPO_RECEITA", P_ID_TIPO_RECEITA));
                parameters.Add(AddParameter("P_VALOR", P_VALOR));

                string procedure = AddScheme("DEL_PL_OUTRAS_RECEITAS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                ret.Error = ex.Source;
                ret.Message = ex.Message;
            }
            return ret;
        }
        public Resposta<string> DEL_PRJ_SEDOG(long P_IDPROJ_SEDOG)
        {
            Resposta<string> ret = new Resposta<string>();
            ret.Error = "";
            ret.Message = "";
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));

                string procedure = AddScheme("DEL_PRJ_SEDOG");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                ret.Error = ex.Source;
                ret.Message = ex.Message;
            }
            return ret;
        }
        public List<TopDespesasMarketingViewModel> SLT_TOP_DESPESAS_MARKETING(int ano, string tipo)
        {
            List<TopDespesasMarketingViewModel> ret = new List<TopDespesasMarketingViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ANO", ano));
                parameters.Add(AddParameter("P_TIPO", tipo));
                string procedure = AddScheme("SLT_TOP_DESPESAS_MARKETING");

                dt = GetTable(procedure, parameters.ToArray());
                ret = dt.DataTableToList<TopDespesasMarketingViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<ArtistaProjeto> SLT_PROJETO_ARTISTA(string projeto)
        {
            List<ArtistaProjeto> ret = new List<ArtistaProjeto>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_PROJETO", projeto));
                string procedure = AddScheme("SLT_PROJETO_ARTISTA ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<ArtistaProjeto>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<DigitalCustomerUsageType> SLT_DIGITAL_CUSTOMER_USAGETYPE(long P_IDPROJ_SEDOG)
        {
            List<DigitalCustomerUsageType> ret = new List<DigitalCustomerUsageType>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                string procedure = AddScheme("SLT_DIGITAL_CUSTOMER_USAGETYPE");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<DigitalCustomerUsageType>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<TanqueViewModel> SEL_TANQUE(int mes, int ano, TipoTanque tipo)
        {
            List<TanqueViewModel> ret = new List<TanqueViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_MES", mes));
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = "";
                switch (tipo)
                {
                    case TipoTanque.gravacao:
                        procedure = AddScheme("SEL_TANQUE_GRAVACAO");
                        break;
                    case TipoTanque.marketing:
                        procedure = AddScheme("SEL_TANQUE_MARKETING");
                        break;
                    case TipoTanque.receita:
                        procedure = AddScheme("SEL_TANQUE_RECEITA");
                        break;
                    case TipoTanque.ebitda:
                        procedure = AddScheme("SEL_TANQUE_EBITDA");
                        break;
                    case TipoTanque.overhead:
                        procedure = AddScheme("SEL_TANQUE_OVERHEAD");
                        break;
                }

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TanqueViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<TopProfitLossHomeViewModel> SLT_TOP_GRAFICO_PROFIT_LOSS(int ano)
        {
            List<TopProfitLossHomeViewModel> ret = new List<TopProfitLossHomeViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = AddScheme("SLT_TOP_GRAFICO_PROFIT_LOSS");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TopProfitLossHomeViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<TopClientesViewModel> SLT_TOP_GRAFICO_CLIENTE(string deMes, string deAno, string ateMes, string ateAno, int qtd)
        {
            List<TopClientesViewModel> ret = new List<TopClientesViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_MES_DE", deMes));
                parameters.Add(AddParameter("P_ANO_DE", deAno));
                parameters.Add(AddParameter("P_MES_ATE", ateMes));
                parameters.Add(AddParameter("P_ANO_ATE", ateAno));
                parameters.Add(AddParameter("P_TOP", qtd));
                string procedure = AddScheme("SLT_TOP_GRAFICO_CLIENTE");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<TopClientesViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<ProjetoTotalViewModel> SEL_PROJETO_POR_ISRCS(string projeto)
        {
            List<ProjetoTotalViewModel> ret = new List<ProjetoTotalViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ISRCLIST", projeto));
                string procedure = AddScheme("SEL_PROJETO_POR_ISRCS ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<ProjetoTotalViewModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<OverheadViewModel> SLT_OVERHEAD_UMB(int visao, int mes, int ano)
        {
            List<OverheadViewModel> ret = new List<OverheadViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_VISAO", visao));
                parameters.Add(AddParameter("P_MES", mes));
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = AddScheme("SLT_OVERHEAD_UMB ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<OverheadViewModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<DespesasDeMarketingTotalPorArtista> SLT_DESPESAS_MARKETING_TOTAL_POR_ARTISTA_MES_ANO(long P_IDPROJ_SEDOG)
        {
            List<DespesasDeMarketingTotalPorArtista> ret = new List<DespesasDeMarketingTotalPorArtista>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                string procedure = AddScheme("SLT_DESPESAS_MARKETING_TOTAL_POR_ARTISTA_MES_ANO_COPA ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<DespesasDeMarketingTotalPorArtista>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<DespesasDeMarketingTotalPorArtista> SLT_DESPESAS_MARKETING_TOTAL_POR_ARTISTA(long P_IDPROJ_SEDOG)
        {
            List<DespesasDeMarketingTotalPorArtista> ret = new List<DespesasDeMarketingTotalPorArtista>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                string procedure = AddScheme("SLT_DESPESAS_MARKETING_TOTAL_POR_ARTISTA ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<DespesasDeMarketingTotalPorArtista>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<DespesasDeMarketingTotalPorArtista> SLT_DESPESAS_MARKETING_TOTAL_POR_ARTISTA_MES_ANO_COPA(long P_IDPROJ_SEDOG)
        {
            List<DespesasDeMarketingTotalPorArtista> ret = new List<DespesasDeMarketingTotalPorArtista>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                string procedure = AddScheme("SLT_DESPESAS_MARKETING_TOTAL_POR_ARTISTA_MES_ANO_COPA ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<DespesasDeMarketingTotalPorArtista>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<DespesasDeMarketingTotalPorArtista> SLT_DESPESAS_MARKETING_TOTAL_POR_ARTISTA_COPA(long P_IDPROJ_SEDOG)
        {
            List<DespesasDeMarketingTotalPorArtista> ret = new List<DespesasDeMarketingTotalPorArtista>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", P_IDPROJ_SEDOG));
                string procedure = AddScheme("SLT_DESPESAS_MARKETING_TOTAL_POR_ARTISTA_COPA ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<DespesasDeMarketingTotalPorArtista>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<DespesasDeMarketingCTBRepertorioPorGrupoViewModel> RODA_DESPESAS_MARKETING_CTB_MCS_REPERTORIO_GRUPO(int mes, int ano, string origem)
        {
            List<DespesasDeMarketingCTBRepertorioPorGrupoViewModel> ret = new List<DespesasDeMarketingCTBRepertorioPorGrupoViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ANO", ano));
                parameters.Add(AddParameter("P_MES", mes));
                parameters.Add(AddParameter("P_TIPO", "GRUPO"));
                parameters.Add(AddParameter("P_ORIGEM", origem));
                string procedure = AddScheme("RODA_DESPESAS_MARKETING_CTB_MCS_REPERTORIO ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<DespesasDeMarketingCTBRepertorioPorGrupoViewModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<DespesasDeMarketingCTBRepertorioPorContasViewModel> RODA_DESPESAS_MARKETING_CTB_MCS_REPERTORIO_CONTAS(int mes, int ano, string origem)
        {
            List<DespesasDeMarketingCTBRepertorioPorContasViewModel> ret = new List<DespesasDeMarketingCTBRepertorioPorContasViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ANO", ano));
                parameters.Add(AddParameter("P_MES", mes));
                parameters.Add(AddParameter("P_TIPO", "ABERTO"));
                parameters.Add(AddParameter("P_ORIGEM", origem));
                string procedure = AddScheme("RODA_DESPESAS_MARKETING_CTB_MCS_REPERTORIO ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<DespesasDeMarketingCTBRepertorioPorContasViewModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<DespesasDeMarketingCTBRepertorioPorGrupoContaViewModel> RODA_DESPESAS_MARKETING_CTB_MCS_REPERTORIO_GRUPOCONTAS(int mes, int ano, string origem)
        {
            List<DespesasDeMarketingCTBRepertorioPorGrupoContaViewModel> ret = new List<DespesasDeMarketingCTBRepertorioPorGrupoContaViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ANO", ano));
                parameters.Add(AddParameter("P_MES", mes));
                parameters.Add(AddParameter("P_TIPO", "CONTA"));
                parameters.Add(AddParameter("P_ORIGEM", origem));
                string procedure = AddScheme("RODA_DESPESAS_MARKETING_CTB_MCS_REPERTORIO ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<DespesasDeMarketingCTBRepertorioPorGrupoContaViewModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<DespesasDeMarketingPorArtista> SLT_DESPPESAS_MARKETING_POR_ARTISTA(string tipo, int mes, string artista)
        {
            List<DespesasDeMarketingPorArtista> ret = new List<DespesasDeMarketingPorArtista>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_TIPO", tipo));
                parameters.Add(AddParameter("P_MES", mes));
                parameters.Add(AddParameter("P_ARTISTA", artista));
                string procedure = AddScheme("SLT_DESPESAS_MARKETING_POR_ARTISTA ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<DespesasDeMarketingPorArtista>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<CamposTanqueViewModel> SEL_CAMPOS_TANQUE(string tanque)
        {
            List<CamposTanqueViewModel> ret = new List<CamposTanqueViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_NOME", tanque));
                string procedure = AddScheme("SEL_CAMPOS_TANQUE");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<CamposTanqueViewModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<ValoresTanqueViewModel> SEL_VALORES_TANQUE(string sqlTanque)
        {
            List<ValoresTanqueViewModel> ret = new List<ValoresTanqueViewModel>();
            try
            {
                DataTable dt = new DataTable();
                dt = GetTableFromSQLString(sqlTanque);
                long o = 0;
                foreach(DataRow row in dt.Rows)
                {
                    ValoresTanqueViewModel r = new ValoresTanqueViewModel();
                    long.TryParse(row["FORE"].ToString(), out o);
                    r.FORE = o;
                    o = 0;
                    long.TryParse(row["REAL"].ToString(), out o);
                    r.REAL = o;
                    o = 0;
                    long.TryParse(row["PLAN"].ToString(), out o);
                    r.PLAN = o;
                    r.DESCMES = row["DESCMES"].ToString();
                    r.ANO = row["ANO"].ToString();
                    r.TANQUE = row["TANQUE"].ToString();
                    ret.Add(r);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public void ATUALIZA_TANQUE_COMBUSTIVEL(int ano, int mes)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ANO", ano));
                parameters.Add(AddParameter("P_MES", mes));
                string procedure = AddScheme("ATUALIZA_TANQUE_COMBUSTIVEL");
                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ARPUViewModel> SLT_ARPU_ANO(int ano)
        {
            List<ARPUViewModel> ret = new List<ARPUViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = AddScheme("SLT_ARPU_ANO");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<ARPUViewModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<MarketShareViewModel> SLT_MARKET_SHARE_ANO(int ano)
        {
            List<MarketShareViewModel> ret = new List<MarketShareViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ANO", ano));
                string procedure = AddScheme("SLT_MARKET_SHARE_ANO");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<MarketShareViewModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<NRIProjetos360ViewModel> SEL_NRI_PROJETOS()
        {
            List<NRIProjetos360ViewModel> ret = new List<NRIProjetos360ViewModel>();
            try
            {
                DataTable dt = new DataTable();
                string procedure = AddScheme("SEL_NRI_PROJETOS");

                dt = GetTable(procedure);

                ret = dt.DataTableToList<NRIProjetos360ViewModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public NRIProjetos360ViewModel SEL_NRI_PROJETO(long idProjeto)
        {
            NRIProjetos360ViewModel ret = new NRIProjetos360ViewModel();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PROJNRI", idProjeto));
                string procedure = AddScheme("SEL_NRI_PROJETO");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<NRIProjetos360ViewModel>().First();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<NRIProjetosContrato> SEL_NRI_PROJETO_CONTRATO(long idProjeto)
        {
            List<NRIProjetosContrato> ret = new List<NRIProjetosContrato>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PROJNRI", idProjeto));
                string procedure = AddScheme("SEL_NRI_PROJETO_CONTRATO");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<NRIProjetosContrato>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<NRIProjetosReceitas> SEL_NRI_PROJETO_RECEITA(long idProjeto)
        {
            List<NRIProjetosReceitas> ret = new List<NRIProjetosReceitas>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PROJNRI", idProjeto));
                string procedure = AddScheme("SEL_NRI_PROJETO_RECEITA");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<NRIProjetosReceitas>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<NRIProjetosAnexosViewModel> SEL_NRI_PROJETOS_ANEXOS(long idProjeto)
        {
            List<NRIProjetosAnexosViewModel> ret = new List<NRIProjetosAnexosViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PROJNRI", idProjeto));
                string procedure = AddScheme("SEL_NRI_PROJETOS_ANEXOS");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<NRIProjetosAnexosViewModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public NRIProjetosReceitas SEL_NRI_PROJETO_RECEITA_POR_SEQ(long id_seq)
        {
            NRIProjetosReceitas ret = new NRIProjetosReceitas();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_SEQ", id_seq));
                string procedure = AddScheme("SEL_NRI_PROJETO_RECEITA_POR_SEQ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<NRIProjetosReceitas>()[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public void UPD_NRI_PROJETOS(long idProjetoNRI, string idArtista , string artista , string projeto, string bu , string data, string obs, string expiracao)
        {
            List<NRIProjetosReceitas> ret = new List<NRIProjetosReceitas>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PROJNRI", idProjetoNRI));
                parameters.Add(AddParameter("P_ID_ARTISTA", idArtista));
                parameters.Add(AddParameter("P_ARTISTA", artista));
                parameters.Add(AddParameter("P_PROJETO", projeto));
                parameters.Add(AddParameter("P_BU", bu));
                parameters.Add(AddParameter("P_DATA_INICIO", data));
                parameters.Add(AddParameter("P_OBS", obs));
                parameters.Add(AddParameter("P_DATA_EXPIRACAO", expiracao));
                string procedure = AddScheme("UPD_NRI_PROJETOS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void INS_NRI_PROJETOS_ANEXOS(long idProjetoNRI, string arquivo, string obs)
        {
            List<NRIProjetosReceitas> ret = new List<NRIProjetosReceitas>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PROJNRI", idProjetoNRI));
                parameters.Add(AddParameter("P_NOME_ARQUIVO", arquivo));
                parameters.Add(AddParameter("P_OBS", obs));
                string procedure = AddScheme("INS_NRI_PROJETOS_ANEXOS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DEL_NRI_PROJETOS_ANEXOS(long idProjetoNRI, string arquivo)
        {
            List<NRIProjetosReceitas> ret = new List<NRIProjetosReceitas>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PROJNRI", idProjetoNRI));
                parameters.Add(AddParameter("P_NOME_ARQUIVO", arquivo));
                string procedure = AddScheme("DEL_NRI_PROJETOS_ANEXOS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DEL_NRI_CONTRATO(long idProjetoNRI, long idTipoContrato)
        {
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PROJNRI", idProjetoNRI));
                parameters.Add(AddParameter("P_ID_TIPO_CONTRATO", idTipoContrato));
                string procedure = AddScheme("DEL_NRI_CONTRATO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DEL_NRI_RECEITA(long idProjetoNRI, long idTipoReceita, long id_seq)
        {
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PROJNRI", idProjetoNRI));
                parameters.Add(AddParameter("P_ID_TIPO_RECEITA", idTipoReceita));
                parameters.Add(AddParameter("P_ID_SEQ", id_seq));
                string procedure = AddScheme("DEL_NRI_RECEITA");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NRITipoReceita> SEL_NRI_TIPO_RECEITA()
        {
            List<NRITipoReceita> ret = new List<NRITipoReceita>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                string procedure = AddScheme("SEL_NRI_TIPO_RECEITA");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<NRITipoReceita>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<NRITipoContrato> SEL_NRI_TIPO_CONTRATO()
        {
            List<NRITipoContrato> ret = new List<NRITipoContrato>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                string procedure = AddScheme("SEL_NRI_TIPO_CONTRATO ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<NRITipoContrato>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<NRIResponsavel> SEL_NRI_RESPONSAVEL()
        {
            List<NRIResponsavel> ret = new List<NRIResponsavel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                string procedure = AddScheme("SEL_NRI_RESPONSAVEL ");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<NRIResponsavel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public void INS_NRI_PROJETOS_CONTRATO(long idProjetoNRI, long idTipoContrato, string descricao)
        {
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PROJNRI", idProjetoNRI));
                parameters.Add(AddParameter("P_ID_TIPO_CONTRATO", idTipoContrato));
                parameters.Add(AddParameter("P_DESCRICAO", descricao));
                string procedure = AddScheme("INS_NRI_PROJETOS_CONTRATO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void INS_NRI_PROJETOS_RECEITA(long idProjetoNRI, long idTipoReceita, long idResponsavel,decimal percent,string acordo, int quantidade, int saldo, string obs, int emailCobranca)
        {
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PROJNRI", idProjetoNRI));
                parameters.Add(AddParameter("P_ID_TIPO_RECEITA", idTipoReceita));
                parameters.Add(AddParameter("P_ID_RESPONSAVEL", idResponsavel));
                parameters.Add(AddParameter("P_PERCENT", percent));
                parameters.Add(AddParameter("P_ACORDO", acordo));
                parameters.Add(AddParameter("P_QUANTIDADE", quantidade));
                parameters.Add(AddParameter("P_SALDO", saldo));
                parameters.Add(AddParameter("P_OBS_RECEITA", obs));
                parameters.Add(AddParameter("P_EMAIL_COBRANCA", emailCobranca));
                string procedure = AddScheme("INS_NRI_PROJETOS_RECEITA");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DEL_NRI_PROJETO(long idProjetoNRI)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_PROJNRI", idProjetoNRI));
                string procedure = AddScheme("DEL_NRI_PROJETO");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void INS_NRI_PROJETOS (long idArtista , string artista, string projeto, string bu, DateTime dtInicial, string obs)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_ARTISTA", idArtista));
                parameters.Add(AddParameter("P_ARTISTA", artista));
                parameters.Add(AddParameter("P_PROJETO", projeto));
                parameters.Add(AddParameter("P_BU", bu));
                parameters.Add(AddParameter("P_DATA_INICIAL", dtInicial.ToString("yyyy-MM-dd")));
                parameters.Add(AddParameter("P_OBS", obs));
                string procedure = AddScheme("INS_NRI_PROJETOS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NRIHsitoricoShowViewModel> SEL_NRI_HISTORICO_SHOWS(long id_seq)
        {
            List<NRIHsitoricoShowViewModel> ret = new List<NRIHsitoricoShowViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_SEQ", id_seq));
                string procedure = AddScheme("SEL_NRI_HISTORICO_SHOWS");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<NRIHsitoricoShowViewModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public List<NRIHsitoricoCobrancaViewModel> SEL_NRI_HISTORICO_COBRANCAS(long id_seq)
        {
            List<NRIHsitoricoCobrancaViewModel> ret = new List<NRIHsitoricoCobrancaViewModel>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_SEQ", id_seq));
                string procedure = AddScheme("SEL_NRI_HISTORICO_COBRANCAS");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<NRIHsitoricoCobrancaViewModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public void INS_NRI_HISTORICO_SHOWS(long id_seq,decimal valor, string obs, string data)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_SEQ", id_seq));
                parameters.Add(AddParameter("P_VALOR", valor));
                parameters.Add(AddParameter("P_OBS", obs));
                parameters.Add(AddParameter("P_DATA", data));
                string procedure = AddScheme("INS_NRI_HISTORICO_SHOWS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DEL_NRI_HISTORICO_SHOWS(long id)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID", id));
                string procedure = AddScheme("DEL_NRI_HISTORICO_SHOWS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void INS_NRI_HISTORICO_COBRANCAS(long id_seq, string contato, string comentarios, decimal valor, string data)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_SEQ", id_seq));
                parameters.Add(AddParameter("P_CONTATO", contato));
                parameters.Add(AddParameter("P_COMENTARIOS", comentarios));
                parameters.Add(AddParameter("P_VALOR", valor));
                parameters.Add(AddParameter("P_DATA", data)); //campo incluído dia 07/11/2017
                string procedure = AddScheme("INS_NRI_HISTORICO_COBRANCAS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UPD_NRI_HISTORICO_COBRANCAS(long id)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_SEQ", id));
                string procedure = AddScheme("UPD_NRI_HISTORICO_COBRANCAS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DEL_NRI_HISTORICO_COBRANCAS(long id)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_SEQ", id));
                string procedure = AddScheme("DEL_NRI_HISTORICO_COBRANCAS");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UPD_NRI_ALERTA_EMAIL(long id)
        {
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_ID_SEQ", id));
                string procedure = AddScheme("UPD_NRI_ALERTA_EMAIL");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Medidor_KPI> SEL_PL_MEDIDOR_KPI()
        {
            List<Medidor_KPI> ret = new List<Medidor_KPI>();
            try
            {
                DataTable dt = new DataTable();
                string procedure = AddScheme("SEL_PL_MEDIDOR_KPI");

                dt = GetTable(procedure);

                ret = dt.DataTableToList<Medidor_KPI>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public List<TopClientesVendasAnual> SLT_TOP_CLIENTES_VENDAS_COMPARATIVO_ANUAL(string tipo, int mes1, int ano1, int mes2, int ano2, char sumarizado)
        {
            var retorno = new List<TopClientesVendasAnual>();
            try
            {
                var dataTable = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_TIPO", tipo));
                parameters.Add(AddParameter("P_MES1", mes1));
                parameters.Add(AddParameter("P_ANO1", ano1));
                parameters.Add(AddParameter("P_MES2", mes2));
                parameters.Add(AddParameter("P_ANO2", ano2));
                parameters.Add(AddParameter("P_SUMARIZADO", sumarizado));

                var procedure = AddScheme("SLT_TOP_CLIENTES_VENDAS_COMPARATIVO_ANUAL");

                dataTable = GetTable(procedure, parameters.ToArray());

                retorno = dataTable.DataTableToList<TopClientesVendasAnual>();
            }
            catch (Exception e)
            {
                throw e;
            }
            return retorno;
        }
        public List<TopClientesFisicoDigitalTotal> SLT_TOP_VENDAS(string mesDe, string anoDe, string mesAte, string anoAte, string diaAte)
        {
            var retorno = new List<TopClientesFisicoDigitalTotal>();
            try
            {
                var dataTable = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_MES_DE", mesDe));
                parameters.Add(AddParameter("P_ANO_DE", anoDe));
                parameters.Add(AddParameter("P_MES_ATE", mesAte));
                parameters.Add(AddParameter("P_ANO_ATE", anoAte));
                parameters.Add(AddParameter("P_DIA_ATE", diaAte));

                var procedure = AddScheme("SLT_TOP_VENDAS");

                dataTable = GetTable(procedure, parameters.ToArray());

                retorno = dataTable.DataTableToList<TopClientesFisicoDigitalTotal>();
            }
            catch (Exception e)
            {
                throw e;
            }
            return retorno;
        }
        public List<TopClientesFisicoDigital> SLT_TOP10_CLIENTE(string mesDe, string anoDe, string mesAte, string anoAte, int top)
        {
            var retorno = new List<TopClientesFisicoDigital>();
            try
            {
                var dataTable = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_MES_DE", mesDe));
                parameters.Add(AddParameter("P_ANO_DE", anoDe));
                parameters.Add(AddParameter("P_MES_ATE", mesAte));
                parameters.Add(AddParameter("P_ANO_ATE", anoAte));
                parameters.Add(AddParameter("P_TOP", top));

                var procedure = AddScheme("SLT_TOP10_CLIENTE");

                dataTable = GetTable(procedure, parameters.ToArray());

                retorno = dataTable.DataTableToList<TopClientesFisicoDigital>();
            }
            catch (Exception e)
            {
                throw e;
            }
            return retorno;
        }
        public List<TopClientesFisicoViewModel> SLT_TOP_CLIENTES_DETALHADO(DateTime dataInicial, DateTime dataFinal)
        {
            var retorno = new List<TopClientesFisicoViewModel>();
            try
            {

                var dataTable = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_DATAINICIAL", dataInicial.ToString("yyyy-MM-dd")));
                parameters.Add(AddParameter("P_DATAFINAL", dataFinal.ToString("yyyy-MM-dd")));
                var procedure = AddScheme("SLT_TOP_CLIENTES_DETALHADO");

                dataTable = GetTable(procedure, parameters.ToArray());

                retorno = dataTable.DataTableToList<TopClientesFisicoViewModel>();
            }
            catch (Exception e)
            {
                throw e;
            }
            return retorno;
        }
        public List<ISRCs> SEL_ISRC_IDPROJ_SEDOG_MISSING(long IDProjetoSEDOG)
        {
            List<ISRCs> ret = new List<ISRCs>();
            try
            {
                DataTable dt = new DataTable();
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_IDPROJ_SEDOG", IDProjetoSEDOG));
                string procedure = AddScheme("SEL_ISRC_IDPROJ_SEDOG_MISSING");

                dt = GetTable(procedure, parameters.ToArray());

                ret = dt.DataTableToList<ISRCs>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public Resposta<string> INS_ISRC_SUMMARY(string P_VALUEINSERT)
        {
            Resposta<string> ret = new Resposta<string>();
            ret.Dados = "";
            ret.Error = "";
            ret.Message = "";
            try
            {
                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_VALUEINSERT", P_VALUEINSERT));

                string procedure = AddScheme("INS_ISRC_SUMMARY");

                ExecutaProcedureNoQuery(procedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                ret.Error = ex.Source;
                ret.Message = ex.Message;

            }
            return ret;
        }

    }
}