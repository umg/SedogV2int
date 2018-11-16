using Microsoft.Web.Services3.Design;
using SEDOGv2.net.umusic.catalogueservices;
using System;
using System.Collections.Generic;
using UMI.ITDI.CentralCatalogue.WSSecurity.UsernameAssertionLibrary;

namespace SEDOGv2.Helpers
{
    public class GetCapa
    {
        SecureDataServices wse;
        Product p;
        string r2ToeURL;
        //r2Release.WSReleaseResult release;
        //r2Resource.WSResourceSearchResult resource;

        string ProdutoSemCapa = @"http://itdi.global.umusic.net/WResourcePreviewer/TOECache/default_TOE.gif?ranNum=98138285";
        string ServidorDeCapas = @"http://ukbcewvapp076/imageserver/Catalogo400/";

        public static string GetCoverUrlString(string UPC, string UPC_DIG)
        {
            GetCapa gCapa = new GetCapa();
            try
            {
                if (gCapa.ImgExists(gCapa.ServidorDeCapas + UPC + ".jpg"))
                    return gCapa.ServidorDeCapas + UPC + ".jpg";
                else
                {
                    if (gCapa.Busca_Produto_R2(UPC.ToUpper().Replace("U", "")))
                    {
                        return gCapa.r2ToeURL;
                    }
                    else
                    {
                        if (gCapa.Busca_Produto_wse(UPC.ToUpper().Replace("U", "")))
                            return gCapa.p.coverArt.coverArtToenailUrl;
                        else
                            if (gCapa.ImgExists(gCapa.ServidorDeCapas + UPC_DIG + ".jpg"))
                            return gCapa.ServidorDeCapas + UPC_DIG + ".jpg";
                        else
                        {
                            if (gCapa.Busca_Produto_R2(UPC_DIG.ToUpper().Replace("U", "")))
                            {
                                return gCapa.r2ToeURL;
                            }
                            else
                            {
                                if (gCapa.Busca_Produto_wse(UPC_DIG.ToUpper().Replace("U", "")))
                                    return gCapa.p.coverArt.coverArtToenailUrl;
                                else
                                    return gCapa.ProdutoSemCapa;
                            }
                        }
                    }
                }
            }
            catch
            {
                return gCapa.ProdutoSemCapa;
            }

        }
        /* formata string de consulta para a quantidade necessária de caracteres */

        private string FormatUPC(string _upc)
        {
            string _mask = "00000000000000";
            if (_upc.Length < _mask.Length)
                return _mask.Substring(0, _mask.Length - _upc.Length) + _upc;
            else
                return _upc;
        }

        /* procura pelo código de produto no WebService */

        private bool Busca_Produto_wse(string busca)
        {
            wse = new SecureDataServices();
            UsernameClientAssertion assert = new UsernameClientAssertion("ws-br@umusic.com", "79128971661085433221941696121923151207671510584144");
            List<PolicyAssertion> assertions = new List<PolicyAssertion>();
            assertions.Add(assert);
            wse.SetPolicy(new Policy(assertions.ToArray()));

            string upc;
            upc = FormatUPC(busca);
            try
            {
                long productid;
                productid = wse.GetProductIdByUpc(upc);
                byte[] asset = new byte[1024];
                p = wse.GetProductById(31629808175);// productid);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Busca_Produto_R2(string busca)
        {
            bool ret = false;
            System.Net.NetworkCredential nc = new System.Net.NetworkCredential("umgbrazil", "yuqU49Fa");
            r2Release.WSReleaseSearchServiceService wsr = new r2Release.WSReleaseSearchServiceService();
            r2Release.WSSearchOption wso = new r2Release.WSSearchOption();
            r2Release.XMLControlBean xml = new r2Release.XMLControlBean();
            wsr.Credentials = nc;

            string upc;
            upc = FormatUPC(busca);
            try
            {

                //http://itdi.global.umusic.net/WResourcePreviewer/TOECache/338/15UMGIM46338_TOE.jpg?ranNum=460062218


                xml.includeResourceLinks = true;
                xml.includeResources = true;
                xml.includePackageComponents = true;

                wso.verbose = true;
                wso.maxResultRows = "500";
                wso.direction = "-1";
                var r = wsr.getIdByUPC(upc, false);
                var gbi = wsr.getById(r, wso);
                var s = wsr.getXML(gbi.upc, gbi.releaseId, gbi.companyId, xml);

                var isrcId = "";
                var isrc = "";
                System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
                xDoc.LoadXml(s);
                foreach (System.Xml.XmlNode rel in xDoc.ChildNodes)
                {
                    if (rel.Name.ToUpper() == "RELEASE")
                    {
                        foreach (System.Xml.XmlNode resLinks in rel.ChildNodes)
                        {
                            if (resLinks.Name.ToUpper() == "RESOURCELINKS")
                            {
                                foreach (System.Xml.XmlNode resLink in resLinks.ChildNodes)
                                {
                                    if (resLink.Attributes["primaryIndicator"] != null)
                                    {
                                        if (resLink.Attributes["primaryIndicator"].Value == "Y")
                                        {
                                            foreach (System.Xml.XmlNode res in resLink.ChildNodes[0].ChildNodes)
                                            {
                                                if (res.Name.ToUpper() == "ISRCS")
                                                {
                                                    isrcId = res.ChildNodes[0].Attributes["isrcId"].Value;
                                                    isrc = res.ChildNodes[0].Attributes["isrc"].Value;
                                                    r2ToeURL = "http://itdi.global.umusic.net/WResourcePreviewer/TOECache/" + isrc.Substring(9, 3) + "/" + isrc + "_TOE.jpg?ranNum=460062218";
                                                    ret = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return ret;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private bool ImgExists(string path)
        {
            bool exists = true;

            try
            {
                System.Net.WebRequest req = System.Net.WebRequest.Create(path);
                System.Net.WebResponse res = req.GetResponse();
            }
            catch
            {
                exists = false;
            }
            return exists;
        }
    }

}