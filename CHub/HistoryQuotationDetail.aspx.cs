using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IdioSoft.ClassCommon;
using System.Web.UI.HtmlControls;

namespace CHub
{
    public partial class HistoryQuotationDetail : ClassLibrary.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string BriefID = "";
            #region "得到传递的参数"
            try
            {
                BriefID = Request.QueryString["ID"].ToString();
                BriefID = BriefID.funUuid_StringToUniqueidentifier(Guid.NewGuid().ToString()).ToString();
                ViewState["BriefID"] = BriefID;
            }
            catch
            {
                BriefID = Guid.NewGuid().ToString();
                ViewState["BriefID"] = BriefID;
            }
            #endregion

            subDBDetail_Brief(BriefID);
            int intCount = objClassDbAccess.funString_SQLExecuteScalar("SELECT COUNT(*) AS CountRecords FROM CHub_Info_ExportDetail where BriefID='" + BriefID + "'").funInt_StringToInt(0);
            if (intCount > 0)
            {
                subDBDetail_Detail(BriefID);
            }

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

                string strLimited = ((IdioSoft.Public.LoginUserInfo)Session["UserInfo"]).SystemLimited;
                if (ViewState["PageType"].ToString().ToLower() != "my")
                {
                    ((CHub.Main)Master).TitleName = "History Quotation List";
                    if (objLoginUserInfo.funBln_Limited("6000", strLimited))
                    {
                        lnkbtnDelete.Visible = true;
                    }
                    else
                    {
                        lnkbtnDelete.Visible = false;
                    }
                }
                else
                {
                    ((CHub.Main)Master).TitleName = "My History Quotation";
                    if (objLoginUserInfo.funBln_Limited("7000", strLimited))
                    {
                        lnkbtnDelete.Visible = true;
                    }
                    else
                    {
                        lnkbtnDelete.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// 报表文字信息
        /// </summary>
        /// <param name="BriefID"></param>
        private void subDBDetail_Brief(string BriefID)
        {
            DataSet ds = new DataSet();
            ds = objClassDbAccess.funDataset_SQLExecuteNonQuery("SELECT Customer, Channel, Project, QuotationNo, BonusRate, SalesRegion, BUResponsible, TotalKP, TotalTP, GrossMargin FROM CHub_Info_ExportBrief where ID='" + BriefID + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblCustomer.Text = ds.Tables[0].Rows[0]["Customer"].ToString();
                lblChannel.Text = ds.Tables[0].Rows[0]["Channel"].ToString();
                lblProject.Text = ds.Tables[0].Rows[0]["Project"].ToString();
                lblQuotationNo.Text = ds.Tables[0].Rows[0]["QuotationNo"].ToString();


                lblSalesRegion.Text = ds.Tables[0].Rows[0]["SalesRegion"].ToString();
                lblBUResponsible.Text = ds.Tables[0].Rows[0]["BUResponsible"].ToString();

                //lblLP.Text = ds.Tables[0].Rows[0]["TotalLP"].ToString();
                lblKP.Text = ds.Tables[0].Rows[0]["TotalKP"].ToString();
                if (objLoginUserInfo.funBln_Limited("5000", objLoginUserInfo.SystemLimited))
                {
                    lblBonusRate.Text = (decimal.Parse(ds.Tables[0].Rows[0]["BonusRate"].ToString()) * decimal.Parse("100")).ToString("F2") + "%";
                    trBonusRate.Visible = true;

                    lblTP.Text = ds.Tables[0].Rows[0]["TotalTP"].ToString();
                    trTP.Visible = true;

                    try
                    {
                        lblGrossMargin.Text = (decimal.Parse(ds.Tables[0].Rows[0]["GrossMargin"].ToString()) * decimal.Parse("100")).ToString("F2") + "%";
                    }
                    catch
                    {
                        lblGrossMargin.Text = ds.Tables[0].Rows[0]["GrossMargin"].ToString();
                    }
                    trGrossMargin.Visible = true;
                }
                else
                {
                    trBonusRate.Visible = false;
                    trTP.Visible = false;
                    trGrossMargin.Visible = false;
                }
            }
        }
        /// <summary>
        /// 选件信息
        /// </summary>
        /// <param name="BriefID"></param>
        private void subDBDetail_Detail(string BriefID)
        {
            //清空div
            divList.Controls.Clear();

            HtmlTable objTable = new HtmlTable();
            HtmlTableRow objRow = new HtmlTableRow();
            HtmlTableCell objCell = new HtmlTableCell();

            objTable = new HtmlTable();
            objTable.CellPadding = 0;
            objTable.CellSpacing = 0;
            objTable.Border = 0;
            objTable.Width = "100%";

            objRow = new HtmlTableRow();
            objCell = new HtmlTableCell();
            if (objLoginUserInfo.funBln_Limited("5000", objLoginUserInfo.SystemLimited))
            {
                objCell.ColSpan = 6;
            }
            else
            {
                objCell.ColSpan = 5;
            }
            objCell.InnerHtml = "<p><strong>Options List</strong></p>";
            objCell.Attributes.Add("style", "padding-top: 4px; padding-bottom: 4px;");
            objRow.Cells.Add(objCell);

            #region "列头行"
            objRow = new HtmlTableRow();

            //显示MLFB
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "MLFB-Z=<br>Options";//"产品名称";
            objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
            objCell.Width = "30%";
            objRow.Cells.Add(objCell);

            //显示数量
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "Quantity";// "需求数量";
            objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
            objCell.Width = "15%";
            objRow.Cells.Add(objCell);

            //折扣率
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "Discount<br>Factor";//"折扣率";
            objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
            objCell.Width = "15%";
            objRow.Cells.Add(objCell);

            //价格
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "LP<br>(w/o VAT)";//"价格";
            objCell.Align = "right";
            objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
            objCell.Attributes.Add("nowrap", "nowrap");
            objRow.Cells.Add(objCell);

            //价格
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "K-Price<br>(w/o VAT)";//"价格";
            objCell.Align = "right";
            objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
            objCell.Attributes.Add("nowrap", "nowrap");
            objRow.Cells.Add(objCell);

            //成本价
            if (objLoginUserInfo.funBln_Limited("5000", objLoginUserInfo.SystemLimited))
            {
                objCell = new HtmlTableCell();
                objCell.InnerHtml = "TP<br>(w/o VAT)";
                objCell.Align = "right";
                objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
                objCell.Attributes.Add("nowrap", "nowrap");
                objRow.Cells.Add(objCell);

                objCell = new HtmlTableCell();
                objCell.InnerHtml = "Gross<br>Margin";
                objCell.Align = "Center";
                objCell.Attributes.Add("style", "padding:4px;border-bottom:1px solid #CCCCCC;");
                objCell.Attributes.Add("nowrap", "nowrap");
                objRow.Cells.Add(objCell);
            }

            objTable.Rows.Add(objRow);
            #endregion

            DataSet ds = new DataSet();
            ds = objClassDbAccess.funDataset_SQLExecuteNonQuery("SELECT MLFBOptions, QTY, DiscountFactor, LP, KP, TP, GrossMargin FROM CHub_Info_ExportDetail WHERE (BriefID = '" + BriefID + "') Order By OrderNo");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                #region "循环画产品信息"
                objRow = new HtmlTableRow();

                //显示MLFB+Options
                objCell = new HtmlTableCell();
                objCell.InnerHtml = "<p>" + ds.Tables[0].Rows[i]["MLFBOptions"].ToString() + "</p>";
                objCell.Attributes.Add("style", "padding:4px; ");
                objRow.Cells.Add(objCell);

                //显示数量
                objCell = new HtmlTableCell();
                objCell.InnerHtml = "<p>" + ds.Tables[0].Rows[i]["QTY"].ToString() + "</p>";
                objCell.Attributes.Add("style", "padding:4px; ");
                objRow.Cells.Add(objCell);

                //折扣率
                objCell = new HtmlTableCell();
                objCell.InnerHtml = "<p>" + (decimal.Parse(ds.Tables[0].Rows[i]["DiscountFactor"].ToString()) * decimal.Parse("100")).ToString("F2") + "%</p>";
                objCell.Attributes.Add("style", "padding:4px; ");
                objRow.Cells.Add(objCell);

                //客户价
                objCell = new HtmlTableCell();
                objCell.InnerHtml = "<p>" + ds.Tables[0].Rows[i]["LP"].ToString() + "</p>";
                objCell.Attributes.Add("style", "padding:4px; ");
                objCell.Align = "right";
                objRow.Cells.Add(objCell);

                //客户价
                objCell = new HtmlTableCell();
                objCell.InnerHtml = "<p>" + ds.Tables[0].Rows[i]["KP"].ToString() + "</p>";
                objCell.Attributes.Add("style", "padding:4px; ");
                objCell.Align = "right";
                objRow.Cells.Add(objCell);

                //成本价
                if (objLoginUserInfo.funBln_Limited("5000", objLoginUserInfo.SystemLimited))
                {
                    objCell = new HtmlTableCell();
                    objCell.InnerHtml = "<p>" + ds.Tables[0].Rows[i]["TP"].ToString() + "</p>";
                    objCell.Attributes.Add("style", "padding:4px; ");
                    objCell.Align = "right";
                    objRow.Cells.Add(objCell);

                    objCell = new HtmlTableCell();
                    objCell.InnerHtml = "<p>" + (decimal.Parse(ds.Tables[0].Rows[i]["GrossMargin"].ToString()) * decimal.Parse("100")).ToString("F2") + "%</p>";
                    objCell.Attributes.Add("style", "padding:4px; ");
                    objCell.Align = "right";
                    objRow.Cells.Add(objCell);
                }

                objTable.Rows.Add(objRow);
                #endregion

            }
            divList.Controls.Add(objTable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            string strSQL = "";
            strSQL = "update CHub_Info_ExportBrief set IsDel=1 where ID='" + ViewState["BriefID"].ToString() + "'";
            string strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL);
            strSQL = "Update CHub_Info_ExportDetail set IsDel=1 where BriefID='" + ViewState["BriefID"].ToString() + "'";
            strError += objClassDbAccess.funString_SQLExecuteNonQuery(strSQL);

            if (strError == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteSuccessful", "alert('成功删除，请关闭此页面，并且刷新列表！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteError", "alert('删除数据出错！" + strError.funString_JsToString() + "');", true);
            }
        }
    }
}
