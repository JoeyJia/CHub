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
    public partial class AccountDetail : ClassLibrary.Page
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
                    UserID = "";
                }
                #endregion
                ViewState["UserID"] = UserID;
                #endregion
                ((CHub.Main)Master).TitleName = "LoginUserDetail";

                subTreeView_Load();
                //载入详细信息
                subDB_LoadDetail(UserID.funUuid_StringToUniqueidentifier("").funString_StringToDBString("Null"));

                tveSystemLimited.Enable = false;

                if (ViewState["UserID"].ToString() == objLoginUserInfo.ID)
                {
                    //本人，随时可以修改
                    lnkbtnModify.Visible = true;
                }
                else
                {
                    //非本人，判断是否有修改权限，有就可以修改，没有就不能修改
                    string strLimited = ((IdioSoft.Public.LoginUserInfo)Session["UserID"]).SystemLimited;
                    lnkbtnModify.Visible = ((IdioSoft.Public.LoginUserInfo)Session["UserID"]).funBln_Limited("1003", strLimited);
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
            tveSystemLimited.TreeViewSQL = "SELECT LimitedID, Description FROM CHub_Basic_Limited where 1=1 ";
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
                lblLoginName.Text = ds.Tables[0].Rows[0]["LoginName"].ToString();
                if (objLoginUserInfo.ID == ViewState["UserID"].ToString())
                {
                    lblPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                }
                else
                {
                    lblPassword.Text = "非本人，不能查看";
                    lblPassword.CssClass = "FontRed";
                }
                lblEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                lblUserNameEN.Text = ds.Tables[0].Rows[0]["UserNameEN"].ToString();
                lblUserNameCN.Text = ds.Tables[0].Rows[0]["UserNameCN"].ToString();
                lblGender.Text = ds.Tables[0].Rows[0]["Gender"].ToString();
                lblRegion.Text = ds.Tables[0].Rows[0]["RegionName"].ToString();
                lblDutyName.Text = ds.Tables[0].Rows[0]["DutyName"].ToString();

                string strSystemLimited = ds.Tables[0].Rows[0]["SystemLimited"].ToString();
                if (strSystemLimited.Trim() == "" || strSystemLimited.Trim() == ",,")
                {
                    trSystemLimited.Visible = false;
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

        /// <summary>
        /// 点击修改链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccountOperation.aspx?ID=" + ViewState["UserID"].ToString() + "&Type=modify");
        }
    }
}
