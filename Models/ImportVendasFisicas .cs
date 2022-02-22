using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class ImportVendasFisicas
    {
        public string YEAR { get; set; }
        public string MONTH { get; set; }
        public string SKU { get; set; }
        public string BARCODE { get; set; }
        public string ARTISTNAME { get; set; }
        public string PRODUCTNAME { get; set; }
        public string MIDIA { get; set; }
        public string RELEASEDATE { get; set; }
        public string TYPESALES { get; set; }
        public decimal NETSALESVALUE { get; set; }
        public int NETSALESQUANTITY { get; set; }
        public decimal TAXSALESVALUE { get; set; }
        public decimal RETURNSALEVALUE { get; set; }
        public decimal RETURNSALESQUANTITY { get; set; }
        public decimal TAXSALESQUANTITY { get; set; }

    }
}