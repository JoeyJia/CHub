using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IdioSoft.ClassCommon;
using System.IO;
using OfficeOpenXml;
using System.Text.RegularExpressions;

namespace CHub
{
    public partial class SPRImport : ClassLibrary.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((CHub.Main)Master).TitleName = "SPR";
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            subDB();
        }

        private void subDB()
        {
            string strExcel = fuMain.funString_FileUpLoadAttachment("", 50, "Temp/SPR/");
            if (strExcel == "")
            {
                lblError.Visible = true;
                lblError.Text = "Excel上传失败";
                return;
            }

            string strRowError = "";
            int intRow = 2;
            string strSQL = "";
            string strError = "";
            string FilePath = Server.MapPath("Temp/SPR/") + strExcel;
            FileInfo existingFile = new FileInfo(FilePath);
            using (ExcelPackage xlPackage = new ExcelPackage(existingFile))
            {
                // get the first worksheet in the workbook
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];

                //strSQL = "truncate table CHub_Info_SPR";
                //strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL).funString_JsToString();

                while (worksheet.Cell(intRow, 1).Value.Trim() != "")
                {
                    string SPRID = Guid.NewGuid().ToString();
                    string SPRNo = worksheet.Cell(intRow, 1).Value.Trim().funString_SQLToString();
                    string MLFB = worksheet.Cell(intRow, 2).Value.Trim().funString_SQLToString();
                    string Options = worksheet.Cell(intRow, 3).Value.Trim().funString_SQLToString();
                    decimal Voltage = worksheet.Cell(intRow, 4).Value.Trim().funDec_StringToDecimal(0);
                    int Quantity = worksheet.Cell(intRow, 5).Value.Trim().funInt_StringToInt(1);
                    decimal OthersPerUnit = worksheet.Cell(intRow, 5).Value.Trim().funDec_StringToDecimal(0);
                    decimal OthersPerItem = worksheet.Cell(intRow, 6).Value.Trim().funDec_StringToDecimal(0);

                    string CurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string CurrentUserID = objLoginUserInfo.ID;

                    //以下判断不用加了，因为SPR万一需要导入两个一样的用来表明这个下单了两个电机呢？
                    //strSQL = "SELECT COUNT(*) AS RecordCount FROM CHub_Info_SPR WHERE IsDel=0 and SPRNo='" + SPRNo + "' AND MLFB='" + MLFB + "' AND Options='" + Options + "'";
                    //int intCount = objClassDbAccess.funString_SQLExecuteScalar(strSQL).funInt_StringToInt(0);
                    //if (intCount == 0)
                    //{
                    #region "选件价格"
                    //先删除这个SPR的全部选件
                    strSQL = "Update CHub_Info_SPROptions SET IsDel=1, DeleteDate='" + CurrentDate + "', DeleteUserID='" + CurrentUserID + "' WHERE SPRID='" + SPRID + "'";
                    strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL);
                    //新增SPR的选件及价格，并将存到主表中的SPR选件格式化
                    string[] aryOptions = Regex.Split(Options, "\\+"); //Regex.Split(Options, "\r\n");
                    Options = "";
                    for (int i = 0; i < aryOptions.Length; i++)
                    {
                        if (aryOptions[i].Trim().Length > 0)
                        {
                            string[] aryOptionItem = aryOptions[i].Split(',');
                            string OptionItem = "";
                            string CurrentPrice = "0";
                            if (aryOptionItem.Length > 0)
                            {
                                OptionItem = aryOptionItem[0].Trim();
                                Options = Options + "+" + OptionItem;
                            }
                            if (aryOptionItem.Length > 1)
                            {
                                CurrentPrice = aryOptionItem[1].Trim().funDec_StringToDecimal(0).ToString();
                            }

                            strSQL = "INSERT INTO CHub_Info_SPROptions(SPRID, OptionValue, CurrentPrice, CreateDate, CreateUserID) Values(";
                            strSQL += "'" + SPRID + "', '" + OptionItem + "', " + CurrentPrice + ", '" + CurrentDate + "', '" + CurrentUserID + "')";
                            strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL);
                        }
                    }
                    if (Options.Length > 0)
                    {
                        Options = Options.Substring(1);
                    }
                    ///最好能查找一下选件有没有重复并且价格不同的，防止读取价格的时候出现一个选件两个价格
                    #endregion

                    strSQL = @"INSERT INTO CHub_Info_SPR
                      (ID, SPRNo, MLFB, Options, Voltage, Quantity, OthersPerUnit, OthersPerItem, IsDel, CreateDate, CreateUserID) VALUES (";
                    strSQL += "'" + SPRID + "', '" + SPRNo + "', '" + MLFB + "', '" + Options + "', " + Voltage + ", " + Quantity + ", " + OthersPerUnit + ", " + OthersPerItem + "";
                    strSQL += ", 0, '" + CurrentDate + "', '" + CurrentUserID + "')";
                    strError = objClassDbAccess.funString_SQLExecuteNonQuery(strSQL).funString_JsToString();
                    //}
                    //else
                    //{
                    //    strRowError += "第" + intRow.ToString() + "行SPR号已经存在，不能导入<br>";
                    //}
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
                if (strRowError.Length > 0)
                {
                    lblError.Visible = true;
                    lblError.Text = strRowError + "<br><br>其余数据正确导入！";
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "JSOK", "alert('导入完成！');window.location='OrderDefault.aspx';", true);
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
