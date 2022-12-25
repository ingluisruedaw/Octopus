using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Octopus.Extensions;

public static class Serializer
{
    public static string GetKeyString(object? key, Type type)
    {
        try
        {
            string pubKeyString;
            {
                var sw = new StringWriter();
                var xs = new XmlSerializer(type);
                xs.Serialize(sw, key);
                pubKeyString = sw.ToString();
            }

            XDocument doc = XDocument.Parse(pubKeyString);
            return doc.ToString();
        }
        catch (XmlException ex)
        {
            throw ex;
        }
    }

    public static string ReadXmlFileString(string path)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(File.ReadAllText(path));
            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xmlDoc.WriteTo(xw);
            return sw.ToString();
        }
        catch (XmlException ex)
        {
            throw ex;
        }
    }
}