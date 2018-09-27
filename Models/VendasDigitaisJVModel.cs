using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class VendasDigitaisJVModel
    {
        public int IDProjeto { get; set; }
        public string Artista { get; set; }
        public string Projeto { get; set; }
        public string Grupo { get; set; }
        public decimal Qtde { get; set; }
        public decimal Valor { get; set; }
    }
}