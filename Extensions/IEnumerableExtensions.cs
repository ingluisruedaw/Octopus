using Octopus.Objects;
using System.Data;
using System.Diagnostics;

namespace System.Collections.Generic;

public static class IEnumerableExtensions
{
    [DebuggerStepThrough]
    public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
    {
        var newDataTable = new DataTable();
        var impliedType = typeof(T);
        var _propInfo = impliedType.GetProperties();
        foreach (var pi in _propInfo)
            newDataTable.Columns.Add(pi.Name, pi.PropertyType);

        foreach (T item in collection)
        {
            var newDataRow = newDataTable.NewRow();
            newDataRow.BeginEdit();
            foreach (var pi in _propInfo)
                newDataRow[pi.Name] = pi.GetValue(item, null);
            newDataRow.EndEdit();
            newDataTable.Rows.Add(newDataRow);
        }
        return newDataTable;
    }

    [DebuggerStepThrough]
    public static IEnumerable<T> ShuffleSecure<T>(this IEnumerable<T> source)
    {
        var sourceArray = source.ToArray();
        for (int counter = default; counter < sourceArray.Length; counter++)
        {
            int randomIndex = new Lazy<RandomSecureVersion>(() => new RandomSecureVersion()).Value.Next(counter, sourceArray.Length);
            yield return sourceArray[randomIndex];

            sourceArray[randomIndex] = sourceArray[counter];
        }
    }
}