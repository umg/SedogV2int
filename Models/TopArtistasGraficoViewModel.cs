using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class TopArtistasGraficoViewModel
    {
        public int ACCOUNTINGYEARMONTH { get; set; }
        public string ARTISTNAME { get; set; }
        public int NETREVENUE { get; set; }
    }
    public class TopArtistasGrafico
    {
        public string Artista { get; set; }
        public string BgColor { get; set; }
        public string Color { get; set; }
        public List<int> AnoMes { get; set; }
        public List<int> NetRevenue { get; set; }
    }
}
