using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class VendasDigitaisPorFaixa
    {
        public string ISRC { get; set; }
        public string Artista { get; set; }
        public string Titulo { get; set; }
        public string AccountingYearMonth { get; set; }
        public string usageType { get; set; }
        public decimal Valor { get; set; }
    }
}