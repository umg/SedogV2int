using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class TopDespesasMarketingViewModel
    {
        public long IDPROJ_SEDOG { get; set; }
        public string NACIONALIDADE { get; set; }
        public string ARTISTA { get; set; }
        public string PROJETO { get; set; }
        public decimal CUSTO_INICIAL { get; set; }
        public decimal ACAO_TV { get; set; }
        public decimal ACAO_IMPRENSA { get; set; }
        public decimal ACAO_RADIO { get; set; }
        public decimal ACAO_INTERNET { get; set; }
        public decimal ACAO_OUTROS { get; set; }
        public decimal MIDIA_IMPRESSA { get; set; }
        public decimal MIDIA_TV { get; set; }
        public decimal MIDIA_DIGITAL { get; set; }
        public decimal MIDIA_RADIO { get; set; }
        public decimal MIDIA_INFLUENTES { get; set; }
        public decimal MIDIA_OUTROS { get; set; }
        public decimal PROD_VIDEO { get; set; }
        public decimal APOIO_TURNE { get; set; }
        public decimal POSTER_BANNER { get; set; }
        public decimal WEBSITES { get; set; }
        public decimal CLIPAGEM { get; set; }
        public decimal CD_PROMO { get; set; }
        public decimal TOT_MARKETING_E_CUSTO_INICIAL { get; set; }
        public decimal TOTAL_NET_SALES { get; set; }
        public decimal RESULTADO_LIQUIDO { get; set; }
    }
}