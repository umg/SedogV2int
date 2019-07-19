using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class CadastroProjetoViewModel : Response
    {
        public PLProjeto PLProjeto { get; set; }
        public List<TipoContrato> _TipoContrato { get; set; }
        public List<TipoRelease> _TipoRelease { get; set; }
        public List<TipoProcesso> _TipoProcesso { get; set; }
        public List<Usuario> _Usuario { get; set; }
        public List<Genero_Musical> _Genero { get; set; }
        public List<Medidor_KPI> _Kpi { get; set; }

    }

}