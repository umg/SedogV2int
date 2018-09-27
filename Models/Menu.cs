using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class Menu
    {
        public string Title;
        public string Icone;
        public List<SubMenu> SubMenus;
    }
    public class SubMenu
    {
        public string Title;
        public string Path;
        public string Descricao;
    }
}