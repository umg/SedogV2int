using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class RespostaCadastroProjetoViewModel
    {
        public string IdProjetoSedog { get; set; }
        public string NomeDoProjeto { get; set; }
        public string IdArtista { get; set; }
        public string NomeDoArtista { get; set; }
        public string Obs { get; set; }
        public string TipoContrato { get; set; }
        public string TipoProcesso { get; set; }
        public string TipoRelease { get; set; }
        public string Lancamento { get; set; }
        public string Origem { get; set; }
        public List<Resposta<string>> Includes { get; set; }
    }
}