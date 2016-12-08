using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IdioSoft.ClassCommon;

namespace CHub
{
    public partial class HistoryQuotationDefault : ClassLibrary.Page
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
                #region "得到传递的参数"
                try
                {
                    ViewState["PageType"] = Request.QueryString["Type"].ToString().ToLower();
                    if (ViewState["PageType"].ToString() != "list")
                    {
                        ViewState["PageType"] = "my";
                    }
                }
                catch
                {
                    ViewState["PageType"] = "my";
                }
                #endregion
                if (ViewState["PageType"].ToString().ToLower() != "my")
                {
                    ((CHub.Main)Master).TitleName = "History Quotation List";
                }
                else
                {
                    ((CHub.Main)Master).TitleName = "My History Quotation";
                }


            }

            //SearchGridList1.FieldSQL = "SELECT 登录名称, Email, 用户名称, 性别, 区域, 用户角色, 创建者_英文 FROM View_1_LoginUserList";
            //SearchGridList1.FieldSQL = "SELECT [Customer/OEM], Channel, Project, [Quotation No.], [Sales Region], [BU Responsible], [Create Date], [Create User] FROM View_1_ExportBriefList";
            SearchGridList1.FieldSQL = "SELECT [Customer/OEM], Channel, Project, [Quotation No.], [Sales Region], [BU Responsible], [Create User] FROM View_1_ExportBriefList";
            SearchGridList1.GridFieldKeyName = "ID";

            //SearchGridList1.SQL = "select ID, 登录名称, Email, 用户名称, 性别, 区域, 用户角色, 创建时间, 创建者_英文 from View_1_LoginUserList where 1=1";
            string strSQL = "SELECT ID, [Create Date], [BU Responsible], [Sales Region], [Customer/OEM], Channel, Project, [Quotation No.], [K-Price], TP, [Gross Margin] FROM View_1_ExportBriefList where 1=1";
            if (!objLoginUserInfo.funBln_Limited("6000", objLoginUserInfo.SystemLimited) || ViewState["PageType"].ToString().ToLower() == "my")
            {
                strSQL = strSQL + " and CreateUserID='" + objLoginUserInfo.ID + "'";
            }
            SearchGridList1.SQL = strSQL;
            //SearchGridList1.btnAddnewClick += new LDBUTools.ControlLibrary.SearchGridList.delgbtnAddnewClick(SearchGridList1_btnAddnewClick);
            //SearchGridList1.btnModifyClick += new LDBUTools.ControlLibrary.SearchGridList.delgbtnModifyClick(SearchGridList1_btnModifyClick);
            SearchGridList1.btnDeleteClick += new CHub.ControlLibrary.SearchGridList.delgbtnDeleteClick(SearchGridList1_btnDeleteClick);
            SearchGridList1.btnDetailClick += new CHub.ControlLibrary.SearchGridList.delgbtnDetailClick(SearchGridList1_btnDetailClick);

            SearchGridList1.IsAutoRowDatabound = false;
            SearchGridList1.grdRowDataBound += new CHub.ControlLibrary.SearchGridList.delgrdMainRowDataBound(SearchGridList1_grdRowDataBound);

            string strLimited = ((IdioSoft.Public.LoginUserInfo)Session["UserInfo"]).SystemLimited;
            SearchGridList1.IsStop = false;
            SearchGridList1.IsAddnew = false;
            SearchGridList1.IsModify = false;
            //SearchGridList1.IsDelete = false;
            if (ViewState["PageType"].ToString().ToLower() != "my")
            {
                if (objLoginUserInfo.funBln_Limited("6000", strLimited))
                {
                    SearchGridList1.IsDelete = true;
                }
                else
                {
                    SearchGridList1.IsDelete = false;
                }
            }
            else
            {
                if (objLoginUserInfo.funBln_Limited("7000", strLimited))
                {
                    SearchGridList1.IsDelete = true;
                }
                else
                {
                    SearchGridList1.IsDelete = false;
                }
            }

            SearchGridList1.IsDetail = false;

        }


        #region "按钮事件"
        //protected void SearchGridList1_btnAddnewClick(object o, EventArgs e)
        //{
        //    Response.Redirect("LUOperation.aspx?Type=addnew");
        //}
        //protected void SearchGridList1_btnModifyClick(object o, EventArgs e)
        //{
        //    string strID = SearchGridList1.GridSelectID;
        //    if (strID.ToString().Trim() != "")
        //    {
        //        Response.Redirect("LUOperation.aspx?ID=" + strID + "&Type=modify");
        //    }
        //}
        //protected void SearchGridList1_btnDeleteClick(object o, EventArgs e)
        //{
        //    string strID = SearchGridList1.GridSelectID;
        //    if (strID.ToString().Trim() != "")
        //    {
        //        string strSQL = "";
        //        strSQL = "update CHub_Management_LoginUser set isDel=1, DeleteDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', DeleteUserID='" + objLoginUserInfo.ID + "' where ID='" + strID + "'";
        //        string strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL);
        //        SearchGridList1.subgrdMain_Load();
        //    }
        //}
        protected void SearchGridList1_btnDetailClick(object o, EventArgs e)
        {
            string strID = SearchGridList1.GridSelectID;
            if (strID.ToString().Trim() != "")
            {
                Response.Redirect("HistoryQuotationDetail.aspx?ID=" + strID + "&Type=" + ViewState["PageType"].ToString());
            }
        }
        protected void SearchGridList1_btnDeleteClick(object o, EventArgs e)
        {
            string strID = SearchGridList1.GridSelectID;
            if (strID.ToString().Trim() != "")
            {
                string strSQL = "";
                strSQL = "update CHub_Info_ExportBrief set IsDel=1 where ID='" + strID + "'";
                string strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL);
                strSQL = "Update CHub_Info_ExportDetail set IsDel=1 where BriefID='" + strID + "'";
                strError += objClassDbAccess.funString_SQLExecuteNonQuery(strSQL);

                if (strError == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteSuccessful", "alert('成功删除！');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteError", "alert('删除数据出错！" + strError.funString_JsToString() + "');", true);
                }
            }

            SearchGridList1.subgrdMain_Load();
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
                try
                {
                    e.Row.Cells[2].Text = "<a href=\"HistoryQuotationDetail.aspx?ID=" + e.Row.Cells[1].Text + "&Type=" + ViewState["PageType"].ToString() + "\" target='_blank'  class='aLink'>" + e.Row.Cells[2].Text + "</a>";
                }
                catch
                {
                }
            }
        }

        #endregion
    }
}
