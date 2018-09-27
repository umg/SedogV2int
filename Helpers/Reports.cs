using System;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace SEDOGv2.Helpers
{
    

    public class Reports
    {
        public void Dispose()
        {
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }


        public byte[] ExportToPDF(string reportModelPath, string reportSavePath, string reportPrintName, System.Collections.IEnumerable dt, List<ReportParamValues> paramList, List<DataTable> dtSubReportList)
        {
            ReportDocument oRpt = new ReportDocument();

            try
            {
                string sFileAdress = string.Concat(reportSavePath, reportPrintName, ".pdf");
                DiskFileDestinationOptions crDiskFileDestinationOptions = new DiskFileDestinationOptions();
                ExportOptions crExportOptions = new ExportOptions();


                crDiskFileDestinationOptions.DiskFileName = sFileAdress;

                oRpt.Load(reportModelPath);


                crExportOptions = oRpt.ExportOptions;
                crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
                crExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                crExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;

                oRpt.SetDataSource(dt);

                int dtSubCounter = 0;
                if (dtSubReportList != null)
                {

                    foreach (DataTable dsub in dtSubReportList)
                    {
                        oRpt.Subreports[dtSubCounter].SetDataSource(dsub);
                        dtSubCounter++;
                    }
                }


                //Load paramaters  
                if (paramList != null)
                {
                    foreach (ReportParamValues value in paramList)
                    {
                        ParameterDiscreteValue paramMonPaid = new ParameterDiscreteValue();
                        ParameterValues paramValues = new ParameterValues();
                        paramMonPaid.Value = value.paraValor;
                        paramValues.Add(paramMonPaid);
                        oRpt.SetParameterValue(value.paraName, paramValues);
                    }
                }

                oRpt.Export(crExportOptions);

                return StreamFile(sFileAdress);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oRpt.Close();
                oRpt.Dispose();

                GC.SuppressFinalize(oRpt);
            }
        }

        public byte[] ExportToPDF(string reportModelPath, string reportSavePath, string reportPrintName, System.Collections.IEnumerable dt, List<ReportParamValues> paramList, List<System.Collections.IEnumerable> dtSubReportList)
        {
            ReportDocument oRpt = new ReportDocument();

            try
            {
                string sFileAdress = string.Concat(reportSavePath, reportPrintName, ".pdf");
                DiskFileDestinationOptions crDiskFileDestinationOptions = new DiskFileDestinationOptions();
                ExportOptions crExportOptions = new ExportOptions();


                crDiskFileDestinationOptions.DiskFileName = sFileAdress;

                oRpt.Load(reportModelPath);


                crExportOptions = oRpt.ExportOptions;
                crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
                crExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                crExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;

                oRpt.SetDataSource(dt);

                int dtSubCounter = 0;
                if (dtSubReportList != null)
                {

                    foreach (System.Collections.IEnumerable dsub in dtSubReportList)
                    {
                        oRpt.Subreports[dtSubCounter].SetDataSource(dsub);
                        dtSubCounter++;
                    }
                }


                //Load paramaters  
                if (paramList != null)
                {
                    foreach (ReportParamValues value in paramList)
                    {
                        ParameterDiscreteValue paramMonPaid = new ParameterDiscreteValue();
                        ParameterValues paramValues = new ParameterValues();
                        paramMonPaid.Value = value.paraValor;
                        paramValues.Add(paramMonPaid);
                        oRpt.SetParameterValue(value.paraName, paramValues);
                    }
                }

                oRpt.Export(crExportOptions);

                return StreamFile(sFileAdress);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oRpt.Close();
                oRpt.Dispose();

                GC.SuppressFinalize(oRpt);
            }
        }

        public byte[] ExportToEXCEL(string reportModelPath, string reportSavePath, string reportPrintName, System.Collections.IEnumerable dt, List<ReportParamValues> paramList, List<DataTable> dtSubReportList)
        {
            ReportDocument oRpt = new ReportDocument();

            try
            {
                string sFileAdress = string.Concat(reportSavePath, reportPrintName, ".xls");
                DiskFileDestinationOptions crDiskFileDestinationOptions = new DiskFileDestinationOptions();
                ExportOptions crExportOptions = new ExportOptions();


                crDiskFileDestinationOptions.DiskFileName = sFileAdress;

                oRpt.Load(reportModelPath);


                crExportOptions = oRpt.ExportOptions;
                crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
                crExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                crExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.Excel;

                oRpt.SetDataSource(dt);

                int dtSubCounter = 0;
                if (dtSubReportList != null)
                {

                    foreach (DataTable dsub in dtSubReportList)
                    {
                        oRpt.Subreports[dtSubCounter].SetDataSource(dsub);
                        dtSubCounter++;
                    }
                }


                //Load paramaters  
                if (paramList != null)
                {
                    foreach (ReportParamValues value in paramList)
                    {
                        ParameterDiscreteValue paramMonPaid = new ParameterDiscreteValue();
                        ParameterValues paramValues = new ParameterValues();
                        paramMonPaid.Value = value.paraValor;
                        paramValues.Add(paramMonPaid);
                        oRpt.SetParameterValue(value.paraName, paramValues);
                    }
                }

                oRpt.Export(crExportOptions);

                return StreamFile(sFileAdress);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oRpt.Close();
                oRpt.Dispose();

                GC.SuppressFinalize(oRpt);
            }
        }


        private byte[] StreamFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            // Create a byte array of file stream length
            byte[] ImageData = new byte[fs.Length];

            //Read block of bytes from stream into the byte array
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));

            //Close the File Stream
            fs.Close();
            return ImageData; //return the byte data
        }

    }

    public class ReportParamValues
    {
        public string paraName;
        public string paraValor;
    }
}