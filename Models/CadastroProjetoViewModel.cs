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
    }
}