using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class ImportAIF
    {
        public int Linhas { get; set; }
        public string IdProjetoSedog { get; set; }
        public string Projeto { get; set; }
        public string R2Projects { get; set; }
        public string Year { get; set; }
        public string ForeignIncome { get; set; }
        public string ArtistRoyalties { get; set; }
        public string ProducerRoyalties { get; set; }
        public string OtherRoyalty { get; set; }
        public string AllRoyalty { get; set; }
        public string ForeignMargin { get; set; }
        public string PercAIFMargin { get; set; }

    }
}