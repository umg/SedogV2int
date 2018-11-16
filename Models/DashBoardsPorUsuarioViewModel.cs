using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class DashBoardsPorUsuarioViewModel
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Tipo { get; set; }
        public string Selecionado { get; set; }
        public string Menu { get; set; }
    }
}