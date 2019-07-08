using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SEDOGv2.Models;
using SEDOGv2.Helpers;
using SEDOGv2.Models.Context;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;

namespace SEDOGv2.Helpers
{
    public static class functions
    {
        public static int ConvertDateToCordis(this DateTime dt)
        {
            string s = dt.ToString("yyMMdd");

            if (dt.Year >= 2000)
                return Convert.ToInt32(string.Concat("1", dt.ToString("yyMMdd")));
            else
                return Convert.ToInt32(dt.ToString("yyMMdd"));
        }

        public static List<TopParceirosGrafico> GeraTopParceirosGrafico(DateTime mesAtual)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            //DateTime mesAtual = DateTime.Now.AddMonths(-1);

            int anomes = (mesAtual.Year * 100) + mesAtual.Month;
            int anomesInicial = (mesAtual.AddMonths(-11).Year * 100) + mesAtual.AddMonths(-11).Month;
            List<int> listAnomes = new List<int>();

            for (int i = -11; i < 1; i++)
            {
                listAnomes.Add((mesAtual.AddMonths(i).Year * 100) + mesAtual.AddMonths(i).Month);
            }

            List<TopParceirosGraficoViewModel> topParceirosGraficoViewModel = provider.SLT_TOPGRAFICO_PARCEIROS_DIGITAL(anomes, anomesInicial);

            List<TopParceirosGrafico> topParceirosGrafico = new List<Models.TopParceirosGrafico>();
            int col = 0;
            foreach (var tpgvm in topParceirosGraficoViewModel.Select(d => d.CUSTOMERNAME).Distinct())
            {
                TopParceirosGrafico t = new TopParceirosGrafico();
                t.Customer = tpgvm;
                t.NetRevenue = new List<int>();
                t.AnoMes = listAnomes;
                t.Color = ColorScheme.Colors[col];
                t.BgColor = ColorScheme.BgColors[col];
                topParceirosGrafico.Add(t);
                col++;
                if (col == 10) col = 0;
            }
            foreach (var tpg in topParceirosGrafico)
            {
                foreach (var AnoMes in tpg.AnoMes)
                {
                    var sel = topParceirosGraficoViewModel.Where(d => d.CUSTOMERNAME == tpg.Customer && d.ACCOUNTINGYEARMONTH == AnoMes);
                    if (sel.Count() > 0)
                    {
                        tpg.NetRevenue.Add(sel.First().NETREVENUE);
                    }
                    else
                    {
                        tpg.NetRevenue.Add(0);
                    }
                }
            }

            return topParceirosGrafico;
        }

        public static List<TopArtistasGrafico> GeraTopArtistasGrafico(DateTime mesAtual)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            //DateTime mesAtual = DateTime.Now.AddMonths(-1);

            int anomes = (mesAtual.Year * 100) + mesAtual.Month;
            int anomesInicial = (mesAtual.AddMonths(-11).Year * 100) + mesAtual.AddMonths(-11).Month;
            List<int> listAnomes = new List<int>();

            for (int i = -11; i < 1; i++)
            {
                listAnomes.Add((mesAtual.AddMonths(i).Year * 100) + mesAtual.AddMonths(i).Month);
            }

            List<TopArtistasGraficoViewModel> topArtistasGraficoViewModel = provider.SLT_TOPGRAFICO_ARTISTAS_DIGITAL(anomes, anomesInicial);

            List<TopArtistasGrafico> topArtistasGrafico = new List<Models.TopArtistasGrafico>();
            int col = 0;
            foreach (var tpgvm in topArtistasGraficoViewModel.Select(d => d.ARTISTNAME).Distinct())
            {
                TopArtistasGrafico t = new TopArtistasGrafico();
                t.Artista = tpgvm;
                t.NetRevenue = new List<int>();
                t.AnoMes = listAnomes;
                t.Color = ColorScheme.Colors[col];
                t.BgColor = ColorScheme.BgColors[col];
                topArtistasGrafico.Add(t);
                col++;
            }
            foreach (var tpg in topArtistasGrafico)
            {
                foreach (var AnoMes in tpg.AnoMes)
                {
                    var sel = topArtistasGraficoViewModel.Where(d => d.ARTISTNAME == tpg.Artista && d.ACCOUNTINGYEARMONTH == AnoMes);
                    if (sel.Count() > 0)
                    {
                        tpg.NetRevenue.Add(sel.First().NETREVENUE);
                    }
                    else
                    {
                        tpg.NetRevenue.Add(0);
                    }
                }
            }

