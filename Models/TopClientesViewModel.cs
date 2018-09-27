using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class TopClientesViewModel
    {
        public long CODIGO_CLIENTE { get; set; }
        public string CLIENTE { get; set; }
        public string TIPO { get; set; }
        public decimal RECEITA_YTD { get; set; }
        public decimal PERC_RECEITA_YTD { get; set; }
        public decimal TOT_RECEITA_YTD { get; set; }
    }
}