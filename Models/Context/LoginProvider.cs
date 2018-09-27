using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using SEDOGv2.Helpers;

namespace SEDOGv2.Models.Context
{
    public class LoginProvider : Conn
    {

        /// <summary>
        /// Método de login, o método usa a procedure PIWEB.SLT_‎Usuario para consultar o usuário no banco de dados.
        /// </summary>
        /// <param name="login">login do usuário</param>
        /// <param name="pass">pass do usuário</param>
        /// <returns>Objeto Resposta preenchido com o usuário e as páginas que ele pode acessar</returns>

        public Resposta<Usuario> Login(string login, string pass, string servidor = "", string ambiente = "", string culture="")
        {
            Resposta<Usuario> resp = new Resposta<Usuario>();
            OleDbDataReader dr = null;
            try
            {

                /*Prineiro login tem que passar usuário e senha*/
                string aux = ConfigurationManager.AppSettings["ModelConnection"];
                aux = aux.Replace("[password]", pass);
                aux = aux.Replace("[user]", login);

                if (string.IsNullOrEmpty(ambiente))
                    aux = aux.Replace("[library]", Ambiente);
                else
                    aux = aux.Replace("[library]", ambiente);

                if (string.IsNullOrEmpty(servidor))
                    aux = aux.Replace("[servidor]", Servidor);
                else
                    aux = aux.Replace("[servidor]", servidor);

                List<OleDbParameter> parameters = new List<OleDbParameter>();
                parameters.Add(AddParameter("P_LOGIN", login.ToUpper()));

                if (string.IsNullOrEmpty(ambiente))
                {
                    dr = ExecutaProcedure(appSettings.Ambiente + ".SLT_USUARIO", parameters.ToArray(), aux);
                }
                else
                {
                    dr = ExecutaProcedure(ambiente + ".SLT_USUARIO", parameters.ToArray(), aux);
                }

                if (dr.HasRows)
                {
                    resp.Dados = new Usuario();
                    resp.Dados.Culture = culture;
                    resp.Dados.Pages = new List<Pages>();
                    resp.Dados.Pass = pass;
                    while (dr.Read())
                    {
                        resp.Dados.Login = dr["LOGIN"].ToString();
                        resp.Dados.Nome = dr["NOME"].ToString();
                        resp.Dados.Email = dr["EMAIL"].ToString();
                        resp.Dados.Acessos = dr["ACESSOS"].ToString();

                        resp.Dados.Departamento = dr["DEPTO"].ToString();

                        Pages p = new Pages();
                        p.ID = Convert.ToInt32(dr["ID"]);
                        p.Page = dr["PAGE"].ToString();
                        p.IDSubTitle = Convert.ToInt32(dr["IDSUBTITLE"]);
                        p.SubTitle = dr["SUBTITLE"].ToString();
                        p.IDTitle = Convert.ToInt32(dr["IDTITLE"]);
                        p.Title = dr["TITLE"].ToString();
                        p.Path = dr["PATH"].ToString();
                        p.Descricao = dr["DESCRICAO"].ToString();
                        p.Icone = dr["ICONE"].ToString();
                        resp.Dados.Pages.Add(p);
                    }

                    resp.Dados.Menus = new List<Menu>();

                    foreach (var idTitle in resp.Dados.Pages.Select(s=>s.IDTitle).Distinct())
                    {
                        var pages = resp.Dados.Pages.Where(d => d.IDTitle == idTitle);
                        Menu m = new Menu();
                        m.Icone = pages.First().Icone;
                        m.Title = pages.First().Title;
                        m.SubMenus = new List<SubMenu>();
                        foreach(var p in pages)
                        {
                            SubMenu smenu = new SubMenu();
                            smenu.Title = p.Page;
                            smenu.Descricao = p.Descricao;
                            smenu.Path = "";
                            if (p.Path != "")
                            {
#if DEBUG
                                smenu.Path = "/" + p.Path;
#else
                                smenu.Path = "/SEDOGv2INT/" + p.Path;
#endif
                            }
                            m.SubMenus.Add(smenu);
                        }
                        resp.Dados.Menus.Add(m);
                    }

                    parameters = new List<OleDbParameter>();
                    parameters.Add(AddParameter("P_LOGIN", login.ToUpper()));
                    resp.Dados.IDsObjetosDashboard = new List<string>();
                    foreach (System.Data.DataRow row in GetTable(appSettings.Ambiente + ".SLT_USUARIO_OBJETOS", parameters.ToArray(), aux).Rows)
                    {
                        resp.Dados.IDsObjetosDashboard.Add(row["OBJETO"].ToString());
                    }
                }
                else
                {
                    resp.Message = "UserNotFound";
                    resp.Error = "UserNotFound";
                }
            }
            catch (System.Data.OleDb.OleDbException eo)
            {
                //CWBCO1048 - TimeOut
                resp.Error = eo.Message;
            }
            catch (Exception ex)
            {
                resp.Message = "DefaultError";
                resp.Error = ex.Message;
            }
            finally
            {
                if (dr != null)
                    if (!dr.IsClosed)
                        dr.Close();
            }


            return resp;
        }
    }
}
