using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace IdioSoft.ClassCommon
{
    /// <summary>
    /// ��־������
    /// </summary>
    public  class OperationLog
    {
        /// <summary>   
        /// ��������־�ļ�д�����ݿ�
        /// </summary>   
        /// <param name="OperationSQL">������SQL����洢������</param>
        /// <param name="OperationDescription">��������</param>
        /// <param name="OperationUser">�����˵�ID</param>
        public  void DoLog(string OperationSQL, string OperationDescription, string OperationUser)
        {
            try
            {
                IdioSoft.ClassDbAccess.ClassDbAccess objClassDbAccess = new IdioSoft.ClassDbAccess.ClassDbAccess();
                string strSQL = "INSERT INTO CHub_System_SQLLog (SQL, Notes, CreateDate, CreateUserID) VALUES ('";
                strSQL = strSQL + CommonFunction.funString_SQLToString(OperationSQL) + "','" + CommonFunction.funString_SQLToString(OperationDescription) + "',";
                strSQL = strSQL + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + OperationUser + "')";
                objClassDbAccess.funString_SQLExecuteNonQuery(strSQL);
            }
            catch
            {

            }
        }
    }
}
