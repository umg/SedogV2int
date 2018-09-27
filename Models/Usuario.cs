using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class Usuario
    {
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Departamento { get; set; }
        public string Pass { get; set; }
        public List<Pages> Pages { get; set; }
        public List<Menu> Menus { get; set; }
        public List<string> IDsObjetosDashboard { get; set; }
        public string Acessos { get; set; }
        public string Culture { get; set; }
    }
}