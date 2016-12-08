using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IdioSoft.ClassCommon;
using System.Collections.Generic;

namespace CHub
{
    public partial class MyInformation : ClassLibrary.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboRegionName.subComboBox_LoadItems("SELECT RegionName FROM CHub_Basic_Region ORDER BY OrderNo", 0, null);//载入区域
                cboDutyID.subComboBox_LoadItems("SELECT ID, DutyName FROM CHub_Basic_Duty ORDER BY OrderNo", 0, null);//载入用户角色
                subTreeView_Load();//初始化系统权限

                ((CHub.Main)Master).TitleName = "MyInformation";
                //载入详细信息
                subDB_LoadDetail(objLoginUserInfo.ID);
            }
        }

        /// <summary>
        /// 初始化系统权限
        /// </summary>
        private void subTreeView_Load()
        {
            tveSystemLimited.CheckALLTitle = "Check ALL";
            tveSystemLimited.ShowCheckBoxes = TreeNodeTypes.All;
            tveSystemLimited.TreeViewSQL = "SELECT LimitedID, Description FROM CHub_Basic_Limited where 1=1 ORDER BY OrderNo";
            tveSystemLimited.TreeViewSQLParentID = "ParentID";
            tveSystemLimited.subNodes_Load(null, "0");
        }

        /// <summary>
        /// 载入用户的详细信息
        /// </summary>
        /// <param name="UserID"></param>
        private void subDB_LoadDetail(string UserID)
        {
            string strSQL = "SELECT LoginName, Password, Email, UserNameEN, UserNameCN, Gender, RegionName, DutyName, SystemLimited FROM View_Management_LoginUser_Detail where ID='" + UserID + "'";

            DataSet ds = new DataSet();
            ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtLoginName.Value = ds.Tables[0].Rows[0]["LoginName"].ToString();
                txtPassword.Value = ds.Tables[0].Rows[0]["Password"].ToString();
                txtEmail.Value = ds.Tables[0].Rows[0]["Email"].ToString();
                txtUserNameEN.Value = ds.Tables[0].Rows[0]["UserNameEN"].ToString();
                txtUserNameCN.Value = ds.Tables[0].Rows[0]["UserNameCN"].ToString();
                cboGender.subComboBox_SelectItemByValue(ds.Tables[0].Rows[0]["Gender"].ToString());
                cboRegionName.subComboBox_SelectItemByValue(ds.Tables[0].Rows[0]["RegionName"].ToString());

                cboDutyID.subComboBox_SelectItemByText(ds.Tables[0].Rows[0]["DutyName"].ToString());
                cboDutyID.Disabled = true;

                string strSystemLimited = ds.Tables[0].Rows[0]["SystemLimited"].ToString();
                if (strSystemLimited.Trim() == "" || strSystemLimited.Trim() == ",,")
                {

                }
                else
                {
                    string[] Limted = strSystemLimited.Split(',');
                    List<string> lstLimited = new List<string>();
                    tveSystemLimited.TreeViewNodeValuesForCheck = Limted.ToList();
                    tveSystemLimited.TreeViewCheckStatusByList = true;
                }
                tveSystemLimited.Enable = false;//自己不能修改自己的权限
            }
        }

        #region "校验数据"
        private string funString_CheckInfo()
        {
            if (txtLoginName.Value.Trim() == "")
            {
                return "登录名称不能为空";
            }
            //是本人，那么判断密码
            if (txtPassword.Value.Trim() == "")
            {
                return "密码不能为空";
            }
            //if (txtUserNameEN.Value.Trim() == "")
            //{
            //    return "用户名称(英文)不能为空";
            //}
            string strSQL = "";
            strSQL = "select Count(*) from CHub_Management_LoginUser where LoginName='" + txtLoginName.Value.Trim() + "' and ID<>'" + objLoginUserInfo.ID + "' and IsDel=0";
            int intCount = objClassDbAccess.funString_SQLExecuteScalar(strSQL).funInt_StringToInt(0);
            if (intCount > 0)
            {
                return "登录名称不能重复！";
            }
            return "";
        }
        #endregion

        #region "更新数据库"
        private string funString_DBSave()
        {
            string strSQL = "";
            #region "获得更新的值"
            string LoginName = txtLoginName.Value.funString_SQLToString();
            string Password = txtPassword.Value.funString_SQLToString();
            string UserNameEN = LoginName;// txtUserNameEN.Value.funString_SQLToString();
            string UserNameCN = LoginName;// txtUserNameCN.Value.funString_SQLToString();
            string Gender = cboGender.funComboBox_SelectedValue();
            string Email = txtEmail.Value.funString_SQLToString();
            string RegionName = cboRegionName.funComboBox_SelectedValue();
            string DutyID = cboDutyID.funComboBox_SelectedValue().funInt_StringToInt(0).ToString();

            string CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string UpdateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string DeleteDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            List<string> lstLimited = tveSystemLimited.TreeViewCheckNodeValues;
            string SystemLimited = "";
            for (int i = 0; i < lstLimited.Count; i++)
            {
                SystemLimited += lstLimited[i] + ",";
            }
            if (SystemLimited != "")
            {
                SystemLimited = "," + SystemLimited;
            }
            else
            {
                SystemLimited = ",,";
            }
            #endregion

            //strSQL = "UPDATE CHub_Management_LoginUser SET LoginName ='" + LoginName + "', Password ='" + Password + "'";
            strSQL = "UPDATE CHub_Management_LoginUser SET Password ='" + Password + "'";
            strSQL += ", UserNameEN ='" + UserNameEN + "', UserNameCN ='" + UserNameCN + "', Gender = '" + Gender + "', Email = '" + Email + "', RegionName = '" + RegionName + "', DutyID = " + DutyID + " ";
            strSQL += ", UpdateDate ='" + UpdateDate + "', UpdateUserID ='" + objLoginUserInfo.ID + "' where ID='" + objLoginUserInfo.ID + "'";
            objOperationLog.DoLog(strSQL, "修改用户", objLoginUserInfo.ID);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "submitOk", "alert('保存数据成功！');window.location='MyInformation.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "submitError", "alert('保存数据出错！" + strError.funString_JsToString() + "');", true);
            }
        }
    }
}
