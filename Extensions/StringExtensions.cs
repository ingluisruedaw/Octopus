﻿using System.Xml;
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

    public static string ReadXmlFileString(this string path)
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