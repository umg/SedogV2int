using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class DetalhesDeProdutos
    {
        public int IDPROJ_SEDOG { get; set; }
        public string PROJETO { get; set; }
        public int ID_TIPO_CONTRATO { get; set; }
        public int ID_TIPO_PROCESSO { get; set; }
        public int ID_TIPO_RELEASE { get; set; }
        public string OBS { get; set; }
        public string TITULO_PRODUTO { get; set; }
        public string ARTISTA { get; set; }
        public string SELO { get; set; }
        public string COD_PRODUTO { get; set; }
        public string COD_BARRAS { get; set; }
        public string COD_BARRAS_DIG { get; set; }
        public string PACKING { get; set; }
        public string MIDIA { get; set; }
        public string URL { get; set; }
        public string DATA_LANCAMENTO { get; set; }
        public string BUS { get; set; }
        public string TIPO_CONTRATO { get; set; }
        public string QTD { get; set; }
    }
}
