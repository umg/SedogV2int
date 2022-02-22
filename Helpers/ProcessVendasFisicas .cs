using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Text;
using System.IO;
using ExcelDataReader;
using System.Data;
using SEDOGv2.Models;

namespace SEDOGv2.Helpers
{
    public class ProcessVendasFisicas
    {
        public DataTable ProcessVendasFisica(string filepath)
        {
            string insertHeader = "";
            if (appSettings.Ambiente == "ESSEDOG") {
                insertHeader = "INSERT INTO ESSEDOG.VENDAS_FISICAS(YEAR, MONTH, SKU, BARCODE, ARTISTNAME, PRODUCTNAME, MIDIA, RELEASEDATE, TYPESALES, NETSALESVALUE, NETSALESQUANTITY, TAXSALESVALUE, RETURNSALESVALUE, RETURNSALESQUANTITY, TAXSALESQUANTITY) VALUES ";
            } else
            {
                insertHeader = "INSERT INTO PTSEDOG.VENDAS_FISICAS(YEAR, MONTH, SKU, BARCODE, ARTISTNAME, PRODUCTNAME, MIDIA, RELEASEDATE, TYPESALES, NETSALESVALUE, NETSALESQUANTITY, TAXSALESVALUE, RETURNSALESVALUE, RETURNSALESQUANTITY, TAXSALESQUANTITY) VALUES ";
            }
            //string insertHeader = "INSERT INTO ESSEDOG . VENDAS_FISICAS VALUES ";
            
                //string deleteHeader = "DELETE FROM BRDIGITAL . STREAMCHART";

            // List<ImportAIF> listAif = new List<ImportAIF>();
            DataTable dt = new DataTable();


            using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {

                        }
                    } while (reader.NextResult());

                    var result = reader.AsDataSet();
                    if (result.Tables.Count > 0)
                    {
                        if (result.Tables["Export Worksheet"] != null)
                        {
                            Models.Context.Conn db = new Models.Context.Conn();

                            DataTable totalStreamTable = result.Tables["Export Worksheet"];
                            int totalCols = totalStreamTable.Columns.Count;
                            int totalRows = totalStreamTable.Rows.Count;

                            int c = 0;

                            //db.ExecuteCommandSQL("DELETE MXSEDOG . AIF_INCOMING");

                            //ret.Linhas = totalRows - 4;
                            StreamWriter sw = File.AppendText(HttpContext.Current.Server.MapPath("~/temp/log_vendasfisicasINT.txt"));

                            StringBuilder sb = new StringBuilder();
                            {
                                for (int r = 1; r < totalRows; r++)
                                {
                                   


                                    if (totalStreamTable.Rows[r][2].ToString().Replace("{}","").Trim().Equals("")) {
                                        c++;
                                        sb.Clear();
                                    } else
                                    {
                                       

                                        sb.Append(" ('" + totalStreamTable.Rows[r][0] + "' , ");
                                        sb.Append(" '" + totalStreamTable.Rows[r][1] + "' , ");
                                        sb.Append(" '" + totalStreamTable.Rows[r][2] + "' , ");
                                        sb.Append(" '" + totalStreamTable.Rows[r][3] +  "' , ");
                                        sb.Append(" '" + totalStreamTable.Rows[r][4] + "' , ");
                                        sb.Append(" '" + totalStreamTable.Rows[r][5] + "' , ");
                                        sb.Append(" '" + totalStreamTable.Rows[r][6] + "' , ");
                                        sb.Append(" '" + totalStreamTable.Rows[r][7] + "' , ") ;
                                        sb.Append(" '" + totalStreamTable.Rows[r][8] + "' , ");
                                        sb.Append(" '" + totalStreamTable.Rows[r][9] + "' , ");
                                        sb.Append(" '" + totalStreamTable.Rows[r][10] + "' , ");
                                        sb.Append(" '" + totalStreamTable.Rows[r][11] + "' , ");
                                        sb.Append(" '" + totalStreamTable.Rows[r][12] + "' , ");
                                        sb.Append(" '" + totalStreamTable.Rows[r][13] + "' , ");
                                        sb.Append(" '" + totalStreamTable.Rows[r][14] + "' ) ");




                                        db.ExecuteCommandSQL(insertHeader + sb.ToString());
                                        c++;


                                        sb.Clear();
                                        
                                    }
                                   



                                }
                          

                                //string selRetorno = "SELECT AIF.IDPROJ_SEDOG, PRJ.PROJETO, R2_PROJECT, YEAR,FOREIGN_INCOME, ARTIST_ROYALTIES, PRODUCER_ROYALTY, OTHER_ROYALTY, ALL_ROYALTIES, FOREIGN_MARGIN, PERC_AIF_MARGIN FROM MXSEDOG . AIF_INCOMING AIF INNER JOIN MXSEDOG . PL_PROJETO_SEDOG PRJ ON AIF.IDPROJ_SEDOG = PRJ.IDPROJ_SEDOG";


                                //dt = db.GetTableFromSQLString(selRetorno);

                            }
                        }
                    }
                }
            }
            return dt;
        }
    }
}