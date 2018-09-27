using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class NRIHsitoricoShowViewModel
    {
        public long ID_SEQ { get; set; }
        public DateTime DATA_REGISTRO { get; set; }
         public decimal VALOR { get; set; }
        public string OBS { get; set; }
        public long ID { get; set; }
    }
}