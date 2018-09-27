using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class Resposta<T>
    {
        public string Error;
        public string Message;
        public T Dados;
    }
}
