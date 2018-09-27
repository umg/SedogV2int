using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class TanqueViewModel
    {
        public decimal YTD { get; set; }
        public decimal YTD_PLAN { get; set; }
        public decimal YTD_REAL { get; set; }
        public decimal TOTAL_ANO { get; set; }
        public decimal TOTAL_ANO_PLAN { get; set; }
    }

    public enum TipoTanque
    {
        gravacao,
        marketing,
        receita,
        ebitda,
        overhead
    }
}