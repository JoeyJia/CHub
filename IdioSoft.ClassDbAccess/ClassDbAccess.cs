using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Data.OracleClient;

namespace IdioSoft.ClassDbAccess
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class ClassDbAccess
    {
        public OracleConnection conn = new OracleConnection();
        private string strConn = "";
        /// <summary>
        /// 初始化类
        /// </summary>
        public ClassDbAccess()
        {
            try
            {
                this.strConn = ConfigurationManager.ConnectionStrings["DataConnString"].ToString();
                //"Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = SDWSJVI6002)(PORT = 1521))(CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME= CNGWT)));User Id=GWAPP;Password=Cummins1234;";
                this.conn = new OracleConnection();
                //全局数据连接
                this.conn.ConnectionString = this.strConn;
            }
            catch (Exception e)
            {

            }
        }
        #region "数据库连接开关操作"
        //Open数据库
        private void Open()
        {
            if ((conn.State == System.Data.ConnectionState.Closed) || (conn.State == ConnectionState.Broken))
            {
                try
                {
                    conn.Open();
                }
                catch
                {
                }
            }
        }
        //Close 数据库连接
        private void Close()
        {
            try
            {
                conn.Close();
            }
            catch
            {
            }
        }
        #endregion
        #region "SQL语句数据库操作"
        /// <summary>   
        /// 返回一个DataSet
        /// </summary>   
        /// <param name="strSQL">SQL语句</param>
        public DataSet funDataset_SQLExecuteNonQuery(string strSQL)
        {
            DataSet dsMain = new DataSet();
            try
            {

                OracleCommand sqlcom = new OracleCommand();
                this.Open();
                sqlcom.CommandText = strSQL;
                sqlcom.Connection = conn;
                sqlcom.CommandTimeout = 600;
                OracleDataAdapter daMain = new OracleDataAdapter();
                daMain.SelectCommand = sqlcom;
                sqlcom.ExecuteNonQuery();
                daMain.Fill(dsMain);
            }
            catch (Exception e)
            {
                dsMain = null;
            }
            finally
            {
                this.Close();
            }
            return dsMain;
        }
        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="intStartRow">Start RowIndex</param>
        /// <param name="intMaxCount">PageSize</param>
        /// <returns></returns>
        public DataSet funDataset_SQLExecuteNonQuery(string strSQL, int intStartRow, int intMaxCount)
        {
            DataSet dsMain = new DataSet();
            try
            {
                OracleCommand sqlcom = new OracleCommand();
                this.Open();
                sqlcom.CommandText = strSQL;
                sqlcom.Connection = conn;
                sqlcom.CommandTimeout = 600;
                OracleDataAdapter daMain = new OracleDataAdapter();
                daMain.SelectCommand = sqlcom;
                sqlcom.ExecuteNonQuery();
                daMain.Fill(dsMain, intStartRow, intMaxCount, "TempTable");
            }
            catch
            {
                dsMain = null;
            }
            finally
            {
                this.Close();
            }
            return dsMain;
        }

        /// <summary>   
        /// 无返回执行SQL,如果返回不为空,则为有错
        /// </summary>   
        /// <param name="strSQL">SQL语句</param>
        public string funString_SQLExecuteNonQuery(string strSQL)
        {
            string strError = "";
            try
            {
                OracleCommand sqlcom = new OracleCommand();
                this.Open();
                sqlcom.CommandText = strSQL;
                sqlcom.CommandType = CommandType.Text;
                sqlcom.CommandTimeout = 600;
                sqlcom.Connection = conn;
                sqlcom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strError = ex.Message + "\n" + strSQL;
            }
            finally
            {
                this.Close();
            }
            return strError;
        }
        /// <summary>   
        /// 返回一个单一的值
        /// </summary>   
        /// <param name="strSQL">SQL语句</param>
        public string funString_SQLExecuteScalar(string strSQL)
        {
            string strReturn = "";
            try
            {
                OracleCommand sqlcom = new OracleCommand();
                this.Open();
                sqlcom.CommandText = strSQL;
                sqlcom.Connection = conn;
                sqlcom.CommandTimeout = 600;
                strReturn = sqlcom.ExecuteScalar().ToString();
            }
            catch
            {
                strReturn = "";
            }
            finally
            {
                this.Close();
            }
            return strReturn;
        }
        #endregion
        #region "用存储过程开发"
        /// <summary>   
        /// 无返回存储过程,这可返回行数,返回空时为出错.
        /// </summary>   
        /// <param name="ls">参数列表</param>
        /// <param name="spName">存储过程名</param>
        public string funString_SPExecuteNonQuery(List<object> ls, string spName)
        {
            string strReturn = "";
            this.Open();
            OracleCommand sqlcom = new OracleCommand();
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Connection = conn;
            sqlcom.CommandText = spName;
            //初始化参数
            OracleCommandBuilder.DeriveParameters(sqlcom);
            for (int i = 0; i < ls.Count; i++)
            {
                sqlcom.Parameters[i + 1].Value = (ls[i] == null) ? DBNull.Value : ls[i];
            }
            try
            {
                sqlcom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strReturn = ex.Message + "\n" + spName;
            }
            finally
            {
                this.Close();
            }
            return strReturn;
        }
        /// <summary>   
        /// 返回一个数据内容
        /// </summary>   
        /// <param name="ls">参数列表</param>
        /// <param name="spName">存储过程名</param>
        public string funString_SPExecuteScalar(List<object> ls, string spName)
        {
            string strReturn = "";
            this.Open();
            OracleCommand sqlcom = new OracleCommand();
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Connection = conn;
            sqlcom.CommandText = spName;
            //初始化参数
            OracleCommandBuilder.DeriveParameters(sqlcom);
            for (int i = 0; i < ls.Count; i++)
            {
                sqlcom.Parameters[i + 1].Value = (ls[i] == null) ? DBNull.Value : ls[i];
            }
            try
            {
                //返回一个数据值
                strReturn = sqlcom.ExecuteScalar().ToString();
            }
            catch
            {
                strReturn = "";
            }
            finally
            {
                this.Close();
            }
            return strReturn;
        }

        /// <summary>   
        /// 返回一个DataTable
        /// </summary>   
        /// <param name="ls">参数列表</param>
        /// <param name="spName">存储过程名</param>
        public DataTable funDataTable_SPExecuteNonQuery(List<object> ls, string spName)
        {
            DataTable dt = new DataTable();
            this.Open();
            OracleCommand sqlcom = new OracleCommand();
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Connection = conn;
            sqlcom.CommandText = spName;
            //初始化参数
            OracleCommandBuilder.DeriveParameters(sqlcom);
            for (int i = 0; i < ls.Count; i++)
            {
                sqlcom.Parameters[i + 1].Value = (ls[i] == null) ? DBNull.Value : ls[i];
            }
            try
            {
                DataSet ds = new DataSet();
                OracleDataAdapter daMain = new OracleDataAdapter();
                daMain.SelectCommand = sqlcom;
                //返回一个DataTable
                sqlcom.ExecuteNonQuery();
                daMain.Fill(ds);
                dt = ds.Tables[0];
            }
            catch
            {
                dt = null;
            }
            finally
            {
                this.Close();
            }
            return dt;
        }
        #endregion
    }
}