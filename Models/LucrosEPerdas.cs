using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class LucrosEPerdas
    {

        public string IDArtista { get; set; }
        public int IDPL { get; set; }
        public string Nome { get; set; }
        public Decimal Valor { get; set; }
        public string Tipo { get; set; }
        public string Artista { get; set; }
        public string Projeto { get; set; }
        public string OBS { get; set; }
    }

    public class ReceitaDespesaTotal
    {
        public long IDPROJ_SEDOG { get; set; }
        public decimal RECEITAS { get; set; }
        public decimal DESPESAS { get; set; }
        public decimal TOTAL { get; set; }
    }
}