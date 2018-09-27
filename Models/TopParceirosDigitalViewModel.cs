using System.ComponentModel.DataAnnotations;

namespace SEDOGv2.Models
{
    public class TopParceirosDigitalViewModel
    {
        public int ACCOUNTINGYEARMONTH { get; set; }
        public string CUSTOMERNAME { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal NETREVENUE { get; set; }
    }
}