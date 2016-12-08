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
    public partial class OptionsDetail : ClassLibrary.Page
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
                    OptionsID = "";
                }
                #endregion
                ViewState["OptionsID"] = OptionsID;
                #endregion
                ((CHub.Main)Master).TitleName = "OptionsDetail";

                //载入详细信息
                subDB_LoadDetail(OptionsID.funUuid_StringToUniqueidentifier("").funString_StringToDBString("Null"));
            }
        }

        /// <summary>
        /// 载入用户的详细信息
        /// </summary>
        /// <param name="OptionsID"></param>
        private void subDB_LoadDetail(string OptionsID)
        {
            string strSQL = "SELECT  MLFB, OptionValue, CurrentPrice, IsDiscount, Type FROM View_Info_Options where ID=" + OptionsID;

            DataSet ds = new DataSet();
            ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMLFB.Text = ds.Tables[0].Rows[0]["MLFB"].ToString();
                lblOptions.Text = ds.Tables[0].Rows[0]["OptionValue"].ToString();
                lblPrice.Text = ds.Tables[0].Rows[0]["CurrentPrice"].ToString();
            }
        }

        /// <summary>
        /// 点击修改链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            Response.Redirect("OptionsOperation.aspx?ID=" + ViewState["OptionsID"].ToString() + "&Type=modify");
        }
    }
}
