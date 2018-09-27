using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class Faixa
    {
        public string CountryofSale { get; set; }
        public string AccountingYearMonth { get; set; }
        public string ArtistCode { get; set; }
        public string ArtistName { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string UPC { get; set; }
        public string ISRC { get; set; }
        public string InvoiceAmount { get; set; }
        public string NetRevenueAmount { get; set; }
    }
}