using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace CodeGenerator.Shared.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Indicates whether the specified System.Collections.Generic.IEnumerable`1 is null or empty
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">The System.Collections.Generic.IEnumerable`1 to evaluate.</param>
        /// <returns>true if the System.Collections.Generic.IEnumerable`1 is null or empty; otherwise, false.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.FastAny();
            //return source == null || !source.Any();
        }

        /// <summary>
        /// Creates and returns a System.Data.DataTable from the elements in the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence to create a System.Data.DataTable from.</param>
        /// <returns>A new System.Data.DataTable containing columns and rows based on the elements in source.</returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> source)
        {
            return source.ToDataTable(string.Concat(typeof(T).Name, "_Table"));
        }

        /// <summary>
        /// Creates and returns a System.Data.DataTable from the elements in the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence to create a System.Data.DataTable from.</param>
        /// <param name="tableName">The value to set for the System.Data.DataTable's Name property.</param>
        /// <returns>A new System.Data.DataTable containing columns and rows based on the elements in source.</returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> source, string tableName)
        {
            var table = new DataTable(tableName) { Locale = CultureInfo.InvariantCulture };

            var properties = typeof(T).GetProperties();

            #region If T Is String Or Has No Properties

            if (properties.IsNullOrEmpty() || typeof(T) == typeof(string))
            {
                table.Columns.Add(new DataColumn("Value", typeof(string)));

                foreach (T item in source)
                {
                    DataRow row = table.NewRow();
                    row["Value"] = item.ToString();
                    table.Rows.Add(row);
                }

                return table;
            }

            #endregion If T Is String Or Has No Properties

            #region Else Normal Collection

            foreach (PropertyInfo property in properties)
            {
                table.Columns.Add(new DataColumn(property.Name, property.PropertyType));
            }

            foreach (T item in source)
            {
                DataRow row = table.NewRow();

                foreach (PropertyInfo property in properties)
                {
                    row[property.Name] = property.GetValue(item, null);
                }

                table.Rows.Add(row);
            }

            #endregion Else Normal Collection

            return table;
        }

        internal static bool FastAny<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is TSource[] array)
            {
                return array.Length > 0;
            }

            if (source is ICollection<TSource> collection)
            {
                return collection.Count > 0;
            }

            using (var enumerator = source.GetEnumerator())
            {
                return enumerator.MoveNext();
            }
        }
    }
}