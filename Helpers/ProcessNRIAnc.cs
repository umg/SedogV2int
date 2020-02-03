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
    public class ProcessNRIAnc
    {
        public DataTable ProcessaNRIAnc(string filepath)
        {
            string insertHeader = "INSERT INTO MXSEDOG . NRI VALUES";
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
                        if (result.Tables["NRI BY ARTIST"] != null)
                        {
                            Models.Context.Conn db = new Models.Context.Conn();

                            DataTable totalStreamTable = result.Tables["NRI BY ARTIST"];
                            int totalCols = totalStreamTable.Columns.Count;
                            int totalRows = totalStreamTable.Rows.Count;

                            int c = 0;

                            db.ExecuteCommandSQL("DELETE MXSEDOG . NRI");

                            //ret.Linhas = totalRows - 4;

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


                                    if (totalStreamTable.Rows[r][0].ToString().Replace("{}","").Trim().Equals("")) {
                                        c++;
                                        sb.Clear();
                                    } else
                                    {
                                        //sb.Append(" (' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][2])) + "' , ");
                                        //sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][3])) + "' , ");
                                        //sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][4]) ) /1000 + "' , ");
                                        //sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][5]) ) /1000 + "' , ");
                                        //sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][6]) ) /1000 + "' , ");
                                        //sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][7]) ) /1000 + "' , ");
                                        //sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][8]) ) /1000 + "' , ");
                                        //sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][9]) ) /1000 + "' , ");
                                        //sb.Append(" ' " + System.Convert.ToDecimal(totalStreamTable.Rows[r][10]) * 100 + "' ) ") ;

                                        //sb.Append("'" + totalStreamTable.Rows[r][11].ToString().Replace("'", "''") + "' , ");
                                        //sb.Append(totalStreamTable.Rows[r][12].ToString() + " , ");
                                        //sb.Append(totalStreamTable.Rows[r][13].ToString() + ") , ");

                                        sb.Append(" (' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][0])) + "' , ");
                                        sb.Append(" ' " + Decimal.Round(System.Convert.ToDecimal(totalStreamTable.Rows[r][1])) + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][2] +  "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][3] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][4] + "' , ");
                                        //sb.Append(" ' " + totalStreamTable.Rows[r][5] + "' , ");
                                        //sb.Append(" ' " + totalStreamTable.Rows[r][6] + "' , ");
                                        //sb.Append(" ' " + totalStreamTable.Rows[r][7] + "' , ");
                                        //sb.Append(" ' " + totalStreamTable.Rows[r][8] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][9] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][10] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][11] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][12] + "' , ");
                                        
                                        sb.Append(" ' " + totalStreamTable.Rows[r][13] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][14] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][15] + "' , ");
                                        sb.Append(" ' " + ((Convert.ToInt32(totalStreamTable.Rows[r][13])) - (Convert.ToInt32(totalStreamTable.Rows[r][14]))) + "' , ");

                                        sb.Append(" ' " + totalStreamTable.Rows[r][16] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][17] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][18] + "' , ");
                                        sb.Append(" ' " + ((Convert.ToInt32(totalStreamTable.Rows[r][16])) - (Convert.ToInt32(totalStreamTable.Rows[r][17]))) + "' , ");

                                        sb.Append(" ' " + totalStreamTable.Rows[r][19] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][20] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][21] + "' , ");
                                        sb.Append(" ' " + ((Convert.ToInt32(totalStreamTable.Rows[r][19])) - (Convert.ToInt32(totalStreamTable.Rows[r][20]))) + "' , ");

                                        sb.Append(" ' " + totalStreamTable.Rows[r][22] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][23] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][24] + "' , ");
                                        sb.Append(" ' " + ((Convert.ToInt32(totalStreamTable.Rows[r][22])) - (Convert.ToInt32(totalStreamTable.Rows[r][23]))) + "' , ");

                                        sb.Append(" ' " + totalStreamTable.Rows[r][25] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][26] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][27] + "' , ");
                                        sb.Append(" ' " + ((Convert.ToInt32(totalStreamTable.Rows[r][25])) - (Convert.ToInt32(totalStreamTable.Rows[r][26]))) + "' , ");

                                        sb.Append(" ' " + totalStreamTable.Rows[r][28] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][29] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][30] + "' , ");
                                        sb.Append(" ' " + ((Convert.ToInt32(totalStreamTable.Rows[r][28])) - (Convert.ToInt32(totalStreamTable.Rows[r][29]))) + "' , ");

                                        sb.Append(" ' " + totalStreamTable.Rows[r][31] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][32] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][33] + "' , ");

                                        sb.Append(" ' " + ((Convert.ToInt32(totalStreamTable.Rows[r][31])) - (Convert.ToInt32(totalStreamTable.Rows[r][32]))) + "' , ");


                                        sb.Append(" ' " + totalStreamTable.Rows[r][34] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][35] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][36] + "' , ");

                                        sb.Append(" ' " + ((Convert.ToInt32(totalStreamTable.Rows[r][34])) - (Convert.ToInt32(totalStreamTable.Rows[r][35]))) + "' , ");

                                        sb.Append(" ' " + totalStreamTable.Rows[r][37] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][38] + "' , ");
                                        sb.Append(" ' " + totalStreamTable.Rows[r][39] + "' , ");

                                        sb.Append(" ' " + ((Convert.ToInt32(totalStreamTable.Rows[r][37])) - (Convert.ToInt32(totalStreamTable.Rows[r][38]))) + "')");


                                        //ImportAIF ret = new ImportAIF();

                                        //ret.IdProjetoSedog = totalStreamTable.Rows[r][2].ToString();
                                        //ret.R2Projects = totalStreamTable.Rows[r][3].ToString();
                                        //ret.ForeignIncome = totalStreamTable.Rows[r][4].ToString();
                                        //ret.ArtistRoyalties = totalStreamTable.Rows[r][5].ToString();
                                        //ret.ProducerRoyalties = totalStreamTable.Rows[r][6].ToString();
                                        //ret.OtherRoyalty = totalStreamTable.Rows[r][7].ToString();
                                        //ret.AllRoyalty = totalStreamTable.Rows[r][8].ToString();
                                        //ret.ForeignMargin = totalStreamTable.Rows[r][9].ToString();
                                        //ret.PercAIFMargin = totalStreamTable.Rows[r][10].ToString();



                                        db.ExecuteCommandSQL(insertHeader + sb.ToString());
                                        c++;

                                        //listAif.Add(ret);

                                        sb.Clear();
                                        
                                    }
                                   


                                    //sb.Append(totalStreamTable.Rows[r][10].ToString() + "  ");

                                }
                                //if (c > 0)
                                //{
                                //db.ExecuteCommandSQL(insertHeader + sb.ToString(0, sb.Length - 2));
                                // db.ExecuteCommandSQL(insertHeader + sb.ToString());
                                // }

                                string selRetorno = "SELECT NRI.IDPROJ_SEDOG, YEAR, RELEASE_DATE, RELEASE_DATE_YTD, ARTISTNAME, " +
                                    "LICENSE_INCOME, LICENSE_ROYALTIES, LICENSE_MARGIN, LICENSE_PERCENT, " +
                                    "PREMIUM_INCOME, PREMIUM_MARGIN, PREMIUM_PERCENT, PREMIUM_DIFF, " + 
                                    "SPONSORSHIP_INCOME, SPONSORSHIP_MARGIN, SPONSORSHIP_PERCENT, SPONSORSHIP_DIFF ," + 
                                    "OTHERADV_INCOME,OTHERADV_MARGIN , OTHERADV_PERCENT,OTHERADV_DIFF ," + 
                                    "MANAGMNT_COMISSION_INCOME,MANAGMNT_COMISSION_MARGIN,MANAGMNT_COMISSION_PERCENT,MANAGMNT_COMISSION_DIFF," + 
                                    "LIVEEVENT_INCOME,LIVEEVENT_MARGIN,LIVEEVENT_PERCENT,LIVEEVENT_DIFF, " + 
                                    "LIVEAGENCY_INCOME,LIVEAGENCY_MARGIN,LIVEAGENCY_PERCENT,LIVEAGENCY_DIFF, " + 
                                    "PASSIVETOURING_INCOME,PASSIVETOURING_MARGIN,PASSIVETOURING_PERCENT,PASSIVETOURING_DIFF," +
                                    "PASSIVEPUBLISHING_INCOME,PASSIVEPUBLISHING_MARGIN,PASSIVEPUBLISHING_PERCENT,PASSIVEPUBLISHING_DIFF," +
                                    "ALLNRI_INCOME,ALLNRI_MARGIN,ALLNRI_PERCENT,ALLNRI_DIFF " +
                                    "FROM MXSEDOG . NRI NRI INNER JOIN MXSEDOG . PL_PROJETO_SEDOG PRJ ON NRI.IDPROJ_SEDOG = PRJ.IDPROJ_SEDOG";
                                
                                dt = db.GetTableFromSQLString(selRetorno);

                            }
                        }
                    }
                }
            }
            return dt;
        }
    }
}