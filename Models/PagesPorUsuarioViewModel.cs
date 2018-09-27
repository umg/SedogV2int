using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class PagesPorUsuarioViewModel
    {
        public long ID { get; set; }
        public string PAGE { get; set; }
        public string DESCRICAO { get; set; }
        public long SELECIONADO { get; set; }
    }
}