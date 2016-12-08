using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CHub
{
    public partial class DriveOptionsDefault : ClassLibrary.Page
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
                ((CHub.Main)Master).TitleName = "DriveOptions";
            }

            SearchGridList1.FieldSQL = "SELECT MLFB, OptionValue FROM ViewDrive_Info_Options WHERE 1=0";
            SearchGridList1.GridFieldKeyName = "ID";
            SearchGridList1.IsShowSelect = true;

            SearchGridList1.SQL = "select  ID, MLFB, OptionValue, CurrentPrice, Type, [Create Date], [Create User] from ViewDrive_Info_Options where 1=1";
            SearchGridList1.btnAddnewClick += new CHub.ControlLibrary.SearchGridList.delgbtnAddnewClick(SearchGridList1_btnAddnewClick);
            SearchGridList1.btnModifyClick += new CHub.ControlLibrary.SearchGridList.delgbtnModifyClick(SearchGridList1_btnModifyClick);
            SearchGridList1.btnDeleteClick += new CHub.ControlLibrary.SearchGridList.delgbtnDeleteClick(SearchGridList1_btnDeleteClick);
            SearchGridList1.btnDetailClick += new CHub.ControlLibrary.SearchGridList.delgbtnDetailClick(SearchGridList1_btnDetailClick);
            SearchGridList1.IsOther1 = true;
            SearchGridList1.btnOther1Text = "导入";
            SearchGridList1.btnOther1Click += new CHub.ControlLibrary.SearchGridList.delgbtnOther1Click(SearchGridList1_btnOther1Click);
            SearchGridList1.IsStop = false;
            SearchGridList1.IsAutoRowDatabound = false;

            SearchGridList1.grdRowDataBound += new CHub.ControlLibrary.SearchGridList.delgrdMainRowDataBound(SearchGridList1_grdRowDataBound);


            //string strLimited = ((IdioSoft.Public.LoginUserInfo)Session["UserInfo"]).SystemLimited;
            //SearchGridList1.IsAddnew = ((IdioSoft.Public.LoginUserInfo)Session["UserInfo"]).funBln_Limited("1002", strLimited);
            //SearchGridList1.IsModify = ((IdioSoft.Public.LoginUserInfo)Session["UserInfo"]).funBln_Limited("1003", strLimited);
            //SearchGridList1.IsDelete = ((IdioSoft.Public.LoginUserInfo)Session["UserInfo"]).funBln_Limited("1004", strLimited);

        }


        #region "按钮事件"
        protected void SearchGridList1_btnAddnewClick(object o, EventArgs e)
        {
            Response.Redirect("DriveOptionsOperation.aspx?Type=addnew");
        }
        protected void SearchGridList1_btnModifyClick(object o, EventArgs e)
        {
            string strID = SearchGridList1.GridSelectID;
            if (strID.ToString().Trim() != "")
            {
                Response.Redirect("DriveOptionsOperation.aspx?ID=" + strID + "&Type=modify");
            }
        }
        protected void SearchGridList1_btnDeleteClick(object o, EventArgs e)
        {
            string strID = SearchGridList1.GridSelectID;
            if (strID.ToString().Trim() != "")
            {
                string strSQL = "";
                strSQL = "select MLFB, OptionValue from Drive_Info_LP where ID = '" + strID + "'";
                DataSet ds = new DataSet();
                ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    strSQL = "DELETE Drive_Info_LP where MLFB='" + ds.Tables[0].Rows[0]["MLFB"].ToString() + "' AND PriceType='Options' AND OptionValue='" + ds.Tables[0].Rows[0]["OptionValue"].ToString() + "'";
                    string strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL);
                }
                //string strType = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                //if (strType.ToString().Trim().ToLower() == "lp")
                //{
                //strSQL = "delete CHub_Info_LP where ID='" + strID + "'";
                //}
                //if (strType.ToString().Trim().ToLower() == "tp")
                //{
                //    strSQL = "delete CHub_Info_TP where ID='" + strID + "'";
                //}

                //string strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL);
                SearchGridList1.subgrdMain_Load();
            }
        }
        protected void SearchGridList1_btnDetailClick(object o, EventArgs e)
        {
            string strID = SearchGridList1.GridSelectID;
            if (strID.ToString().Trim() != "")
            {
                Response.Redirect("DriveOptionsDetail.aspx?ID=" + strID + "");
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
        protected void SearchGridList1_btnOther1Click(object o, EventArgs e)
        {
            Response.Redirect("DriveOptionsImport.aspx");
        }
        #endregion
    }
}
