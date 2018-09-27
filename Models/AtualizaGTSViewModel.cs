using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class AtualizaGTSViewModel
    {
        public List<GTSForecast> _GTSForecast { get; set; }
        public List<GTSReceitaShows> _GTSReceitaShows { get; set; }
        public List<GTSMarginCachePartc> _GTSMarginCachePartc { get; set; }
        public List<GTSSaldos> _GTSSaldos { get; set; }
        public List<GTSRegiaoReceitaShows> _GTSRegiaoReceitaShows { get; set; }
        public bool GTSIsReceita {get;set;}
        public bool GTSIsShows { get; set; }
        public InfoGTSViewModel Infos { get; set; }
        public List<ArtistaProjeto> Artista { get; set; }
    }

    public class InfoGTSViewModel
    {
        public string IDArtista { get; set; }
        public string Nome { get; set; }
        public string Ano { get; set; }
        public string Tipo { get; set; }
        public string IDTipo { get; set; }
        public bool Pesquisa { get; set; }
    }

    public class GTSForecast
    {
        public string IDARTISTA { get; set; }
        public string NOME { get; set; }
        public int ANO { get; set; }
        public int MES { get; set; }
        public decimal VALOR_FCAST_RECEITA { get; set; }
        public decimal VALOR_PLAN_RECEITA { get; set; }
        public decimal VALOR_FCAST_MARGIN { get; set; }
        public decimal VALOR_PLAN_MARGIN { get; set; }
    }

    public class GTSReceitaShows
    {
        public string IDARTISTA { get; set; }
        public string NOME { get; set; }
        public int IDREGIAO { get; set; }
        public string REGIAO { get; set; }
        public int ANO { get; set; }
        public int MES { get; set; }
        public decimal VALOR { get; set; }
        public int QUANTIDADE { get; set; }
        public string[] Regioes = { "Sul", "Sudeste", "Norte", "Nordeste", "Centro Oeste", "Exterior" };
    }

    public class GTSRegiaoReceitaShows
    {
        public string IDARTISTA { get; set; }
        public string NOME { get; set; }
        public int ANO { get; set; }
        public int MES { get; set; }

        public decimal VLSul { get; set; }
        public int QTDSul { get; set; }

        public decimal VLSudeste { get; set; }
        public int QTDSudeste { get; set; }

        public decimal VLNorte { get; set; }
        public int QTDNorte { get; set; }

        public decimal VLNordeste { get; set; }
        public int QTDNordeste { get; set; }

        public decimal VLCentroOeste { get; set; }
        public int QTDCentroOeste { get; set; }

        public decimal VLExterior { get; set; }
        public int QTDExterior { get; set; }
        public string[] Regioes = { "Sul", "Sudeste", "Norte", "Nordeste", "Centro Oeste", "Exterior" };
    }

    public class GTSMarginCachePartc
    {
        public string IDARTISTA { get; set; }
        public string NOME { get; set; }
        public int ANO { get; set; }
        public int MES { get; set; }
        public decimal VALOR_ARTISTA { get; set; }
        public decimal VALOR_LIVE { get; set; }
        public decimal VALOR_GTS { get; set; }
        public decimal VALOR_CACHE { get; set; }
        public decimal VALOR_MARGEM { get; set; }
    }

    public class GTSSaldos
    {
        public string IDARTISTA { get; set; }
        public string NOME { get; set; }
        public decimal VALOR { get; set; }
    }

}