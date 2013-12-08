using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using XMLContracts;


namespace Helper
{
    public class XMLGenerator
    {
        public static string getXMLForAuthenticationServer(AuthenticationServerResponseXML obj)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(AuthenticationServerResponseXML));
            //var subReq = new MyObject();
            StringWriter sww = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);
            xsSubmit.Serialize(writer, obj);
            var xml = sww.ToString(); // Your xml
            return xml;
        }

        public static string getXMLForAuthToken(authToken obj)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(authToken));
            //var subReq = new MyObject();
            StringWriter sww = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);
            xsSubmit.Serialize(writer, obj);
            var xml = sww.ToString(); // Your xml
            return xml;
        }

        public static string getXMLForReceiver(ReceiverResponseXML obj)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(ReceiverResponseXML));
            //var subReq = new MyObject();
            StringWriter sww = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);
            xsSubmit.Serialize(writer, obj);
            var xml = sww.ToString(); // Your xml
            return xml;
        }

        public static string getXMLForAuthenticationServer(AuthenticationServerResponseXMLFixed obj)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(AuthenticationServerResponseXMLFixed));
            //var subReq = new MyObject();
            StringWriter sww = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);
            xsSubmit.Serialize(writer, obj);
            var xml = sww.ToString(); // Your xml
            return xml;
        }

        public static string getXMLForAuthToken(authTokenFixed obj)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(authTokenFixed));
            //var subReq = new MyObject();
            StringWriter sww = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);
            xsSubmit.Serialize(writer, obj);
            var xml = sww.ToString(); // Your xml
            return xml;
        }

        public static string getXMLForReceiver(ReceiverResponseXMLFixed obj)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(ReceiverResponseXMLFixed));
            //var subReq = new MyObject();
            StringWriter sww = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);
            xsSubmit.Serialize(writer, obj);
            var xml = sww.ToString(); // Your xml
            return xml;
        }


        public static string getNodeValue(string xml,string nodePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            var node = doc.SelectSingleNode(nodePath);
            return node.InnerText;
        }

        public static string getNodeXML(string xml, string nodePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            var node = doc.SelectSingleNode(nodePath);
            return node.InnerXml;
        }
    }
}
