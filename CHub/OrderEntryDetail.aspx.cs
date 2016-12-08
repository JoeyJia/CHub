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
    public partial class OrderEntryDetail : ClassLibrary.Page
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
                    SPRID = "";
                }
                #endregion
                ViewState["SPRID"] = SPRID;
                #endregion
                ((CHub.Main)Master).TitleName = "OrderEntryDetail";

                //载入详细信息
                subDB_LoadDetail(SPRID.funUuid_StringToUniqueidentifier("").funString_StringToDBString("Null"));
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
                lblSPRNo.Text = ds.Tables[0].Rows[0]["SPRNo"].ToString();
                lblMLFB.Text = ds.Tables[0].Rows[0]["MLFB"].ToString();
                //lblOptions.Text = ds.Tables[0].Rows[0]["Options"].ToString();
                lblVoltage.Text = ds.Tables[0].Rows[0]["Voltage"].ToString();
                lblQuantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                lblOthersPerUnit.Text = ds.Tables[0].Rows[0]["OthersPerUnit"].ToString();
                lblOthersPerItem.Text = ds.Tables[0].Rows[0]["OthersPerItem"].ToString();

                strSQL = "SELECT OptionValue, CurrentPrice FROM CHub_Info_SPROptions WHERE (IsDel = 0) AND (SPRID=" + SPRID + ")";
                ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
                string strOptions = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    strOptions += ds.Tables[0].Rows[i]["OptionValue"].ToString() + "," + ds.Tables[0].Rows[i]["CurrentPrice"].ToString() + "<br>";
                }
                lblOptions.Text = strOptions;
            }
        }

        /// <summary>
        /// 点击修改链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderEntry.aspx?ID=" + ViewState["SPRID"].ToString() + "&Type=modify");
        }
    }
}
