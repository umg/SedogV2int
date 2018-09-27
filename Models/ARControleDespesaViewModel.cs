using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class ARControleDespesaViewModel
    {
        public DespesasARConsolidado consolidado { get; set; }
        public List<DespesasARCapitalizado> Capitalizado { get; set; }
        public List<DespesasARTipo> Tipo { get; set; }
        public List<DespesasARAcumulado> Acumulado { get; set; }

    }

    public class DespesasARAcumulado
    {
        public string BU { get; set; }
        public string Descrição { get; set; }
        public string Conta { get; set; }
        public string SubConta { get; set; }
        public string DescConta { get; set; }
        public decimal Despesa { get; set; }
        public decimal Orcamento { get; set; }
    }

    public class DespesasARConsolidado
    {
        public decimal DespesaJDE { get; set; }
        public decimal DespesaMes { get; set; }
        public decimal FcastMes { get; set; }
        public decimal VariacaoMes { get; set;}
        public decimal DespesaYTD { get; set; }
        public decimal FcastYTD { get; set; }
        public decimal VariacaoYTD { get; set; }
    }

    public class DespesasARCapitalizado
    {
        public string BU { get; set; }
        public string Descrição { get; set; }
        public decimal Valor { get; set; }

    }

    public class DespesasARTipo
    {
        public string Tipo { get; set; }
        public decimal Despesa { get; set; }
    }
}