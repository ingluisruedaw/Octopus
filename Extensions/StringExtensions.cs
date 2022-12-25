using System.Xml.Serialization;

namespace System;

public static class StringExtensions
{
    public static T ToSerialize<T>(this string content)
    {
        var sr = new StringReader(content);
        var xs = new XmlSerializer(typeof(T));
        return (T)xs.Deserialize(sr);
    }
}