            return topArtistasGrafico;
        }

        public static List<TotalDigitalMensalViewModel> GetTotalMensal(DateTime mesAtual)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            //DateTime mesAtual = DateTime.Now.AddMonths(-1);

            int anomes = (mesAtual.Year * 100) + mesAtual.Month;
            int anomesInicial = (mesAtual.AddMonths(-11).Year * 100) + mesAtual.AddMonths(-11).Month;

            return provider.SLT_TOTAL_PARCEIROS_DIGITAL(anomes, anomesInicial);
        }

        public static List<TotalByTypeDigitalViewModel> GetTotalTypoActual(DateTime mesAtual)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            //DateTime mesAtual = DateTime.Now.AddMonths(-1);
            int anomes = (mesAtual.Year * 100) + mesAtual.Month;
            return provider.SLT_TOTAL_TIPO_ACTUAL(anomes);
        }

        public static List<TotalByTypeDigitalViewModel> GetTotalTypoYTD(DateTime mesAtual)
        {
            PLProjetoProvider provider = new PLProjetoProvider();
            //DateTime mesAtual = DateTime.Now.AddMonths(-1);
            int anomes = (mesAtual.Year * 100) + mesAtual.Month;
            int anomesInicial = (mesAtual.AddMonths(-11).Year * 100) + mesAtual.AddMonths(-11).Month;
            return provider.SLT_TOTAL_TIPO_YTD(anomes, anomesInicial);
        }

        public static string[] GetTotalUT(DateTime mesAtual)
        {
            string[] ret = new string[2];
            PLProjetoProvider provider = new PLProjetoProvider();
            //DateTime mesAtual = DateTime.Now.AddMonths(-1);
            int anomes = (mesAtual.Year * 100) + mesAtual.Month;
            var sel = provider.SLT_TOTAL_USAGETYPE_ACTUAL(anomes);
            ret[0] = string.Join(",", sel.Select(d => string.Format("\"{0}\"", d.TYPE)));
            ret[1] = string.Join(",", sel.Select(d => d.NETREVENUE).ToArray());
            return ret;
        }

        public static string[] GetTotalSaleType(DateTime mesAtual)
        {
            string[] ret = new string[2];
            PLProjetoProvider provider = new PLProjetoProvider();
            //DateTime mesAtual = DateTime.Now.AddMonths(-1);
            int anomes = (mesAtual.Year * 100) + mesAtual.Month;
            var sel = provider.SLT_TOTAL_SALETYPE_ACTUAL(anomes);
            ret[0] = string.Join(",", sel.Select(d => string.Format("\"{0}\"", d.TYPE)));
            ret[1] = string.Join(",", sel.Select(d => d.NETREVENUE).ToArray());
            return ret;
        }

        public static string GetUltimos12Meses(DateTime mesAtual)
        {
            //DateTime mesAtual = DateTime.Now.AddMonths(-1);
            string meses = "";
            for (int i = -11; i < 1; i++)
            {
                meses += "\"" + mesAtual.AddMonths(i).ToString("MMMM") + "\" ,";
            }
            return meses.Substring(0, meses.Length - 1);
        }

        public static bool SendEmail(string[] destinatarios, string[] copias, string[] copiasOcultas, string assunto, string corpoMensagem, System.Net.Mail.Attachment[] arquivos)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            string config = ConfigurationManager.AppSettings["appConfig"].ToString();
            appConfig.appConfig app = new appConfig.appConfig(config);
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(app.listServer.smtpNome);

            mail.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["FROM"].ToString());
            foreach (string to in destinatarios)
                if (!string.IsNullOrEmpty(to))
                    mail.To.Add(to);

            foreach (string cc in copias)
                if (!string.IsNullOrEmpty(cc))
                    mail.CC.Add(cc);

            foreach (string bcc in copiasOcultas)
                if (!string.IsNullOrEmpty(bcc))
                    mail.Bcc.Add(bcc);

            mail.IsBodyHtml = true;

            mail.Subject = assunto;
            mail.Body = corpoMensagem;

            foreach (System.Net.Mail.Attachment att in arquivos)
                mail.Attachments.Add(att);

            client.Send(mail);

            return true;
        }

        public static string cleanString(this string value)
        {
            Regex reg = new Regex("[*'\",_&#^@'/]");
            value = reg.Replace(value, "");

            var normalizedString = value.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static MemoryStream ZipXMLFile(string XML, string zipEntryName)
        {
            MemoryStream memStreamIn = new MemoryStream(Encoding.UTF8.GetBytes(XML));

            MemoryStream outputMemStream = new MemoryStream();
            ZipOutputStream zipStream = new ZipOutputStream(outputMemStream);
            zipStream.SetLevel(9); //0-9, 9 being the highest level of compression

            ZipEntry newEntry = new ZipEntry(zipEntryName);
            newEntry.DateTime = DateTime.Now;

            zipStream.PutNextEntry(newEntry);

            StreamUtils.Copy(memStreamIn, zipStream, new byte[4096]);
            zipStream.CloseEntry();

            zipStream.IsStreamOwner = false;    // False stops the Close also Closing the underlying stream.
            zipStream.Close();          // Must finish the ZipOutputStream before using outputMemStream.

            outputMemStream.Position = 0;
            return outputMemStream;
        }

    }
    public static class ColorScheme
    {
        public static readonly string[] BgColors = { "rgba(0, 72, 70,0.5)", "rgba(23,123,172,0.5)", "rgba(255,165,0,0.5)", "rgba(255,0,0,0.5)", "rgba(120,120,120,0.5)", "rgba(155,217,48,0.5)", "rgba(247,10,10,0.5)", "rgba(90,103,45,0.5)", "rgba(207,48,155,0.5)", "rgba(10,247,10,0.5)", "rgba(145,114,90,0.5)", "rgba(0,255,50,0.5)", "rgba(33,33,33,0.5)", "rgba(75,192,192,0.5)", "rgba(0,0,255,0.5)" };
        public static readonly string[] Colors = { "rgba(0, 72, 70,1)", "rgba(23,123,172,1)", "rgba(255,165,0,1)", "rgba(255,0,0,1)", "rgba(120,120,120,1)", "rgba(155,217,48,1)", "rgba(247,10,10,1)", "rgba(90,103,45,1)", "rgba(207,48,155,1)", "rgba(10,247,10,1)", "rgba(145,114,90,1)", "rgba(0,255,50,1)", "rgba(33,33,33,1)", "rgba(75,192,192,1)", "rgba(0,0,255,1)" };
    }

   

}