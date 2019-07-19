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
    public class ProcessAIF
    {
        public ImportAIF ProcessAif(string filepath)
        {
            string insertHeader = "INSERT INTO MXDIGITAL . AIF_INCOMING VALUES";
            //string deleteHeader = "DELETE FROM BRDIGITAL . STREAMCHART";

            ImportAIF ret = new ImportAIF();

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
                        if (result.Tables["AIF Summary"] != null)
                        {
                            Models.Context.Conn db = new Models.Context.Conn();

                            DataTable totalStreamTable = result.Tables["AIF Summary"];
                            int totalCols = totalStreamTable.Columns.Count;
                            int totalRows = totalStreamTable.Rows.Count;

                            int c = 0;

                            ret.Linhas = totalRows - 4;

                            StringBuilder sb = new StringBuilder();
                            {
                                for (int r = 4; r < totalRows; r++)
                                {
                                    //if (c >= 500)
                                    //{
                                    //    db.ExecuteCommandSQL(insertHeader + sb.ToString(0, sb.Length - 2));
                                    //    c = 0;
                                    //    sb = new StringBuilder();
                                    //}
                                    //sb.Append(" (' " + totalStreamTable.Rows[r][1].ToString() + "' , ");

                                    //Funcionando tipo String
                                    //sb.Append(" (' " + totalStreamTable.Rows[r][2].ToString() + "' , ");
                                    //sb.Append(" ' " + totalStreamTable.Rows[r][3].ToString() + "' , ");
                                    //sb.Append(" ' " + totalStreamTable.Rows[r][4].ToString() + "' , ");
                                    //sb.Append(" ' " + totalStreamTable.Rows[r][5].ToString() + "' , ");
                                    //sb.Append(" ' " + totalStreamTable.Rows[r][6].ToString() + "' , ");
                                    //sb.Append(" ' " + totalStreamTable.Rows[r][7].ToString() + "' , ");
                                    //sb.Append(" ' " + totalStreamTable.Rows[r][8].ToString() + "' , ");
                                    //sb.Append(" ' " + totalStreamTable.Rows[r][9].ToString() + "' , ");
                                    //sb.Append(" ' " + totalStreamTable.Rows[r][10].ToString() + "' ) ");
                                    //Funcionando tipo String


                                    if (totalStreamTable.Rows[r][2].ToString().Replace("{}","").Trim().Equals("")) {
                                        c++;
                                        sb.Clear();
                                    } else
                                    {
                                        sb.Append(" (' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][2])) + "' , ");
                                        sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][3])) + "' , ");
                                        sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][4]) ) /1000 + "' , ");
                                        sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][5]) ) /1000 + "' , ");
                                        sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][6]) ) /1000 + "' , ");
                                        sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][7]) ) /1000 + "' , ");
                                        sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][8]) ) /1000 + "' , ");
                                        sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][9]) ) /1000 + "' , ");
                                        sb.Append(" ' " + System.Convert.ToDecimal(totalStreamTable.Rows[r][10]) * 100 + "' ) ") ;
                                        //sb.Append("'" + totalStreamTable.Rows[r][11].ToString().Replace("'", "''") + "' , ");
                                        //sb.Append(totalStreamTable.Rows[r][12].ToString() + " , ");
                                        //sb.Append(totalStreamTable.Rows[r][13].ToString() + ") , ");

                                        db.ExecuteCommandSQL(insertHeader + sb.ToString());
                                        c++;
                                        sb.Clear();
                                    }                                                                                                     //sb.Append(totalStreamTable.Rows[r][10].ToString() + "  ");
                                
                                }
                                //if (c > 0)
                                //{
                                    //db.ExecuteCommandSQL(insertHeader + sb.ToString(0, sb.Length - 2));
                                   // db.ExecuteCommandSQL(insertHeader + sb.ToString());
                               // }
                            }
                        }
                    }
                }
            }
            return ret;
        }
    }
}