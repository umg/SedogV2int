using AtualizaDadosCopa.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;

namespace AtualizaDadosCopa.Helpers
{

    public static class appSettings
    {
        public static string ConnectionString
        {
            get
            {
                string aux = ConfigurationManager.AppSettings["ModelConnection"];

                aux = aux.Replace("[password]", "CARVALHORA");
                aux = aux.Replace("[user]", "CARVALHORA");
                aux = aux.Replace("[library]", "MXSAP");
                string[] _app = ConfigurationManager.AppSettings["dados"].Split(';');
                aux = aux.Replace("[servidor]", _app[0]) ;

                return aux;
            }
        }

        //public static string Ambiente
        //{
        //    get
        //    {
        //        string[] _app = ConfigurationManager.AppSettings["dados"].Split(';');
        //        return _app[1];
        //    }
        //}

        //public static string Servidor
        //{
        //    get
        //    {

        //        string[] _app = ConfigurationManager.AppSettings["dados"].Split(';');
        //        return _app[0];

        //    }
        //}



        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static T DataTableToObject<T>(this DataTable table) where T : class, new()
        {
            try
            {
                T obj = new T();

                foreach (var row in table.AsEnumerable())
                {
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }

                return obj;
            }
            catch
            {
                return null;
            }
        }

        public static string[] DestinatarioEmailTabDireitos
        {
            get
            {
                return ConfigurationManager.AppSettings["DestinatarioEmailTabDireitos"].Split(';');
            }
        }

        public static string[] CopiaEmailTabDireitos
        {
            get
            {
                return ConfigurationManager.AppSettings["CopiaEmailTabDireitos"].Split(';');
            }
        }

        public static string[] DestinatarioEmailTabMensal
        {
            get
            {
                return ConfigurationManager.AppSettings["DestinatarioEmailTabMensal"].Split(';');
            }
        }

        public static string[] CopiaEmailTabMensal
        {
            get
            {
                return ConfigurationManager.AppSettings["CopiaEmailTabMensal"].Split(';');
            }
        }

        public static string[] DestinatarioEmailTabGeral
        {
            get
            {
                return ConfigurationManager.AppSettings["DestinatarioEmailTabGeral"].Split(';');
            }
        }

        public static string[] CopiaEmailTabGeral
        {
            get
            {
                return ConfigurationManager.AppSettings["CopiaEmailTabGeral"].Split(';');
            }
        }

        public static string[] DestinatarioEmailTopProdutoCliente
        {
            get
            {
                return ConfigurationManager.AppSettings["DestinatarioEmailTopProdutoCliente"].Split(';');
            }
        }

        public static string[] CopiaEmailTopProdutoCliente
        {
            get
            {
                return ConfigurationManager.AppSettings["CopiaEmailTopProdutoCliente"].Split(';');
            }
        }
    }
}
