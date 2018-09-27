using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class DespesasDeMarketingPorArtistaViewModel
    {
        public List<DespesasDeMarketingPorArtista> DespesaDeMarketingPorArtistaReport { get; set; }
        public List<PLProjeto> PLProjetos { get; set; }
        public string Message { get; set; }
    }
    public class DespesasDeMarketingPorArtista
    {
        public string ARTISTA { get; set; }
        public string MCDL01 { get; set; }
        public decimal CUSTO_INICIAL { get; set; }
        public decimal TV { get; set; }
        public decimal RADIO { get; set; }
        public decimal MIDIA_ON_LINE { get; set; }
        public decimal IMPRENSA { get; set; }
        public decimal MERCHANDISING { get; set; }
        public decimal OUTROS { get; set; }
        public decimal AMOSTRAS { get; set; }
        public decimal ACAO_PROMO_OUTROS { get; set; }
        public decimal VIDEOCLIPS { get; set; }
        public decimal FREELANCERS { get; set; }
        public decimal CLIPPING { get; set; }
        public decimal NBD { get; set; }
        public decimal DIGITAL { get; set; }
        public decimal PUBLICIDADE_ON_LINE { get; set; }
    }
}