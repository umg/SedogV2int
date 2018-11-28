using System;
using System.Xml;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using SEDOGv2.Models;
using SEDOGv2.Models.Context;
using System.Text.RegularExpressions;
using SEDOGv2.Helpers;

namespace SEDOGv2.Helpers
{
    public class DashBoard_Helper: Conn
    {
        private string sHtml;
        private string sScriptArea;
        private string sNav;
        private string[] colorsArray = { "rgba(255,165,0,1)", "rgba(0, 72, 70,1)", "rgba(23,123,172,1)", "rgba(90,90,90,1)", "rgba(255,0,0,1)", "rgba(120,120,120,1)", "rgba(30,120,39,1)", "rgba(20,230,220,1)", "rgba(53,64,12,1)", "rgba(65,56,200,1)", "rgba(45,85,95,1)", "rgba(36,87,44,1)", "rgba(250,44,78,1)", "rgba(66,99,77,1)" };
        //private string[] meses = { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" };
        public string path { get; set; }
        public string SHtml { get { return sHtml; } }
        public string SScriptArea { get { return sScriptArea; } }
        public string SNav { get { return sNav; } }
        public void LoadMyDashBoard(Usuario user)
        {
            StringBuilder sbRet = new StringBuilder();
            StringBuilder sbRet_scriptArea = new StringBuilder();
            StringBuilder sbRet_nav = new StringBuilder();
            string tempS = "";
            string pattern = "col-..-([0-9])+";
            Regex rgx = new Regex(pattern);
            XmlDocument xDoc = new XmlDocument();

            //if(File.Exists(path + @"\Content\DashBoards\" + user.Login + ".xml"))
            //{
            //    xDoc.Load(path + @"\Content\DashBoards\" + user.Login + ".xml");
            //}
            //else
            {
                xDoc.Load(path + @"\Content\DashBoards\" + appSettings.Ambiente + "DashBoard.xml");
            }


            sbRet_nav.Append("<ul class=\"nav nav-tabs dashNavPils\">");
            foreach (XmlNode dashboard in xDoc["dashboards"].ChildNodes)
            {

                string dashboardName = dashboard.Attributes["name"].Value;
                string dashboardId = dashboard.Attributes["id"].Value;

                if (!user.IDsObjetosDashboard.Exists(e => e.Contains(dashboardId)))
                    continue;

                sbRet_nav.Append("<li><a href=\"#" + dashboard.Attributes["id"].Value + "\" data-toggle=\"tab\">" + dashboard.Attributes["name"].Value + "</a></li>");
                sbRet.Append("<div id=\"" + dashboard.Attributes["id"].Value + "\" class=\"dashboardDiv\">");

                foreach (XmlNode obj in dashboard.ChildNodes)
                {

                    bool loadObject = false;
                    
                    foreach (XmlNode detalhe in obj.ChildNodes)
                    {
                        if (detalhe.Name == "twoColumn")
                        {
                            loadObject = true;
                            continue;
                        }
                        if (detalhe.Attributes != null)
                        {
                            if (detalhe.Attributes["id"] != null) {
                                if (user.IDsObjetosDashboard.Exists(e => e.Contains(detalhe.Attributes["id"].Value)))
                                {
                                    loadObject = true;
                                }
                            }
                        }
                    }

                    if (!loadObject) continue;

                    if(obj.Attributes["type"].Value== "scoreboard")
                    {
                        int totalDeObjetosReais = 0;
                        foreach (XmlNode detalhe in obj.ChildNodes)
                        {
                            if (user.IDsObjetosDashboard.Exists(e => e.Contains(detalhe.Attributes["id"].Value)))
                            {
                                totalDeObjetosReais++;
                            }
                        }
                        if (obj.ChildNodes.Count != totalDeObjetosReais)
                        {
                            switch(totalDeObjetosReais)
                            {
                                case 1:
                                    obj.Attributes["class"].Value = rgx.Replace(obj.Attributes["class"].Value, "").Insert(0, "col-lg-2 col-md-2 col-xs-2");
                                    break;
                                case 2:
                                    obj.Attributes["class"].Value = rgx.Replace(obj.Attributes["class"].Value, "").Insert(0, "col-lg-3 col-md-3 col-xs-3");
                                    break;
                                case 3:
                                    obj.Attributes["class"].Value = rgx.Replace(obj.Attributes["class"].Value, "").Insert(0, "col-lg-4 col-md-4 col-xs-4");
                                    break;
                                case 4:
                                    obj.Attributes["class"].Value = rgx.Replace(obj.Attributes["class"].Value, "").Insert(0, "col-lg-6 col-md-6 col-xs-6");
                                    break;
                                case 5:
                                    obj.Attributes["class"].Value = rgx.Replace(obj.Attributes["class"].Value, "").Insert(0, "col-lg-7 col-md-7 col-xs-7");
                                    break;
                                case 6:
                                    obj.Attributes["class"].Value = rgx.Replace(obj.Attributes["class"].Value, "").Insert(0, "col-lg-8 col-md-8 col-xs-8");
                                    break;
                                case 7:
                                    obj.Attributes["class"].Value = rgx.Replace(obj.Attributes["class"].Value, "").Insert(0, "col-lg-9 col-md-9 col-xs-9");
                                    break;
                                case 8:
                                    obj.Attributes["class"].Value = rgx.Replace(obj.Attributes["class"].Value, "").Insert(0, "col-lg-9 col-md-9 col-xs-9");
                                    break;
                                case 9:
                                    obj.Attributes["class"].Value = rgx.Replace(obj.Attributes["class"].Value, "").Insert(0, "col-lg-10 col-md-10 col-xs-10");
                                    break;
                                case 10:
                                    obj.Attributes["class"].Value = rgx.Replace(obj.Attributes["class"].Value, "").Insert(0, "col-lg-10 col-md-10 col-xs-10");
                                    break;
                                case 11:
                                    obj.Attributes["class"].Value = rgx.Replace(obj.Attributes["class"].Value, "").Insert(0, "col-lg-12 col-md-12 col-xs-12");
                                    break;
                                case 12:
                                    obj.Attributes["class"].Value = rgx.Replace(obj.Attributes["class"].Value, "").Insert(0, "col-lg-12 col-md-12 col-xs-12");
                                    break;
                            }
                        }
                    }

                    sbRet.Append("<div class=\"" + obj.Attributes["class"].Value + "\">");//ABRE OBJETO

                    sbRet.Append("<h4 class=\"barraTitulo\"><i class=\"" + obj.Attributes["icon"].Value + "\"></i>&nbsp;" + obj.Attributes["name"].Value);
                    if (obj.Attributes["forcastPlan"].Value == "true")
                    {
                        if (obj.Attributes["type"].Value == "scorecard")
                            sbRet.Append("<div class=\"pull-right text-small switchSpacer\">Fcast<label class=\"switch\"><input type=\"checkbox\" class=\"switchCheckBoxExtra\"><div class=\"slider\"></div></label>Plan</div>");
                        else
                            sbRet.Append("<div class=\"pull-right text-small switchSpacer\">Fcast<label class=\"switch\"><input type=\"checkbox\" class=\"switchCheckBoxFcast\"><div class=\"slider\"></div></label>Plan</div>");
                    }
                    if (obj.Attributes["mesAnoSelect"].Value == "true")
                    {
                        if (obj.Attributes["type"].Value == "scorecard")
                            sbRet.Append("<div class=\"pull-right text-small switchSpacer\">YTD<label class=\"switch\"><input type=\"checkbox\" class=\"switchCheckBoxExtra\"><div class=\"slider\"></div></label>Mon</div>");
                        else
                            sbRet.Append("<div class=\"pull-right text-small switchSpacer\">YTD<label class=\"switch\"><input type=\"checkbox\" class=\"switchCheckBoxMensal\"><div class=\"slider\"></div></label>Mon</div>");
                    }
                    sbRet.Append("</h4>");

                    sbRet.Append("<div class=\"graficoContainer");//ABRE ITEM
                    if (obj.Attributes["rowFlex"].Value == "true")
                    {
                        sbRet.Append(" rowFlex");
                    }
                    sbRet.Append("\">");//ABRE ITEM


                    foreach (XmlNode detalhe in obj.ChildNodes)
                    {
                        if (detalhe.Name != "twoColumn")
                        {
                            if (!user.IDsObjetosDashboard.Exists(e => e.Contains(detalhe.Attributes["id"].Value)))
                                continue;
                        }

                        switch (detalhe.Name)
                        {
                            case "twoColumn":
                                foreach (XmlNode col in detalhe.ChildNodes)
                                {
                                    sbRet.Append("<div class=\"" + col.Attributes["class"].Value + "\">");//ABRE COLUNA
                                    foreach (XmlNode subObj in col.ChildNodes)
                                    {

                                        bool loadSubObject = false;

                                        foreach (XmlNode subDetalhe in subObj.ChildNodes)
                                        {
                                            if (subDetalhe.Attributes != null)
                                            {
                                                if (subDetalhe.Attributes["id"] != null)
                                                {
                                                    if (user.IDsObjetosDashboard.Exists(e => e.Contains(subDetalhe.Attributes["id"].Value)))
                                                    {
                                                        loadSubObject = true;
                                                    }
                                                }
                                            }
                                        }

                                        if (!loadSubObject) continue;

                                        if (subObj.Attributes["type"].Value == "scoreboard")
                                        {
                                            int totalDeObjetosReais = 0;
                                            foreach (XmlNode subDetalhe in subObj.ChildNodes)
                                            {
                                                if (user.IDsObjetosDashboard.Exists(e => e.Contains(subDetalhe.Attributes["id"].Value)))
                                                {
                                                    totalDeObjetosReais++;
                                                }
                                            }
                                            if (subObj.ChildNodes.Count != totalDeObjetosReais)
                                            {
                                                switch (totalDeObjetosReais)
                                                {
                                                    case 1:
                                                        subObj.Attributes["class"].Value = rgx.Replace(subObj.Attributes["class"].Value, "").Insert(0, "col-lg-2 col-md-2 col-xs-2");
                                                        break;
                                                    case 2:
                                                        subObj.Attributes["class"].Value = rgx.Replace(subObj.Attributes["class"].Value, "").Insert(0, "col-lg-3 col-md-3 col-xs-3");
                                                        break;
                                                    case 3:
                                                        subObj.Attributes["class"].Value = rgx.Replace(subObj.Attributes["class"].Value, "").Insert(0, "col-lg-4 col-md-4 col-xs-4");
                                                        break;
                                                    case 4:
                                                        subObj.Attributes["class"].Value = rgx.Replace(subObj.Attributes["class"].Value, "").Insert(0, "col-lg-6 col-md-6 col-xs-6");
                                                        break;
                                                    case 5:
                                                        subObj.Attributes["class"].Value = rgx.Replace(subObj.Attributes["class"].Value, "").Insert(0, "col-lg-7 col-md-7 col-xs-7");
                                                        break;
                                                    case 6:
                                                        subObj.Attributes["class"].Value = rgx.Replace(subObj.Attributes["class"].Value, "").Insert(0, "col-lg-8 col-md-8 col-xs-8");
                                                        break;
                                                    case 7:
                                                        subObj.Attributes["class"].Value = rgx.Replace(subObj.Attributes["class"].Value, "").Insert(0, "col-lg-9 col-md-9 col-xs-9");
                                                        break;
                                                    case 8:
                                                        subObj.Attributes["class"].Value = rgx.Replace(subObj.Attributes["class"].Value, "").Insert(0, "col-lg-9 col-md-9 col-xs-9");
                                                        break;
                                                    case 9:
                                                        subObj.Attributes["class"].Value = rgx.Replace(subObj.Attributes["class"].Value, "").Insert(0, "col-lg-10 col-md-10 col-xs-10");
                                                        break;
                                                    case 10:
                                                        subObj.Attributes["class"].Value = rgx.Replace(subObj.Attributes["class"].Value, "").Insert(0, "col-lg-10 col-md-10 col-xs-10");
                                                        break;
                                                    case 11:
                                                        subObj.Attributes["class"].Value = rgx.Replace(subObj.Attributes["class"].Value, "").Insert(0, "col-lg-12 col-md-12 col-xs-12");
                                                        break;
                                                    case 12:
                                                        subObj.Attributes["class"].Value = rgx.Replace(subObj.Attributes["class"].Value, "").Insert(0, "col-lg-12 col-md-12 col-xs-12");
                                                        break;
                                                }
                                            }
                                        }

                                        sbRet.Append("<div class=\"" + subObj.Attributes["class"].Value + "\">");//ABRE OBJETO

                                        sbRet.Append("<h4 class=\"barraTitulo\"><i class=\"" + subObj.Attributes["icon"].Value + "\"></i>&nbsp;" + subObj.Attributes["name"].Value);
                                        if (subObj.Attributes["forcastPlan"].Value == "true")
                                        {
                                            if (subObj.Attributes["type"].Value == "scorecard")
                                                sbRet.Append("<div class=\"pull-right text-small switchSpacer\">Fcast<label class=\"switch\"><input type=\"checkbox\" class=\"switchCheckBoxExtra\"><div class=\"slider\"></div></label>Plan</div>");
                                            else
                                                sbRet.Append("<div class=\"pull-right text-small switchSpacer\">Fcast<label class=\"switch\"><input type=\"checkbox\" class=\"switchCheckBoxFcast\"><div class=\"slider\"></div></label>Plan</div>");
                                        }
                                        if (obj.Attributes["mesAnoSelect"].Value == "true")
                                        {
                                            if (obj.Attributes["type"].Value == "scorecard")
                                                sbRet.Append("<div class=\"pull-right text-small switchSpacer\">YTD<label class=\"switch\"><input type=\"checkbox\" class=\"switchCheckBoxExtra\"><div class=\"slider\"></div></label>Mon</div>");
                                            else
                                                sbRet.Append("<div class=\"pull-right text-small switchSpacer\">YTD<label class=\"switch\"><input type=\"checkbox\" class=\"switchCheckBoxMensal\"><div class=\"slider\"></div></label>Mon</div>");
                                        }
                                        sbRet.Append("</h4>");

                                        sbRet.Append("<div class=\"graficoContainer");//ABRE ITEM
                                        if (subObj.Attributes["rowFlex"].Value == "true")
                                        {
                                            sbRet.Append(" rowFlex");
                                        }
                                        sbRet.Append("\">");//ABRE ITEM

                                        foreach (XmlNode subDetalhe in subObj.ChildNodes)
                                        {

                                            if (!user.IDsObjetosDashboard.Exists(e => e.Contains(subDetalhe.Attributes["id"].Value)))
                                                continue;

                                            /*******************************/

                                            switch (subDetalhe.Name)
                                            {
                                                case "image":
                                                    sbRet.Append("<img src=\""+ subDetalhe["src"].InnerText + "\" alt=\"\">");
                                                    break;
                                                case "carousel":
                                                    int car = 0;
                                                    sbRet.Append("<div id=\"" + subDetalhe.Attributes["id"].Value + "\" class=\"carousel slide\" data-ride=\"carousel\">");

                                                    sbRet.Append("<ol class=\"carousel-indicators\">");
                                                    foreach (XmlNode subImg in subDetalhe.ChildNodes)
                                                    {
                                                        sbRet.Append("<li data-target=\"#" + subDetalhe.Attributes["id"].Value + "\" data-slide-to=\"" + car.ToString() + "\" class=\"" + (car == 0 ? "active" : "") + "\"></li>");
                                                        car++;
                                                    }
                                                    sbRet.Append("</ol>");

                                                    sbRet.Append("<div class=\"carousel-inner\">");
                                                    car = 0;
                                                    foreach(XmlNode subImg in subDetalhe.ChildNodes)
                                                    {
                                                        sbRet.Append("<div class=\"item" + (car == 0 ? " active" : "") + "\">");
                                                        sbRet.Append("<img src=\"" + subImg["src"].InnerText + "\" alt=\"\">");
                                                        sbRet.Append("</div>");
                                                        car++;
                                                    }

                                                    sbRet.Append("<a class=\"left carousel-control\" href=\"#" + subDetalhe.Attributes["id"].Value + "\" data-slide=\"prev\">");
                                                    sbRet.Append("<span class=\"fa fa-chevron-left\"></span>");
                                                    sbRet.Append("<span class=\"sr-only\">Previous</span>");
                                                    sbRet.Append("</a>");
                                                    sbRet.Append("<a class=\"right carousel-control\" href=\"#" + subDetalhe.Attributes["id"].Value + "\" data-slide=\"next\">");
                                                    sbRet.Append("<span class=\"fa fa-chevron-right\"></span>");
                                                    sbRet.Append("<span class=\"sr-only\">Next</span>");
                                                    sbRet.Append("</a>");


                                                    sbRet.Append("</div>");
                                                    sbRet.Append("</div>");
                                                    break;
                                                case "mapaRegiao":
                                                    sbRet.Append("<canvas class=\"MapaRegiao\" ");
                                                    foreach (XmlNode subCol in subDetalhe.SelectSingleNode("datas").ChildNodes)
                                                    {
                                                        sbRet.Append("data-" + subCol.Attributes["id"].Value.Trim().Replace(" ", "").ToLower() + "=\"" + subCol.InnerText + "\" ");
                                                    }
                                                    sbRet.Append("data-percent=\"" + subDetalhe.Attributes["percent"].Value + "\"></canvas>");
                                                    break;
                                                case "tanque":
                                                    sbRet.Append("<div class=\"" + subDetalhe.Attributes["class"].Value + "\">");
                                                    sbRet.Append("<h5>" + subDetalhe.Attributes["name"].Value + "</h5>");
                                                    //sbRet.Append("<canvas class=\"tanque\" data-total=\"" + subDetalhe["TOTAL_ANO"].InnerText + "\" data-atual=\"" + subDetalhe["YTD_REAL"].InnerText + "\" data-provisao=\"" + subDetalhe["YTD"].InnerText + "\" data-totalplan=\"" + subDetalhe["TOTAL_ANO_PLAN"].InnerText + "\" data-provisaoplan=\"" + subDetalhe["YTD_PLAN"].InnerText + "\"></canvas>");
                                                    sbRet.Append("<canvas class=\"tanque\" data-total=\"" + subDetalhe["TOTAL_ANO"].InnerText + "\" data-atual=\"" + subDetalhe["YTD_REAL"].InnerText + "\" data-provisao=\"" + subDetalhe["YTD"].InnerText + "\" data-totalplan=\"" + subDetalhe["TOTAL_ANO_PLAN"].InnerText + "\" data-provisaoplan=\"" + subDetalhe["YTD_PLAN"].InnerText + "\"" + "\" data-provisaoplan_mes=\"" + subDetalhe["YTD_PLAN_MES"].InnerText + "\"" + "\" data-atual_mes=\"" + subDetalhe["YTD_REAL_MES"].InnerText + "\" " + "\" data-provisao_mes=\"" + subDetalhe["YTD_MES"].InnerText + "\"></canvas>");
                                                    sbRet.Append("</div>");
                                                    break;
                                                case "metric":
                                                    sbRet.Append("<table class=\"table\">");
                                                    string subheaderMetric = "<tr>";
                                                    string subitemMetric = "<tr>";
                                                    foreach (XmlNode subCol in subDetalhe.ChildNodes)
                                                    {
                                                        subitemMetric += "<td><table class=\"table table-condensed\">";
                                                        subheaderMetric += "<th style=\"text-align:center;font-size:18px\">" + subCol.Attributes["name"].Value + "</th>";
                                                        foreach (XmlNode subItm in subCol.ChildNodes)
                                                        {
                                                            subitemMetric += "<tr><td align=\"right\" style=\"width:33%;font-weight:bold;font-size:14px\">" + subItm.Attributes["name"].Value + "</td>";
                                                            subitemMetric += "<td><canvas class=\"MetricHorizontalBar\" data-color=\"" + subItm.Attributes["color"].Value + "\" data-barcolor=\"" + subItm.Attributes["barcolor"].Value + "\" data-totalfcast=\"" + subItm["TOTALFCAST"].InnerText + "\" data-totalplan=\"" + subItm["TOTALPLAN"].InnerText + "\" data-atual=\"" + subItm["ATUAL"].InnerText + "\" data-plan=\"" + subItm["PLAN"].InnerText + "\" data-fcast=\"" + subItm["FCAST"].InnerText + "\"></canvas></td></tr>";
                                                        }
                                                        subitemMetric += "</table></td>";
                                                    }
                                                    subitemMetric += "</tr>";
                                                    subheaderMetric += "</tr>";
                                                    sbRet.Append(subheaderMetric);
                                                    sbRet.Append(subitemMetric);
                                                    sbRet.Append("</table>");
                                                    break;
                                                case "table":
                                                    sbRet.Append("<table class=\"" + subDetalhe.Attributes["class"].Value + "\">");//ABRE TABLE
                                                    foreach (XmlNode subItens in subDetalhe.ChildNodes)
                                                    {
                                                        switch (subItens.Name)
                                                        {
                                                            case "header":
                                                                sbRet.Append("<thead><tr>");
                                                                foreach (XmlNode subColuna in subItens.ChildNodes)
                                                                {
                                                                    sbRet.Append("<th>" + subColuna.InnerText + "</th>");
                                                                }
                                                                sbRet.Append("</tr></thead>");
                                                                break;
                                                            case "row":
                                                                sbRet.Append("<tr>");
                                                                foreach (XmlNode subColuna in subItens.ChildNodes)
                                                                {
                                                                    sbRet.Append("<td");
                                                                    if (subColuna.Attributes["style"] != null)
                                                                    {
                                                                        sbRet.Append(" style=\"" + subColuna.Attributes["style"].Value + "\"");
                                                                    }
                                                                    sbRet.Append(">");

                                                                    if (subColuna.Attributes["cube"] == null)
                                                                        sbRet.Append(subColuna.InnerText + "</td>");
                                                                    else
                                                                        sbRet.Append("<div class=\"cubeLegend\" style=\"background-color:" + subColuna.Attributes["cube"].Value + "\"></div>" + subColuna.InnerText + "</td>");

                                                                }
                                                                sbRet.Append("</tr>");
                                                                break;
                                                        }
                                                    }
                                                    sbRet.Append("</table>");//FECHA TABLE
                                                    break;
                                                case "tableIRP":
                                                    sbRet.Append("<table class=\"" + subDetalhe.Attributes["class"].Value + "\">");//ABRE TABLE
                                                    foreach (XmlNode subItens in subDetalhe.ChildNodes)
                                                    {
                                                        switch (subItens.Name)
                                                        {
                                                            case "header":
                                                                sbRet.Append("<tr>");
                                                                foreach (XmlNode subColuna in subItens.ChildNodes)
                                                                {
                                                                    sbRet.Append("<th>" + subColuna.InnerText + "</th>");
                                                                }
                                                                sbRet.Append("</tr>");
                                                                break;
                                                            case "row":
                                                                sbRet.Append("<tr>");
                                                                foreach (XmlNode subColuna in subItens.ChildNodes)
                                                                {
                                                                    sbRet.Append("<td");
                                                                    if (subColuna.Attributes["style"] != null)
                                                                    {
                                                                        sbRet.Append(" style=\"" + subColuna.Attributes["style"].Value + "\"");
                                                                    }
                                                                    sbRet.Append(">");

                                                                    if (subColuna.Name != "IRP")
                                                                        sbRet.Append(subColuna.InnerText + "</td>");
                                                                    else
                                                                    {
                                                                        double irp = double.Parse(subColuna.InnerText);
                                                                        if (irp < 1)
                                                                        {
                                                                            sbRet.Append("<i style=\"color:red\" class=\"fa fa-thumbs-down fa-2x\"></i></td>");
                                                                        }
                                                                        else if (irp <= 1.15)
                                                                        {
                                                                            sbRet.Append("<i class=\"fa fa-thumbs-o-up fa-2x\"></i></td>");
                                                                        }
                                                                        else if (irp <= 1.25)
                                                                        {
                                                                            sbRet.Append("<i class=\"fa fa-thumbs-up fa-2x\"></i></td>");
                                                                        }
                                                                        else
                                                                        {
                                                                            sbRet.Append("<i style=\"color:#ffd700\" class=\"fa fa-thumbs-up fa-2x\"></i></td>");
                                                                        }
                                                                    }
                                                                }
                                                                sbRet.Append("</tr>");
                                                                break;
                                                        }
                                                    }
                                                    sbRet.Append("</table>");//FECHA TABLE
                                                    break;
                                                case "barChartSimple":
                                                    sbRet.Append("<canvas id=\"" + subDetalhe.Attributes["name"].Value + "\"></canvas>");

                                                    sbRet_scriptArea.Append("var data" + subDetalhe.Attributes["name"].Value + " = {");
                                                    foreach (XmlNode subItens in subDetalhe.ChildNodes)
                                                    {
                                                        switch (subItens.Name)
                                                        {
                                                            case "labels":
                                                                sbRet_scriptArea.Append("labels: [");
                                                                tempS = "";
                                                                foreach (XmlNode subLbl in subItens.ChildNodes)
                                                                {
                                                                    tempS += "'" + subLbl.InnerText + "' ,";
                                                                }
                                                                sbRet_scriptArea.Append(tempS.Substring(0, tempS.Length - 1));
                                                                sbRet_scriptArea.Append("] ,");
                                                                break;
                                                            case "datas":
                                                                sbRet_scriptArea.Append("datasets: [{ ");
                                                                sbRet_scriptArea.Append("label: '" + subDetalhe.Attributes["label"].Value + "',");
                                                                sbRet_scriptArea.Append("backgroundColor: '" + subDetalhe.Attributes["barColor"].Value + "' ,");
                                                                sbRet_scriptArea.Append("data: [ ");
                                                                tempS = "";
                                                                foreach (XmlNode subDat in subItens.ChildNodes)
                                                                {
                                                                    tempS += "'" + subDat.InnerText + "' ,";
                                                                }
                                                                sbRet_scriptArea.Append(tempS.Substring(0, tempS.Length - 1));
                                                                sbRet_scriptArea.Append("]}]");
                                                                break;
                                                        }
                                                    }
                                                    sbRet_scriptArea.Append("};");

                                                    sbRet_scriptArea.Append("var ctx" + subDetalhe.Attributes["name"].Value + " = $(\"#" + subDetalhe.Attributes["name"].Value + "\");");
                                                    sbRet_scriptArea.Append("var chart" + subDetalhe.Attributes["name"].Value + " = new Chart(ctx" + subDetalhe.Attributes["name"].Value + ", { type: '" + subDetalhe.Attributes["tipo"].Value + "', data: data" + subDetalhe.Attributes["name"].Value);
                                                    sbRet_scriptArea.Append(" , options: { responsive: true,  legend: {display: true},tooltips: {callbacks: {label: function (tooltipItem, data) {var subDataset = data.datasets[tooltipItem.datasetIndex];var precentage = parseFloat(subDataset.data[tooltipItem.index]);return precentage.formatMoney();}}}}");
                                                    sbRet_scriptArea.Append("});");
                                                    break;
                                                case "pieChartSimple":
                                                    sbRet.Append("<canvas id=\"" + subDetalhe.Attributes["name"].Value + "\"></canvas>");

                                                    sbRet_scriptArea.Append("var data" + subDetalhe.Attributes["name"].Value + " = {");
                                                    foreach (XmlNode subItens in subDetalhe.ChildNodes)
                                                    {
                                                        switch (subItens.Name)
                                                        {
                                                            case "labels":
                                                                sbRet_scriptArea.Append("labels: [");
                                                                tempS = "";
                                                                foreach (XmlNode subLbl in subItens.ChildNodes)
                                                                {
                                                                    tempS += "'" + subLbl.InnerText + "' ,";
                                                                }
                                                                sbRet_scriptArea.Append(tempS.Substring(0, tempS.Length - 1));
                                                                sbRet_scriptArea.Append("] ,");
                                                                break;
                                                            case "datas":
                                                                sbRet_scriptArea.Append("datasets: [{ ");
                                                                tempS = "";
                                                                string colors = "";
                                                                foreach (XmlNode subDat in subItens.ChildNodes)
                                                                {
                                                                    tempS += "'" + subDat.InnerText + "' ,";
                                                                    colors += "'" + subDat.Attributes["color"].Value + "' ,";
                                                                }
                                                                sbRet_scriptArea.Append("backgroundColor: [");
                                                                sbRet_scriptArea.Append(colors.Substring(0, colors.Length - 1));
                                                                sbRet_scriptArea.Append("] ,");
                                                                sbRet_scriptArea.Append("data: [ ");
                                                                sbRet_scriptArea.Append(tempS.Substring(0, tempS.Length - 1));
                                                                sbRet_scriptArea.Append("]}]");
                                                                break;
                                                        }
                                                    }
                                                    sbRet_scriptArea.Append("};");

                                                    sbRet_scriptArea.Append("var ctx" + subDetalhe.Attributes["name"].Value + " = $(\"#" + subDetalhe.Attributes["name"].Value + "\");");
                                                    sbRet_scriptArea.Append("var chart" + subDetalhe.Attributes["name"].Value + " = new Chart(ctx" + subDetalhe.Attributes["name"].Value + ", { type: '" + subDetalhe.Attributes["tipo"].Value + "', data: data" + subDetalhe.Attributes["name"].Value);
                                                    if (subDetalhe.Attributes["show"].Value == "percentual")
                                                        sbRet_scriptArea.Append(" , options: { responsive: true, legend: {display: true},tooltips: {callbacks: {label: function (tooltipItem, data) {var subDataset = data.datasets[tooltipItem.datasetIndex];var total = subDataset.data.reduce(function (previousValue, currentValue, currentIndex, array) {return parseFloat(previousValue) + parseFloat(currentValue);});var currentValue = subDataset.data[tooltipItem.index];var precentage = Math.floor(((currentValue / total) * 100) + 0.5);return precentage + \"%\";}}}}");
                                                    else
                                                        sbRet_scriptArea.Append(" , options: { responsive: true, legend: {display: true},tooltips: {callbacks: {label: function (tooltipItem, data) {var subDataset = data.datasets[tooltipItem.datasetIndex];var precentage = parseFloat(subDataset.data[tooltipItem.index]);return precentage.formatMoney();}}}}");

                                                    sbRet_scriptArea.Append("});");
                                                    break;
                                                case "barChartComplex":
                                                case "barChartSingleLine":
                                                case "barChartMultiBarAndLine":
                                                    sbRet.Append("<canvas id=\"" + subDetalhe.Attributes["name"].Value + "\"></canvas>");
                                                    sbRet_scriptArea.Append("var data" + subDetalhe.Attributes["name"].Value + " = {");
                                                    foreach (XmlNode subItens in subDetalhe.ChildNodes)
                                                    {
                                                        switch (subItens.Name)
                                                        {
                                                            case "labels":
                                                                sbRet_scriptArea.Append("labels: [");
                                                                tempS = "";
                                                                foreach (XmlNode subLbl in subItens.ChildNodes)
                                                                {
                                                                    tempS += "'" + subLbl.InnerText + "' ,";
                                                                }
                                                                sbRet_scriptArea.Append(tempS.Substring(0, tempS.Length - 1));
                                                                sbRet_scriptArea.Append("] ,");
                                                                break;
                                                            case "datasets":
                                                                sbRet_scriptArea.Append("datasets: [");
                                                                foreach (XmlNode subDataset in subItens.ChildNodes)
                                                                {
                                                                    switch (subDataset.Attributes["tipo"].Value)
                                                                    {
                                                                        case "bar":
                                                                            tempS = "";
                                                                            foreach (XmlNode subDat in subDataset.ChildNodes)
                                                                            {
                                                                                tempS += subDat.InnerText + " ,";
                                                                            }
                                                                            sbRet_scriptArea.Append("{type: 'bar',label: [!Labels!], hidden: [!Hidden!], backgroundColor: [!BarColor!], data: [[!Data!]]},".Replace("[!Data!]", tempS.Substring(0, tempS.Length - 1)).Replace("[!Hidden!]", subDataset.Attributes["hidden"].Value).Replace("[!BarColor!]", subDataset.Attributes["barColor"].Value).Replace("[!Labels!]", subDataset.Attributes["label"].Value));
                                                                            break;
                                                                        case "subMultiBar":
                                                                            foreach (XmlNode subMultiBar in subDataset.ChildNodes)
                                                                            {
                                                                                tempS = "";
                                                                                foreach (XmlNode subDat in subMultiBar.ChildNodes)
                                                                                {
                                                                                    tempS += subDat.InnerText + " ,";
                                                                                }
                                                                                sbRet_scriptArea.Append("{label: '[!Label!]',backgroundColor: '[!BarColor!]',data: [[!Data!]]},".Replace("[!Data!]", tempS.Substring(0, tempS.Length - 1)).Replace("[!Label!]", subMultiBar.Attributes["label"].Value).Replace("[!BarColor!]", subMultiBar.Attributes["barColor"].Value));
                                                                            }
                                                                            break;
                                                                        default:
                                                                            tempS = "";
                                                                            foreach (XmlNode subDat in subDataset.ChildNodes)
                                                                            {
                                                                                tempS += subDat.InnerText + " ,";
                                                                            }
                                                                            sbRet_scriptArea.Append("{type: 'line',label: '" + subDataset.Attributes["label"].Value + "',fill: false,lineTension: 0.1,backgroundColor: '[!Color!]',borderColor: '[!Color!]',borderCapStyle: 'butt',borderDash: [],borderDashOffset: 0.0,borderJoinStyle: 'miter',pointBorderColor: '[!Color!]',pointBackgroundColor: '[!Color!]',pointBorderWidth: 1,pointHoverRadius: 8,pointHoverBackgroundColor: '[!Color!]',pointHoverBorderColor: '[!Color!]',pointHoverBorderWidth: 2,pointRadius: 1,pointHitRadius: 10,spanGaps: false,data: [[!Data!]]} , ".Replace("[!Data!]", tempS.Substring(0, tempS.Length - 1)).Replace("[!Color!]", subDataset.Attributes["color"].Value));
                                                                            break;
                                                                    }
                                                                }
                                                                sbRet_scriptArea.Append("] ");
                                                                break;
                                                        }
                                                    }
                                                    sbRet_scriptArea.Append("};");

                                                    sbRet_scriptArea.Append("var ctx" + subDetalhe.Attributes["name"].Value + " = $(\"#" + subDetalhe.Attributes["name"].Value + "\");");
                                                    sbRet_scriptArea.Append("var chart" + subDetalhe.Attributes["name"].Value + " = new Chart(ctx" + subDetalhe.Attributes["name"].Value + ", { type: 'bar', data: data" + subDetalhe.Attributes["name"].Value);
                                                    sbRet_scriptArea.Append(" , options: { responsive: true, legend: {display: true},scales: {xAxes: [{ticks: {}}],yAxes: [{ticks: {beginAtZero: true,stepSize: " + subDetalhe.Attributes["step"].Value + ",userCallback: function (value, index, values) {value = Math.round(value).toString();value = value.split(/(?=(?:...)*$)/);value = value.join('.');return value;}}}]},tooltips: {enabled: true,callbacks: {label: function (tooltipItem, data) {var datasetLabel = data.datasets[tooltipItem.datasetIndex].label || 'Other';tooltipItem.yLabel = Math.round(tooltipItem.yLabel).toString();tooltipItem.yLabel = tooltipItem.yLabel.split(/(?=(?:...)*$)/);tooltipItem.yLabel = tooltipItem.yLabel.join('.');return datasetLabel + ': ' + tooltipItem.yLabel;}}}}");
                                                    sbRet_scriptArea.Append(" });");
                                                    break;
                                                case "piramid":
                                                    foreach (XmlNode subItens in subDetalhe.ChildNodes)
                                                    {
                                                        sbRet.Append("<canvas class=\"" + subDetalhe.Attributes["class"].Value + "\" ");
                                                        sbRet.Append("data-total = \"" + subItens.Attributes["total"].Value + "\" ");
                                                        sbRet.Append("data-atual = \"" + subItens.Attributes["atual"].Value + "\" ");
                                                        sbRet.Append("data-name = \"" + subItens.InnerText + "\" ");
                                                        sbRet.Append("data-artist = \"" + subItens.Attributes["artist"].Value + "\" ");
                                                        sbRet.Append("data-percentual = \"" + subItens.Attributes["percentual"].Value + "\" ");
                                                        sbRet.Append("></canvas>");
                                                    }
                                                    break;
                                                case "radar":
                                                    sbRet.Append("<canvas id=\"" + subDetalhe.Attributes["name"].Value + "\"></canvas>");

                                                    sbRet_scriptArea.Append("var data" + subDetalhe.Attributes["name"].Value + " = {");
                                                    string subdatasets = "";
                                                    foreach (XmlNode subItens in subDetalhe.ChildNodes)
                                                    {
                                                        switch (subItens.Name)
                                                        {
                                                            case "labels":
                                                                sbRet_scriptArea.Append("labels: [");
                                                                tempS = "";
                                                                foreach (XmlNode subLbl in subItens.ChildNodes)
                                                                {
                                                                    tempS += "'" + subLbl.InnerText + "' ,";
                                                                }
                                                                sbRet_scriptArea.Append(tempS.Substring(0, tempS.Length - 1));
                                                                sbRet_scriptArea.Append("] ,");
                                                                break;
                                                            case "datas":
                                                                //sbRet_scriptArea.Append("datasets: [{ ");
                                                                tempS = "";
                                                                string colors = "";

                                                                subdatasets += ("{label: \"" + subItens.Attributes["label"].Value + "\",");

                                                                foreach (XmlNode subDat in subItens.ChildNodes)
                                                                {
                                                                    tempS += subDat.InnerText + " ,";

                                                                }

                                                                subdatasets += "backgroundColor: '" + subItens.Attributes["color"].Value + "' ,";
                                                                subdatasets += "data: [";
                                                                subdatasets += tempS.Substring(0, tempS.Length - 1);
                                                                subdatasets += "]},";

                                                                //sbRet_scriptArea.Append("backgroundColor: [");
                                                                //sbRet_scriptArea.Append(colors.Substring(0, colors.Length - 1));
                                                                //sbRet_scriptArea.Append("] ,");
                                                                //sbRet_scriptArea.Append("data: [ ");
                                                                //sbRet_scriptArea.Append(tempS.Substring(0, tempS.Length - 1));
                                                                //sbRet_scriptArea.Append("]}]");
                                                                break;
                                                        }
                                                    }
                                                    sbRet_scriptArea.Append("subdatasets: [ ");
                                                    sbRet_scriptArea.Append(subdatasets);
                                                    sbRet_scriptArea.Append("]};");

                                                    sbRet_scriptArea.Append("var ctx" + subDetalhe.Attributes["name"].Value + " = $(\"#" + subDetalhe.Attributes["name"].Value + "\");");
                                                    sbRet_scriptArea.Append("var chart" + subDetalhe.Attributes["name"].Value + " = new Chart(ctx" + subDetalhe.Attributes["name"].Value + ", { type: '" + subDetalhe.Attributes["tipo"].Value + "', data: data" + subDetalhe.Attributes["name"].Value);
                                                    sbRet_scriptArea.Append(", options:{ responsive: true"+ /*", scale: {ticks: {stepSize: 20,beginAtZero: true,max: 100}}"+*/ ",tooltips: {callbacks: {label: function (tooltipItem, data) {var subDataset = data.datasets[tooltipItem.datasetIndex];var precentage = parseFloat(subDataset.data[tooltipItem.index]);return precentage.formatMoney() + '%';}}}}");
                                                    sbRet_scriptArea.Append("});");
                                                    break;
                                            }

                                            /******************************/

                                        }

                                        sbRet.Append("</div>");//FECHA ITEM
                                        sbRet.Append("</div>");//FECHA OBJETO
                                    }
                                    sbRet.Append("</div>");//FECHA COLUNA
                                }
                                break;
                            case "mapaRegiao":
                                sbRet.Append("<canvas class=\"MapaRegiao\" ");
                                foreach (XmlNode col in detalhe.SelectSingleNode("datas").ChildNodes)
                                {
                                    sbRet.Append("data-" + col.Attributes["id"].Value.Trim().Replace(" ","").ToLower() + "=\"" + col.InnerText + "\" ");
                                }
                                sbRet.Append("data-percent=\""+ detalhe.Attributes["percent"].Value + "\"></canvas>");
                                break;
                            case "tanque":
                                sbRet.Append("<div class=\"" + detalhe.Attributes["class"].Value + "\">");
                                sbRet.Append("<h5>" + detalhe.Attributes["name"].Value + "</h5>");
                                //sbRet.Append("<canvas class=\"tanque\" data-total=\"" + detalhe["TOTAL_ANO"].InnerText + "\" data-atual=\"" + detalhe["YTD_REAL"].InnerText + "\" data-provisao=\"" + detalhe["YTD"].InnerText + "\" data-totalplan=\"" + detalhe["TOTAL_ANO_PLAN"].InnerText + "\" data-provisaoplan=\"" + detalhe["YTD_PLAN"].InnerText + "\"></canvas>");
                                sbRet.Append("<canvas class=\"tanque\" data-total=\"" + detalhe["TOTAL_ANO"].InnerText + "\" data-atual=\"" + detalhe["YTD_REAL"].InnerText + "\" data-provisao=\"" + detalhe["YTD"].InnerText + "\" data-totalplan=\"" + detalhe["TOTAL_ANO_PLAN"].InnerText + "\" data-provisaoplan=\"" + detalhe["YTD_PLAN"].InnerText + "\"" + "\" data-provisaoplan_mes=\"" + detalhe["YTD_PLAN_MES"].InnerText + "\"" + "\" data-atual_mes=\"" + detalhe["YTD_REAL_MES"].InnerText + "\" " + "\" data-provisao_mes=\"" + detalhe["YTD_MES"].InnerText + "\"></canvas>");
                                sbRet.Append("</div>");
                                break;
                            case "metric":
                                sbRet.Append("<table class=\"table\">");
                                string headerMetric = "<tr>";
                                string itemMetric = "<tr>";
                                foreach (XmlNode col in detalhe.ChildNodes)
                                {
                                    itemMetric += "<td><table class=\"table table-condensed\">";
                                    headerMetric += "<th style=\"text-align:center;font-size:18px\">" + col.Attributes["name"].Value + "</th>";
                                    foreach (XmlNode itm in col.ChildNodes)
                                    {
                                        itemMetric += "<tr><td align=\"right\" style=\"width:33%;font-weight:bold;font-size:14px\">" + itm.Attributes["name"].Value + "</td>";
                                        itemMetric += "<td><canvas class=\"MetricHorizontalBar\" data-color=\"" + itm.Attributes["color"].Value + "\" data-barcolor=\"" + itm.Attributes["barcolor"].Value + "\" data-totalfcast=\"" + itm["TOTALFCAST"].InnerText + "\" data-totalplan=\"" + itm["TOTALPLAN"].InnerText + "\" data-atual=\"" + itm["ATUAL"].InnerText + "\" data-plan=\"" + itm["PLAN"].InnerText + "\" data-fcast=\"" + itm["FCAST"].InnerText + "\"></canvas></td></tr>";
                                    }
                                    itemMetric += "</table></td>";
                                }
                                itemMetric += "</tr>";
                                headerMetric += "</tr>";
                                sbRet.Append(headerMetric);
                                sbRet.Append(itemMetric);
                                sbRet.Append("</table>");
                                break;
                            case "table":
                                sbRet.Append("<table class=\"" + detalhe.Attributes["class"].Value + "\">");//ABRE TABLE
                                foreach (XmlNode itens in detalhe.ChildNodes)
                                {
                                    switch (itens.Name)
                                    {
                                        case "header":
                                            sbRet.Append("<thead><tr>");
                                            foreach (XmlNode coluna in itens.ChildNodes)
                                            {
                                                sbRet.Append("<th>" + coluna.InnerText + "</th>");
                                            }
                                            sbRet.Append("</tr></thead>");
                                            break;
                                        case "row":
                                            sbRet.Append("<tr>");
                                            foreach (XmlNode coluna in itens.ChildNodes)
                                            {
                                                sbRet.Append("<td");
                                                if (coluna.Attributes["style"] != null)
                                                {
                                                    sbRet.Append(" style=\"" + coluna.Attributes["style"].Value + "\"");
                                                }
                                                sbRet.Append(">");

                                                if (coluna.Attributes["cube"] == null)
                                                    sbRet.Append(coluna.InnerText + "</td>");
                                                else
                                                    sbRet.Append("<div class=\"cubeLegend\" style=\"background-color:" + coluna.Attributes["cube"].Value + "\"></div>" + coluna.InnerText + "</td>");

                                            }
                                            sbRet.Append("</tr>");
                                            break;
                                    }
                                }
                                sbRet.Append("</table>");//FECHA TABLE
                                break;
                            case "tableIRP":
                                sbRet.Append("<table class=\"" + detalhe.Attributes["class"].Value + "\">");//ABRE TABLE
                                foreach (XmlNode itens in detalhe.ChildNodes)
                                {
                                    switch (itens.Name)
                                    {
                                        case "header":
                                            sbRet.Append("<tr>");
                                            foreach (XmlNode coluna in itens.ChildNodes)
                                            {
                                                sbRet.Append("<th>" + coluna.InnerText + "</th>");
                                            }
                                            sbRet.Append("</tr>");
                                            break;
                                        case "row":
                                            bool cansee = false;
                                            if (itens["LOGIN_RESPONSAVEL"] != null)
                                            {
                                                if (itens["LOGIN_RESPONSAVEL"].InnerText == user.Login) cansee = true;
                                                string nacionalidade = "";
                                                if (itens["NACIONALIDADE"] != null)
                                                    nacionalidade = itens["NACIONALIDADE"].InnerText.Trim();
                                                if (user.Acessos.IndexOf("TableIRPAER") > -1 && user.Acessos.IndexOf("TableIRPAER:" + nacionalidade) > -1) cansee = true;
                                            }
                                            else cansee = true;

                                            if (cansee)
                                            {
                                                sbRet.Append("<tr>");
                                                foreach (XmlNode coluna in itens.ChildNodes)
                                                {
                                                    sbRet.Append("<td");
                                                    if (coluna.Attributes["style"] != null)
                                                    {
                                                        sbRet.Append(" style=\"" + coluna.Attributes["style"].Value + "\"");
                                                    }
                                                    sbRet.Append(">");

                                                    if (coluna.Name != "IRP")
                                                        sbRet.Append(coluna.InnerText + "</td>");
                                                    else
                                                    {
                                                        double irp = double.Parse(coluna.InnerText);
                                                        if (irp < 1)
                                                        {
                                                            sbRet.Append("<i style=\"color:red\" class=\"fa fa-thumbs-down fa-2x\"></i></td>");
                                                        }
                                                        else if (irp <= 1.15)
                                                        {
                                                            sbRet.Append("<i class=\"fa fa-thumbs-o-up fa-2x\"></i></td>");
                                                        }
                                                        else if (irp <= 1.25)
                                                        {
                                                            sbRet.Append("<i style=\"color:#004846\" class=\"fa fa-thumbs-up fa-2x\"></i></td>");
                                                        }
                                                        else
                                                        {
                                                            sbRet.Append("<i style=\"color:#ffd700\" class=\"fa fa-thumbs-up fa-2x\"></i></td>");
                                                        }
                                                    }
                                                }
                                                sbRet.Append("</tr>");
                                            }
                                            break;
                                    }
                                }
                                sbRet.Append("</table>");//FECHA TABLE
                                break;
                            case "barChartSimple":
                                sbRet.Append("<canvas id=\"" + detalhe.Attributes["name"].Value + "\"></canvas>");

                                sbRet_scriptArea.Append("var data" + detalhe.Attributes["name"].Value + " = {");
                                foreach (XmlNode itens in detalhe.ChildNodes)
                                {
                                    switch (itens.Name)
                                    {
                                        case "labels":
                                            sbRet_scriptArea.Append("labels: [");
                                            tempS = "";
                                            foreach (XmlNode lbl in itens.ChildNodes)
                                            {
                                                tempS += "'" + lbl.InnerText + "' ,";
                                            }
                                            sbRet_scriptArea.Append(tempS.Substring(0, tempS.Length - 1));
                                            sbRet_scriptArea.Append("] ,");
                                            break;
                                        case "datas":
                                            sbRet_scriptArea.Append("datasets: [{ ");
                                            sbRet_scriptArea.Append("label: '" + detalhe.Attributes["label"].Value + "',");
                                            sbRet_scriptArea.Append("backgroundColor: '" + detalhe.Attributes["barColor"].Value + "' ,");
                                            sbRet_scriptArea.Append("data: [ ");
                                            tempS = "";
                                            foreach (XmlNode dat in itens.ChildNodes)
                                            {
                                                tempS += "'" + dat.InnerText + "' ,";
                                            }
                                            sbRet_scriptArea.Append(tempS.Substring(0, tempS.Length - 1));
                                            sbRet_scriptArea.Append("]}]");
                                            break;
                                    }
                                }
                                sbRet_scriptArea.Append("};");

                                sbRet_scriptArea.Append("var ctx" + detalhe.Attributes["name"].Value + " = $(\"#" + detalhe.Attributes["name"].Value + "\");");
                                sbRet_scriptArea.Append("var chart" + detalhe.Attributes["name"].Value + " = new Chart(ctx" + detalhe.Attributes["name"].Value + ", { type: '" + detalhe.Attributes["tipo"].Value + "', data: data" + detalhe.Attributes["name"].Value);
                                sbRet_scriptArea.Append(" , options: { responsive: true,  legend: {display: true},tooltips: {callbacks: {label: function (tooltipItem, data) {var dataset = data.datasets[tooltipItem.datasetIndex];var precentage = parseFloat(dataset.data[tooltipItem.index]);return precentage.formatMoney();}}}}");
                                sbRet_scriptArea.Append("});");
                                break;
                            case "pieChartSimple":
                                sbRet.Append("<canvas id=\"" + detalhe.Attributes["name"].Value + "\"></canvas>");

                                sbRet_scriptArea.Append("var data" + detalhe.Attributes["name"].Value + " = {");
                                foreach (XmlNode itens in detalhe.ChildNodes)
                                {
                                    switch (itens.Name)
                                    {
                                        case "labels":
                                            sbRet_scriptArea.Append("labels: [");
                                            tempS = "";
                                            foreach (XmlNode lbl in itens.ChildNodes)
                                            {
                                                tempS += "'" + lbl.InnerText + "' ,";
                                            }
                                            sbRet_scriptArea.Append(tempS.Substring(0, tempS.Length - 1));
                                            sbRet_scriptArea.Append("] ,");
                                            break;
                                        case "datas":
                                            sbRet_scriptArea.Append("datasets: [{ ");
                                            tempS = "";
                                            string colors = "";
                                            foreach (XmlNode dat in itens.ChildNodes)
                                            {
                                                tempS += "'" + dat.InnerText + "' ,";
                                                colors += "'" + dat.Attributes["color"].Value + "' ,";
                                            }
                                            sbRet_scriptArea.Append("backgroundColor: [");
                                            sbRet_scriptArea.Append(colors.Substring(0, colors.Length - 1));
                                            sbRet_scriptArea.Append("] ,");
                                            sbRet_scriptArea.Append("data: [ ");
                                            sbRet_scriptArea.Append(tempS.Substring(0, tempS.Length - 1));
                                            sbRet_scriptArea.Append("]}]");
                                            break;
                                    }
                                }
                                sbRet_scriptArea.Append("};");

                                sbRet_scriptArea.Append("var ctx" + detalhe.Attributes["name"].Value + " = $(\"#" + detalhe.Attributes["name"].Value + "\");");
                                sbRet_scriptArea.Append("var chart" + detalhe.Attributes["name"].Value + " = new Chart(ctx" + detalhe.Attributes["name"].Value + ", { type: '" + detalhe.Attributes["tipo"].Value + "', data: data" + detalhe.Attributes["name"].Value);
                                if (detalhe.Attributes["show"].Value == "percentual")
                                    sbRet_scriptArea.Append(" , options: { responsive: true,  legend: {display: true},tooltips: {callbacks: {label: function (tooltipItem, data) {var dataset = data.datasets[tooltipItem.datasetIndex];var total = dataset.data.reduce(function (previousValue, currentValue, currentIndex, array) {return parseFloat(previousValue) + parseFloat(currentValue);});var currentValue = dataset.data[tooltipItem.index];var precentage = Math.floor(((currentValue / total) * 100) + 0.5);return precentage + \"%\";}}}}");
                                else
                                    sbRet_scriptArea.Append(" , options: { responsive: true,  legend: {display: true},tooltips: {callbacks: {label: function (tooltipItem, data) {var dataset = data.datasets[tooltipItem.datasetIndex];var precentage = parseFloat(dataset.data[tooltipItem.index]);return precentage.formatMoney();}}}}");

                                sbRet_scriptArea.Append("});");
                                break;
                            case "barChartComplex":
                            case "barChartSingleLine":
                            case "barChartMultiBarAndLine":
                                sbRet.Append("<canvas id=\"" + detalhe.Attributes["name"].Value + "\"></canvas>");
                                sbRet_scriptArea.Append("var data" + detalhe.Attributes["name"].Value + " = {");
                                foreach (XmlNode itens in detalhe.ChildNodes)
                                {
                                    switch (itens.Name)
                                    {
                                        case "labels":
                                            sbRet_scriptArea.Append("labels: [");
                                            tempS = "";
                                            foreach (XmlNode lbl in itens.ChildNodes)
                                            {
                                                tempS += "'" + lbl.InnerText + "' ,";
                                            }
                                            sbRet_scriptArea.Append(tempS.Substring(0, tempS.Length - 1));
                                            sbRet_scriptArea.Append("] ,");
                                            break;
                                        case "datasets":
                                            sbRet_scriptArea.Append("datasets: [");
                                            foreach (XmlNode dataset in itens.ChildNodes)
                                            {
                                                switch (dataset.Attributes["tipo"].Value)
                                                {
                                                    case "bar":
                                                        tempS = "";
                                                        foreach (XmlNode dat in dataset.ChildNodes)
                                                        {
                                                            tempS += dat.InnerText + " ,";
                                                        }
                                                        if (!string.IsNullOrEmpty(tempS))
                                                            sbRet_scriptArea.Append("{type: 'bar',label: [!Labels!], hidden: [!Hidden!], backgroundColor: [!BarColor!], data: [[!Data!]]},".Replace("[!Data!]", tempS.Substring(0, tempS.Length - 1)).Replace("[!Hidden!]", dataset.Attributes["hidden"].Value).Replace("[!BarColor!]", dataset.Attributes["barColor"].Value).Replace("[!Labels!]", dataset.Attributes["label"].Value));
                                                        break;
                                                    case "multiBar":
                                                        foreach (XmlNode multiBar in dataset.ChildNodes)
                                                        {
                                                            tempS = "";
                                                            foreach (XmlNode dat in multiBar.ChildNodes)
                                                            {
                                                                tempS += dat.InnerText + " ,";
                                                            }
                                                            if (!string.IsNullOrEmpty(tempS))
                                                                sbRet_scriptArea.Append("{label: '[!Label!]',backgroundColor: '[!BarColor!]',data: [[!Data!]]},".Replace("[!Data!]", tempS.Substring(0, tempS.Length - 1)).Replace("[!Label!]", multiBar.Attributes["label"].Value).Replace("[!BarColor!]", multiBar.Attributes["barColor"].Value));
                                                        }
                                                        break;
                                                    default:
                                                        tempS = "";
                                                        foreach (XmlNode dat in dataset.ChildNodes)
                                                        {
                                                            tempS += dat.InnerText + " ,";
                                                        }
                                                        if (!string.IsNullOrEmpty(tempS))
                                                            sbRet_scriptArea.Append("{type: 'line',label: '" + dataset.Attributes["label"].Value + "',fill: false,lineTension: 0.1,backgroundColor: '[!Color!]',borderColor: '[!Color!]',borderCapStyle: 'butt',borderDash: [],borderDashOffset: 0.0,borderJoinStyle: 'miter',pointBorderColor: '[!Color!]',pointBackgroundColor: '[!Color!]',pointBorderWidth: 1,pointHoverRadius: 8,pointHoverBackgroundColor: '[!Color!]',pointHoverBorderColor: '[!Color!]',pointHoverBorderWidth: 2,pointRadius: 1,pointHitRadius: 10,spanGaps: false,data: [[!Data!]]} , ".Replace("[!Data!]", tempS.Substring(0, tempS.Length - 1)).Replace("[!Color!]", dataset.Attributes["color"].Value));
                                                        break;
                                                }
                                            }
                                            sbRet_scriptArea.Append("] ");
                                            break;
                                    }
                                }
                                sbRet_scriptArea.Append("};");

                                sbRet_scriptArea.Append("var ctx" + detalhe.Attributes["name"].Value + " = $(\"#" + detalhe.Attributes["name"].Value + "\");");
                                sbRet_scriptArea.Append("var chart" + detalhe.Attributes["name"].Value + " = new Chart(ctx" + detalhe.Attributes["name"].Value + ", { type: 'bar', data: data" + detalhe.Attributes["name"].Value);
                                sbRet_scriptArea.Append(" , options: { responsive: true, legend: {display: true},scales: {xAxes: [{ticks: {}}],yAxes: [{ticks: {beginAtZero: true,stepSize: " + detalhe.Attributes["step"].Value + ",userCallback: function (value, index, values) {value = Math.round(value).toString();value = value.split(/(?=(?:...)*$)/);value = value.join('.');return value;}}}]},tooltips: {enabled: true,callbacks: {label: function (tooltipItem, data) {var datasetLabel = data.datasets[tooltipItem.datasetIndex].label || 'Other';tooltipItem.yLabel = Math.round(tooltipItem.yLabel).toString();tooltipItem.yLabel = tooltipItem.yLabel.split(/(?=(?:...)*$)/);tooltipItem.yLabel = tooltipItem.yLabel.join('.');return datasetLabel + ': ' + tooltipItem.yLabel;}}}}");
                                sbRet_scriptArea.Append(" });");
                                break;
                            case "piramid":
                                foreach (XmlNode itens in detalhe.ChildNodes)
                                {
                                    sbRet.Append("<canvas class=\"" + detalhe.Attributes["class"].Value + "\" ");
                                    sbRet.Append("data-total = \"" + itens.Attributes["total"].Value + "\" ");
                                    sbRet.Append("data-atual = \"" + itens.Attributes["atual"].Value + "\" ");
                                    sbRet.Append("data-name = \"" + itens.InnerText + "\" ");
                                    sbRet.Append("data-artist = \"" + itens.Attributes["artist"].Value + "\" ");
                                    sbRet.Append("data-percentual = \"" + itens.Attributes["percentual"].Value + "\" ");
                                    sbRet.Append("></canvas>");
                                }
                                break;
                            case "radar":
                                sbRet.Append("<canvas id=\"" + detalhe.Attributes["name"].Value + "\"></canvas>");

                                sbRet_scriptArea.Append("var data" + detalhe.Attributes["name"].Value + " = {");
                                string datasets = "";
                                foreach (XmlNode itens in detalhe.ChildNodes)
                                {
                                    switch (itens.Name)
                                    {
                                        case "labels":
                                            sbRet_scriptArea.Append("labels: [");
                                            tempS = "";
                                            foreach (XmlNode lbl in itens.ChildNodes)
                                            {
                                                tempS += "'" + lbl.InnerText + "' ,";
                                            }
                                            sbRet_scriptArea.Append(tempS.Substring(0, tempS.Length - 1));
                                            sbRet_scriptArea.Append("] ,");
                                            break;
                                        case "datas":
                                            //sbRet_scriptArea.Append("datasets: [{ ");
                                            tempS = "";
                                            string colors = "";

                                            datasets += ("{label: \"" + itens.Attributes["label"].Value + "\",");

                                            foreach (XmlNode dat in itens.ChildNodes)
                                            {
                                                tempS += dat.InnerText + " ,";

                                            }

                                            datasets += "backgroundColor: '" + itens.Attributes["color"].Value + "' ,";
                                            datasets += "data: [";
                                            datasets += tempS.Substring(0, tempS.Length - 1);
                                            datasets += "]},";

                                            //sbRet_scriptArea.Append("backgroundColor: [");
                                            //sbRet_scriptArea.Append(colors.Substring(0, colors.Length - 1));
                                            //sbRet_scriptArea.Append("] ,");
                                            //sbRet_scriptArea.Append("data: [ ");
                                            //sbRet_scriptArea.Append(tempS.Substring(0, tempS.Length - 1));
                                            //sbRet_scriptArea.Append("]}]");
                                            break;
                                    }
                                }
                                sbRet_scriptArea.Append("datasets: [ ");
                                sbRet_scriptArea.Append(datasets);
                                sbRet_scriptArea.Append("]};");

                                sbRet_scriptArea.Append("var ctx" + detalhe.Attributes["name"].Value + " = $(\"#" + detalhe.Attributes["name"].Value + "\");");
                                sbRet_scriptArea.Append("var chart" + detalhe.Attributes["name"].Value + " = new Chart(ctx" + detalhe.Attributes["name"].Value + ", { type: '" + detalhe.Attributes["tipo"].Value + "', data: data" + detalhe.Attributes["name"].Value);
                                sbRet_scriptArea.Append(", options:{ responsive: true"+ /*", scale: {ticks: {stepSize: 20,beginAtZero: true,max: 100}}"+*/ ",tooltips: {callbacks: {label: function (tooltipItem, data) {var subDataset = data.datasets[tooltipItem.datasetIndex];var precentage = parseFloat(subDataset.data[tooltipItem.index]);return precentage.formatMoney() + '%';}}}}");
                                sbRet_scriptArea.Append("});");
                                break;
                        }
                    }
                    sbRet.Append("</div>");//FECHA ITEM
                    sbRet.Append("</div>");//FECHA OBJETO
                }
                sbRet.Append("</div>");
            }
            sbRet_nav.Append("</ul>");

            sHtml = sbRet.ToString();
            sScriptArea = sbRet_scriptArea.ToString();
            sNav = sbRet_nav.ToString();

        }
        public void LoadDataBaseDashboard(int anoAtual, int mesAtual)
        {
            string errorPosition = "";
            try
            {
                decimal o = 0;
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path + @"\Content\DashBoards\"+ appSettings.Ambiente + "DashBoardDataBase.xml");

                DateTime dtAnterior = new DateTime(anoAtual, mesAtual, 1).AddMonths(-11);
                DateTime dtAntual = new DateTime(anoAtual, mesAtual, 1);

                foreach (XmlNode element in xDoc["elements"].ChildNodes)
                {
                    string procedure = AddScheme(element.Attributes["procedure"].Value);
                    string tipo = element.Name;

                    errorPosition = tipo + " - " + procedure;
                    List<OleDbParameter> parameters = new List<OleDbParameter>();
                    DataTable result;
                    List<string> cols = new List<string>();
                    XmlAttribute attribute = null;
                    XmlNode xRow = null;
                    XmlNode xDataDataSet = null;
                    XmlNode xLabels = null;
                    XmlNode xDataDataSets = null;
                    XmlNode xDatas = null;

                    int c = 0;

                    switch (tipo)
                    {
                        case "mapaRegiao":
                            foreach (XmlNode n in element.SelectNodes("datas"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }
                            result = GetTable(procedure, parameters.ToArray());
                            string lastArtista = "";
                            List<XmlNode> xmlNodes = new List<XmlNode>();
                            foreach (DataRow row in result.Rows)
                            {
                                if (lastArtista != row["ARTISTA"].ToString())
                                {
                                    if (lastArtista != string.Empty)
                                    {
                                        xDatas = xDoc.CreateElement("datas");
                                        attribute = xDoc.CreateAttribute("artista");
                                        attribute.Value = lastArtista;
                                        xDatas.Attributes.Append(attribute);
                                        foreach (XmlNode xn in xmlNodes)
                                        {
                                            xDatas.AppendChild(xn);
                                        }
                                        element.AppendChild(xDatas);
                                        xmlNodes = new List<XmlNode>();
                                    }
                                    lastArtista = row["ARTISTA"].ToString();
                                }

                                XmlNode xData = xDoc.CreateElement("data");
                                attribute = xDoc.CreateAttribute("id");
                                attribute.Value = row["REGIAO"].ToString();
                                xData.Attributes.Append(attribute);
                                xData.InnerText = row["QTD_SHOWS"].ToString();
                                xmlNodes.Add(xData);
                            }
                            if (lastArtista != string.Empty)
                            {
                                xDatas = xDoc.CreateElement("datas");
                                attribute = xDoc.CreateAttribute("artista");
                                attribute.Value = lastArtista;
                                xDatas.Attributes.Append(attribute);
                                foreach (XmlNode xn in xmlNodes)
                                {
                                    xDatas.AppendChild(xn);
                                }
                                element.AppendChild(xDatas);
                            }
                            break;
                        case "piramid":
                            foreach (XmlNode n in element.SelectNodes("data"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }
                            result = GetTable(procedure, parameters.ToArray());
                            foreach (DataRow row in result.Rows)
                            {
                                XmlNode xData = xDoc.CreateElement("data");

                                o = 0;
                                attribute = xDoc.CreateAttribute("total");
                                if (decimal.TryParse(row["EBITIDA_PROJETADO"].ToString(), out o))
                                    attribute.Value = o.ToString("0.#########0", CultureInfo.CreateSpecificCulture(Helpers.appSettings._User.Culture));
                                xData.Attributes.Append(attribute);

                                o = 0;
                                attribute = xDoc.CreateAttribute("atual");
                                if (decimal.TryParse(row["VALOR"].ToString(), out o))
                                    attribute.Value = o.ToString("0.#########0", CultureInfo.CreateSpecificCulture(Helpers.appSettings._User.Culture));
                                xData.Attributes.Append(attribute);

                                attribute = xDoc.CreateAttribute("artist");
                                attribute.Value = row["ARTISTA"].ToString();
                                xData.Attributes.Append(attribute);

                                o = 0;
                                attribute = xDoc.CreateAttribute("percentual");
                                if (decimal.TryParse(row["PERC"].ToString(), out o))
                                    attribute.Value = o.ToString("0.#########0", CultureInfo.CreateSpecificCulture(Helpers.appSettings._User.Culture));
                                xData.Attributes.Append(attribute);

                                xData.InnerText = row["PROJETO"].ToString();

                                element.AppendChild(xData);
                            }

                            break;
                        case "radar":
                            foreach (XmlNode n in element.SelectSingleNode("labels").SelectNodes("label"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }

                            foreach (XmlNode n in element.SelectSingleNode("datas").SelectNodes("data"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }

                            result = GetTable(procedure, parameters.ToArray());

                            foreach (DataRow row in result.Rows)
                            {
                                XmlNode xLabel = xDoc.CreateElement("label");
                                xLabel.InnerText = row["LABEL"].ToString();
                                element.SelectSingleNode("labels").AppendChild(xLabel);

                                o = 0;
                                XmlNode xData = xDoc.CreateElement("data");
                                if (decimal.TryParse(row["DATA"].ToString(), out o))
                                    xData.InnerText = o.ToString("#0", CultureInfo.CreateSpecificCulture(Helpers.appSettings._User.Culture));

                                element.SelectSingleNode("datas").AppendChild(xData);
                            }

                            break;
                        case "metric":
                            foreach (XmlNode n in element.SelectSingleNode("col").SelectNodes("item"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }

                            parameters.Add(AddParameter("P_ANO", anoAtual - 2000));
                            parameters.Add(AddParameter("P_MES_REFERENCIA", mesAtual));
                            result = GetTable(procedure, parameters.ToArray());

                            int count = 0;

                            foreach (DataRow row in result.Rows)
                            {
                                XmlNode xItem = xDoc.CreateElement("item");

                                attribute = xDoc.CreateAttribute("name");
                                attribute.Value = row["CLASSIFICACAO"].ToString();
                                xItem.Attributes.Append(attribute);

                                attribute = xDoc.CreateAttribute("color");
                                attribute.Value = "rgba(0, 0, 0,1)";
                                xItem.Attributes.Append(attribute);

                                attribute = xDoc.CreateAttribute("barcolor");
                                if (count < int.Parse(element.Attributes["block"].Value))
                                    attribute.Value = "rgba(0, 72, 70,1)";
                                else
                                    attribute.Value = "rgba(255, 165, 0,1)";

                                if (int.Parse(element.Attributes["block"].Value) == 99)
                                {
                                    int iAtual = 0;
                                    int iPlan = 0;
                                    int.TryParse(row["ATUAL"].ToString(), out iAtual);
                                    int.TryParse(row["PLAN"].ToString(), out iPlan);
                                    if (iAtual < iPlan)
                                        attribute.Value = "rgba(204, 0, 0, 1)";
                                }

                                xItem.Attributes.Append(attribute);

                                int iO = 0;

                                xRow = xDoc.CreateElement("ATUAL");
                                iO = 0;
                                int.TryParse(row["ATUAL"].ToString(), out iO);
                                xRow.InnerText = iO.ToString();
                                xItem.AppendChild(xRow);

                                xRow = xDoc.CreateElement("TOTALFCAST");
                                iO = 0;
                                int.TryParse(row["FCAST_YTD"].ToString(), out iO);
                                xRow.InnerText = iO.ToString();
                                xItem.AppendChild(xRow);

                                xRow = xDoc.CreateElement("TOTALPLAN");
                                iO = 0;
                                int.TryParse(row["PLAN_YTD"].ToString(), out iO);
                                xRow.InnerText = iO.ToString();
                                xItem.AppendChild(xRow);

                                xRow = xDoc.CreateElement("FCAST");
                                iO = 0;
                                int.TryParse(row["FCAST"].ToString(), out iO);
                                xRow.InnerText = iO.ToString();
                                xItem.AppendChild(xRow);

                                xRow = xDoc.CreateElement("PLAN");
                                iO = 0;
                                int.TryParse(row["PLAN"].ToString(), out iO);
                                xRow.InnerText = iO.ToString();
                                xItem.AppendChild(xRow);

                                element.SelectSingleNode("col").AppendChild(xItem);

                                count++;
                            }
                            break;
                        case "tableIRP":
                            foreach (XmlNode n in element.SelectNodes("row"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }
                            var tables = GetTables(procedure, parameters.ToArray());
                            result = tables[tables.Count - 1];
                            foreach (XmlNode col in element["header"].ChildNodes)
                            {
                                cols.Add(col.Name);
                            }
                            foreach (DataRow row in result.Rows)
                            {
                                xRow = xDoc.CreateElement("row");
                                foreach (var col in cols)
                                {
                                    o = 0;
                                    XmlNode xCol = xDoc.CreateElement(col);
                                    if (decimal.TryParse(row[col].ToString(), out o))
                                        if (row[col].GetType() == Type.GetType("System.Decimal"))
                                            xCol.InnerText = o.ToString("#,#0.#0", CultureInfo.CreateSpecificCulture(Helpers.appSettings._User.Culture));
                                        else
                                            xCol.InnerText = o.ToString("#,#0", CultureInfo.CreateSpecificCulture(Helpers.appSettings._User.Culture));
                                    else
                                    {
                                        if (col == "IRP")
                                            xCol.InnerText = "0";
                                        else
                                            xCol.InnerText = row[col].ToString();
                                    }
                                    xRow.AppendChild(xCol);
                                }
                                element.AppendChild(xRow);
                            }
                            break;
                        case "barChartMultiBarAndLine":
                            foreach (XmlNode n in element.SelectNodes("labels"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }

                            foreach (XmlNode n in element.SelectNodes("datasets"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }

                            xLabels = xDoc.CreateElement("labels");
                            xDataDataSets = xDoc.CreateElement("datasets");


                            for (int i = 0; i < 12; i++)
                            {
                                
                                XmlNode xLabel = xDoc.CreateElement("label");
                                if (element.Attributes["useJanToDec"] != null)
                                {
                                    if (element.Attributes["useJanToDec"].Value == "True")
                                    {
                                        xLabel.InnerText = new DateTime(DateTime.Now.Year, 1, 1).AddMonths(i).ToString("MMM");
                                    }
                                }
                                else
                                    xLabel.InnerText = dtAnterior.AddMonths(i).ToString("MMM");
                                xLabels.AppendChild(xLabel);
                            }
                            foreach (DataTable tbl in GetTables(procedure, parameters.ToArray()))
                            {
                                c = 0;

                                for (int i = 0; i < tbl.Columns.Count; i++)
                                {
                                    XmlAttribute attributeTipo, attributeLabel, barColor;

                                    if (i + 1 == tbl.Columns.Count)
                                    {
                                        xDataDataSet = xDoc.CreateElement("dataset");

                                        attributeTipo = xDoc.CreateAttribute("tipo");
                                        attributeTipo.Value = "line";
                                        xDataDataSet.Attributes.Append(attributeTipo);

                                        attributeLabel = xDoc.CreateAttribute("label");
                                        attributeLabel.Value = tbl.Columns[i].ColumnName;
                                        xDataDataSet.Attributes.Append(attributeLabel);

                                        barColor = xDoc.CreateAttribute("color");
                                        barColor.Value = "rgba(70, 200, 70, 1)";
                                        xDataDataSet.Attributes.Append(barColor);

                                        foreach (DataRow row in tbl.Rows)
                                        {
                                            XmlNode xData = xDoc.CreateElement("data");
                                            xData.InnerText = decimal.Parse(row[i].ToString()).ToString("#0");
                                            xDataDataSet.AppendChild(xData);
                                        }
                                    }
                                    else
                                    {
                                        xDataDataSet = xDoc.CreateElement("dataset");

                                        attributeTipo = xDoc.CreateAttribute("tipo");
                                        attributeTipo.Value = "multiBar";
                                        xDataDataSet.Attributes.Append(attributeTipo);

                                        XmlNode xDataMultibar = xDoc.CreateElement("multibar");

                                        attributeLabel = xDoc.CreateAttribute("label");
                                        attributeLabel.Value = tbl.Columns[i].ColumnName;
                                        xDataMultibar.Attributes.Append(attributeLabel);

                                        barColor = xDoc.CreateAttribute("barColor");
                                        barColor.Value = GetColor(c++);
                                        xDataMultibar.Attributes.Append(barColor);

                                        foreach (DataRow row in tbl.Rows)
                                        {
                                            XmlNode xData = xDoc.CreateElement("data");
                                            xData.InnerText = decimal.Parse(row[i].ToString()).ToString("#0");
                                            xDataMultibar.AppendChild(xData);
                                        }
                                        xDataDataSet.AppendChild(xDataMultibar);
                                    }
                                    xDataDataSets.AppendChild(xDataDataSet);
                                }
                            }
                            element.AppendChild(xLabels);
                            element.AppendChild(xDataDataSets);
                            break;
                        case "barChartSingleLine":
                            foreach (XmlNode n in element.SelectNodes("labels"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }

                            foreach (XmlNode n in element.SelectNodes("datasets"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }
                            xLabels = xDoc.CreateElement("labels");
                            xDataDataSets = xDoc.CreateElement("datasets");

                            for (int i = 0; i < 12; i++)
                            {
                                XmlNode xLabel = xDoc.CreateElement("label");
                                xLabel.InnerText = dtAnterior.AddMonths(i).ToString("MMM");
                                xLabels.AppendChild(xLabel);
                            }


                            if (element.Attributes["param"] != null)
                            {
                                switch (element.Attributes["param"].Value)
                                {
                                    case "ano2digitosmes":
                                        parameters.Add(AddParameter("P_ANO", anoAtual - 2000));
                                        parameters.Add(AddParameter("P_MES", mesAtual));
                                        break;
                                }
                            }

                            foreach (DataTable tbl in GetTables(procedure, parameters.ToArray()))
                            {
                                foreach (DataRow row in tbl.Rows)
                                {
                                    xDataDataSet = null;

                                    XmlAttribute attributeTipo, attributeLabel, attributeHidden, attributebarColor;

                                    xDataDataSet = xDoc.CreateElement("dataset");

                                    attributeTipo = xDoc.CreateAttribute("tipo");
                                    attributeTipo.Value = "bar";
                                    xDataDataSet.Attributes.Append(attributeTipo);

                                    attributeLabel = xDoc.CreateAttribute("label");
                                    attributeLabel.Value = "'Total'";
                                    xDataDataSet.Attributes.Append(attributeLabel);

                                    attributeHidden = xDoc.CreateAttribute("hidden");
                                    attributeHidden.Value = "false";
                                    xDataDataSet.Attributes.Append(attributeHidden);

                                    attributebarColor = xDoc.CreateAttribute("barColor");
                                    attributebarColor.Value = "'rgba(125, 125, 125, 1)'";
                                    xDataDataSet.Attributes.Append(attributebarColor);

                                    for (int i = 0; i < tbl.Columns.Count; i++)
                                    {
                                        XmlNode xData = xDoc.CreateElement("data");
                                        xData.InnerText = decimal.Parse(row[i].ToString()).ToString("#0");
                                        xDataDataSet.AppendChild(xData);
                                    }
                                    xDataDataSets.AppendChild(xDataDataSet);
                                }
                                element.AppendChild(xLabels);
                                element.AppendChild(xDataDataSets);
                            }
                            break;
                        case "barChartComplex":
                            foreach (XmlNode n in element.SelectNodes("labels"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }

                            foreach (XmlNode n in element.SelectNodes("datasets"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }

                            xLabels = xDoc.CreateElement("labels");
                            xDataDataSets = xDoc.CreateElement("datasets");

                            for (int i = 0; i < 12; i++)
                            {
                                XmlNode xLabel = xDoc.CreateElement("label");
                                xLabel.InnerText = dtAnterior.AddMonths(i).ToString("MMM");
                                xLabels.AppendChild(xLabel);
                            }

                            xDataDataSet = null;
                            c = 0;
                            if (element.Attributes["param"] != null)
                            {
                                switch (element.Attributes["param"].Value)
                                {
                                    case "ano2digitosmes":
                                        parameters.Add(AddParameter("P_ANO", anoAtual - 2000));
                                        parameters.Add(AddParameter("P_MES", mesAtual));
                                        break;
                                }
                            }

                            foreach (DataTable tbl in GetTables(procedure, parameters.ToArray()))
                            {
                                string label = "";
                                foreach (DataRow row in tbl.Rows)
                                {
                                    if (row["LABEL"].ToString() != label)
                                    {
                                        if (label != string.Empty)
                                            xDataDataSets.AppendChild(xDataDataSet);

                                        label = row["LABEL"].ToString();

                                        xDataDataSet = xDoc.CreateElement("dataset");

                                        XmlAttribute attributeTipo, attributeLabel, attributeHidden, attributebarColor, attributeColor;

                                        switch (row["LABEL"].ToString())
                                        {
                                            case "BARHIDDEN":
                                                attributeTipo = xDoc.CreateAttribute("tipo");
                                                attributeTipo.Value = "bar";
                                                xDataDataSet.Attributes.Append(attributeTipo);

                                                attributeLabel = xDoc.CreateAttribute("label");
                                                attributeLabel.Value = "'Total'";
                                                xDataDataSet.Attributes.Append(attributeLabel);

                                                attributeHidden = xDoc.CreateAttribute("hidden");
                                                attributeHidden.Value = "true";
                                                xDataDataSet.Attributes.Append(attributeHidden);

                                                attributebarColor = xDoc.CreateAttribute("barColor");
                                                attributebarColor.Value = "'rgba(125, 125, 125, 1)'";
                                                xDataDataSet.Attributes.Append(attributebarColor);
                                                break;
                                            case "BARNORMAL":
                                                attributeTipo = xDoc.CreateAttribute("tipo");
                                                attributeTipo.Value = "bar";
                                                xDataDataSet.Attributes.Append(attributeTipo);

                                                attributeLabel = xDoc.CreateAttribute("label");
                                                attributeLabel.Value = "'Total'";
                                                xDataDataSet.Attributes.Append(attributeLabel);

                                                attributeHidden = xDoc.CreateAttribute("hidden");
                                                attributeHidden.Value = "false";
                                                xDataDataSet.Attributes.Append(attributeHidden);

                                                attributebarColor = xDoc.CreateAttribute("barColor");
                                                attributebarColor.Value = "'rgba(125, 125, 125, 1)'";
                                                xDataDataSet.Attributes.Append(attributebarColor);
                                                break;
                                            default:
                                                attributeTipo = xDoc.CreateAttribute("tipo");
                                                attributeTipo.Value = "line";
                                                xDataDataSet.Attributes.Append(attributeTipo);

                                                attributeLabel = xDoc.CreateAttribute("label");
                                                attributeLabel.Value = row["LABEL"].ToString();
                                                xDataDataSet.Attributes.Append(attributeLabel);

                                                attributeColor = xDoc.CreateAttribute("color");
                                                attributeColor.Value = GetColor(c++);
                                                xDataDataSet.Attributes.Append(attributeColor);
                                                break;
                                        }
                                    }
                                    XmlNode xData = xDoc.CreateElement("data");
                                    xData.InnerText = decimal.Parse(row["DATA"].ToString()).ToString("#0");
                                    xDataDataSet.AppendChild(xData);
                                }
                                if (xDataDataSet != null)
                                    xDataDataSets.AppendChild(xDataDataSet);
                            }

                            element.AppendChild(xLabels);
                            element.AppendChild(xDataDataSets);
                            break;
                        case "pieChartSimple":
                            foreach (XmlNode n in element.SelectNodes("labels"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }

                            foreach (XmlNode n in element.SelectNodes("datas"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }
                            xLabels = xDoc.CreateElement("labels");
                            xDatas = xDoc.CreateElement("datas");

                            if (element.Attributes["param"] != null)
                            {
                                switch (element.Attributes["param"].Value)
                                {
                                    case "ano2digitosmes":
                                        parameters.Add(AddParameter("P_ANO", anoAtual - 2000));
                                        parameters.Add(AddParameter("P_MES", mesAtual));
                                        break;
                                }
                            }

                            result = GetTable(procedure, parameters.ToArray());
                            c = 0;
                            foreach (DataRow row in result.Rows)
                            {
                                XmlNode xLabel = xDoc.CreateElement("label");
                                XmlNode xData = xDoc.CreateElement("data");
                                xLabel.InnerText = row["LABEL"].ToString();
                                if (decimal.Parse(row["DATA"].ToString()) < 0)
                                    xData.InnerText = "0";
                                else
                                    xData.InnerText = decimal.Parse(row["DATA"].ToString()).ToString("#0");

                                XmlAttribute attributeColor = xDoc.CreateAttribute("color");
                                attributeColor.Value = GetColor(c++);
                                if (c > 5) c = 0;
                                xData.Attributes.Append(attributeColor);

                                xLabels.AppendChild(xLabel);
                                xDatas.AppendChild(xData);
                            }
                            element.AppendChild(xLabels);
                            element.AppendChild(xDatas);
                            break;
                        case "tanque":
                            parameters.Add(AddParameter("P_MES", mesAtual));
                            parameters.Add(AddParameter("P_ANO", anoAtual));
                            result = GetTable(procedure, parameters.ToArray());
                            decimal zero = 0;
                            if (result.Rows.Count > 0)
                            {
                                decimal.TryParse(result.Rows[0]["YTD"].ToString(), out zero);
                                element["YTD"].InnerText = zero.ToString("#0");
                                zero = 0;
                                decimal.TryParse(result.Rows[0]["YTD_PLAN"].ToString(), out zero);
                                element["YTD_PLAN"].InnerText = zero.ToString("#0");
                                zero = 0;
                                decimal.TryParse(result.Rows[0]["YTD_REAL"].ToString(), out zero);
                                element["YTD_REAL"].InnerText = zero.ToString("#0");
                                zero = 0;
                                decimal.TryParse(result.Rows[0]["TOTAL_ANO"].ToString(), out zero);
                                element["TOTAL_ANO"].InnerText = zero.ToString("#0");
                                zero = 0;
                                decimal.TryParse(result.Rows[0]["TOTAL_ANO_PLAN"].ToString(), out zero);
                                element["TOTAL_ANO_PLAN"].InnerText = zero.ToString("#0");

                                zero = 0;
                                decimal.TryParse(result.Rows[0]["YTD_MES"].ToString(), out zero);
                                element["YTD_MES"].InnerText = zero.ToString("#0");
                                zero = 0;
                                decimal.TryParse(result.Rows[0]["YTD_PLAN_MES"].ToString(), out zero);
                                element["YTD_PLAN_MES"].InnerText = zero.ToString("#0");
                                zero = 0;
                                decimal.TryParse(result.Rows[0]["YTD_REAL_MES"].ToString(), out zero);
                                element["YTD_REAL_MES"].InnerText = zero.ToString("#0");
                            }
                            else
                            {
                                element["YTD"].InnerText = "0";
                                element["YTD_PLAN"].InnerText = "0";
                                element["YTD_REAL"].InnerText = "0";
                                element["TOTAL_ANO"].InnerText = "0";
                                element["TOTAL_ANO_PLAN"].InnerText = "0";

                                element["YTD_MES"].InnerText = "0";
                                element["YTD_PLAN_MES"].InnerText = "0";
                                element["YTD_REAL_MES"].InnerText = "0";
                            }
                            break;

                        case "table":
                            foreach (XmlNode n in element.SelectNodes("row"))
                            {
                                n.ParentNode.RemoveChild(n);
                            }

                            switch (element.Attributes["param"].Value)
                            {
                                case "anoemes":
                                    parameters.Add(AddParameter("P_ANO", anoAtual));
                                    parameters.Add(AddParameter("P_MES", mesAtual));
                                    break;
                                case "anomes":
                                    parameters.Add(AddParameter("P_ANOMES", (anoAtual * 100) + mesAtual));
                                    break;
                                case "ano2digitos":
                                    parameters.Add(AddParameter("P_ANO", anoAtual - 2000));
                                    break;
                                case "stringMesEAno":
                                    parameters.Add(AddParameter("P_MES", mesAtual.ToString("00")));
                                    parameters.Add(AddParameter("P_ANO", anoAtual.ToString("0000")));
                                    break;
                                case "ano2digitosmes":
                                    parameters.Add(AddParameter("P_ANO", (anoAtual - 2000).ToString("00")));
                                    parameters.Add(AddParameter("P_MES", mesAtual.ToString("00")));
                                    break;
                                default:
                                    if (element.Attributes["param"].Value != string.Empty)
                                    {
                                        foreach (string _s in element.Attributes["param"].Value.Split(','))
                                        {
                                            string[] _sI = _s.Split(':');
                                            switch (_sI[0])
                                            {
                                                case "int":
                                                    parameters.Add(AddParameter(_sI[2], int.Parse(_sI[1])));
                                                    break;
                                                case "decimal":
                                                    parameters.Add(AddParameter(_sI[2], decimal.Parse(_sI[1])));
                                                    break;
                                                default:
                                                    parameters.Add(AddParameter(_sI[2], _sI[1]));
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                            }

                            if (parameters.Count > 0)
                                result = GetTable(procedure, parameters.ToArray());
                            else
                                result = GetTable(procedure);
                            if (element["header"] != null)
                            {
                                foreach (XmlNode col in element["header"].ChildNodes)
                                {
                                    cols.Add(col.Name);
                                }
                            }
                            c = 0;
                            foreach (DataRow row in result.Rows)
                            {
                                bool hasCube = false;
                                bool redIfZeroLess = false;
                                bool greenIfZeroplus = false;
                                int colCount = 0;

                                if (element.Attributes["hasCube"] != null)
                                    hasCube = element.Attributes["hasCube"].Value == "True";
                                if (element.Attributes["redIfZeroLess"] != null)
                                    redIfZeroLess = element.Attributes["redIfZeroLess"].Value == "True";
                                if (element.Attributes["greenIfZeroplus"] != null)
                                    greenIfZeroplus = element.Attributes["greenIfZeroplus"].Value == "True";

                                xRow = xDoc.CreateElement("row");
                                if (cols.Count > 0)
                                {
                                    foreach (var col in cols)
                                    {
                                        colCount++;
                                        o = 0;
                                        XmlNode xCol = xDoc.CreateElement(col);
                                        if (decimal.TryParse(row[col].ToString(), out o))
                                        {
                                            if (redIfZeroLess)
                                            {
                                                if (o <= 0)
                                                {
                                                    if (colCount >= int.Parse(element.Attributes["ifZeroLessPlusCol"].Value))
                                                    {
                                                        attribute = xDoc.CreateAttribute("style");
                                                        attribute.Value = "background-color:rgba(255, 0, 0, 1);color:rgba(255 , 255, 255, 1);";
                                                        xCol.Attributes.Append(attribute);
                                                    }
                                                }
                                            }

                                            if (greenIfZeroplus)
                                            {
                                                if (o > 0)
                                                {
                                                    if (colCount >= int.Parse(element.Attributes["ifZeroLessPlusCol"].Value))
                                                    {
                                                        attribute = xDoc.CreateAttribute("style");
                                                        attribute.Value = "background-color:rgba(125, 255, 125, 1);color:rgba(0, 0, 0, 1);";
                                                        xCol.Attributes.Append(attribute);
                                                    }
                                                }
                                            }

                                            xCol.InnerText = o.ToString("#,##0", CultureInfo.CreateSpecificCulture(Helpers.appSettings._User.Culture));
                                            if (o >= 1000000)
                                            {
                                                long num = (long)o;
                                                xCol.InnerText = (num / 1000000D).ToString("#,##0", CultureInfo.CreateSpecificCulture(Helpers.appSettings._User.Culture)) + "M";
                                            }
                                            if (element.Attributes["noseoarator"] != null)
                                            {
                                                if (element.Attributes["noseoarator"].Value.IndexOf(col) > -1)
                                                    xCol.InnerText = o.ToString("#0", CultureInfo.CreateSpecificCulture(Helpers.appSettings._User.Culture));
                                            }
                                            if (element.Attributes["decimais"] != null)
                                            {
                                                if (element.Attributes["decimais"].Value.Split('|')[1].IndexOf(col) > -1)
                                                    xCol.InnerText = o.ToString("#,##0." + new String('0', int.Parse(element.Attributes["decimais"].Value.Split('|')[0])), CultureInfo.CreateSpecificCulture(Helpers.appSettings._User.Culture));
                                            }
                                        }
                                        else
                                        {
                                            if (cols[0] == col && hasCube && c < 3)
                                            {
                                                attribute = xDoc.CreateAttribute("cube");
                                                if (c == 0)
                                                    attribute.Value = "rgba(0, 72, 70,1)";
                                                else
                                                    attribute.Value = "rgba(255,165,0,1)";
                                                xCol.Attributes.Append(attribute);
                                            }
                                            xCol.InnerText = row[col].ToString();
                                        }
                                        xRow.AppendChild(xCol);
                                    }
                                }
                                else
                                {
                                    o = 0;
                                    XmlNode xCol = xDoc.CreateElement("VALOR");
                                    if (decimal.TryParse(row["VALOR"].ToString(), out o))
                                        xCol.InnerText = o.ToString("#.#", CultureInfo.CreateSpecificCulture(Helpers.appSettings._User.Culture));
                                    else
                                        xCol.InnerText = row["VALOR"].ToString();
                                    xRow.AppendChild(xCol);
                                }
                                element.AppendChild(xRow);
                                c++;
                            }
                            break;

                        case "tableGTS":
                            switch (element.Attributes["param"].Value)
                            {
                                case "anoemes":
                                    parameters.Add(AddParameter("P_ANO", anoAtual));
                                    parameters.Add(AddParameter("P_MES", mesAtual));
                                    break;
                                case "anomes":
                                    parameters.Add(AddParameter("P_ANOMES", (anoAtual * 100) + mesAtual));
                                    break;
                                case "ano2digitos":
                                    parameters.Add(AddParameter("P_ANO", anoAtual - 2000));
                                    break;
                                case "stringMesEAno":
                                    parameters.Add(AddParameter("P_MES", mesAtual.ToString("00")));
                                    parameters.Add(AddParameter("P_ANO", anoAtual.ToString("0000")));
                                    break;
                                case "ano2digitosmes":
                                    parameters.Add(AddParameter("P_ANO", (anoAtual - 2000).ToString("00")));
                                    parameters.Add(AddParameter("P_MES", mesAtual.ToString("00")));
                                    break;
                                default:
                                    if (element.Attributes["param"].Value != string.Empty)
                                    {
                                        foreach (string _s in element.Attributes["param"].Value.Split(','))
                                        {
                                            string[] _sI = _s.Split(':');
                                            switch (_sI[0])
                                            {
                                                case "int":
                                                    parameters.Add(AddParameter(_sI[2], int.Parse(_sI[1])));
                                                    break;
                                                case "decimal":
                                                    parameters.Add(AddParameter(_sI[2], decimal.Parse(_sI[1])));
                                                    break;
                                                default:
                                                    parameters.Add(AddParameter(_sI[2], _sI[1]));
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                            }

                            if (parameters.Count > 0)
                                result = GetTable(procedure, parameters.ToArray());
                            else
                                result = GetTable(procedure);
                            if (element["header"] != null)
                            {
                                foreach (XmlNode col in element["header"].ChildNodes)
                                {
                                    cols.Add(col.Name);
                                }
                            }
                            c = 0;
                            foreach (DataRow row in result.Rows)
                            {
                                o = 0;
                                if (decimal.TryParse(row["VALOR"].ToString(), out o))
                                    element.ChildNodes[c].ChildNodes[0].InnerText = o.ToString("#.#", CultureInfo.CreateSpecificCulture(Helpers.appSettings._User.Culture));
                                else
                                    element.ChildNodes[c].ChildNodes[0].InnerText = row["VALOR"].ToString();
                                c++;
                            }
                            break;
                    }
                }

                xDoc.Save(path + @"\Content\DashBoards\" + appSettings.Ambiente + "DashBoardDataBase.xml");
            }
            catch (Exception ex)
            {
                throw new Exception(errorPosition + " -- " + ex.Message);
            }
        }
        protected string GetColor(int posArray)
        {
            return colorsArray[posArray];
        }
        public void LoadDataToXml()
        {
            XmlDocument xInput = new XmlDocument();
            xInput.Load(path + @"\Content\DashBoards\" + appSettings.Ambiente + "DashBoardDataBase.xml");

            XmlDocument xOutput= new XmlDocument();
            xOutput.Load(path + @"\Content\DashBoards\" + appSettings.Ambiente + "DashBoard.xml");

            foreach (XmlNode nodeInput in xInput.SelectNodes("//*[@id]"))//["elements"].ChildNodes)
            {
                XmlNodeList nodes = xOutput.SelectNodes("//*[@id='" + nodeInput.Attributes["id"].Value + "']");
                foreach (XmlNode node in nodes)
                {
                    if (node != null)
                    {
                        node.InnerXml = nodeInput.InnerXml;
                    }
                }

            }
            xOutput.Save(path + @"\Content\DashBoards\" + appSettings.Ambiente + "DashBoard.xml");
        }

    }
}