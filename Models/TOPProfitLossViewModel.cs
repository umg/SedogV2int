using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class TOPProfitLossViewModel : Response
    {
        public List<TOPProfitLoss> TOPProfitLoss { get; set; }
    }


    public class TOPProfitLoss
    {
        public int IDProj_SEDOG { get; set; }
        public string Artista { get; set; }
        public string Projeto { get; set; }
        public decimal Profit_Loss { get; set; }
        public decimal Profit_Loss_RTD { get; set; }
    }
}