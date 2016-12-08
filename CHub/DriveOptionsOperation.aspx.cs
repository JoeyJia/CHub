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
    public partial class DriveOptionsOperation : ClassLibrary.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region "得到传递的参数"
                #region "OptionsID"
                string OptionsID = "";
                try
                {
                    OptionsID = Request.QueryString["ID"].ToString();
                }
                catch
                {
                    OptionsID = Guid.NewGuid().ToString();
                }
                #endregion
                ViewState["OptionsID"] = OptionsID;

                #region "OperType"
                string OperType = "";
                try
                {
                    OperType = Request.QueryString["Type"].ToString();
                    if (OperType.ToLower() != "modify")
                    {
                        OperType = "Addnew";
                    }
                    ViewState["OperType"] = OperType;
                }
                catch
                {
                    OperType = "Addnew";
                    ViewState["OperType"] = "Addnew";
                }
                #endregion
                #endregion

                if (OperType.ToLower() == "modify")
                {
                    ((CHub.Main)Master).TitleName = "DriveOptions";
                    //载入详细信息
                    subDB_LoadDetail(OptionsID.funUuid_StringToUniqueidentifier("").funString_StringToDBString("Null"));
                }
                else
                {
                    ((CHub.Main)Master).TitleName = "DriveOptionsAddnew";
                }
            }
        }

        /// <summary>
        /// 载入用户的详细信息
        /// </summary>
        /// <param name="OptionsID"></param>
        private void subDB_LoadDetail(string OptionsID)
        {
            string strSQL = "SELECT MLFB, OptionValue, CurrentPrice FROM ViewDrive_Info_Options where ID=" + OptionsID;

            DataSet ds = new DataSet();
            ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtMLFB.Value = ds.Tables[0].Rows[0]["MLFB"].ToString();
                txtOptions.Value = ds.Tables[0].Rows[0]["OptionValue"].ToString();
                txtPrice.Value = ds.Tables[0].Rows[0]["CurrentPrice"].ToString();
            }
        }

        #region "校验数据"
        private string funString_CheckInfo()
        {
            if (txtOptions.Value.Trim() == "")
            {
                //return "登录名称不能为空";
                return "Options can not be empty!";
            }
            if (txtMLFB.Value.Trim() == "")
            {
                return "MLFB can not be empty!";
            }
            //else
            //{
            //    string MLFB = txtMLFB.Value.funString_SQLToString();
            //    string strSQL = "SELECT COUNT(*) AS CountRecords FROM Drive_Info_LP WHERE PriceType='MLFB' AND MLFB='" + MLFB + "'";
            //    int intCount = objClassDbAccess.funString_SQLExecuteScalar(strSQL).ToString().funInt_StringToInt(0);
            //    if (intCount == 0)
            //    {
            //        return "MLFB does not exist!";
            //    }
            //}
            return "";
        }
        #endregion

        #region "更新数据库"
        private string funString_DBSave()
        {
            string strSQL = "";
            string OptionsID = ViewState["OptionsID"].ToString();

            #region "获得更新的值"
            string MLFB = txtMLFB.Value.funString_SQLToString();
            string Options = txtOptions.Value.funString_SQLToString();
            decimal Price = txtPrice.Value.funDec_StringToDecimal(0);

            int IsDiscount = 1;
            string ImportType = "Web";
            string PriceType = "Options";

            string CurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string CurrentUserID = objLoginUserInfo.ID;
            #endregion

            strSQL = "SELECT ID FROM Drive_Info_LP WHERE PriceType='Options' AND MLFB='" + MLFB + "' AND OptionValue='" + Options + "'";
            string DBID = objClassDbAccess.funString_SQLExecuteScalar(strSQL).ToString();
            if (DBID.Length == 0)
            {
                //不能使用OptionsID来作为ID，因为如果是点击修改进来后，将选件改掉，变成新增，那么ID已经存在，就会报错
                strSQL = @"INSERT INTO Drive_Info_LP
                      (ID, MLFB, PriceType, OptionValue, CurrentPrice, IsDiscount, CreateDate, CreateUserID, ImportType) VALUES (";
                strSQL += "'" + Guid.NewGuid().ToString() + "','" + MLFB + "','" + PriceType + "', '" + Options + "', " + Price + "";
                strSQL += ", " + IsDiscount + ", '" + CurrentDate + "', '" + CurrentUserID + "','" + ImportType + "')";
                objOperationLog.DoLog(strSQL, "新增DriveOptions", objLoginUserInfo.ID);
            }
            else
            {
                //strSQL = @"update Drive_Info_LP set CurrentPrice = " + Price + ", IsDiscount = " + IsDiscount + ", ImportType='" + ImportType + "', CreateDate='" + CurrentDate + "', CreateUserID='" + CurrentUserID + "' where ID = '" + DBID + "'";
                strSQL = @"update Drive_Info_LP set CurrentPrice = " + Price + ", IsDiscount = " + IsDiscount + ", ImportType='" + ImportType + "', CreateDate='" + CurrentDate + "', CreateUserID='" + CurrentUserID + "'";
                strSQL += "  where PriceType='Options' AND MLFB='" + MLFB + "' AND OptionValue='" + Options + "'";
                objOperationLog.DoLog(strSQL, "修改DriveOptions", objLoginUserInfo.ID);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "submitOk", "alert('Save data success！');window.location='DriveOptionsDefault.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "submitError", "alert('保存数据出错！" + strError.funString_JsToString() + "');", true);
            }
        }
    }
}
