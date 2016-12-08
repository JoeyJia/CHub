/*----------------------------------------------------------------------------------------
// 版本号：1.0
// Version:1.0
//
// 文件功能摘要：站点登录页
// File Function Summary:Site Login Page
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
using System.Data;
using System.Web.UI;
using IdioSoft.ClassCommon;
using System.DirectoryServices;
using System.Management;

namespace CHub
{
    public partial class _Default : ClassLibrary.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["UserInfo"] = null;
            }
            if (funBoolean_IsLogin())
            {
                Response.Redirect("HomePage.aspx");
            }
        }

        /// <summary>
        /// 校验登录名、密码
        /// </summary>
        /// <returns></returns>
        private string funCheckValue_Submit()
        {
            string strError = "";

            if (txtLoginName.Value.Trim() == "")
            {
                //return "登录名称不能为空！";
                return "Login Name can not be empty!";
            }
            if (txtPassword.Value.Trim() == "")
            {
                //return "密码不能为空！";
                return "Password can not be empty!";
            }

            return strError;
        }

        #region "判断是否可以登录"
        /// <summary>
        /// 点击提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strError = "";
           // strError =  funCheckValue_Submit();
            if (strError == "")
            {
                if (funBoolean_IsLogin())
                {
                    Response.Redirect("HomePage.aspx");
                }
                else
                {
                    trError.Visible = true;
                    //lblError.Text = "'登录名称'或'密码'有误，请重新输入！";
                    lblError.Text = "Wrong login name or password, please input again!";
                }
            }
            else
            {
                trError.Visible = true;
                lblError.Text = strError;
            }
        }
        /// <summary>
        /// 判断是否正确登录，并且将登录信息记录到Session中
        /// </summary>
        /// <returns></returns>
        /// 

        private static string AllProperties = "name,givenName,samaccountname,mail";
        private bool funBoolean_IsLogin()
        {
            bool blnIsLogin = false;

            #region "存储到Session中"
            string strSQL = "";
            string strPersonNo = txtLoginName.Value.funString_SQLToString();
            string strPassword = txtPassword.Value.funString_SQLToString();
            strSQL = @"SELECT ID, LoginName, Password, Email, UserNameEN, UserNameCN, Gender, RegionName, DutyID, SystemLimited
                       FROM CHub_Management_LoginUser where LoginName='" + strPersonNo + "' and Password='" + strPassword + "' and isdel=0";
            DataSet ds = new DataSet();
            //ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);

            string ldapPath = "LDAP://" + GetDomainName();
            string domainAndUsername = Environment.UserDomainName + "\\" + strPersonNo;

            try
            {
                

                string[] properties = AllProperties.Split(new char[] { '\r', '\n', ',' },
                               StringSplitOptions.RemoveEmptyEntries);

                try
                {
           
                    //DirectoryEntry entry = new DirectoryEntry(ldapPath, domainAndUsername, strPassword);
                    //DirectorySearcher search = new DirectorySearcher(entry);
                    //search.Filter = "(samaccountname=" + strPersonNo + ")";

                    //foreach (string p in properties)
                    //    search.PropertiesToLoad.Add(p);

                    //SearchResult result = search.FindOne();

                    if (true)//result != null)
                    {
                        objLoginUserInfo.LoginName = "joey";// result.Properties["name"][0].ToString();
                        objLoginUserInfo.Email= "joey"; //result.Properties["mail"][0].ToString();
                        objLoginUserInfo.ID = "joey";//strPersonNo.ToString();
                       
                        objLoginUserInfo.Password = strPassword.ToString();
                      
                        objLoginUserInfo.UserNameEn = "UserNameEN";
                        objLoginUserInfo.UserNameCn = "UserNameCN";
                        objLoginUserInfo.Gender = "Gender";
                        objLoginUserInfo.RegionName ="RegionName";
                        objLoginUserInfo.DutyID = 1;
                        objLoginUserInfo.SystemLimited ="1000,1001,1002,2000,2001,3000,3001,3002,3004,4000,5000,6000,7000";
                        objLoginUserInfo.PageSize = 10;
                        //foreach (string p in properties)
                        //{
                        //    //ResultPropertyValueCollection collection = result.Properties[p];
                           
                        //}
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                blnIsLogin = true;
                #region "载入用户信息"
               
                #endregion
                Session["UserInfo"] = objLoginUserInfo;
                objOperationLog.DoLog(strSQL, "User Login", objLoginUserInfo.ID);


            }
            catch  {

            }
          
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    blnIsLogin = true;
            //    #region "载入用户信息"
            //    objLoginUserInfo.ID = ds.Tables[0].Rows[0]["ID"].ToString();
            //    objLoginUserInfo.LoginName = ds.Tables[0].Rows[0]["LoginName"].ToString();
            //    objLoginUserInfo.Password = ds.Tables[0].Rows[0]["Password"].ToString();
            //    objLoginUserInfo.Email = ds.Tables[0].Rows[0]["Email"].ToString();
            //    objLoginUserInfo.UserNameEn = ds.Tables[0].Rows[0]["UserNameEN"].ToString();
            //    objLoginUserInfo.UserNameCn = ds.Tables[0].Rows[0]["UserNameCN"].ToString();
            //    objLoginUserInfo.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
            //    objLoginUserInfo.RegionName = ds.Tables[0].Rows[0]["RegionName"].ToString();
            //    objLoginUserInfo.DutyID = ds.Tables[0].Rows[0]["DutyID"].ToString().funInt_StringToInt(0);
            //    objLoginUserInfo.SystemLimited = ds.Tables[0].Rows[0]["SystemLimited"].ToString();
            //    objLoginUserInfo.PageSize = 10;
            //    #endregion
            //    Session["UserInfo"] = objLoginUserInfo;
            //    objOperationLog.DoLog(strSQL, "User Login", objLoginUserInfo.ID);
            //}
            #endregion
            return blnIsLogin;
        }
        #endregion

        #region "找回密码"
        /// <summary>
        /// 当忘记密码链接被点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkForgetPwd_Click(object sender, EventArgs e)
        {
            trFindPwd.Visible = true;
            trError.Visible = false;
        }
        /// <summary>
        /// 填写Email后点击按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFindPwd_OnServerClick(object sender, EventArgs e)
        {
            string strSQL = "";
            DataSet dsLogin = funDataset_LoginInfo(txtEMail.Value.funString_SQLToString());
            if (dsLogin != null && dsLogin.Tables[0].Rows.Count > 0)
            {
                DataSet ds = new DataSet();
                strSQL = "SELECT ID, Email, UserName, Password, SmtpServer, Type FROM CHub_System_SendEmail where Type='FindPassword'";
                ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
                string strTemplateName = Server.MapPath("Document/Email/FindPassword.htm");
                string strBody = strTemplateName.funString_GetHtmlFile();
                strBody = strBody.Replace("!loginname!", dsLogin.Tables[0].Rows[0]["LoginName"].ToString());
                strBody = strBody.Replace("!password!", dsLogin.Tables[0].Rows[0]["Password"].ToString());
                strBody = strBody.Replace("!date!", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                string strError = ds.Tables[0].Rows[0]["Email"].ToString().funString_SendNoMailByWebMail(ds.Tables[0].Rows[0]["UserName"].ToString(), ds.Tables[0].Rows[0]["Password"].ToString(), strBody, ds.Tables[0].Rows[0]["SmtpServer"].ToString(), txtEMail.Value, "Find CHub Password", "", false, "", null);
                if (strError == "")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "btnFindPassword", "alert('Email发送成功！');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "btnFindPassword", "alert('Email sent successfully!');", true);
                    trFindPwd.Visible = false;
                }
                else
                {
                    trError.Visible = true;
                    //lblError.Text = "发送邮件失败，请联系系统管理人员！";
                    lblError.Text = "Failed to send email,please contact the system administrators!";
                }
            }
            else
            {
                trError.Visible = true;
                //lblError.Text = "系统中没有找到您填写的Email，请重新填写或联系系统管理人员！";
                lblError.Text = "The system can not find your email,please re-enter or contact the system administrators!";
            }
        }


        /// <summary>
        /// 根据填写的Email查找登录者信息
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns></returns>
        private DataSet funDataset_LoginInfo(string strEmail)
        {
            string strSQL = "SELECT LoginName, Password, UserNameEn, Email FROM CHub_Management_LoginUser where Email='" + strEmail + "' and Email!=''";
            DataSet ds = new DataSet();
            ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
            return ds;
        }
        #endregion

        private static string GetDomainName()
        {
            // 注意：这段代码需要在Windows XP及较新版本的操作系统中才能正常运行。
            SelectQuery query = new SelectQuery("Win32_ComputerSystem");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject mo in searcher.Get())
                {
                    if ((bool)mo["partofdomain"])
                        return mo["domain"].ToString();
                }
            }
            return null;
        }
        

        public static void ShowUserInfo(string loginName, string domainName)
        {
            if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(domainName))
                return;

            string[] properties = AllProperties.Split(new char[] { '\r', '\n', ',' },
                                StringSplitOptions.RemoveEmptyEntries);

            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + domainName);
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(samaccountname=" + loginName + ")";

                foreach (string p in properties)
                    search.PropertiesToLoad.Add(p);

                SearchResult result = search.FindOne();

                if (result != null)
                {
                    foreach (string p in properties)
                    {
                        ResultPropertyValueCollection collection = result.Properties[p];
                        for (int i = 0; i < collection.Count; i++)
                            Console.WriteLine(p + ": " + collection[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
