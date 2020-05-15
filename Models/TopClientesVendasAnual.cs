namespace SEDOGv2.Models
{
    public class TopClientesVendasAnual
    {
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public string Tipo { get; set; }
        public decimal Mes2 { get; set; }
        public decimal Ano2 { get; set; }
        public decimal Mes1 { get; set; }
        public decimal Ano1 { get; set; }
    }
}