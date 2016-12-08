using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHub
{
    public partial class FileDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strFile = "";
            try
            {
                strFile = Request["File"].ToString();
            }
            catch
            {
                strFile = "";
            }
            if (!IsPostBack)
            {
                if (strFile.Length > 0)
                {
                    strFile = HttpContext.Current.Server.UrlDecode(strFile);

                    IdioSoft.Public.LoginUserInfo objLoginUserInfo;
                    if (Session["UserInfo"] != null)
                    {
                        objLoginUserInfo = (IdioSoft.Public.LoginUserInfo)Session["UserInfo"];
                    }
                    else
                    {
                        Session["UserInfo"] = new IdioSoft.Public.LoginUserInfo();
                        objLoginUserInfo = (IdioSoft.Public.LoginUserInfo)Session["UserInfo"];
                    }
                    string strDirPath = "Temp/ExcelExport/" + objLoginUserInfo.ID.Replace("-", "") + "/";

                    hlnkFile.NavigateUrl = strDirPath + strFile;
                    hlnkFile.Text = strFile;
                }
            }
        }
    }
}