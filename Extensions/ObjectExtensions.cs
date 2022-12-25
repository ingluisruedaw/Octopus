using System.Reflection;

namespace System;

public static class ObjectExtensions
{
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
}