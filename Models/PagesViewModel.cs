using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class PagesViewModel
    {
        public long ID { get; set; }
        public long IDSUBTITLE { get; set; }
        public string MENU { get; set; }
        public string PAGE { get; set; }
        public string PATH { get; set; }
        public string DESCRICAO { get; set; }
        public string ICONE { get; set; }
    }
}