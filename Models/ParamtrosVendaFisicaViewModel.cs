using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class ParametrosVendaFisicaViewModel
    {
        public int ID { get; set; }
        public string MIDIA { get; set; }
        public string AVERAGE_MANUFACTURING_PRICE { get; set; }
        public string OBSOLENCENCE_PROVISION_PERCENT { get; set; }
        public string RETURNS_PROVISION_PERCENT { get; set; }
        public string BAD_DEBTS_PROVISION_PERCENT { get; set; }
        public string COPYRIGHT_PERCENT { get; set; }
        public string ARTIST_RIGHT_PERCENT { get; set; }
        public string OTHER_ROYALTY_PERCENT { get; set; }
        public string PRODUCER_ROYALTY_PERCENT { get; set; }
        public string DISTRIBUITION_COST_PERCENT { get; set; }
        public string MANUFACTURING_COST_PERCENT { get; set; }
        public string SALES_COMISSION_PERCENT { get; set; }
        public string TAX_PERCENT { get; set; }

    }
}