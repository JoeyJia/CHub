using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CHub.ClassLibrary
{
    public class Page : System.Web.UI.Page
    {
        #region "属性"
        /// <summary>
        /// 数据操作
        /// </summary>
        public IdioSoft.ClassDbAccess.ClassDbAccess objClassDbAccess
        {
            get
            {
                return new IdioSoft.ClassDbAccess.ClassDbAccess();
            }
        }
        /// <summary>
        /// 日志操作
        /// </summary>
        public IdioSoft.ClassCommon.OperationLog objOperationLog
        {
            get
            {
                return new IdioSoft.ClassCommon.OperationLog();
            }
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        public IdioSoft.Public.LoginUserInfo objLoginUserInfo
        {
            get
            {
                if (Session["UserInfo"] != null)
                {
                    return (IdioSoft.Public.LoginUserInfo)Session["UserInfo"];
                }
                else
                {
                    Session["UserInfo"] = new IdioSoft.Public.LoginUserInfo();
                    return (IdioSoft.Public.LoginUserInfo)Session["UserInfo"];
                }
            }
            set
            {
                Session["UserInfo"] = value;
            }
        }
        #endregion
    }
}
