using System.Collections.Generic;

namespace SEDOGv2.Models
{
    public class TopClientesVendasAnualViewModel
    {
        public List<TopClientesVendasAnual> Fracionado { get; set; }
        public List<TopClientesVendasAnual> Total { get; set; }

        public TopClientesVendasAnualViewModel()
        {
            Fracionado = new List<TopClientesVendasAnual>();
            Total = new List<TopClientesVendasAnual>();
        }
    }
}