using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IdioSoft.ClassCommon;
using System.IO;
using OfficeOpenXml;

namespace CHub
{
    public partial class OptionsImport : ClassLibrary.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((CHub.Main)Master).TitleName = "Options";
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            subDB();
        }

        private void subDB()
        {
            string strExcel = fuMain.funString_FileUpLoadAttachment("", 50, "Temp/Options/");
            if (strExcel == "")
            {
                lblError.Visible = true;
                lblError.Text = "Excel上传失败";
                return;
            }

            string CurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string CurrentUserID = objLoginUserInfo.ID;
            string PriceType = "Options";
            int IsDiscount = 1;
            string ImportType = "Web";
            string OperationVersion = CurrentUserID.Replace("-", "") + DateTime.Now.ToString("yyyyMMddHHmmss");

            int intRow = 2;
            string strSQL = "";
            string strError = "";
            string FilePath = Server.MapPath("Temp/Options/") + strExcel;
            FileInfo existingFile = new FileInfo(FilePath);
            using (ExcelPackage xlPackage = new ExcelPackage(existingFile))
            {
                // get the first worksheet in the workbook
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];

                while (worksheet.Cell(intRow, 1).Value != "")
                {
                    string MLFB = worksheet.Cell(intRow, 1).Value.funString_SQLToString();
                    string Options = worksheet.Cell(intRow, 2).Value.funString_SQLToString();
                    decimal Price = worksheet.Cell(intRow, 3).Value.funDec_StringToDecimal(0);
                    //                    strSQL = "SELECT ID FROM CHub_Info_LP WHERE PriceType='Options' AND MLFB='" + MLFB + "' AND OptionValue='" + Options + "'";
                    //                    string DBID = objClassDbAccess.funString_SQLExecuteScalar(strSQL).ToString();
                    //                    if (DBID.Length > 0)
                    //                    {
                    //                        strSQL = @"update CHub_Info_LP set CurrentPrice = " + Price + ", IsDiscount = " + IsDiscount + ", ImportType='" + ImportType + "', CreateDate='" + CurrentDate + "', CreateUserID='" + CurrentUserID + "' WHERE ID='" + DBID + "'";
                    //                    }
                    //                    else
                    //                    {
                    //                        strSQL = @"INSERT INTO CHub_Info_LP
                    //                      (MLFB, PriceType, OptionValue, CurrentPrice, IsDiscount, CreateDate, CreateUserID, ImportType) VALUES (";
                    //                        strSQL += "'" + MLFB + "','" + PriceType + "', '" + Options + "', " + Price + "";
                    //                        strSQL += ", " + IsDiscount + ", '" + CurrentDate + "', '" + CurrentUserID + "','" + ImportType + "')";
                    //                    }
                    //                    strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL).funString_JsToString();

                    #region "将数据导入临时表"
                    strSQL = "INSERT INTO CHub_Info_LPOptionsImport(MLFB, Options, CurrentPrice, OperationVersion, CreateDate, CreateUserID, ExcelRow) ";
                    strSQL += " VALUES ('" + MLFB + "', '" + Options + "', " + Price + ", '" + OperationVersion + "', '" + CurrentDate + "', '" + CurrentUserID + "', " + intRow + ")";
                    strError += objClassDbAccess.funString_SQLExecuteNonQuery(strSQL).funString_JsToString();
                    #endregion

                    intRow++;
                }
            }// the using statement automatically calls Dispose() which closes the package.

            if (strError != "")
            {
                lblError.Visible = true;
                lblError.Text = "Excel上传失败！";
                return;
            }
            else
            {
                #region "开始导入数据"
                //Update
                strSQL = @"UPDATE CHub_Info_LP set CurrentPrice = a.CurrentPrice, IsDiscount = " + IsDiscount + ", ImportType='" + ImportType + "', CreateDate='" + CurrentDate + "', CreateUserID='" + CurrentUserID + "'";
                strSQL += " FROM (SELECT MLFB, Options, CurrentPrice FROM CHub_Info_LPOptionsImport WHERE OperationVersion='" + OperationVersion + "') a WHERE CHub_Info_LP.MLFB=a.MLFB AND CHub_Info_LP.PriceType='Options' AND CHub_Info_LP.OptionValue=a.Options";
                strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL).funString_JsToString();
                //Delete
                strSQL = "DELETE FROM CHub_Info_LPOptionsImport WHERE (OperationVersion='" + OperationVersion + "') AND (MLFB+'-Z='+Options IN (SELECT MLFB+'-Z='+OptionValue FROM CHub_Info_LP WHERE PriceType='Options'))";
                strError += objClassDbAccess.funString_SQLExecuteNonQuery(strSQL).funString_JsToString();
                //Insert
                strSQL = "INSERT INTO CHub_Info_LP(MLFB, PriceType, OptionValue, CurrentPrice, IsDiscount, CreateDate, CreateUserID, ImportType) ";
                strSQL += "SELECT MLFB, 'Options' AS PriceType, Options, CurrentPrice, " + IsDiscount + ", '" + CurrentDate + "', '" + CurrentUserID + "', '" + ImportType + "' FROM CHub_Info_LPOptionsImport WHERE (OperationVersion='" + OperationVersion + "')";
                strError += objClassDbAccess.funString_SQLExecuteNonQuery(strSQL).funString_JsToString();
                //Delete
                strSQL = "DELETE FROM CHub_Info_LPOptionsImport WHERE (OperationVersion='" + OperationVersion + "')";
                strError += objClassDbAccess.funString_SQLExecuteNonQuery(strSQL).funString_JsToString();
                #endregion
                if (strError != "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Excel导入失败！";
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "JSOK", "alert('导入完成！');window.location='OptionsDefault.aspx';", true);
                }
            }
            if (intRow == 2)
            {
                lblError.Visible = true;
                lblError.Text = "Excel数据为空！";
                return;
            }
        }
    }
}
