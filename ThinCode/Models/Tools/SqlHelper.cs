using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using ThinCode.Models;

namespace ThinCode
{
    public class SqlHelper
    {
        public static CodeliteEntities1 dbContext = new CodeliteEntities1();

        #region -----------------Variable--------------
        private static string msg;
        private static string executeClassDetails;

        private static SqlHelper mInstance;
        private static string mDataSource = string.Empty;
        private static string mInitalCatalog = string.Empty;
        private static string mUserInstance = string.Empty;
        private static string mAttachedDb = string.Empty;
        private static string mIntegratedSecurity = string.Empty;
        private static string mUserPass = "";
        private static string cONNECTION_STRING = string.Empty;
        private static string mDBUserName = "";
        private static string mDBPassWord = "";
        private static List<SqlParameter> mParameterList = new List<SqlParameter>();
        StackTrace mTraceError = new StackTrace();
        #endregion

        #region ...............Entities..........

        public static string Msg
        {
            get
            {
                return msg;
            }

            set
            {
                msg = value;
            }
        }

        public static string ExecuteClassDetails
        {
            get
            {
                return executeClassDetails;
            }

            set
            {
                executeClassDetails = value;
            }
        }

        public static SqlHelper MInstance
        {
            get
            {
                return mInstance;
            }

            set
            {
                mInstance = value;
            }
        }

        public static string MDataSource
        {
            get
            {
                return mDataSource;
            }

            set
            {
                mDataSource = value;
            }
        }

        public static string MInitalCatalog
        {
            get
            {
                return mInitalCatalog;
            }

            set
            {
                mInitalCatalog = value;
            }
        }

        public static string MUserInstance
        {
            get
            {
                return mUserInstance;
            }

            set
            {
                mUserInstance = value;
            }
        }

        public static string MAttachedDb
        {
            get
            {
                return mAttachedDb;
            }

            set
            {
                mAttachedDb = value;
            }
        }

        public static string MIntegratedSecurity
        {
            get
            {
                return mIntegratedSecurity;
            }

            set
            {
                mIntegratedSecurity = value;
            }
        }

        public static string MDBUserPass
        {
            get
            {
                return mUserPass;
            }

            set
            {
                mUserPass = value;
            }
        }

        public static string CONNECTION_STRING
        {
            get
            {
                return cONNECTION_STRING;
            }

            set
            {
                cONNECTION_STRING = value;
            }
        }

        public static List<SqlParameter> MParameterList
        {
            get
            {
                return mParameterList;
            }

            set
            {
                mParameterList = value;
            }
        }

        public static string MDBUserName
        {
            get
            {
                return mDBUserName;
            }

            set
            {
                mDBUserName = value;
            }
        }

        public static string MDBPassWord
        {
            get
            {
                return mDBPassWord;
            }

            set
            {
                mDBPassWord = value;
            }
        }

        #endregion

        private SqlHelper()
        {

        }

        public static SqlHelper GetInstance()
        {
            if (mInstance == null)
            {

                CONNECTION_STRING = string.Empty;
                mDataSource = System.Configuration.ConfigurationManager.AppSettings["DataSource"].ToString();
                mInitalCatalog = System.Configuration.ConfigurationManager.AppSettings["InitialCatalog"].ToString();
                mDBUserName = "sa";
                mDBPassWord = "!dcuseronly";

                mUserPass = "user id=" + mDBUserName + ";password=" + mDBPassWord;
                CONNECTION_STRING = "Data Source=" + mDataSource + "; Initial Catalog=" + mInitalCatalog + "; " + mUserPass;

                //-----Connection Test----
                if (!TestDBConnection(CONNECTION_STRING))
                {
                    // HttpContext.Current.Response.RedirectPermanent("ErrorPage.aspx", true); 
                }
                mInstance = new SqlHelper();
            }
            return mInstance;
        }

        public static bool TestDBConnection(string _ConnString)
        {
            using (SqlConnection cn = new SqlConnection(_ConnString))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }
                    string query = "SELECT 1";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    object o = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                   // ex.ToWriteLog(Environment.StackTrace);
                    msg = ex.Message;

                    return false;
                }
                return true;
            }
        }
        public bool ExcuteQuery(string query)
        {
            Array parameterList = MParameterList.ToArray();
            MParameterList.Clear();
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand(query, con);
                    if (parameterList.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameterList);
                    }
                    cmd.ExecuteReader();
                    return true;
                }
                catch (Exception ex)
                {
                    //ex.ToWriteLog(Environment.StackTrace);
                    msg = ex.Message;
                    return false;
                }
            }
        }

        public object ExcuteScalar(string query)
        {
            Array parameterList = MParameterList.ToArray();
            MParameterList.Clear();
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand(query, con);
                    if (parameterList.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameterList);
                    }
                    object obj = cmd.ExecuteScalar();
                    return obj;
                }
                catch (Exception ex)
                {
                    //ex.ToWriteLog(Environment.StackTrace);
                    msg = ex.Message;
                    return null;
                }
            }
        }

        public DataTable ExcuteNonQuery(string query)
        {
            Array parameterList = MParameterList.ToArray();
            MParameterList.Clear();
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlDataAdapter sqlAdptr = new SqlDataAdapter(query, con);
                    if (parameterList.Length > 0)
                    {
                        sqlAdptr.SelectCommand.Parameters.AddRange(parameterList);
                    }
                    DataTable dataTable = new DataTable();
                    sqlAdptr.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception ex)
                {
                   // ex.ToWriteLog(Environment.StackTrace);
                    msg = ex.Message;
                    return null;
                }
            }
        }

        public bool ExecuteTransection(List<string> lstQuery, List<List<SqlParameter>> sqlparam)
        {
            if (lstQuery.Count > 0)
            {
                int index = 0;

                using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
                {
                    try
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        SqlCommand cmd = con.CreateCommand();
                        SqlTransaction trans;
                        trans = con.BeginTransaction(IsolationLevel.ReadCommitted);
                        cmd.Connection = con;
                        cmd.Transaction = trans;

                        int isExecute = 0;
                        foreach (var item in lstQuery)
                        {
                            cmd.CommandText = item.ToString();

                            if (sqlparam != null)
                            {
                                //clear parameter then add range
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddRange(sqlparam[index].ToArray());
                            }
                            index++;
                            isExecute = cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                       // ex.ToWriteLog(Environment.StackTrace);
                        msg = ex.Message;

                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Add sql paramiter by call this method
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="ParameterValue"></param>
        public static void SetParamiterWithValue(string parameterName, string ParameterValue)
        {
            MParameterList.Add(new SqlParameter("@" + parameterName + "", ParameterValue));
        }
        public static object Convert_null_To_DBNull<T> (T arg)
        {
            return arg.ISValidObject() ? (object)arg : DBNull.Value;
        }
    }
}