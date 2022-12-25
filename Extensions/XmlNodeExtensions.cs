using System.Xml.Serialization;

namespace System.Xml;

public static class XmlNodeExtensions
{
    public static T ToObject<T>(this XmlNode node)
    {
        var stm = new MemoryStream();
        var stw = new StreamWriter(stm);
        stw.Write(node.OuterXml);
        stw.Flush();
        stm.Position = default;
        var ser = new XmlSerializer(typeof(T));

        return (T)(ser.Deserialize(stm));
    }
}
