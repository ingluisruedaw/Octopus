using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Xml;

public static class XmlNodeExtensions
{
    [DebuggerStepThrough]
    public static T ToObject<T>(this XmlNode node)
    {
        var stm = new MemoryStream();
        var stw = new StreamWriter(stm);
        stw.Write(node.OuterXml);
        stw.Flush();
        stm.Position = default;
        var ser = new XmlSerializer(typeof(T));

#pragma warning disable CS8603 // Possible null reference return.
        return (T)ser.Deserialize(stm);
#pragma warning restore CS8603 // Possible null reference return.
    }
}
