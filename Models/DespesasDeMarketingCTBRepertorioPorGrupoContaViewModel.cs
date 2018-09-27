using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class DespesasDeMarketingCTBRepertorioPorGrupoContaViewModel
    {
        public int ID_GRUPO { get; set; }
        public string GRUPO { get; set; }
        public string PDOBJ { get; set; }
        public string PDSUB { get; set; }
        public string DESCRICAO { get; set; }
        public decimal VALOR { get; set; }
    }
}