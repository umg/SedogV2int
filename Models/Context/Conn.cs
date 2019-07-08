using System;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDOGv2.Models.Context
{
    public class Conn
    {
        protected OleDbCommand Command;
        OleDbConnection _odbCon;
        private string _connectionString;
        protected string connectionString
        {
            get
            {
                return Helpers.appSettings.ConnectionString;
            }
        }

        private string _ambiente;
        /// <summary>
        /// Ele busca no WEB.CONFIG baseado no Pais passado na session.
        /// </summary>
        public string Ambiente
        {
            get
            {
                if (HttpContext.Current.Session["SEDOGv2.AMBIENTE"] != null)
                    return HttpContext.Current.Session["SEDOGv2.AMBIENTE"].ToString();
                else
                {
                    string[] _app = ConfigurationManager.AppSettings["dados"].Split(';');
                    return _app[1];
                }
            }
            set
            {
                HttpContext.Current.Session["SEDOGv2.AMBIENTE"] = value;
            }
        }

        public static string Servidor
        {
            get
            {
                if (HttpContext.Current.Session["SEDOGv2.SERVIDOR"] != null)
                    return HttpContext.Current.Session["SEDOGv2.SERVIDOR"].ToString();
                else
                {
                    string[] _app = ConfigurationManager.AppSettings["dados"].Split(';');
                    return _app[0];
                }
            }
            set
            {
                HttpContext.Current.Session["SEDOGv2.SERVIDOR"] = value;
            }
        }

        protected OleDbParameter AddParameter(string parameterName, object value)
        {

            OleDbParameter parameter = new OleDbParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            return parameter;
        }

        protected OleDbParameter AddParameter(string parameterName, object value, OleDbType type)
        {

            OleDbParameter parameter = new OleDbParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            parameter.OleDbType = type;
            return parameter;
        }

        protected OleDbParameter AddParameter(string parameterName, object value, ParameterDirection direction)
        {
            OleDbParameter parameter = new OleDbParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            parameter.Direction = direction;
            return parameter;
        }

        protected OleDbParameter AddParameter(string parameterName, object value, ParameterDirection direction, OleDbType type)
        {
            OleDbParameter parameter = new OleDbParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            parameter.Direction = direction;
            parameter.OleDbType = type;
            return parameter;
        }

        protected OleDbParameter AddParameter(string parameterName, object value, ParameterDirection direction, OleDbType type, int size)
        {
            OleDbParameter parameter = new OleDbParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            parameter.Direction = direction;
            parameter.OleDbType = type;
            parameter.Size = size;
            return parameter;
        }
        

        protected OleDbConnection Open()
        {
            if (_odbCon == null)
                _odbCon = new OleDbConnection();

            if (_odbCon.State == System.Data.ConnectionState.Closed)
            {
                _odbCon.ConnectionString = connectionString;
                _odbCon.Open();
            }
            return _odbCon;
        }

        protected OleDbConnection Open(string _connectionString)
        {
            if (_odbCon == null)
                _odbCon = new OleDbConnection();

            if (_odbCon.State == System.Data.ConnectionState.Closed)
            {
                _odbCon.ConnectionString = _connectionString;
                _odbCon.Open();
            }
            return _odbCon;
        }

        protected void Close()
        {
            if (_odbCon != null && _odbCon.State == System.Data.ConnectionState.Open)
                _odbCon.Close();
        }

        public string AddScheme(string commandText)
        {
            return string.Concat(Ambiente, ".", commandText);
        }

        /// <summary>
        /// Método para executar uma consulta em texto SQL sem retorno
        /// </summary>
        /// <param name="commandText">Texto a ser executado no SQL</param>
        public void ExecuteCommandSQL(string CommandText)
        {
            try
            {
                Command = new OleDbCommand();
                Command.Connection = Open();
                Command.CommandType = System.Data.CommandType.Text;
                Command.CommandText = CommandText;
                Command.Parameters.Clear();

                Command.ExecuteNonQuery();
            }
            finally
            {
                Command.Connection.Close();
            }
        }


        /// <summary>
        /// Método para executar uma consulta SQL sem retorno
        /// </summary>
        /// <param name="StoreProcedure">A procedure a ser executada no SQL</param>
        /// <param name="parametro">os parâmetros da procedure</param>
        protected void ExecutaProcedureNoQuery(string StoreProcedure, OleDbParameter[] parametro)
        {
            try
            {
                Command = new OleDbCommand();
                Command.Connection = Open();
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                Command.CommandText = StoreProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddRange(parametro);

                Command.ExecuteNonQuery();
            }
            finally
            {
                Command.Connection.Close();
            }

        }

        /// <summary>
        /// Método para executar uma consulta SQL sem retorno
        /// </summary>
        /// <param name="StoreProcedure">A procedure a ser executada no SQL</param>
        /// <param name="parametro">os parâmetros da procedure</param>
        /// <param name="_connectionString">String de conexão diferenciada</param>
        protected void ExecutaProcedureNoQuery(string StoreProcedure, OleDbParameter[] parametro, string _connectionString)
        {
            try
            {
                Command = new OleDbCommand();
                Command.Connection = Open(_connectionString);
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                Command.CommandText = StoreProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddRange(parametro);

                Command.ExecuteNonQuery();
            }
            finally
            {
                Command.Connection.Close();
            }

        }

        /// <summary>
        /// Método para executar uma consulta SQL sem retorno
        /// </summary>
        /// <param name="StoreProcedure">A procedure a ser executada no SQL</param>
        protected void ExecutaProcedureNoQuery(string StoreProcedure)
        {
            try
            {
                Command = new OleDbCommand();
                Command.Connection = Open();
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                Command.CommandText = StoreProcedure;
                Command.Parameters.Clear();

                Command.ExecuteNonQuery();
            }
            finally
            {
                Command.Connection.Close();
            }

        }

        /// <summary>
        /// Método para executar uma consulta SQL sem retorno
        /// </summary>
        /// <param name="StoreProcedure">A procedure a ser executada no SQL</param>
        /// <param name="_connectionString">Passando uma connection String</param>
        protected void ExecutaProcedureNoQuery(string StoreProcedure, string _connectionString)
        {
            try
            {
                Command = new OleDbCommand();
                Command.Connection = Open(_connectionString);
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                Command.CommandText = StoreProcedure;
                Command.Parameters.Clear();

                Command.ExecuteNonQuery();
            }
            finally
            {
                Command.Connection.Close();
            }

        }


        /*ExecutaProcedure
         * 4. extensões diferentes do método
         * 1 - Passando apenas a storedProcedure
         * 2 - Passando a storedProcedure e os parametros
         * 3 - Passando a storedProcedure e a connection String
         * 4 - Passando a storedprocedure, parametros e connection string
         */

        /// <summary>
        /// Método para executar uma consulta SQL retornando um Datareader
        /// </summary>
        /// <param name="storeProcedure">A procedure a ser executada no SQL</param>
        /// <returns>System.Data.OleDb.OleDbDataReader</returns>
        protected OleDbDataReader ExecutaProcedure(string storeProcedure)
        {
            Command = new OleDbCommand();
            Command.Connection = Open();
            Command.Parameters.Clear();
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = storeProcedure;
            return Command.ExecuteReader();
        }

        /// <summary>
        /// Método para executar uma consulta SQL retornando um Datareader
        /// </summary>
        /// <param name="storeProcedure">A procedure a ser executada no SQL</param>
        /// <returns>System.Data.OleDb.OleDbDataReader</returns>
        protected OleDbDataReader ExecutaSQLString(string sqlString)
        {
            Command = new OleDbCommand();
            Command.Connection = Open();
            Command.Parameters.Clear();
            Command.CommandType = CommandType.Text;
            Command.CommandText = sqlString;
            return Command.ExecuteReader();
        }

        /// <summary>
        /// Método para executar uma consulta SQL retornando um Datareader
        /// </summary>
        /// <param name="storeProcedure">A procedure a ser executada no SQL</param>
        /// <param name="parameters">os parâmetros da procedure</param>
        /// <returns>System.Data.OleDb.OleDbDataReader</returns>
        protected OleDbDataReader ExecutaProcedure(string storeProcedure, OleDbParameter[] parameters)
        {
            Command = new OleDbCommand();
            Command.Connection = Open();
            Command.Parameters.Clear();
            Command.CommandType = CommandType.StoredProcedure;

            for (int i = 0; i <= parameters.Length - 1; i++)
                Command.Parameters.Add(parameters[i]);

            Command.CommandText = storeProcedure;
            return Command.ExecuteReader();
        }

        /// <summary>
        /// Método para executar uma consulta SQL retornando um Datareader, mas passando uma connection string diferente ou caso você esteja usando o thread
        /// </summary>
        /// <param name="storeProcedure">A procedure a ser executada no SQL</param>
        /// <param name="_connectionString">Passando uma connection String</param>
        /// <returns>System.Data.OleDb.OleDbDataReader</returns>
        protected OleDbDataReader ExecutaProcedure(string storeProcedure, string _connectionString)
        {
            Command = new OleDbCommand();
            Command.Connection = Open(_connectionString);
            Command.Parameters.Clear();
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = storeProcedure;
            return Command.ExecuteReader();
        }


        /// <summary>
        /// Método para executar uma consulta SQL retornando um Datareader, mas passando uma connection string diferente ou caso você esteja usando o thread
        /// </summary>
        /// <param name="storeProcedure">A procedure a ser executada no SQL</param>
        /// <param name="parameters">os parâmetros da procedure</param>
        /// <param name="_connectionString">Passando uma connection String</param>
        /// <returns>System.Data.OleDb.OleDbDataReader</returns>
        protected OleDbDataReader ExecutaProcedure(string storeProcedure, OleDbParameter[] parameters, string _connectionString)
        {
            Command = new OleDbCommand();
            Command.Connection = Open(_connectionString);
            Command.Parameters.Clear();
            Command.CommandType = CommandType.StoredProcedure;

            for (int i = 0; i <= parameters.Length - 1; i++)
                Command.Parameters.Add(parameters[i]);

            Command.CommandText = storeProcedure;
            return Command.ExecuteReader();
        }

        /// <summary>
        /// Método que converte o OleDbDataReader e para DataTable
        /// </summary>
        /// <param name="storeProcedure">A procedure a ser executada no SQL</param>
        /// <returns>System.Data.DataTable</returns>
        public DataTable GetTable(string storeProcedure)
        {
            DataTable tbl = new DataTable();
            tbl.Load(ExecutaProcedure(storeProcedure));
            return tbl;
        }

        /// <summary>
        /// Método que converte o OleDbDataReader e para DataTable
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable GetTable(string storeProcedure, OleDbParameter[] parameters)
        {
            DataTable tbl = new DataTable();
            tbl.Load(ExecutaProcedure(storeProcedure, parameters));
            return tbl;
        }

        /// <summary>
        /// Método que converte o OleDbDataReader e para DataTable
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable GetTableFromSQLString(string sqlString)
        {
            DataTable tbl = new DataTable();
            tbl.Load(ExecutaSQLString(sqlString));
            return tbl;
        }

        /// <summary>
        /// Método que converte o OleDbDataReader e para DataTable
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="parameters"></param>
        /// <param name="_connectionString"></param>
        /// <returns></returns>
        public DataTable GetTable(string storeProcedure, OleDbParameter[] parameters, string _connectionString)
        {
            DataTable tbl = new DataTable();
            tbl.Load(ExecutaProcedure(storeProcedure, parameters, _connectionString));
            return tbl;
        }

        /// <summary>
        /// Método que converte o OleDbDataReader e para DataTable
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="_connectionString"></param>
        /// <returns></returns>
        public DataTable GetTable(string storeProcedure, string _connectionString)
        {
            DataTable tbl = new DataTable();
            tbl.Load(ExecutaProcedure(storeProcedure, _connectionString));
            return tbl;
        }
        /// <summary>
        /// Método que executa uma consulta em text
        /// </summary>
        /// <param name="comando">texto que será executado no SQL</param>
        /// <returns></returns>
        protected int ExecuteCommand(string comando)
        {

            try
            {
                Command = new OleDbCommand();
                Command.Connection = Open();
                Command.CommandType = CommandType.Text;
                Command.CommandText = comando;

                return Command.ExecuteNonQuery();
            }
            finally
            {
                Command.Connection.Close();
            }


        }
        /// <summary>
        /// Método que converte o DataSet e para List DataTables
        /// </summary>
        /// <param name="storeProcedure">A procedure a ser executada no SQL</param>
        /// <param name="parameters"></param>
        /// <returns>List DataTable</returns>
        public List<DataTable> GetTables(string storeProcedure, OleDbParameter[] parameters)
        {
            List<DataTable> ret = new List<DataTable>();
            OleDbDataAdapter da = new OleDbDataAdapter();

            Command = new OleDbCommand();
            Command.Connection = Open();
            Command.Parameters.Clear();

            for (int i = 0; i <= parameters.Length - 1; i++)
                Command.Parameters.Add(parameters[i]);

            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = storeProcedure;

            da.SelectCommand = Command;

            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataTable tbl in ds.Tables)
            {
                ret.Add(tbl);
            }
            return ret;
        }

    }
}
