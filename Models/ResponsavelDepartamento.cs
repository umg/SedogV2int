using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class ResponsavelDepartamento
    {
        public int ID { get; set; }
        public string NOME { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string IDDEPARTAMENTO { get; set; }
        public string EMAIL { get; set; }
        public string COPIAS { get; set; }
        public string COPIASOCULTA { get; set; }
    }
}