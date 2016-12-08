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
    public partial class AccountOperation : ClassLibrary.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region "得到传递的参数"
                #region "UserID"
                string UserID = "";
                try
                {
                    UserID = Request.QueryString["ID"].ToString();
                }
                catch
                {
                    UserID = Guid.NewGuid().ToString();
                }
                #endregion
                ViewState["UserID"] = UserID;

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

                cboRegionName.subComboBox_LoadItems("SELECT RegionName FROM CHub_Basic_Region ORDER BY OrderNo", 0, null);//载入区域
                if (objLoginUserInfo.DutyID == ClassLibrary.clsUserDuty.SystemAdmin)
                {
                    cboDutyID.subComboBox_LoadItems("SELECT ID, DutyName FROM CHub_Basic_Duty ORDER BY OrderNo", 0, null);//载入用户角色
                }
                else
                {
                    cboDutyID.subComboBox_LoadItems("SELECT ID, DutyName FROM CHub_Basic_Duty WHERE ID IN (1," + objLoginUserInfo.DutyID.ToString() + ") ORDER BY OrderNo", 0, null);//载入用户角色
                }
                subTreeView_Load();//初始化系统权限

                if (OperType.ToLower() == "modify")
                {
                    ((CHub.Main)Master).TitleName = "LoginUserModify";
                    //载入详细信息
                    subDB_LoadDetail(UserID.funUuid_StringToUniqueidentifier("").funString_StringToDBString("Null"));
                }
                else
                {
                    ((CHub.Main)Master).TitleName = "LoginUserAddnew";
                    txtEmail.Value = "@Cummins.com";
                }
            }
        }

        /// <summary>
        /// 初始化系统权限
        /// </summary>
        private void subTreeView_Load()
        {
            tveSystemLimited.CheckALLTitle = "Check ALL";
            tveSystemLimited.ShowCheckBoxes = TreeNodeTypes.All;
            string strLimitedIDs = objLoginUserInfo.SystemLimited;
            if (strLimitedIDs.Length == 0)
            {
                strLimitedIDs = "0";
            }
            try
            {
                if (strLimitedIDs.Substring(0, 1) == ",")
                {
                    strLimitedIDs = strLimitedIDs.Substring(1);
                }
                if (strLimitedIDs.Substring(strLimitedIDs.Length - 1, 1) == ",")
                {
                    strLimitedIDs = strLimitedIDs.Substring(0, strLimitedIDs.Length - 1);
                }
            }
            catch
            {
                strLimitedIDs = "0";
            }
            if (objLoginUserInfo.DutyID != ClassLibrary.clsUserDuty.SystemAdmin)
            {
                strLimitedIDs = " AND LimitedID IN (" + strLimitedIDs + ")";
            }
            else
            {
                strLimitedIDs = "";
            }
            tveSystemLimited.TreeViewSQL = "SELECT LimitedID, Description FROM CHub_Basic_Limited where 1=1" + strLimitedIDs + " ORDER BY OrderNo";
            tveSystemLimited.TreeViewSQLParentID = "ParentID";
            tveSystemLimited.subNodes_Load(null, "0");
        }

        /// <summary>
        /// 载入用户的详细信息
        /// </summary>
        /// <param name="UserID"></param>
        private void subDB_LoadDetail(string UserID)
        {
            string strSQL = "SELECT LoginName, Password, Email, UserNameEN, UserNameCN, Gender, RegionName, DutyName, SystemLimited FROM View_Management_LoginUser_Detail where ID=" + UserID;

            DataSet ds = new DataSet();
            ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtLoginName.Value = ds.Tables[0].Rows[0]["LoginName"].ToString();
                if (objLoginUserInfo.ID == ViewState["UserID"].ToString())
                {
                    trPassword.Visible = true;
                    txtPassword.Value = ds.Tables[0].Rows[0]["Password"].ToString();
                }
                else
                {
                    trPassword.Visible = false;
                }
                txtEmail.Value = ds.Tables[0].Rows[0]["Email"].ToString();
                txtUserNameEN.Value = ds.Tables[0].Rows[0]["UserNameEN"].ToString();
                txtUserNameCN.Value = ds.Tables[0].Rows[0]["UserNameCN"].ToString();
                cboGender.subComboBox_SelectItemByValue(ds.Tables[0].Rows[0]["Gender"].ToString());
                cboRegionName.subComboBox_SelectItemByValue(ds.Tables[0].Rows[0]["RegionName"].ToString());
                //cboDutyID.subComboBox_SelectItemByValue(ds.Tables[0].Rows[0]["DutyName"].ToString());
                cboDutyID.subComboBox_SelectItemByText(ds.Tables[0].Rows[0]["DutyName"].ToString());

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
            }
        }

        #region "校验数据"
        private string funString_CheckInfo()
        {
            if (txtLoginName.Value.Trim() == "")
            {
                //return "登录名称不能为空";
                return "Login Name can not be empty!";
            }
            //如果是本人，或者是新增用户，那么判断密码
            if (objLoginUserInfo.ID == ViewState["UserID"].ToString() || ViewState["OperType"].ToString().ToLower() == "addnew")
            {
                if (txtPassword.Value.Trim() == "")
                {
                    return "Password can not be empty!";
                }
            }
            //if (txtUserNameEN.Value.Trim() == "")
            //{
            //    return "用户名称(英文)不能为空";
            //}
            string strSQL = "";
            if (ViewState["OperType"].ToString().Trim().ToLower() == "addnew")
            {
                strSQL = "select Count(*) from CHub_Management_LoginUser where LoginName='" + txtLoginName.Value.Trim() + "' and IsDel=0";
            }
            else
            {
                strSQL = "select Count(*) from CHub_Management_LoginUser where LoginName='" + txtLoginName.Value.Trim() + "' and ID<>'" + ViewState["UserID"].ToString() + "' and IsDel=0";
            }
            int intCount = objClassDbAccess.funString_SQLExecuteScalar(strSQL).funInt_StringToInt(0);
            if (intCount > 0)
            {
                //return "登录名称不能重复！";
                return "Login Name already exists, please input again!";
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
            //因为Cummins's WangYilin要求不需要使用用户名，她们创建登录名称时就使用用户名就行了，所以，代码中将用户自动补充为登录名称即可
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

            if (ViewState["OperType"].ToString().Trim().ToLower() == "addnew")
            {
                strSQL = @"INSERT INTO CHub_Management_LoginUser
                      (ID, LoginName, Password, Email, UserNameEN, UserNameCN, Gender, RegionName, DutyID, SystemLimited, IsDel, CreateDate, CreateUserID) VALUES (";
                strSQL += "'" + ViewState["UserID"].ToString() + "','" + LoginName + "', '" + Password + "', '" + Email + "', '" + UserNameEN + "', '" + UserNameCN + "', '" + Gender + "', '" + RegionName + "', " + DutyID + ", '" + SystemLimited + "'";
                strSQL += ", 0, '" + CreateDate + "', '" + objLoginUserInfo.ID + "')";
                objOperationLog.DoLog(strSQL, "新增用户", objLoginUserInfo.ID);
            }
            if (ViewState["OperType"].ToString().Trim().ToLower() == "modify")
            {
                strSQL = "UPDATE CHub_Management_LoginUser SET LoginName ='" + LoginName + "'";
                if (objLoginUserInfo.ID == ViewState["UserID"].ToString())
                {
                    strSQL += ", Password ='" + Password + "'";
                }
                strSQL += ", UserNameEN ='" + UserNameEN + "', UserNameCN ='" + UserNameCN + "', Gender = '" + Gender + "', Email = '" + Email + "', RegionName = '" + RegionName + "', DutyID = " + DutyID + ", SystemLimited = '" + SystemLimited + "'";
                strSQL += ", UpdateDate ='" + UpdateDate + "', UpdateUserID ='" + objLoginUserInfo.ID + "' where ID='" + ViewState["UserID"].ToString() + "'";
                objOperationLog.DoLog(strSQL, "修改用户", objLoginUserInfo.ID);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "submitOk", "alert('Save data success！');window.location='AccountDefault.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "submitError", "alert('保存数据出错！" + strError.funString_JsToString() + "');", true);
            }
        }
    }
}
