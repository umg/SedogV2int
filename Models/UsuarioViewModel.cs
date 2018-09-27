using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class UsuarioViewModel
    {
        public string LOGIN { get; set; }
        public string NOME { get; set; }
        public string EMAIL { get; set; }
        public string DEPTO { get; set; }
        public List<PagesPorUsuarioViewModel> Paginas { get; set; }
        public List<Departamento> _Departamentos { get; set; }
    }
}