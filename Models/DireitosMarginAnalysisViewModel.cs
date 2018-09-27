using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class DireitosMarginAnalysisViewModel
    {
        public List<TipoContrato> _TipoContrato { get; set; }

        public int IdProjeto { get; set; }
        public string Projeto { get; set; }
        public long IdArtista { get; set; }
        public string Artista { get; set; }
        public string Title { get; set; }
        public DateTime Release { get; set; }
        public decimal FisicaVendaLiquida { get; set; }
        public decimal FisicaCustodeFabricacao { get; set; }
        public decimal FisicaDireitosAutorais { get; set; }
        public decimal FisicaDistribuicao { get; set; }
        public decimal FisicaDireitosArtisticos { get; set; }
        public decimal FisicaMargemVenda { get; set; }
        public decimal DigitalReceitaVenda { get; set; }
        public decimal DigitalCustosdeDistribuicao { get; set; }
        public decimal DigitalCustosdeDigitalizacao { get; set; }
        public decimal DigitalDireitosArtisticos { get; set; }
        public decimal DigitalDireitosAutorais { get; set; }
        public decimal DigitalOutrosCustosDireitos { get; set; }
        public decimal DigitalMargemVenda { get; set; }
        public decimal ReceitadeLicenciamento { get; set; }
        public decimal Direitos { get; set; }
        public decimal MargemLicenciamento { get; set; }
        public decimal NovosNegociosReceita { get; set; }
        public decimal NovosNegociosCustosdeFabricacao { get; set; }
        public decimal NovosNegociosCustosdeDistribuicao { get; set; }
        public decimal NovosNegociosDireitosArtisticos { get; set; }
        public decimal NovosNegociosDireitosAutorais { get; set; }
        public decimal NovosNegociosOutrosCustosDireitos { get; set; }
        public decimal NovosNegociosMargem { get; set; }
        public decimal ResultadoCustoInicial { get; set; }
        public decimal ResultadoMarketing { get; set; }
        public decimal ResultadoCustodeGravacao { get; set; }
        public decimal ResultadoProvisaoDevedoresDuvidosos { get; set; }
        public decimal ResultadoProvisaodeDevolução { get; set; }
        public decimal ResultadoProvisaodeObsolescencia { get; set; }
        public decimal ResultadoComissaodeVendas { get; set; }
        public decimal ResultadoValorParceiroJV { get; set; }
        public decimal ResultadoLiquido { get; set; }
    }

}