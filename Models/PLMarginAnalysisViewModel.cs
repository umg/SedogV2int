using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class PLMarginAnalysisViewModel : Response
    {
        public List<PLMarginAnalysis> PLMarginAnalysis { get; set; }
        public List<PLMarginAnalysis> DistinctRelease { get; set; }
        public List<PLMarginAnalysis> GetMarginByRelease(int release)
        {
            return this.PLMarginAnalysis.Where(y => y.ID_TIPO_RELEASE == release).ToList();
        }
    }

    public class PLMarginAnalysis
    {
        public int IDProj_SEDOG { get; set; }
        public int ID_TIPO_RELEASE { get; set; }
        public string TIPO_RELEASE { get; set; }
        public string Artista { get; set; }
        public string Projeto { get; set; }
        public decimal FISICAL_INCOME { get; set; }
        public decimal DIGITAL_INCOME { get; set; }
        public decimal BROADCAST_INCOME { get; set; }
        public decimal NB_INCOME { get; set; }
        public decimal TOTAL_INCOME { get; set; }
        public decimal FISICAL_MARGIN { get; set; }
        public decimal DIGITAL_MARGIN { get; set; }
        public decimal BROADCAST_MARGIN { get; set; }
        public decimal NB_MARGIN { get; set; }
        public decimal TOTAL_MARGIN { get; set; }
        public decimal MARKETING_INITIAL_COSTS { get; set; }
        public decimal RECORDING_COSTS { get; set; }
        public decimal PROFIT_LOSS { get; set; }
        public decimal Percent { get; set; }
    }

}