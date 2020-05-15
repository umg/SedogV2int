namespace SEDOGv2.Models
{
    public class TopClientesFisicoViewModel
    {
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public int QuantidadeVendida { get; set; }
        public decimal ValorVendaBruta { get; set; }
        public decimal Desconto { get; set; }
        public decimal Impostos { get; set; }
        public decimal ValorVendaLiquida { get; set; }
        public int QuantidadeDevolucao { get; set; }
        public decimal ValorDevolucaoLiquido { get; set; }
        public decimal ValorHfm { get; set; }
    }
}