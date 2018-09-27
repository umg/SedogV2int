using System.ComponentModel.DataAnnotations;

namespace SEDOGv2.Models
{
    public class TopArtistaDigitalViewModel
    {
        public int ACCOUNTINGYEARMONTH { get; set; }
        public string ARTISTNAME { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal NETREVENUE { get; set; }
        public string TYPE { get; set; }
    }
}