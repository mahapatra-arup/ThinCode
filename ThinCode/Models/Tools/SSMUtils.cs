using System;
using System.Collections.Generic;
using System.Data;

namespace ThinCode
{

    public static class SoftUtils
    {

        public static string GetDBFormatDate(DateTime date)
        {
            try
            {
                string dateStr = String.Empty;
                dateStr = date.ToString("dd-MMM-yyyy");
                return dateStr;
            }
            catch (Exception)
            {

            }
            return null;

        }

        public static bool IsValidTable(DataTable dtInvoiceNum)
        {
            try
            {
                if (dtInvoiceNum != null && dtInvoiceNum.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public static bool IsNimeric(string txt, char c)
        {
            if (c == '\b')
            {
                return true;
            }
            else if (char.IsWhiteSpace(c))
            {
                return false;
            }
            else
            {
                try
                {
                    double dbl = Double.Parse(txt);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }


        public static List<string> GetListFromDataTbale(DataTable dataTable)
        {

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<string> lst = new List<string>();
                foreach (DataRow dr in dataTable.Rows)
                {
                    string val = dr[0].ToString();
                    lst.Add(val);
                }
                return lst;
            }
            return null;
        }

        public static int GetIdByValue(Dictionary<int, string> dic, string value)
        {
            int key = 0;
            foreach (var entry in dic)
            {
                if (entry.Value.Equals(value))
                {
                    key = entry.Key;
                }

            }
            return key;

        }
    }

    public static class StringExtenssion
    {

        public static string GetDBFormatString(this string strVal)
        {
            if (!strVal.ISNullOrWhiteSpace())
            {
                strVal = strVal.Replace("'", "''");
                strVal = strVal.Replace("\"", "\"\"");
            }

            return strVal;
        }

        public static bool ISNullOrWhiteSpace(this string strVal)
        {

            if (strVal == null || string.IsNullOrEmpty(strVal.Trim()))
            {
                return true;
            }

            return false;
        }
    }

    public static class DataTableExtension
    {
        public static bool IsValidDataTable(this DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsValidList<T>(this List<T> lst)
        {
            if (lst != null && lst.Count > 0)
            {
                return true;
            }
            return false;
        }
        public static bool ISValidObject(this object o)
        {
            if (o != null && o != DBNull.Value)
            {
                if (!o.ToString().ISNullOrWhiteSpace())
                {
                    return true;
                }
            }

            return false;
        }
    }

    public static class DictionaryExtention
    {
        public static bool IsNullOrEmpty<T, U>(this IDictionary<T, U> Dictionary)
        {
            return (Dictionary == null || Dictionary.Count < 1);
        }
    }

    public static class DataViewExtention
    {
        public static bool IsValidDataView(this DataView dtView)
        {
            if (dtView != null && dtView.Count > 0)
            {
                return true;
            }
            return false;
        }
    }

    public static class OperaterExtension
    {
        public static object ConvertNullEmptyToZero(this string value)
        {
            object obj = value.ISNullOrWhiteSpace() ? "0" : value;
            return obj;
        }
    }
}
