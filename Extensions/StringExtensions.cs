using System.Xml.Serialization;

namespace System;

public static class StringExtensions
{
    public static T ToSerialize<T>(this string content)
    {
        var sr = new StringReader(content);
        var xs = new XmlSerializer(typeof(T));
#pragma warning disable CS8603 // Possible null reference return.
        return (T)xs.Deserialize(sr);
#pragma warning restore CS8603 // Possible null reference return.
    }
}