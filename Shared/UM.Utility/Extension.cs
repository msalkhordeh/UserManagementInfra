using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace UM.Utility
{
    public static class Extension
    {
        public const string HeaderRemoteIpAddress = "X-Proxy-Client-Remote-Ip-Address";

        #region Enum
        public static string GetName<TEnum>(this TEnum item)
        {
            return Enum.GetName(typeof(TEnum), item);
        }

        public static TEnum GetValue<TEnum>(this object item)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), item.ToString());
        }

        public static int GetEnumIndex<TEnum>(this TEnum item)
        {
            return (int)Enum.Parse(typeof(TEnum), item.GetName());
        }
        #endregion

        #region Convert Data Type
        public static int ToInt(this object input)
        {
            return int.Parse(input.ToString());
        }

        public static Guid ToGuid(this object input)
        {
            try
            {
                return Guid.Parse(input.ToString());
            }
            catch
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Extension method to convert a data model object to Standard <see cref="DataSet"/> object
        /// </summary>
        /// <param name="value">Represent the object input used to convert</param>
        /// <returns>An instance of <see cref="DataSet"/> that filled with input value datas</returns>
        public static DataSet ToDataSet(this object value)
        {
            var dataset = new DataSet();
            AddToDataSet(dataset, value);
            return dataset;
        }

        /// <summary>
        /// Method to convert a data model object to the given <see cref="DataSet"/> object
        /// </summary>
        /// <param name="dataset">Represent the DataSet object used to fill with the given input datas</param>
        /// <param name="value">Represent the object input used to convert</param>
        /// <returns>The given <see cref="DataSet"/> that filled with input value datas</returns>
        public static void AddToDataSet(DataSet dataset, object value)
        {
            if (dataset == null)
                throw new ArgumentNullException(nameof(dataset));

            if (value == null)
                return;

            var type = value.GetType();
            var table = dataset.Tables[type.FullName];
            if (table == null)
            {
                table = new DataTable(type.FullName);
                dataset.Tables.Add(table);
                foreach (var prop in type.GetProperties().Where(p => p.CanRead))
                {
                    if (IsEnumerable(prop))
                        continue;

                    var col = new DataColumn(prop.Name, prop.PropertyType);
                    table.Columns.Add(col);
                }
            }

            var row = table.NewRow();
            foreach (var prop in type.GetProperties().Where(p => p.CanRead))
            {
                object propValue = prop.GetValue(value);
                if (IsEnumerable(prop))
                {
                    if (propValue != null)
                    {
                        foreach (var child in (ICollection)propValue)
                        {
                            AddToDataSet(dataset, child);
                        }
                    }
                    continue;
                }

                row[prop.Name] = propValue;
            }
            table.Rows.Add(row);
        }

        private static bool IsEnumerable(PropertyInfo pi)
        {
            return typeof(ICollection).IsAssignableFrom(pi.PropertyType);
        }
        #endregion

        #region Date & Time

        public static DateTime UnixTimeStampToDateTime(this double? unixTimeStamp)
        {
            DateTime dtDateTime = new(1970, 1, 1, 0, 0,
                0, 0, DateTimeKind.Utc);
            if (unixTimeStamp.HasValue)
            {
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp.Value).ToLocalTime();
            }

            return dtDateTime;
        }

        public static DateTime JavaTimeStampToDateTime(this double? unixTimeStamp)
        {
            DateTime dtDateTime = new(1970, 1, 1, 0, 0,
                0, 0, DateTimeKind.Utc);
            if (unixTimeStamp.HasValue)
            {
                dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp.Value).ToLocalTime();
            }

            return dtDateTime;
        }

        public static string GetMonthName(this int input, MonthFormat format = MonthFormat.Fullname)
        {
            return format switch
            {
                MonthFormat.Fullname => (input switch
                {
                    1 => "January",
                    2 => "February",
                    3 => "March",
                    4 => "April",
                    5 => "May",
                    6 => "June",
                    7 => "July",
                    8 => "August",
                    9 => "September",
                    10 => "October",
                    11 => "November",
                    12 => "December",
                    _ => default
                }),
                MonthFormat.ThreeChar => (input switch
                {
                    1 => "Jan",
                    2 => "Feb",
                    3 => "Mar",
                    4 => "Apr",
                    5 => "May",
                    6 => "Jun",
                    7 => "Jul",
                    8 => "Aug",
                    9 => "Sep",
                    10 => "Oct",
                    11 => "Nov",
                    12 => "Dec",
                    _ => default
                }),
                _ => default
            };
        }

        public enum MonthFormat
        {
            Fullname,
            ThreeChar
        }
        #endregion

        public static string ThousandSeparator(this object input)
        {
            var number = input.ToString();
            var result = "";
            var mod = number.Length % 3;
            if (mod != 0)
            {
                for (var i = 0; i < mod; i++)
                {
                    result += number[i];
                }
                result += ',';
            }
            for (var i = 0; i < (number.Length - mod) / 3; i++)
            {

                result += string.Concat(number.AsSpan((i * 3) + mod, 3), ",");
            }
            result = result[..^1];
            return result;
        }

        public static T Find<T>(this ICollection<T> enumerable, Func<T, bool> predicate)
        {
            foreach (var current in enumerable)
            {
                if (predicate(current))
                {
                    return current;
                }
            }

            return default;
        }
    }
}