using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class DireitosProfitLoss
    {
        public string Artista { get; set; }
        public string Album { get; set; }
        public string PeriodoInicial { get; set; }
        public string PeriodoFim { get; set; }
        public List<VendasDireitos> _vendasDireito { get; set; }
        public List<CanalDeVendasDigital> _canalDeVendas { get; set; }
        public List<Plataforma> _parceiros { get; set; }
        public List<PL_Projeto_Sedog> _PLProjetos { get; set; }
    }

    public class VendasDireitos
    {
        public string Vendas { get; set; }
        public int Qtde { get; set; }
        public decimal Valor { get; set; }
        public decimal Porcentagem { get; set; }
    }

    public class CanalDeVendasDigital
    {
        public string Grupo { get; set; }
        public int Qtde { get; set; }
        public decimal Valor { get; set; }
        public decimal Porcentagem { get; set; }
    }

    public class Plataforma
    {
        public string CustomerName { get; set; }
        public int QtdDigital { get; set; }
        public decimal Receita { get; set; }
        public decimal Porcentagem { get; set; }
    }

}