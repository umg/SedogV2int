using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using SEDOGv2.Models;

namespace SEDOGv2.Helpers
{



    public static class appSettings
    {


        public static Usuario _User
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["SEDOGv2.USUARIOS"] != null)
                        return (Usuario)HttpContext.Current.Session["SEDOGv2.USUARIOS"];
                    else
                    {
                        return new Usuario { };
                    }
                }else
                {
                    return new Usuario { };
                }

            }
            set
            {
                HttpContext.Current.Session["SEDOGv2.USUARIOS"] = value;
            }
        }

        public static string KeepMeAlive
        {
            get
            {
#if DEBUG
                return "/KeepMeAlive";
#else
                return "/SEDOGv2INT/KeepMeAlive";
#endif
            }
        }


        public static string ConnectionString
        {
            get
            {
                string aux = ConfigurationManager.AppSettings["ModelConnection"];
                if (_User != null)
                {
                    aux = aux.Replace("[password]", _User.Pass);
                    aux = aux.Replace("[user]", _User.Login);
                    aux = aux.Replace("[library]", Ambiente);
                    aux = aux.Replace("[servidor]", Servidor);
                }
                return aux;
            }
        }


        public static string Ambiente
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

        public static Pages currentPage()
        {
            string uri = HttpContext.Current.Request.Url.AbsoluteUri;
            return _User.Pages.Where(d => uri.Substring(uri.LastIndexOf('/') + 1) == d.Path).First();
        }

        public static string pageName()
        {
            string name = "Home";
            string uri = HttpContext.Current.Request.Url.AbsoluteUri;
            Pages curr = _User.Pages.Where(d => uri.Substring(uri.LastIndexOf('/') + 1) == d.Path && d.Path != "").FirstOrDefault();
            if (curr != null)
                name = curr.Page;
            return name;
        }


        public static string HomeMenu
        {
            get
            {

                if (HttpContext.Current.Session[string.Concat("REPORTES.HOME.MENU.", _User.Login)] != null)
                    return HttpContext.Current.Session[string.Concat("REPORTES.HOME.MENU.", _User.Login)].ToString();
                else
                    return "";
            }
            set
            {

                HttpContext.Current.Session[string.Concat("REPORTES.HOME.MENU.", _User.Login)] = value;
            }
        }

        public static string HeaderMenu
        {
            get
            {

                if (HttpContext.Current.Session[string.Concat("REPORTES.HEADER.MENU.", _User.Login)] != null)
                    return HttpContext.Current.Session[string.Concat("REPORTES.HEADER.MENU.", _User.Login)].ToString();
                else
                    return "";
            }
            set
            {

                HttpContext.Current.Session[string.Concat("REPORTES.HEADER.MENU.", _User.Login)] = value;
            }
        }


        public static List<ResponsavelDepartamento> _ResponsavelDepartamento
        {
            get
            {
                if (HttpContext.Current.Session["SEDOGv2.RESPONSAVELDEPARTAMENTO"] != null)
                    return (List<ResponsavelDepartamento>)HttpContext.Current.Session["SEDOGv2.RESPONSAVELDEPARTAMENTO"];
                else
                {
                    return new List<ResponsavelDepartamento>();
                }

            }
            set
            {
                HttpContext.Current.Session["SEDOGv2.RESPONSAVELDEPARTAMENTO"] = value;
            }
        }


        public static string TopMenu()
        {
            if (string.IsNullOrEmpty(HeaderMenu))
            {
                string path = "Home";//ResolveUrl("Home.aspx");
                //string table = string.Concat("<ul class='nav navbar-nav'><li><a href='", path, "'>Home</a></li>");
                string table = "";
                int t = 0;
                int st = 0;

                for (int i = 0; i <= _User.Pages.Count - 1; i++)
                {

                    if (_User.Pages[i].IDTitle != t)
                    {
                        if (t != 0)
                            table = string.Concat(table, "</ul></li>");

                        t = _User.Pages[i].IDTitle;

                        table = string.Concat(table, "<li><a href=\"", path, "\"><span class=\"fa-stack fa-lg pull-left\"><i class=\"fa ", _User.Pages[i].Icone, " fa-stack-1x \"></i></span> ", _User.Pages[i].Page, "</a><ul class=\"nav-pills nav-stacked\" style=\"list-style-type:none;\">"); //string.Concat(table, "<li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false'>", _User.Pages[i].Title, " <span class='caret'></span></a><ul class='dropdown-menu'>");

                    }

                    path =   _User.Pages[i].Path;//ResolveUrl(p._User.Pages[i].Path);

                    table = string.Concat(table, "<li><a href=\"", path, "\"><span class=\"fa-stack fa-lg pull-left\"><i class=\"fa ", _User.Pages[i].Icone, " fa-stack-1x \"></i></span> ", _User.Pages[i].Page, "</a></li>");
                }
                
                HeaderMenu = table;
            }

            return HeaderMenu;
        }

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
                        catch
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

    }
}