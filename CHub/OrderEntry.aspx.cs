using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IdioSoft.ClassCommon;
using System.Text.RegularExpressions;

namespace CHub
{
    public partial class OrderEntry : ClassLibrary.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region "得到传递的参数"
                #region "SPRID"
                string SPRID = "";
                try
                {
                    SPRID = Request.QueryString["ID"].ToString();
                }
                catch
                {
                    SPRID = Guid.NewGuid().ToString();
                }
                #endregion
                ViewState["SPRID"] = SPRID;

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
                    ((CHub.Main)Master).TitleName = "SPR";
                    //载入详细信息
                    subDB_LoadDetail(SPRID.funUuid_StringToUniqueidentifier("").funString_StringToDBString("Null"));
                }
                else
                {
                    ((CHub.Main)Master).TitleName = "SPRAddnew";
                }
            }
        }

        /// <summary>
        /// 载入用户的详细信息
        /// </summary>
        /// <param name="SPRID"></param>
        private void subDB_LoadDetail(string SPRID)
        {
            string strSQL = "SELECT SPRNo, MLFB, Options, Voltage, Quantity, OthersPerUnit, OthersPerItem FROM CHub_Info_SPR where ID=" + SPRID;
            DataSet ds = new DataSet();
            ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtSPRNo.Value = ds.Tables[0].Rows[0]["SPRNo"].ToString();
                txtMLFB.Value = ds.Tables[0].Rows[0]["MLFB"].ToString();
                //txtOptions.Value = ds.Tables[0].Rows[0]["Options"].ToString();
                txtVoltage.Value = ds.Tables[0].Rows[0]["Voltage"].ToString();
                txtQuantity.Value = ds.Tables[0].Rows[0]["Quantity"].ToString();
                txtOthersPerUnit.Value = ds.Tables[0].Rows[0]["OthersPerUnit"].ToString();
                txtOthersPerItem.Value = ds.Tables[0].Rows[0]["OthersPerItem"].ToString();

                strSQL = "SELECT OptionValue, CurrentPrice FROM CHub_Info_SPROptions WHERE (IsDel = 0) AND (SPRID=" + SPRID + ")";
                ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
                string strOptions = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    strOptions += ds.Tables[0].Rows[i]["OptionValue"].ToString() + "," + ds.Tables[0].Rows[i]["CurrentPrice"].ToString() + "+";//\r\n
                }
                if (strOptions.Length > 0)
                {
                    strOptions = strOptions.Substring(0, strOptions.Length - 1);//去掉最后的+号
                }
                txtOptions.Value = strOptions;
            }
        }

        #region "校验数据"
        private string funString_CheckInfo()
        {
            if (txtSPRNo.Value.Trim() == "")
            {
                //return "登录名称不能为空";
                return "SPR No can not be empty!";
            }
            if (txtMLFB.Value.Trim() == "")
            {
                return "MLFB can not be empty!";
            }
            if (txtQuantity.Value.Trim() == "")
            {
                return "Quantity can not be empty!";
            }

            decimal OthersPerUnit = txtOthersPerUnit.Value.funDec_StringToDecimal(0);
            decimal OthersPerItem = txtOthersPerItem.Value.funDec_StringToDecimal(0);
            if (OthersPerUnit != 0 && OthersPerItem != 0)
            {
                return "'Others Per Unit' and 'Others Per Item' are exists, only input one!";
            }
            return "";
        }
        #endregion

        #region "更新数据库"
        private string funString_DBSave()
        {
            string strSQL = "";
            string SPRID = ViewState["SPRID"].ToString();

            #region "获得更新的值"
            string SPRNo = txtSPRNo.Value.Trim().funString_SQLToString();
            string MLFB = txtMLFB.Value.Trim().funString_SQLToString();
            string Options = txtOptions.Value.Trim().funString_SQLToString();
            decimal Voltage = txtVoltage.Value.Trim().funDec_StringToDecimal(0);
            int Quantity = txtQuantity.Value.Trim().funInt_StringToInt(1);
            decimal OthersPerUnit = txtOthersPerUnit.Value.Trim().funDec_StringToDecimal(0);
            decimal OthersPerItem = txtOthersPerItem.Value.Trim().funDec_StringToDecimal(0);

            string CurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string CurrentUserID = objLoginUserInfo.ID;
            #endregion

            string strError = "";
            #region "选件价格"
            //先删除这个SPR的全部选件
            strSQL = "Update CHub_Info_SPROptions SET IsDel=1, DeleteDate='" + CurrentDate + "', DeleteUserID='" + CurrentUserID + "' WHERE SPRID='" + SPRID + "'";
            strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL);
            //新增SPR的选件及价格，并将存到主表中的SPR选件格式化
            string[] aryOptions = Regex.Split(Options, "\\+");//Regex.Split(Options, "\r\n");
            Options = "";
            for (int i = 0; i < aryOptions.Length; i++)
            {
                if (aryOptions[i].Trim().Length > 0)
                {
                    string[] aryOptionItem = aryOptions[i].Split(',');
                    string OptionItem = "";
                    string CurrentPrice = "0";
                    if (aryOptionItem.Length > 0)
                    {
                        OptionItem = aryOptionItem[0].Trim();
                        Options = Options + "+" + OptionItem;
                    }
                    if (aryOptionItem.Length > 1)
                    {
                        CurrentPrice = aryOptionItem[1].Trim().funDec_StringToDecimal(0).ToString();
                    }

                    strSQL = "INSERT INTO CHub_Info_SPROptions(SPRID, OptionValue, CurrentPrice, CreateDate, CreateUserID) Values(";
                    strSQL += "'" + SPRID + "', '" + OptionItem + "', " + CurrentPrice + ", '" + CurrentDate + "', '" + CurrentUserID + "')";
                    strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL);
                }
            }
            if (Options.Length > 0)
            {
                Options = Options.Substring(1);
            }
            ///最好能查找一下选件有没有重复并且价格不同的，防止读取价格的时候出现一个选件两个价格
            #endregion

            if (ViewState["OperType"].ToString().Trim().ToLower() == "addnew")
            {
                strSQL = @"INSERT INTO CHub_Info_SPR
                      (ID, SPRNo, MLFB, Options, Voltage, Quantity, OthersPerUnit, OthersPerItem, IsDel, CreateDate, CreateUserID) VALUES (";
                strSQL += "'" + SPRID + "','" + SPRNo + "', '" + MLFB + "', '" + Options + "', " + Voltage + ", " + Quantity + ", " + +OthersPerUnit + ", " + OthersPerItem + "";
                strSQL += ", 0, '" + CurrentDate + "', '" + objLoginUserInfo.ID + "')";
                objOperationLog.DoLog(strSQL, "新增SPR", objLoginUserInfo.ID);
            }
            if (ViewState["OperType"].ToString().Trim().ToLower() == "modify")
            {
                strSQL = "UPDATE CHub_Info_SPR SET SPRNo ='" + SPRNo + "'";
                strSQL += ", MLFB ='" + MLFB + "', Options ='" + Options + "', Voltage = " + Voltage + ", Quantity=" + Quantity + ", OthersPerUnit = " + OthersPerUnit + ", OthersPerItem = " + OthersPerItem + "";
                strSQL += ", UpdateDate ='" + CurrentDate + "', UpdateUserID ='" + objLoginUserInfo.ID + "' where ID='" + SPRID + "'";
                objOperationLog.DoLog(strSQL, "修改SPR", objLoginUserInfo.ID);
            }
            strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL).funString_JsToString();

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "submitOk", "alert('Save data success！');window.location='OrderDefault.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "submitError", "alert('保存数据出错！" + strError.funString_JsToString() + "');", true);
            }
        }
    }
}
