using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace IdioSoft.ClassCommon
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public  class OperationLog
    {
        /// <summary>   
        /// 将操作日志文件写入数据库
        /// </summary>   
        /// <param name="OperationSQL">操作的SQL语句或存储过程名</param>
        /// <param name="OperationDescription">操作描述</param>
        /// <param name="OperationUser">操作人的ID</param>
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
