using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Diagnostics;

namespace System;

public static class ObjectExtensions
{
    [DebuggerStepThrough]
    public static bool PropsIsNulOrEmpty(this object obj)
    {
        var myType = obj.GetType();
        var props = new List<PropertyInfo>(myType.GetProperties());
        foreach (PropertyInfo prop in props)
        {
            var propValue = prop.GetValue(obj, null);
            if (propValue == null || propValue.Equals(string.Empty))
            {
                return true;
            }
        }

        return false;
    }

    [DebuggerStepThrough]
    public static string GetKeyString(this object obj)
    {
        try
        {
            var type = obj.GetType();
            string pubKeyString;
            {
                var sw = new StringWriter();
                var xs = new XmlSerializer(type);
                xs.Serialize(sw, obj);
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
}