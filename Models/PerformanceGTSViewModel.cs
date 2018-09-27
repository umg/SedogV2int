using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class PerformanceGTSViewModel
    {
        public List<ArtistaProjeto> Artista { get; set; }
        public InfosPerformanceGTS Infos { get; set; }
        
        //public List<ReceitaUltimos12Meses> ReceitaUltimos12Meses { get; set; }
        public string dataGTSReceita12Meses { get; set; }
        public string labelGTSReceita12Meses { get; set; }

        public string regiaoSudesteQTDShow { get; set; }
        public string regiaoSulQTDShow { get; set; }
        public string regiaoCentroOesteQTDShow { get; set; }
        public string regiaoExteriorQTDShow { get; set; }
        public string regiaoNorteQTDShow { get; set; }
        public string regiaoNordesteQTDShow { get; set; }

        public string tanqueMargendataTotal { get; set; }
        public string tanqueMargendataAtual { get; set; }
        public string tanqueMargendataProvisao { get; set; }
        public string tanqueMargendataTotalPlan { get; set; }
        public string tanqueMargendataProvisaoPlan { get; set; }

        public string tanqueReceitadataTotal { get; set; }
        public string tanqueReceitadataAtual { get; set; }
        public string tanqueReceitadataProvisao { get; set; }
        public string tanqueReceitadataTotalPlan { get; set; }
        public string tanqueReceitadataProvisaoPlan { get; set; }


        public decimal detalhesCacheMedioMes { get; set; }
        public decimal detalhesNumeroShowsMes { get; set; }
        public decimal detalhesTotalReceitaMes { get; set; }
        public decimal detalhesSaldoAberto { get; set; }
        public decimal detalhesMarginMes { get; set; }
        public decimal detalhesParticipacaoGTS { get; set; }
        public decimal detalhesParticipacaoArtista { get; set; }
        public decimal detalhesParticipacaoLIVE { get; set; }
    }

    public class InfosPerformanceGTS
    {
        public string IDArtista { get; set; }
        public string Artista { get; set; }
        public int? Ano { get; set; }
        public int? Mes { get; set; }
    }

    public class ReceitaUltimos12Meses
    {
        public string ABREV { get; set; }
        public string ANOMES { get; set; }
        public string LABEL { get; set; }
        public decimal DATA { get; set; }
    }
}