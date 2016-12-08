using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHub
{
    public partial class HomePage : ClassLibrary.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((CHub.Main)Master).TitleName = "Home";

                //根据登录用户权限判断显示的链接
                subHTMLDisplay_User();

                //显示欢迎信息
                //tdWelcome.InnerHtml = "" + objLoginUserInfo.UserNameEn + "，欢迎您登录系统! 您可以点击下面的链接来使用系统功能，也可以在导航的下拉菜单中选择使用系统功能。";
                tdWelcome.InnerHtml = "<p>" + objLoginUserInfo.UserNameEn + ", Welcome log in !You can click on the link below to use the system function, you can also navigate using the drop-down menu, choose system functions.</p>";
            }
        }

        /// <summary>
        /// 根据登录用户权限判断显示的链接
        /// </summary>
        private void subHTMLDisplay_User()
        {
            #region "Login User Management"
            if (objLoginUserInfo.funBln_Limited("1000", objLoginUserInfo.SystemLimited))
            {
                tableLoginUser.Visible = true;
                //Addnew
                if (objLoginUserInfo.funBln_Limited("1002", objLoginUserInfo.SystemLimited))
                {
                    trLoginUserOperation.Visible = true;
                }
                else
                {
                    trLoginUserOperation.Visible = false;
                }
                //List
                if (objLoginUserInfo.funBln_Limited("1001", objLoginUserInfo.SystemLimited))
                {
                    trLoginUserList.Visible = true;
                }
                else
                {
                    trLoginUserList.Visible = false;
                }
            }
            else
            {
                tableLoginUser.Visible = false;
            }
            #endregion

            #region "Motor Options Management"
            if (objLoginUserInfo.funBln_Limited("2000", objLoginUserInfo.SystemLimited))
            {
                tableOptions.Visible = true;
                //Addnew
                if (objLoginUserInfo.funBln_Limited("2002", objLoginUserInfo.SystemLimited))
                {
                    trOptionsOperation.Visible = true;
                }
                else
                {
                    trOptionsOperation.Visible = false;
                }
                //List
                if (objLoginUserInfo.funBln_Limited("2001", objLoginUserInfo.SystemLimited))
                {
                    trOptionsList.Visible = true;
                }
                else
                {
                    trOptionsList.Visible = false;
                }
            }
            else
            {
                tableOptions.Visible = false;
            }
            #endregion

            #region "Options Management"
            if (objLoginUserInfo.funBln_Limited("4000", objLoginUserInfo.SystemLimited))
            {
                tableDriveOptions.Visible = true;
                //Addnew
                if (objLoginUserInfo.funBln_Limited("4002", objLoginUserInfo.SystemLimited))
                {
                    trDriveOptionsOperation.Visible = true;
                }
                else
                {
                    trDriveOptionsOperation.Visible = false;
                }
                //List
                if (objLoginUserInfo.funBln_Limited("4001", objLoginUserInfo.SystemLimited))
                {
                    trDriveOptionsList.Visible = true;
                }
                else
                {
                    trDriveOptionsList.Visible = false;
                }
            }
            else
            {
                tableDriveOptions.Visible = false;
            }
            #endregion

            //tableKPDiscount
            if (objLoginUserInfo.funBln_Limited("7000", objLoginUserInfo.SystemLimited))
            {
                tableKPDiscount.Visible = true;
            }
            else
            {
                tableKPDiscount.Visible = false;
            }
        }
    }
}
