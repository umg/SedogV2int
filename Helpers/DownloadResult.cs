using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEDOGv2.Helpers
{
    public static class DownloadResult
    {
       // public static GridView excelGridView { get; set; }
       // public static string nomeArquivo { get; set; }

          public static string _virtualPath = VirtualPathUtility.ToAbsolute("~/Content/Export/");

        public static byte[] Download(GridView gv, string nomearquivo)
        {

            string[] exportInfo = nomearquivo.Split('.');

            
            DataTable dt = ConvertGridViewToDataTable(gv);
            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dt, "WorksheetName");
            wb.SaveAs(string.Concat(_virtualPath, nomearquivo));


            FileStream fs = new FileStream(nomearquivo, FileMode.Open, FileAccess.Read);

            // Create a byte array of file stream length
            byte[] ImageData = new byte[fs.Length];

            //Read block of bytes from stream into the byte array
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));

            //Close the File Stream
            fs.Close();
            return ImageData; //return the byte data

            /*if (exportInfo[1].Contains("xls") )
            {
                
            }
            else if(exportInfo[1].Contains("pdf"))
            {

            }*/

        }

        public static DataTable ConvertGridViewToDataTable(GridView gv)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                dt.Columns.Add("column" + i.ToString());
            }
            foreach (GridViewRow row in gv.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < gv.Columns.Count; j++)
                {
                    dr["column" + j.ToString()] = row.Cells[j].Text;
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }



        /*public override void ExecuteResult(ControllerContext context)
        {
            //Criar um fluxo para gravar o arquivo do Excel
            HttpContext curContext = HttpContext.Current;
            curContext.Response.Clear();
            curContext.Response.AddHeader("content - disposition","attachment; filename = " +nomeArquivo);
            curContext.Response.Charset = "";
            curContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            curContext.Response.ContentType = "application / vnd.ms - excel";

            //Renderiza o GridView
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            excelGridView.RenderControl(htw);

            byte[] byteArray = Encoding.ASCII.GetBytes(sw.ToString());
            MemoryStream s = new MemoryStream(byteArray);
            StreamReader sr = new StreamReader(s, Encoding.ASCII);

            curContext.Response.Write(sr.ReadToEnd());
            curContext.Response.End();
        }*/
    }
}