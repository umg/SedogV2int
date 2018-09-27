using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class DigitalCustomerUsageTypeReportViewModel
    {
        public List<DigitalCustomerUsageType> DigitalReport { get; set; }
        public List<PLProjeto> PLProjetos { get; set; }
        public string Message { get; set; }
    }
    public class DigitalCustomerUsageType
    {
        public int ACCOUNTINGYEARMONTH { get; set; }
        public string PROJECTNAME { get; set; }
        public string CUSTOMERCODE { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string USAGETYPE { get; set; }
        public string DESCRICAO { get; set; }
        public decimal VALOR { get; set; }
        public int QTDE { get; set; }
    }
}