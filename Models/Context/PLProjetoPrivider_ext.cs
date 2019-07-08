using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using SEDOGv2.Helpers;

namespace SEDOGv2.Models.Context
{
    /// <summary>
    /// Classe criada para poder ter mais de um desenvolvedor trabalhando no sistema ao mesmo tempo.
    /// </summary>
    public class PLProjetoPrivider_ext : Conn
    {
        public void TESTE(string x)
        {
            Console.Write(x);
        }
    }
}