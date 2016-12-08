/*----------------------------------------------------------------------------------------
// Copyright (C) 2010 北京易迪欧科技有限公司版权所有。 
// Copyright (C) 2010 Beijing IDIO Science & Technology Co., Ltd. All Rights Reserved.
//
// 版本号：1.0
// Version:1.0
//
// 文件功能摘要：站点母版页
// File Function Summary:Site Master Page
// 
// 创建标识：2010.08.27 YangMei
// Create Flag:2010.08.27 YangMei
//
// 更新标识：
// Update Flag:
// 更新描述：
// Update Description:
//
// 更新标识：
// Update Flag:
// 更新描述：
// Update Description:
//----------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace LDBUPT
{
    public partial class Main : System.Web.UI.MasterPage
    {
        IdioSoft.Public.LoginUserInfo objLoginUserInfo = new IdioSoft.Public.LoginUserInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            #region "如果用户没有登录，那么不允许使用平台"
            //if (Request.RawUrl.ToLower() != "/default.aspx")
            //{
            if (((IdioSoft.Public.LoginUserInfo)Session["UserInfo"]) == null || ((IdioSoft.Public.LoginUserInfo)Session["UserInfo"]).ID == "")
            {
                Response.Redirect("Default.aspx");
            }
            //}
            #endregion
            objLoginUserInfo = (IdioSoft.Public.LoginUserInfo)Session["UserInfo"];

            if (!IsPostBack)
            {
                #region "如果登录者没有权限，那么不显示(取消，已经放到属性中设置了)"
                //if (!objLoginUserInfo.funBln_Limited("1000", objLoginUserInfo.SystemLimited))
                //{
                //    liHome1.Visible = false;
                //}
                //if (!objLoginUserInfo.funBln_Limited("2000", objLoginUserInfo.SystemLimited))
                //{
                //    liHome2.Visible = false;
                //}
                #endregion
            }

        }

        public string TitleName
        {
            set
            {
                if (Session["UserID"] != null)
                {
                    objLoginUserInfo = (IdioSoft.Public.LoginUserInfo)Session["UserID"];
                }
                else
                {
                    Session["UserID"] = new IdioSoft.Public.LoginUserInfo();
                    objLoginUserInfo = (IdioSoft.Public.LoginUserInfo)Session["UserID"];
                }
                //objLoginUserInfo = (IdioSoft.Public.LoginUserInfo)Session["UserInfo"];

                //<asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>

                string strValue = "";
                strValue = value;
                //strValue = strValue.ToLower();
                switch (strValue)
                {
                    case "LoginUserManagement":
                    case "LoginUserList":
                    case "LoginUserAddnew":
                    case "LoginUserModify":
                    case "LoginUserDetail":
                        #region "LoginUser"
                        //lblTitle.Text = "Login User Management";//当页显示的加粗标题
                        h3Title.InnerHtml = "Login User Management";//当页显示的加粗标题

                        //Home的下拉菜单
                        liHome1.InnerHtml = "<span class='active'>Login User Management</span>";
                        liHome3.InnerHtml = "<a href='ProductsQuotation.aspx?Type=Paste'>Motor Products Quotation(Copy/Paste)</a>";
                        liHome2.InnerHtml = "<a href='ProductsQuotationDrive.aspx?Type=Paste'>Drive Products Quotation(Copy/Paste)</a>";

                        //if (objLoginUserInfo.funBln_Limited("1000", objLoginUserInfo.SystemLimited))
                        //{
                        //    liHome1.InnerHtml = "<a href='AccountDefault.aspx'>Login User Management</a>";
                        //}
                        //else
                        //{
                        //    liHome1.Visible = false;
                        //}
                        if (objLoginUserInfo.funBln_Limited("2000", objLoginUserInfo.SystemLimited))
                        {
                            liHome8.InnerHtml = "<a href='OptionsDefault.aspx'>Motor Options Management</a>";
                        }
                        else
                        {
                            liHome8.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("4000", objLoginUserInfo.SystemLimited))
                        {
                            liHome10.InnerHtml = "<a href='DriveOptionsDefault.aspx'>Drive Options Management</a>";
                        }
                        else
                        {
                            liHome10.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("7000", objLoginUserInfo.SystemLimited))
                        {
                            liHome9.InnerHtml = "<a href='KPOperation.aspx'>Motor KP Discount</a>";
                        }
                        else
                        {
                            liHome9.Visible = false;
                        }

                        //二级导航
                        ddNavigationTwo.Visible = true;
                        hlnkNavigationTwo.NavigateUrl = "AccountDefault.aspx";
                        hlnkNavigationTwo.Text = "Login User Management";
                        if (strValue == "LoginUserManagement" || strValue == "LoginUserDetail")
                        {
                            hlnkNavigationTwo.CssClass = "link active";//显示成被选择样式
                            ddNavigationThree.Visible = false;//隐藏三级导航

                            //二级导航的下拉菜单
                            if (objLoginUserInfo.funBln_Limited("1002", objLoginUserInfo.SystemLimited))
                            {
                                liNavigationTwo1.InnerHtml = "<a href='AccountOperation.aspx'>Add New</a>";
                            }
                            else
                            {
                                liNavigationTwo1.Visible = false;
                            }
                            if (objLoginUserInfo.funBln_Limited("1001", objLoginUserInfo.SystemLimited))
                            {
                                liNavigationTwo2.InnerHtml = "<a href='AccountDefault.aspx'>User List</a>";
                            }
                            else
                            {
                                liNavigationTwo2.Visible = false;
                            }
                        }
                        else
                        {
                            //如果选择后面的，那么不将二级导航显示成被选择样式
                            hlnkNavigationTwo.CssClass = "link";

                            ddNavigationThree.Visible = true;//显示三级导航
                            if (strValue == "LoginUserList" || strValue == "LoginUserModify")
                            {
                                if (objLoginUserInfo.funBln_Limited("1002", objLoginUserInfo.SystemLimited))
                                {
                                    liNavigationTwo1.InnerHtml = "<a href='AccountOperation.aspx'>Add New</a>";
                                }
                                else
                                {
                                    liNavigationTwo1.Visible = false;
                                }
                                liNavigationTwo2.InnerHtml = "<span class='active'>User List</span>";

                                //三级导航内容
                                ddNavigationThree.InnerHtml = "<a href='AccountDefault.aspx' class='link active'>User List</a>";
                            }
                            else
                            {
                                liNavigationTwo1.InnerHtml = "<span class='active'>Add New</span>";
                                if (objLoginUserInfo.funBln_Limited("1001", objLoginUserInfo.SystemLimited))
                                {
                                    liNavigationTwo2.InnerHtml = "<a href='AccountDefault.aspx'>User List</a>";
                                }
                                else
                                {
                                    liNavigationTwo2.Visible = false;
                                }

                                //三级导航内容
                                ddNavigationThree.InnerHtml = "<a href='AccountOperation.aspx' class='link active'>Add New</a>";
                            }
                        }
                        #endregion
                        break;
                    case "SPRAddnew":
                    case "SPRDetail":
                    case "SPR":
                        #region "SPR"
                        h3Title.InnerHtml = "SPR";//当页显示的加粗标题
                        liHome3.InnerHtml = "<a href='ProductsQuotation.aspx'>Motor Products Quotation(Copy/Paste)</a>";
                        liHome2.InnerHtml = "<a href='ProductsQuotationDrive.aspx'>Drive Products Quotation(Copy/Paste)</a>";

                        liHome7.InnerHtml = "<span class='active'>SPR</span>";

                        //二级导航
                        hlnkNavigationTwo.NavigateUrl = "SPRDefault.aspx";
                        hlnkNavigationTwo.Text = "SPR";

                        //Home的下拉菜单
                        if (objLoginUserInfo.funBln_Limited("1000", objLoginUserInfo.SystemLimited))
                        {
                            liHome1.InnerHtml = "<a href='AccountDefault.aspx'>Login User Management</a>";
                        }
                        else
                        {
                            liHome1.Visible = false;
                        }                        
                        if (objLoginUserInfo.funBln_Limited("2000", objLoginUserInfo.SystemLimited))
                        {
                            liHome8.InnerHtml = "<a href='OptionsDefault.aspx'>Motor Options Management</a>";
                        }
                        else
                        {
                            liHome8.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("4000", objLoginUserInfo.SystemLimited))
                        {
                            liHome10.InnerHtml = "<a href='DriveOptionsDefault.aspx'>Drive Options Management</a>";
                        }
                        else
                        {
                            liHome10.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("7000", objLoginUserInfo.SystemLimited))
                        {
                            liHome9.InnerHtml = "<a href='KPOperation.aspx'>Motor KP Discount</a>";
                        }
                        else
                        {
                            liHome9.Visible = false;
                        }

                        //二级导航
                        ddNavigationTwo.Visible = true;
                        hlnkNavigationTwo.CssClass = "link active";//显示成被选择样式
                        ddNavigationThree.Visible = false;//隐藏三级导航

                        //二级导航的下拉菜单
                        divNavigationTwo.Visible = false;
                        #endregion
                        break;

                    case "OptionsAddnew":
                    case "OptionsDetail":
                    case "Options":
                        #region "MotorOptions"
                        h3Title.InnerHtml = "Motor Options Management";//当页显示的加粗标题
                        liHome3.InnerHtml = "<a href='ProductsQuotation.aspx'>Motor Products Quotation(Copy/Paste)</a>";
                        liHome2.InnerHtml = "<a href='ProductsQuotationDrive.aspx'>Drive Products Quotation(Copy/Paste)</a>";
                        liHome7.InnerHtml = "<a href='SPRDefault.aspx'>SPR</a>";

                        liHome8.InnerHtml = "<span class='active'>Motor Options</span>";

                        //二级导航
                        hlnkNavigationTwo.NavigateUrl = "OptionsDefault.aspx";
                        hlnkNavigationTwo.Text = "Motor Options";

                        //Home的下拉菜单
                        if (objLoginUserInfo.funBln_Limited("1000", objLoginUserInfo.SystemLimited))
                        {
                            liHome1.InnerHtml = "<a href='AccountDefault.aspx'>Login User Management</a>";
                        }
                        else
                        {
                            liHome1.Visible = false;
                        }                        
                        if (objLoginUserInfo.funBln_Limited("4000", objLoginUserInfo.SystemLimited))
                        {
                            liHome10.InnerHtml = "<a href='DriveOptionsDefault.aspx'>Drive Options Management</a>";
                        }
                        else
                        {
                            liHome10.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("7000", objLoginUserInfo.SystemLimited))
                        {
                            liHome9.InnerHtml = "<a href='KPOperation.aspx'>Motor KP Discount</a>";
                        }
                        else
                        {
                            liHome9.Visible = false;
                        }

                        //二级导航
                        ddNavigationTwo.Visible = true;
                        hlnkNavigationTwo.CssClass = "link active";//显示成被选择样式
                        ddNavigationThree.Visible = false;//隐藏三级导航

                        //二级导航的下拉菜单
                        divNavigationTwo.Visible = false;
                        #endregion
                        break;

                    case "DriveOptionsAddnew":
                    case "DriveOptionsDetail":
                    case "DriveOptions":
                        #region "DriveOptions"
                        h3Title.InnerHtml = "Drive Options Management";//当页显示的加粗标题
                        liHome3.InnerHtml = "<a href='ProductsQuotation.aspx'>Motor Products Quotation(Copy/Paste)</a>";
                        liHome2.InnerHtml = "<a href='ProductsQuotationDrive.aspx'>Drive Products Quotation(Copy/Paste)</a>";
                        liHome7.InnerHtml = "<a href='SPRDefault.aspx'>SPR</a>";

                        liHome8.InnerHtml = "<span class='active'>Drive Options</span>";

                        //二级导航
                        hlnkNavigationTwo.NavigateUrl = "DriveOptionsDefault.aspx";
                        hlnkNavigationTwo.Text = "Drive Options";

                        //Home的下拉菜单
                        if (objLoginUserInfo.funBln_Limited("1000", objLoginUserInfo.SystemLimited))
                        {
                            liHome1.InnerHtml = "<a href='AccountDefault.aspx'>Login User Management</a>";
                        }
                        else
                        {
                            liHome1.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("2000", objLoginUserInfo.SystemLimited))
                        {
                            liHome8.InnerHtml = "<a href='OptionsDefault.aspx'>Motor Options Management</a>";
                        }
                        else
                        {
                            liHome8.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("7000", objLoginUserInfo.SystemLimited))
                        {
                            liHome9.InnerHtml = "<a href='KPOperation.aspx'>Motor KP Discount</a>";
                        }
                        else
                        {
                            liHome9.Visible = false;
                        }

                        //二级导航
                        ddNavigationTwo.Visible = true;
                        hlnkNavigationTwo.CssClass = "link active";//显示成被选择样式
                        ddNavigationThree.Visible = false;//隐藏三级导航

                        //二级导航的下拉菜单
                        divNavigationTwo.Visible = false;
                        #endregion
                        break;

                    case "KPOperation":
                        #region "KPOperation"
                        h3Title.InnerHtml = "Motor KP Discount";//当页显示的加粗标题

                        //Home的下拉菜单
                        if (objLoginUserInfo.funBln_Limited("1000", objLoginUserInfo.SystemLimited))
                        {
                            liHome1.InnerHtml = "<a href='AccountDefault.aspx'>Login User Management</a>";
                        }
                        else
                        {
                            liHome1.Visible = false;
                        }                        
                        if (objLoginUserInfo.funBln_Limited("2000", objLoginUserInfo.SystemLimited))
                        {
                            liHome8.InnerHtml = "<a href='OptionsDefault.aspx'>Motor Options Management</a>";
                        }
                        else
                        {
                            liHome8.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("4000", objLoginUserInfo.SystemLimited))
                        {
                            liHome10.InnerHtml = "<a href='DriveOptionsDefault.aspx'>Drive Options Management</a>";
                        }
                        else
                        {
                            liHome10.Visible = false;
                        }

                        //二级导航
                        ddNavigationTwo.Visible = true;
                        hlnkNavigationTwo.NavigateUrl = "KPOperation.aspx";
                        hlnkNavigationTwo.Text = "Motor KP Discount";

                        hlnkNavigationTwo.CssClass = "link active";//显示成被选择样式
                        ddNavigationThree.Visible = false;//隐藏三级导航

                        //二级导航的下拉菜单
                        divNavigationTwo.Visible = false;
                        #endregion
                        break;

                    case "ProductsQuotation(Paste)":
                        #region "ProductsQuotation"
                        //lblTitle.Text = "Product Quotation(Paste)";//当页显示的加粗标题
                        h3Title.InnerHtml = "Motor Products Quotation(Copy/Paste)";//当页显示的加粗标题

                        liHome3.InnerHtml = "<span class='active'>Motor Products Quotation(Copy/Paste)</span>";
                        liHome2.InnerHtml = "<a href='ProductsQuotationDrive.aspx?Type=Paste'>Drive Products Quotation(Copy/Paste)</a>";

                        //二级导航
                        hlnkNavigationTwo.NavigateUrl = "ProductsQuotation.aspx?Type=Paste";
                        hlnkNavigationTwo.Text = "Motor Product Quotation(Copy/Paste)";

                        //h3Title.InnerHtml = strValue;

                        //Home的下拉菜单
                        if (objLoginUserInfo.funBln_Limited("1000", objLoginUserInfo.SystemLimited))
                        {
                            liHome1.InnerHtml = "<a href='AccountDefault.aspx'>Login User Management</a>";
                        }
                        else
                        {
                            liHome1.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("2000", objLoginUserInfo.SystemLimited))
                        {
                            liHome8.InnerHtml = "<a href='OptionsDefault.aspx'>Motor Options Management</a>";
                        }
                        else
                        {
                            liHome8.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("4000", objLoginUserInfo.SystemLimited))
                        {
                            liHome10.InnerHtml = "<a href='DriveOptionsDefault.aspx'>Drive Options Management</a>";
                        }
                        else
                        {
                            liHome10.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("7000", objLoginUserInfo.SystemLimited))
                        {
                            liHome9.InnerHtml = "<a href='KPOperation.aspx'>Motor KP Discount</a>";
                        }
                        else
                        {
                            liHome9.Visible = false;
                        }

                        //二级导航
                        ddNavigationTwo.Visible = true;
                        hlnkNavigationTwo.CssClass = "link active";//显示成被选择样式
                        ddNavigationThree.Visible = false;//隐藏三级导航

                        //二级导航的下拉菜单
                        divNavigationTwo.Visible = false;
                        #endregion
                        break;

                    case "DriveProductsQuotation(Paste)":
                        #region "ProductsQuotation"
                        //lblTitle.Text = "Product Quotation(Paste)";//当页显示的加粗标题
                        h3Title.InnerHtml = "Drive Products Quotation(Copy/Paste)";//当页显示的加粗标题

                        liHome2.InnerHtml = "<span class='active'>Drive Products Quotation(Copy/Paste)</span>";
                        liHome3.InnerHtml = "<a href='ProductsQuotation.aspx?Type=Paste'>Motor Products Quotation(Copy/Paste)</a>";

                        //二级导航
                        hlnkNavigationTwo.NavigateUrl = "ProductsQuotationDrive.aspx?Type=Paste";
                        hlnkNavigationTwo.Text = "Drive Product Quotation(Copy/Paste)";

                        //h3Title.InnerHtml = strValue;

                        //Home的下拉菜单
                        if (objLoginUserInfo.funBln_Limited("1000", objLoginUserInfo.SystemLimited))
                        {
                            liHome1.InnerHtml = "<a href='AccountDefault.aspx'>Login User Management</a>";
                        }
                        else
                        {
                            liHome1.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("2000", objLoginUserInfo.SystemLimited))
                        {
                            liHome8.InnerHtml = "<a href='OptionsDefault.aspx'>Motor Options Management</a>";
                        }
                        else
                        {
                            liHome8.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("4000", objLoginUserInfo.SystemLimited))
                        {
                            liHome10.InnerHtml = "<a href='DriveOptionsDefault.aspx'>Drive Options Management</a>";
                        }
                        else
                        {
                            liHome10.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("7000", objLoginUserInfo.SystemLimited))
                        {
                            liHome9.InnerHtml = "<a href='KPOperation.aspx'>Motor KP Discount</a>";
                        }
                        else
                        {
                            liHome9.Visible = false;
                        }

                        //二级导航
                        ddNavigationTwo.Visible = true;
                        hlnkNavigationTwo.CssClass = "link active";//显示成被选择样式
                        ddNavigationThree.Visible = false;//隐藏三级导航

                        //二级导航的下拉菜单
                        divNavigationTwo.Visible = false;
                        #endregion
                        break;

                    case "MyInformation":
                        #region "MyInformation"
                        h3Title.InnerHtml = "My Information";//当页显示的加粗标题

                        //Home的下拉菜单
                        if (objLoginUserInfo.funBln_Limited("1000", objLoginUserInfo.SystemLimited))
                        {
                            liHome1.InnerHtml = "<a href='AccountDefault.aspx'>Login User Management</a>";
                        }
                        else
                        {
                            liHome1.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("2000", objLoginUserInfo.SystemLimited))
                        {
                            liHome8.InnerHtml = "<a href='OptionsDefault.aspx'>Motor Options Management</a>";
                        }
                        else
                        {
                            liHome8.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("4000", objLoginUserInfo.SystemLimited))
                        {
                            liHome10.InnerHtml = "<a href='DriveOptionsDefault.aspx'>Drive Options Management</a>";
                        }
                        else
                        {
                            liHome10.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("7000", objLoginUserInfo.SystemLimited))
                        {
                            liHome9.InnerHtml = "<a href='KPOperation.aspx'>Motor KP Discount</a>";
                        }
                        else
                        {
                            liHome9.Visible = false;
                        }
                        liHome3.InnerHtml = "<a href='ProductsQuotation.aspx?Type=Paste'>Motor Products Quotation(Copy/Paste)</a>";
                        liHome2.InnerHtml = "<a href='ProductsQuotationDrive.aspx?Type=Paste'>Drive Products Quotation(Copy/Paste)</a>";
                        liHome5.InnerHtml = "<span class='active'>My Information</span>";

                        //二级导航
                        ddNavigationTwo.Visible = true;
                        hlnkNavigationTwo.NavigateUrl = "MyInformation.aspx";
                        hlnkNavigationTwo.Text = "My Information";

                        hlnkNavigationTwo.CssClass = "link active";//显示成被选择样式
                        ddNavigationThree.Visible = false;//隐藏三级导航

                        //二级导航的下拉菜单
                        divNavigationTwo.Visible = false;
                        #endregion
                        break;
                    default://"Home"、"SystemLogin"
                        #region "首页"
                        //lblTitle.Text = "System Login";//当页显示的加粗标题
                        if (strValue == "Home")
                        {
                            h3Title.InnerHtml = "Home Page";
                        }
                        else
                        {
                            h3Title.InnerHtml = "Login";//当页显示的加粗标题
                        }

                        //Home的下拉菜单
                        if (objLoginUserInfo.funBln_Limited("1000", objLoginUserInfo.SystemLimited))
                        {
                            liHome1.InnerHtml = "<a href='AccountDefault.aspx'>Login User Management</a>";
                        }
                        else
                        {
                            liHome1.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("2000", objLoginUserInfo.SystemLimited))
                        {
                            liHome8.InnerHtml = "<a href='OptionsDefault.aspx'>Motor Options Management</a>";
                        }
                        else
                        {
                            liHome8.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("4000", objLoginUserInfo.SystemLimited))
                        {
                            liHome10.InnerHtml = "<a href='DriveOptionsDefault.aspx'>Drive Options Management</a>";
                        }
                        else
                        {
                            liHome10.Visible = false;
                        }
                        if (objLoginUserInfo.funBln_Limited("7000", objLoginUserInfo.SystemLimited))
                        {
                            liHome9.InnerHtml = "<a href='KPOperation.aspx'>Motor KP Discount</a>";
                        }
                        else
                        {
                            liHome9.Visible = false;
                        }
                        liHome3.InnerHtml = "<a href='ProductsQuotation.aspx?Type=Paste'>Motor Products Quotation(Copy/Paste)</a>";
                        liHome2.InnerHtml = "<a href='ProductsQuotationDrive.aspx?Type=Paste'>Drive Products Quotation(Copy/Paste)</a>";

                        //如果没有登录，那么还不显示My Information和Exit System链接
                        try
                        {
                            if (objLoginUserInfo.ID != "")
                            {
                                liHome5.Visible = true;
                                liHome6.Visible = true;
                            }
                            else
                            {
                                liHome5.Visible = false;
                                liHome6.Visible = false;
                            }
                        }
                        catch
                        {
                            liHome5.Visible = false;
                            liHome6.Visible = false;
                        }

                        //二级导航
                        ddNavigationTwo.Visible = false;
                        //三级导航
                        ddNavigationThree.Visible = false;
                        #endregion
                        break;
                }
            }
            get
            {
                //return lblTitle.Text;
                return h3Title.InnerHtml;
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
