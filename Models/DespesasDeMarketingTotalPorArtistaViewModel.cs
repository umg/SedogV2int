using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class DespesasDeMarketingTotalPorArtistaViewModel
    {
        public List<DespesasDeMarketingTotalPorArtista> DespesasDeMarketingTotalPorArtistaReport { get; set; }
        public List<PLProjeto> PLProjetos { get; set; }
        public string Message { get; set; }
    }
    public class DespesasDeMarketingTotalPorArtista
    {
        public string TIPO { get; set; }
        public string MCMCU { get; set; }
        public string MCDL01 { get; set; }
        public string PDOBJ { get; set; }
        public string PDSUB { get; set; }
        public string DESCRICAO { get; set; }
        public int ANO { get; set; }
        public int MES { get; set; }
        public decimal VALOR { get; set; }
    }
}