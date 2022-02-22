using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace AtualizaDadosCopa.Models.Context
{
    public class PLProjetoProvider : Conn
    {
        public PLProjetoProvider()
        {
        }

        public PLProjetoProvider(HttpContext context)
        {
            HttpContext.Current = context;
        }


        public void RODA_ATUALIZACAO_TABAUX_COPA()
        {
            try
            {
                DataTable dt = new DataTable();

                string procedure = AddScheme("RODA_ATUALIZACAO_TABAUX_COPA");

                ExecutaProcedureNoQuery(procedure);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
