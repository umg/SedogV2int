using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class DespDepartamento
    {
        public string MCDL01 { get; set; }
        public string GBMCU { get; set; }
        public string GMDL01 { get; set; }
        public string GBOBJ { get; set; }
        public string GBSUB { get; set; }
        public string FCAST { get; set; }
        public decimal MES_ACTION { get; set; }
        public decimal MES_FCAST { get; set; }
        public decimal MES_PLAN { get; set; }
        public decimal DIF_ACTION_FCAST { get; set; }
        public decimal DIF_ACTION_PLAN { get; set; }
        public decimal AC_ACTION { get; set; }
        public decimal AC_FCAST { get; set; }
        public decimal AC_PLAN { get; set; }
        public decimal DIF_YTD_ACTION_FCAST { get; set; }
        public decimal DIF_YTD_ACTION_PLAN { get; set; }
    }
}