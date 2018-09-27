using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class DespDepartamentaisViewModel
    {
        public bool _Listado { get; set; }
        public bool _hasDepartamento { get; set; }
        public List<ResponsavelDepartamento> _ResponsavelDepartamento { get; set; }
        public List<DespDepartamento> _DespDepartamento { get; set; }
        public List<Departamento> _Departamentos { get; set; }
        public int _selectedAno { get; set; }
        public int _selectedMes { get; set; }
        public int _selectedDepartamento { get; set; }


    }
}