using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class PLPresidenciaFinanceiroViewModel : Response
    {
        public List<PLProjeto> PLProjetos { get; set; }
        public List<LucrosEPerdas> LucrosEPerdas { get; set; }
        public List<DetalhesDeProdutos> DetalhesHeaderDosProdutos { get; set; }
        public List<DetalhesDeProdutos> DetalhesDosProdutos { get; set; }
        public ReceitaDespesaTotal RecDespTot { get; set; }
        public List<ISRCs> isrc { get; set; }
    }
}