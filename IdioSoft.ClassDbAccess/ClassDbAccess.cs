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
    /// ���ݿ������
    /// </summary>
    public class ClassDbAccess
    {
        public OracleConnection conn = new OracleConnection();
        private string strConn = "";
        /// <summary>
        /// ��ʼ����
        /// </summary>
        public ClassDbAccess()
        {
            try
            {
                this.strConn = ConfigurationManager.ConnectionStrings["DataConnString"].ToString();
                //"Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = SDWSJVI6002)(PORT = 1521))(CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME= CNGWT)));User Id=GWAPP;Password=Cummins1234;";
                this.conn = new OracleConnection();
                //ȫ����������
                this.conn.ConnectionString = this.strConn;
            }
            catch (Exception e)
            {

            }
        }
        #region "���ݿ����ӿ��ز���"
        //Open���ݿ�
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
        //Close ���ݿ�����
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
        #region "SQL������ݿ����"
        /// <summary>   
        /// ����һ��DataSet
        /// </summary>   
        /// <param name="strSQL">SQL���</param>
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
        /// ����һ��DataSet
        /// </summary>
        /// <param name="strSQL">SQL���</param>
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
        /// �޷���ִ��SQL,������ز�Ϊ��,��Ϊ�д�
        /// </summary>   
        /// <param name="strSQL">SQL���</param>
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
        /// ����һ����һ��ֵ
        /// </summary>   
        /// <param name="strSQL">SQL���</param>
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
        #region "�ô洢���̿���"
        /// <summary>   
        /// �޷��ش洢����,��ɷ�������,���ؿ�ʱΪ����.
        /// </summary>   
        /// <param name="ls">�����б�</param>
        /// <param name="spName">�洢������</param>
        public string funString_SPExecuteNonQuery(List<object> ls, string spName)
        {
            string strReturn = "";
            this.Open();
            OracleCommand sqlcom = new OracleCommand();
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Connection = conn;
            sqlcom.CommandText = spName;
            //��ʼ������
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
        /// ����һ����������
        /// </summary>   
        /// <param name="ls">�����б�</param>
        /// <param name="spName">�洢������</param>
        public string funString_SPExecuteScalar(List<object> ls, string spName)
        {
            string strReturn = "";
            this.Open();
            OracleCommand sqlcom = new OracleCommand();
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Connection = conn;
            sqlcom.CommandText = spName;
            //��ʼ������
            OracleCommandBuilder.DeriveParameters(sqlcom);
            for (int i = 0; i < ls.Count; i++)
            {
                sqlcom.Parameters[i + 1].Value = (ls[i] == null) ? DBNull.Value : ls[i];
            }
            try
            {
                //����һ������ֵ
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
        /// ����һ��DataTable
        /// </summary>   
        /// <param name="ls">�����б�</param>
        /// <param name="spName">�洢������</param>
        public DataTable funDataTable_SPExecuteNonQuery(List<object> ls, string spName)
        {
            DataTable dt = new DataTable();
            this.Open();
            OracleCommand sqlcom = new OracleCommand();
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Connection = conn;
            sqlcom.CommandText = spName;
            //��ʼ������
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
                //����һ��DataTable
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