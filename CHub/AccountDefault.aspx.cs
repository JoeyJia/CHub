using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHub
{
    public partial class AccountDefault : ClassLibrary.Page
    {
        /// <summary>
        /// 载入窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((CHub.Main)Master).TitleName = "LoginUserList";
            }

            SearchGridList1.FieldSQL = "SELECT [Login Name], Email, Gender, Region, Duty, [Create User] FROM View_Management_LoginUser_List";
            SearchGridList1.GridFieldKeyName = "ID";

            if (objLoginUserInfo.DutyID == ClassLibrary.clsUserDuty.SystemAdmin)
            {
                SearchGridList1.SQL = "select  ID, [Login Name], Email, Gender, Region, Duty, [Create Date], [Create User] from View_Management_LoginUser_List where 1=1";
            }
            else
            {
                SearchGridList1.SQL = "select  ID, [Login Name], Email, Gender, Region, Duty, [Create Date], [Create User] from View_Management_LoginUser_List where DutyID IN (1," + objLoginUserInfo.DutyID.ToString() + ")";
            }
            SearchGridList1.btnAddnewClick += new CHub.ControlLibrary.SearchGridList.delgbtnAddnewClick(SearchGridList1_btnAddnewClick);
            SearchGridList1.btnModifyClick += new CHub.ControlLibrary.SearchGridList.delgbtnModifyClick(SearchGridList1_btnModifyClick);
            SearchGridList1.btnDeleteClick += new CHub.ControlLibrary.SearchGridList.delgbtnDeleteClick(SearchGridList1_btnDeleteClick);
            SearchGridList1.btnDetailClick += new CHub.ControlLibrary.SearchGridList.delgbtnDetailClick(SearchGridList1_btnDetailClick);
            SearchGridList1.IsStop = false;
            SearchGridList1.IsAutoRowDatabound = false;

            SearchGridList1.grdRowDataBound += new CHub.ControlLibrary.SearchGridList.delgrdMainRowDataBound(SearchGridList1_grdRowDataBound);


            string strLimited = ((IdioSoft.Public.LoginUserInfo)Session["UserInfo"]).SystemLimited;
            SearchGridList1.IsAddnew = ((IdioSoft.Public.LoginUserInfo)Session["UserInfo"]).funBln_Limited("1002", strLimited);
            SearchGridList1.IsModify = ((IdioSoft.Public.LoginUserInfo)Session["UserInfo"]).funBln_Limited("1003", strLimited);
            SearchGridList1.IsDelete = ((IdioSoft.Public.LoginUserInfo)Session["UserInfo"]).funBln_Limited("1004", strLimited);

        }


        #region "按钮事件"
        protected void SearchGridList1_btnAddnewClick(object o, EventArgs e)
        {
            Response.Redirect("AccountOperation.aspx?Type=addnew");
        }
        protected void SearchGridList1_btnModifyClick(object o, EventArgs e)
        {
            string strID = SearchGridList1.GridSelectID;
            if (strID.ToString().Trim() != "")
            {
                Response.Redirect("AccountOperation.aspx?ID=" + strID + "&Type=modify");
            }
        }
        protected void SearchGridList1_btnDeleteClick(object o, EventArgs e)
        {
            string strID = SearchGridList1.GridSelectID;
            if (strID.ToString().Trim() != "")
            {
                string strSQL = "";
                strSQL = "update CHub_Management_LoginUser set isDel=1, DeleteDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', DeleteUserID='" + objLoginUserInfo.ID + "' where ID='" + strID + "'";
                string strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL);
                SearchGridList1.subgrdMain_Load();
            }
        }
        protected void SearchGridList1_btnDetailClick(object o, EventArgs e)
        {
            string strID = SearchGridList1.GridSelectID;
            if (strID.ToString().Trim() != "")
            {
                Response.Redirect("AccountDetail.aspx?ID=" + strID + "");
            }
        }
        private void SearchGridList1_grdRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //用于不换行属性
            if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.Footer && e.Row.RowType != DataControlRowType.Header)
            {
                //try
                //{
                //    e.Row.Cells[4].Text = e.Row.Cells[4].Text.Substring(0, 50); //Server.HtmlDecode(e.Row.Cells[4].Text).Substring(0, 50);//
                //}
                //catch
                //{
                //    e.Row.Cells[4].Text = e.Row.Cells[4].Text;//Server.HtmlDecode(e.Row.Cells[4].Text);//
                //}
            }
        }

        #endregion
    }
}
