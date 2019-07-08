using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class PLProjeto
    {

        public int IDProj_SEDOG { get; set; }
        public string IDProj { get; set; }
        public string ProjectName { get; set; }
        public string Projeto { get; set; }
        public string IDArtista { get; set; }
        public string Artista { get; set; }
        public string OBS { get; set; }
        public int IDTipoContrato { get; set; }
        public int IDTipoProcesso { get; set; }
        public int IDTipoRelease { get; set; }
        public DateTime DataLancamento { get; set; }
        public string Nacionalidade { get; set; }
    }

    public class ParametrosDireitosProduto
    {
        public int IDPROJ_SEDOG { get; set; }
        public string PROJETO { get; set; }
        public string COD_PRODUTO { get; set; }
        public string PACKING { get; set; }
        public string TITULO_PRODUTO { get; set; }
        public string ARTISTA { get; set; }
        public decimal AUTORAL { get; set; }
        public decimal ARTISTICO { get; set; }
        public decimal PRODUCER { get; set; }
        public decimal OTHER { get; set; }
    }

}