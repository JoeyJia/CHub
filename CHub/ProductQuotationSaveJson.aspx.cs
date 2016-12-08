using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using IdioSoft.Public;

namespace CHub
{
    public partial class ProductQuotationSaveJson : ClassLibrary.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strJson = Request["data"].ToString();
            strJson = strJson.Replace("\r", "");
            strJson = strJson.Replace("\n", "");

            StringBuilder sb = new StringBuilder(strJson);
            sb.Remove(0, sb.ToString().IndexOf(":") + 1);
            sb.Remove(sb.ToString().Length - 1, 1);

            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<cMaterial> lstMaterial = jss.Deserialize<List<cMaterial>>(sb.ToString());  //解析Json成List<>

            objLoginUserInfo.PasteData = lstMaterial;
        }
    }
    
}