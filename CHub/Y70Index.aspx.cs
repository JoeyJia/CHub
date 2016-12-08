using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace CHub
{
    public partial class Y70Index : ClassLibrary.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StringBuilder strTable = new StringBuilder();
                strTable.Append("<table cellpadding=\"4\" cellspacing=\"1\" border=\"0\" width=\"100%\" style=\"background-color:#CCCCCC;\">");
                strTable.Append("  <tr>");
                strTable.Append("    <td style=\"background-color:#EFEFEF; width:40px;\"><strong>Index</strong></td>");
                strTable.Append("    <td style=\"background-color:#EFEFEF;\"><strong>Description</strong></td>");
                strTable.Append("  </tr>");

                string strSQL = "SELECT V70Index, V70Description FROM CHub_Basic_V70 ORDER BY V70Index";
                DataSet ds = new DataSet();
                ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    strTable.Append("  <tr>");
                    strTable.Append("    <td style=\"background-color:#FFFFFF;\">" + ds.Tables[0].Rows[i]["V70Index"].ToString() + "</td>");
                    strTable.Append("    <td style=\"background-color:#FFFFFF;\">" + ds.Tables[0].Rows[i]["V70Description"].ToString() + "</td>");
                    strTable.Append("  </tr>");
                }
                strTable.Append("</table>");

                divList.InnerHtml = strTable.ToString();
            }

        }
    }
}