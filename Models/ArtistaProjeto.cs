using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class ArtistaProjeto
    {
        public string IDARTISTA { get; set; }
        public string ARTISTNAME { get; set; }
        public string PROJECTCODE { get; set; }
        public string PROJECTNAME { get; set; }
        public string IMAGEPATH {
            get
            {
                string path = string.Concat(ConfigurationManager.AppSettings["IMAGEPATH"], this.IDARTISTA, ".png");
                if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                    return path;
                else
                    return string.Concat(ConfigurationManager.AppSettings["IMAGEPATH"], "no-image.png");
            }
        }
    }
}