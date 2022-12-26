using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace System.Data;

public static class DataTableExtensions
{
    [DebuggerStepThrough]
    public static T ToCast<T>(this DataRow dataRow)
    {
        return GetItem<T>(dataRow);
    }

    [DebuggerStepThrough]
    public static List<T> ToList<T>(this DataTable dt)
    {
        List<T> data = new();
        foreach (DataRow row in dt.Rows)
        {
            T item = GetItem<T>(row);
            data.Add(item);
        }
        return data;
    }

    [DebuggerStepThrough]
    private static T GetItem<T>(DataRow dr)
    {
        var temp = typeof(T);
        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.Name == column.ColumnName)
                {
                    var safeValue = dr[column.ColumnName] == null || dr[column.ColumnName] == DBNull.Value
                        ? default
                        : dr[column.ColumnName];
                    pro.SetValue(obj, safeValue, null);
                }
                else
                    continue;
            }
        }
        return obj;
    }

    [DebuggerStepThrough]
    public static string Json(this DataTable data)
    {
        string json = default;
        if (data != null)
        {
            json = DataTableToJsonWithStringBuilder(data);
        }

        return json;
    }

    [DebuggerStepThrough]
    private static string DataTableToJsonWithStringBuilder(DataTable table)
    {
        var jsonString = new StringBuilder();
        if (table.Rows.Count > 0)
        {
            jsonString.Append("[");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j < table.Columns.Count - 1)
                    {
                        jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                                          + "\":" + "\""
                                          + table.Rows[i][j].ToString() + "\",");
                    }
                    else if (j == table.Columns.Count - 1)
                    {
                        jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                                          + "\":" + "\""
                                          + table.Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == table.Rows.Count - 1)
                {
                    jsonString.Append("}");
                }
                else
                {
                    jsonString.Append("},");
                }
            }
            jsonString.Append("]");
        }
        return jsonString.ToString();
    }
}