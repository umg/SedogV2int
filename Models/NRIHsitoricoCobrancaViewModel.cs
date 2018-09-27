using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class NRIHsitoricoCobrancaViewModel
    {
        public long ID_SEQ { get; set; }
        public string CONTATO { get; set; }
        public string COMENTARIOS { get; set; }
        public DateTime DATA_REGISTRO{ get; set; }
        public DateTime DATA_COBRANCA{ get; set; }
        public string ACAO_USER { get; set; }
        public decimal VALOR { get; set; }
        public string STATUS { get; set; }
        public long ID { get; set; }
    }
}