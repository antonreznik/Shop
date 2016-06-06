using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace RealShop.WebRequests.NewPostAPI
{
    public static class RequestToNewPostApi
    {
        //метод для вычитки из xml файла городов где есть отделения Новой почты
        public static List<string> NewPostCities ()
        {
            List<string> NewPostCities = new List<string>();
            string City = "";
            //XmlReader xmlreader = XmlReader.Create(@"D:\Post.xml");
            XmlReader xmlreader = XmlReader.Create(@"h:\root\home\anton1982-001\www\site1\Post.xml");

            while(xmlreader.Read())
            {
                if((xmlreader.NodeType==XmlNodeType.Element) && (xmlreader.Name=="DescriptionRu"))
                {
                    City = xmlreader.ReadInnerXml();
                    NewPostCities.Add(City);
                }
            }

            return NewPostCities;
        }

        //метод для получения референса города Новой почты
        public static List<SelectListItem> GetRefCity(string City)
        {
            Dictionary<string, string> PostOffices = new Dictionary<string, string>();
            string CityInXml = "";
            string RefCity = "";
            //XmlReader xmlreader = XmlReader.Create(@"D:\Post.xml");
            XmlReader xmlreader = XmlReader.Create(@"h:\root\home\anton1982-001\www\site1\Post.xml");
            while (xmlreader.Read())
            {
                if ((xmlreader.NodeType == XmlNodeType.Element) && (xmlreader.Name == "DescriptionRu"))
                {
                    CityInXml = xmlreader.ReadInnerXml();
                }

                if ((xmlreader.NodeType == XmlNodeType.Element) && (xmlreader.Name == "Ref"))
                {
                    RefCity = xmlreader.ReadInnerXml();
                }

                if (CityInXml.Length>1 && RefCity.Length>1)
                {
                    PostOffices.Add(CityInXml, RefCity);
                    CityInXml = "";
                    RefCity = "";
                }
            }
            RefCity = PostOffices[City];
            return RequestForOffices(RefCity);
        }

        //метод для отправки запроса в API Новой почты для получения отделений по референсу города
        public static List<SelectListItem> RequestForOffices(string RefCity)
        {
            
            string office = "";
            List<SelectListItem> PostOffices = new List<SelectListItem>();

            var XML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                         "<file>\n" +
                         "<apiKey>cb72ad94344bc7d7c96fa85b518eeb2d</apiKey>\n" +
                         "<modelName>Address</modelName>\n" +
                         "<calledMethod>getWarehouses</calledMethod>\n" +
                         "<methodProperties>\n"+
                         "<CityRef>"+ RefCity +"</CityRef>\n"+
                         "</methodProperties>\n"+
                         "</file>\n";
            HttpWebRequest request = WebRequest.Create("https://api.novaposhta.ua/v2.0/xml/") as HttpWebRequest;
            request.Method = "Post";
            request.ContentType = "application/x-www-form-urlencoded";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(XML);
            request.ContentLength = data.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(data, 0, data.Length);
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));


                XmlReader xml = XmlReader.Create(response.GetResponseStream());
                while (xml.Read())
                {
                    if ((xml.NodeType == XmlNodeType.Element) && (xml.Name == "DescriptionRu"))
                    {
                        SelectListItem obj = new SelectListItem();
                        office = xml.ReadInnerXml();
                        obj.Text = office;
                        obj.Selected = false;
                        obj.Value = office;
                        //PostOffices.Add(office);
                        PostOffices.Add(obj);
                        obj = null;
                    }
                }
            }
            //GetNewPostCities();
            return PostOffices;          
        }

        //метод для получения городов новой почты
        public static void GetNewPostCities()
        {
            var XML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                         "<file>\n" +
                         "<apiKey>cb72ad94344bc7d7c96fa85b518eeb2d</apiKey>\n" +
                         "<modelName>Address</modelName>\n" +
                         "<calledMethod>getCities</calledMethod>\n" +
                         "<methodProperties/>\n"+
                         "</file>\n";
            HttpWebRequest request = WebRequest.Create("https://api.novaposhta.ua/v2.0/xml/") as HttpWebRequest;
            request.Method = "Post";
            request.ContentType = "application/x-www-form-urlencoded";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(XML);
            request.ContentLength = data.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(data, 0, data.Length);
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));

                XmlDocument xml = new XmlDocument();
                xml.Load(response.GetResponseStream());
                xml.Save("D:\\PostOfficesNew.xml");
            }
        }
    }
}