using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class TopProfitLossHomeViewModel
    {
        public long IDPROJ_SEDOG { get; set; }
        public string ARTISTA { get; set; }
        public string PROJETO { get; set; }
        public decimal PROFIT_LOSS_YTD { get; set; }
        public decimal PROFIT_LOSS_RTD { get; set; }
    }
}