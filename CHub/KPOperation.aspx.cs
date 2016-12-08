using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IdioSoft.ClassCommon;

namespace CHub
{
    public partial class KPOperation : ClassLibrary.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((CHub.Main)Master).TitleName = "KPOperation";
                //载入详细信息
                subDB_LoadDetail();

            }
        }

        /// <summary>
        /// 载入用户的详细信息
        /// </summary>
        /// <param name="OptionsID"></param>
        private void subDB_LoadDetail()
        {
            string strSQL = "SELECT QuotationFunction FROM CHub_Management_Function WHERE (IsDel = 0) AND (QuotationText = '@KPDiscount@')";

            txtKPDiscount.Value = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
        }

        #region "校验数据"
        private string funString_CheckInfo()
        {
            string KPDiscount = txtKPDiscount.Value;
            string tmpKPDiscount = KPDiscount.ToLower();
            try
            {
                tmpKPDiscount = tmpKPDiscount.Replace("lp", "0");
                tmpKPDiscount = tmpKPDiscount.Replace("tp", "0");
                tmpKPDiscount = ClassLibrary.CalculateExpression.Calculate(tmpKPDiscount).ToString();
            }
            catch
            {
                return "Function Error!";
            }
            return "";
        }
        #endregion

        #region "更新数据库"
        private string funString_DBSave()
        {
            string strSQL = "";

            #region "获得更新的值"
            string KPDiscount = txtKPDiscount.Value;

            string CurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string CurrentUserID = objLoginUserInfo.ID;
            #endregion

            strSQL = "SELECT QuotationFunction FROM CHub_Management_Function WHERE (IsDel = 0) AND (QuotationText = '@KPDiscount@')";
            string DBID = objClassDbAccess.funString_SQLExecuteScalar(strSQL).ToString();
            if (DBID.Length == 0)
            {
                strSQL = @"insert into CHub_Management_Function(QuotationText, QuotationFunction, CreateDate, CreateUserID) values('@KPDiscount@','" + KPDiscount + "', '" + CurrentDate + "', '" + CurrentUserID + "')";
                objOperationLog.DoLog(strSQL, "新增KPDiscount", objLoginUserInfo.ID);
            }
            else
            {
                strSQL = @"update CHub_Management_Function set QuotationFunction='" + KPDiscount + "', UpdateDate='" + CurrentDate + "', UpdateUserID='" + CurrentUserID + "'";
                strSQL += "  where (IsDel = 0) AND (QuotationText = '@KPDiscount@')";
                objOperationLog.DoLog(strSQL, "修改KPDiscount", objLoginUserInfo.ID);
            }
            string strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL).funString_JsToString();

            return strError;
        }
        #endregion

        /// <summary>
        /// 点击提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            string strError = "";
            strError = funString_CheckInfo();
            if (strError != "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "submitError", "alert('保存数据出错！" + strError.funString_JsToString() + "');", true);
                return;
            }
            strError = funString_DBSave();
            if (strError == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "submitOk", "alert('Save data success！');window.location='KPOperation.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "submitError", "alert('保存数据出错！" + strError.funString_JsToString() + "');", true);
            }
        }
    }
}
