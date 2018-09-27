using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class OverheadViewModel
    {
        public List<Overhead> Overhead { get; set; }
        public int _selectedAno { get; set; }
        public int _selectedMes { get; set; }
        public string _selectedVisualizacao { get; set; }
    }

    public class Overhead
    {
        public string OBJ_JDE { get; set; }
        public string DESCRICAO { get; set; }
        public string GBSUB { get; set; }
        public decimal MESANT { get; set; }
        public decimal MES { get; set; }
        public decimal DIF { get; set; }
        public decimal YTDANT { get; set; }
        public decimal YTD { get; set; }
        public decimal DIFYTD { get; set; }
        public string FCAST { get; set; }
    }
}

