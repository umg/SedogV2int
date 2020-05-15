using System.Collections.Generic;

namespace SEDOGv2.Models
{
    public class TopClientesFisicoDigitalViewModel
    {
        public List<TopClientesFisicoDigitalTotal> TopClientesFisicoDigitalTotal { get; set; }
        public List<TopClientesFisicoDigital> TopClientesFisicoDigital { get; set; }

        public TopClientesFisicoDigitalViewModel()
        {
            TopClientesFisicoDigitalTotal = new List<TopClientesFisicoDigitalTotal>();
            TopClientesFisicoDigital = new List<TopClientesFisicoDigital>();
        }
    }
}