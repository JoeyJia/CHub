using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IdioSoft.ClassCommon;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Reflection;

namespace CHub
{
    public partial class QuickOrderEntry : ClassLibrary.Page
    {
        //private string _Targ_file = "ztest.xml";

        protected void Page_Load(object sender, EventArgs e)
        {
            #region "得到传递过来的参数"
            string strType = "";
            try
            {
                strType = Request.QueryString["Type"];
                if (strType.ToLower() != "select")
                {
                    strType = "Paste";
                }
            }
            catch
            {
                strType = "Paste";
            }
            #endregion

            #region "判断页面显示"
            if (!IsPostBack)
            {
                //选择显示的样式 
                ViewState["SelectType"] = strType;
                //清空显示的列表
                Session["ProductList"] = null;


                if (strType.ToLower() == "select")
                {
                    ((CHub.Main)Master).TitleName = "DriveProductsQuotation(Select)";
                }
                else
                {
                    ((CHub.Main)Master).TitleName = "DriveProductsQuotation(Paste)";
                }

                if (ViewState["SelectType"].ToString().ToLower() == "select")
                {
                    //默认选件，不能使用Visible = false，否则Excel控件会报错
                    //tablePaste.Visible = false;
                    //tableList.Visible = true;
                    tablePaste.Attributes.Add("style", "display:none;");
                }
                else
                {
                    //默认粘贴
                    //tablePaste.Visible = true;
                    //tableList.Visible = false;
                    tableList.Attributes.Add("style", "display:none;");
                    btnModify.Visible = false;
                }
            }

            subDisplay_ListTable();
            #endregion

            #region "创建文件夹"
            //string strDirPath = Server.MapPath("Temp/ExcelExport/" + objLoginUserInfo.ID.Replace("-", "") + "/");
            //if (!Directory.Exists(strDirPath))
            //{
            //    Directory.CreateDirectory(strDirPath);
            //}
            #endregion

            #region "清除登录者文件夹中的文件"
            //try
            //{
            //    strDirPath = Server.MapPath("Temp/ExcelExport/" + objLoginUserInfo.ID.Replace("-", "") + "/");
            //    foreach (string fileName in Directory.GetFiles(strDirPath))
            //    {
            //        FileInfo f = new FileInfo(fileName);
            //        if (f.Extension.ToLower() != ".xml")
            //        {
            //            try
            //            {
            //                f.Delete();
            //            }
            //            catch (Exception)
            //            {
            //                //lblLog.Text = "文件不能正常删除！";
            //            }

            //        }
            //    }
            //}
            //catch
            //{
            //}
            #endregion
        }

        /// <summary>
        /// 粘贴提交按钮被点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPSubmit_Click(object sender, EventArgs e)
        {
            List<IdioSoft.Public.cMaterial> objSubmitData = objLoginUserInfo.PasteData;

            try
            {
                List<ClassLibrary.DriveProductRowData> lstclsRowData = new List<ClassLibrary.DriveProductRowData>();
                DataSet ds = new DataSet();

                #region "开始循环判断"
                #region "先得到数组"
                List<classDriveQItems> lstTable = new List<classDriveQItems>();
                for (int i = 0; i < objSubmitData.Count; i++)
                {
                    classDriveQItems item = new classDriveQItems();
                    item.MLFB = objSubmitData[i].MLFB;
                    item.Qty = objSubmitData[i].Qty.funInt_StringToInt(1) == 0 ? 1 : objSubmitData[i].Qty.funInt_StringToInt(1);
                    string strDiscount = objSubmitData[i].DiscountFactor;
                    int intCharIndex = strDiscount.IndexOf("%");
                    if (intCharIndex >= 0)
                    {
                        try
                        {
                            strDiscount = strDiscount.Substring(0, intCharIndex);
                            strDiscount = (decimal.Parse(strDiscount) / decimal.Parse("100")).ToString();
                        }
                        catch
                        {
                            strDiscount = "1";
                        }
                    }
                    decimal decDiscount = strDiscount.funString_ValidReplaceString("1234567890.").funDec_StringToDecimal(1);
                    item.Discount = decDiscount;

                    lstTable.Add(item);
                }
                #endregion

                //因为有列头，所以从2行开始取
                for (int i = 0; i < lstTable.Count; i++)
                {
                    //如果有文字，那么证明是有效的文本
                    string strProduct = lstTable[i].MLFB;
                    if (strProduct != "")
                    {
                        ClassLibrary.DriveProductRowData itemclsRowData = new ClassLibrary.DriveProductRowData();

                        #region "处理数据"
                        #region "将每行的数据拆分出来放到数组中 第一列是产品，第二列是数量，第三列是折扣"
                        if (lstTable[i].Qty == 0)
                        {
                            lstTable[i].Qty = 1;
                        }
                        int intQTY = lstTable[i].Qty; //aryRowText[1].funInt_StringToInt(1);
                        itemclsRowData.QTY = intQTY;

                        itemclsRowData.LPFactor = lstTable[i].Discount; //折扣率
                        #endregion

                        itemclsRowData.lstProduct = new List<ClassLibrary.DriveProductRowItem>();
                        itemclsRowData.OptionsText = "";
                        #region "然后将第一列再分拆"
                        #region "将MLFB拆分出来"
                        //数据举例
                        //aryProduct[0] 1LG0253-4AA70-Z
                        //aryProduct[1] K11+K45	
                        string[] aryProduct = Regex.Split(strProduct, "-Z=", RegexOptions.IgnoreCase);

                        //将MLFB号放置到Item类中
                        //clsProductItem itemclsProductItem = new clsProductItem();
                        ClassLibrary.DriveProductRowItem itemclsProductItem = new ClassLibrary.DriveProductRowItem();
                        itemclsProductItem.ProductText = aryProduct[0].Trim();
                        itemclsProductItem.ProductCount = intQTY;
                        //将MLFB号格式化
                        ////itemclsProductItem.ProductText = itemclsProductItem.ProductText.Replace("-Z", "");//将最后一位Z去掉，不知道是否正确
                        //itemclsProductItem.ProductText = itemclsProductItem.ProductText.Replace("-", "");//将MLFB中的-号去掉，得到正常的MLFB

                        //将Item类添加到行类中
                        itemclsRowData.lstProduct.Add(itemclsProductItem);
                        #endregion

                        #region "将产品数组的第二列分拆"
                        ////aryOptions[0] K11
                        ////aryOptions[1] K45	
                        //string[] aryOptions;
                        ////int intProductCount = 1;
                        if (aryProduct.Length > 1)
                        {
                            //此文本时留在有效性判断时使用
                            itemclsRowData.OptionsText = aryProduct[1].Replace("+", ",");//将选件的连接符转换成,号//

                            string[] aryOptions = aryProduct[1].Split('+');// Regex.Split(aryProduct[1], "+", RegexOptions.IgnoreCase);
                            //intProductCount = intProductCount + aryOptions.Length;
                            for (int j = 0; j < aryOptions.Length; j++)
                            {
                                bool blnExists = false;

                                //循环已有的数组，如果已经存在这个选件，那么将选件加上数量即可
                                for (int x = 0; x < itemclsRowData.lstProduct.Count; x++)
                                {
                                    if (itemclsRowData.lstProduct[x].ProductText.ToLower() == aryOptions[j].ToLower().Trim())
                                    {
                                        itemclsRowData.lstProduct[x].ProductCount += intQTY;
                                        blnExists = true;
                                        break;
                                    }
                                }
                                //是新选件，那么新增到数组中
                                if (!blnExists)
                                {
                                    //将选件放置到Item类中
                                    //itemclsProductItem = new clsProductItem();
                                    itemclsProductItem = new ClassLibrary.DriveProductRowItem();
                                    itemclsProductItem.ProductText = aryOptions[j].Trim();
                                    itemclsProductItem.ProductCount = intQTY;
                                    //将Item类添加到行类中
                                    itemclsRowData.lstProduct.Add(itemclsProductItem);
                                }
                            }
                        }
                        #endregion
                        #endregion
                        #endregion

                        #region "开始在数据库判断产品是否有效"
                        string strSQL = "";
                        string strCheckMLFB = "";
                        //得到用来做判断的MLFB号（就是数据库中存储的MLFB号，这要10位，是因为即使是存储的12位的，前10位也应该是唯一的，后面的2位只是用来进一步唯一选定电压和安装方式的）
                        //用10位，是因为如果不从选项中重新判断一回，就没有办法知道数据库中的MLFB号是几位的。
                        //string tmpProductText = itemclsRowData.lstProduct[0].ProductText;
                        strCheckMLFB = itemclsRowData.lstProduct[0].ProductText;
                        //if (strCheckMLFB.Length > 9)
                        //{
                        //    strCheckMLFB = strCheckMLFB.Substring(0, 10);
                        //}
                        #region "判断选件是否都是数据库中允许的"
                        //string tmpOptionValue = "";
                        string strOptionsText = itemclsRowData.OptionsText.Replace(" ", "").Replace(",", "','");//加上空格的去掉是为了防止加上,号时加空格也包括进去，这样IN的时候怕找不到这个选件
                        #region "判断12位的MLFB号，并赋值"
                        itemclsRowData.IsValid = true;

                        #region "开始在数据库判断产品是否有效"
                        strSQL = "SELECT CurrentPrice FROM Drive_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB = '" + strCheckMLFB + "')";
                        string strCountProduct = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                        decimal CurrentPrice = 0;
                        if (strCountProduct != "")
                        {
                            #region "MLFB的表价和成本价"
                            //得到表价
                            CurrentPrice = strCountProduct.funDec_StringToDecimal(0);
                            itemclsRowData.lstProduct[0].ItemLP = CurrentPrice;
                            itemclsRowData.TotalLP = CurrentPrice * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的表价
                            //strSQL = "SELECT IsDiscount FROM Drive_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB = '" + strCheckMLFB + "')";
                            //itemclsRowData.lstProduct[0].IsDiscountLP = objClassDbAccess.funString_SQLExecuteScalar(strSQL).funBoolean_StringToBoolean();
                            ////如果折扣，那么就是折扣率，否则就是1
                            //if (itemclsRowData.lstProduct[0].IsDiscountLP)
                            //{
                            //itemclsRowData.lstProduct[0].ItemDiscount = itemclsRowData.DiscountFactor;
                            //}
                            //else
                            //{
                            //    itemclsRowData.lstProduct[0].ItemDiscount = 1;
                            //}
                            itemclsRowData.TotalDisLP = (CurrentPrice * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)

                            //得到成本价
                            CurrentPrice = objClassDbAccess.funString_SQLExecuteScalar("SELECT Factor FROM Drive_Info_TPFactor where MLFB='" + strCheckMLFB + "'").funDec_StringToDecimal(1);
                            //itemclsRowData.lstProduct[0].TPFactor = CurrentPrice;
                            itemclsRowData.TPFactor = CurrentPrice;
                            CurrentPrice = itemclsRowData.lstProduct[0].ItemLP * CurrentPrice;//TP=LP*Factor
                            itemclsRowData.lstProduct[0].ItemTP = CurrentPrice;
                            itemclsRowData.TotalTP = CurrentPrice * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的成本价

                            ////得到成本价
                            //strCountProduct = objClassDbAccess.funString_SQLExecuteScalar("SELECT CurrentPrice FROM Drive_Info_TP WHERE (PriceType = 'MLFB') AND (MLFB = '" + strCheckMLFB + "')");
                            //CurrentPrice = strCountProduct.funDec_StringToDecimal(0);
                            //itemclsRowData.lstProduct[0].ItemTP = CurrentPrice;
                            //itemclsRowData.TotalTP = CurrentPrice * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的成本价
                            //strSQL = "SELECT IsDiscount FROM Drive_Info_TP WHERE (PriceType = 'MLFB') AND (MLFB = '" + strCheckMLFB + "')";
                            //itemclsRowData.lstProduct[0].IsDiscountTP = objClassDbAccess.funString_SQLExecuteScalar(strSQL).funBoolean_StringToBoolean();
                            //strSQL = "SELECT Factor FROM Drive_Info_TPFactor where MLFB='" + strCheckMLFB + "'";
                            //itemclsRowData.Factor = objClassDbAccess.funString_SQLExecuteScalar(strSQL).funDec_StringToDecimal(1);
                            ////如果折扣，那么就是折扣率，否则就是1
                            //if (itemclsRowData.lstProduct[0].IsDiscountTP)
                            //{
                            //    itemclsRowData.TotalDisTP = (CurrentPrice * itemclsRowData.DiscountFactor) * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                            //}
                            //else
                            //{
                            //    //不用打折
                            //    itemclsRowData.TotalDisTP = CurrentPrice * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                            //}
                            #endregion

                            #region "继续计算配件的价格"
                            if (itemclsRowData.lstProduct.Count > 1)
                            {
                                string strTempOptionValue = "";
                                #region "得到选件价格，并判断选件是否合格"
                                int intOptionCount = 0;

                                #region "得到其他非必选件的价格"
                                #region "得到其他非必选件的表价"
                                strSQL = "SELECT OptionValue, CurrentPrice, IsDiscount FROM Drive_Info_LP WHERE (PriceType = 'Options') AND (MLFB = '" + strCheckMLFB + "') AND (OptionValue IN ('" + strOptionsText + "'))";
                                ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
                                if (ds.Tables[0].Rows.Count + intOptionCount != itemclsRowData.lstProduct.Count - 1)//需要减去MLFB的数量
                                {
                                    //如果配件数量与数据库中的记录条数不符，那么数据就是有误的（这个地方，如果这个产品的这个配件在数据库中找到两条的话，数量也是不相符合的）
                                    itemclsRowData.IsValid = false;
                                    string strVO = strOptionsText.Replace("'", "");
                                    string[] aryVO = strVO.Split(',');
                                    for (int j = 0; j < aryVO.Length; j++)
                                    {
                                        bool blnOFind = false;
                                        for (int z = 0; z < ds.Tables[0].Rows.Count; z++)
                                        {
                                            if (aryVO[j].ToLower() == ds.Tables[0].Rows[z]["OptionValue"].ToString().ToLower())
                                            {
                                                blnOFind = true;
                                                continue;
                                            }
                                        }
                                        if (!blnOFind)
                                        {
                                            itemclsRowData.lstVItem.Add(aryVO[j]);
                                        }
                                    }

                                }
                                //else
                                //{
                                //    //配件数量和数据库记录条数相符，数据有效
                                //    itemclsRowData.IsValid = true;
                                //}

                                #region "循环查找配件，并将表价赋值"
                                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                                {
                                    strTempOptionValue = ds.Tables[0].Rows[j]["OptionValue"].ToString().ToLower().Trim();
                                    for (int x = 1; x < itemclsRowData.lstProduct.Count; x++)
                                    {
                                        if (itemclsRowData.lstProduct[x].ProductText.ToString().ToLower().Trim() == strTempOptionValue)
                                        {
                                            #region "得到选件的表价"
                                            itemclsRowData.lstProduct[x].ItemLP = ds.Tables[0].Rows[j]["CurrentPrice"].ToString().funDec_StringToDecimal(0);//得到各个配件在数据库中的价格
                                            itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[x].ItemLP * decimal.Parse(itemclsRowData.lstProduct[x].ProductCount.ToString());//得到总价格
                                            //itemclsRowData.lstProduct[x].IsDiscountLP = ds.Tables[0].Rows[j]["IsDiscount"].ToString().funBoolean_StringToBoolean();
                                            ////如果折扣，那么就是折扣率，否则就是1
                                            //if (itemclsRowData.lstProduct[x].IsDiscountLP)
                                            //{
                                            //    itemclsRowData.lstProduct[x].ItemDiscount = itemclsRowData.DiscountFactor;
                                            //}
                                            //else
                                            //{
                                            //    itemclsRowData.lstProduct[x].ItemDiscount = 1;
                                            //}
                                            itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[x].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[x].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                            #endregion
                                            #region "得到选件的成本价"
                                            itemclsRowData.lstProduct[x].ItemTP = itemclsRowData.lstProduct[x].ItemLP * itemclsRowData.TPFactor;//得到各个配件在数据库中的价格
                                            itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[x].ItemTP * decimal.Parse(itemclsRowData.lstProduct[x].ProductCount.ToString());//得到总价格
                                            #endregion
                                        }
                                    }
                                }
                                #endregion
                                #endregion

                                #region "得到成本价"
                                //strSQL = "SELECT OptionValue, CurrentPrice, IsDiscount FROM Drive_Info_TP WHERE (PriceType = 'Options') AND (MLFB = '" + strCheckMLFB + "') AND (OptionValue IN ('" + strOptionsText + "'))";
                                //ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
                                #region "循环查找配件，并将价格赋值"
                                //for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                                //{
                                //    strTempOptionValue = ds.Tables[0].Rows[j]["OptionValue"].ToString().ToLower().Trim();
                                //    for (int x = 1; x < itemclsRowData.lstProduct.Count; x++)
                                //    {
                                //        if (itemclsRowData.lstProduct[x].ProductText.ToString().ToLower().Trim() == strTempOptionValue)
                                //        {
                                //            itemclsRowData.lstProduct[x].ItemTP = ds.Tables[0].Rows[j]["CurrentPrice"].ToString().funDec_StringToDecimal(0);//得到各个配件在数据库中的价格
                                //            itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[x].ItemTP * decimal.Parse(itemclsRowData.lstProduct[x].ProductCount.ToString());//得到总价格
                                //            itemclsRowData.lstProduct[x].IsDiscountLP = ds.Tables[0].Rows[j]["IsDiscount"].ToString().funBoolean_StringToBoolean();
                                //            //如果折扣，那么就是折扣率，否则就是1
                                //            if (itemclsRowData.lstProduct[x].IsDiscountLP)
                                //            {
                                //                itemclsRowData.TotalDisTP = (itemclsRowData.TotalDisTP + (itemclsRowData.lstProduct[x].ItemTP) * decimal.Parse(itemclsRowData.lstProduct[x].ProductCount.ToString()));//全部MLFB和选件的成本价(折扣后)
                                //            }
                                //            else
                                //            {
                                //                itemclsRowData.TotalDisTP = (itemclsRowData.TotalDisTP + itemclsRowData.lstProduct[x].ItemTP * decimal.Parse(itemclsRowData.lstProduct[x].ProductCount.ToString()));//全部MLFB和选件的成本价(折扣后)
                                //            }
                                //        }
                                //    }
                                //}
                                //itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP;
                                #endregion
                                #endregion
                                #endregion
                                #endregion
                            }
                            else
                            {
                                itemclsRowData.IsValid = true;//只有电机一个，所以肯定是正确的了
                            }
                            #endregion
                        }
                        else
                        {
                            //没找到产品信息，将此数据设置为无效，继续循环
                            itemclsRowData.IsValid = false;
                        }
                        #endregion
                        #endregion
                        #endregion

                        lstclsRowData.Add(itemclsRowData);//将行的数据保存到list中
                        #endregion
                    }
                }
                #endregion

                //将列表显示出来，用来给用户最后确定
                tablePaste.Attributes.Add("style", "display:none;");
                tableList.Attributes.Add("style", "display:block;");

                //将值默认保存到Session中
                Session["DriveProductList"] = lstclsRowData;
                subDisplay_ListTable();

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Source + "<br>" + ex.Message;
            }


        }

        /// <summary>
        /// 显示选择的结果列表
        /// </summary>
        private void subDisplay_ListTable()
        {
            //为了保存值，将类保存到了Session中
            List<ClassLibrary.DriveProductRowData> lstclsRowData = new List<ClassLibrary.DriveProductRowData>();
            if (Session["DriveProductList"] == null)
            {
                Session["DriveProductList"] = new List<ClassLibrary.DriveProductRowData>();
            }
            lstclsRowData = (List<ClassLibrary.DriveProductRowData>)Session["DriveProductList"];

            //清空div
            divList.Controls.Clear();

            //MLFB使用
            HtmlTable objTable = new HtmlTable();
            HtmlTableRow objRow = new HtmlTableRow();
            HtmlTableCell objCell = new HtmlTableCell();
            ////Option使用
            //HtmlTable objOptionsTable = new HtmlTable();
            //HtmlTableRow objOptionsRow = new HtmlTableRow();
            //HtmlTableCell objOptionsCell = new HtmlTableCell();

            Button objBtn = new Button();

            objTable = new HtmlTable();
            objTable.CellPadding = 0;
            objTable.CellSpacing = 0;
            objTable.Border = 0;
            objTable.Width = "100%";

            #region "列头行"
            objRow = new HtmlTableRow();
            //用来重新选件的
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "&nbsp;";
            objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
            objCell.Width = "80";
            objRow.Cells.Add(objCell);

            ////用来缩放内容的
            //objCell = new HtmlTableCell();
            //objCell.InnerHtml = "&nbsp;";
            //objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
            //objCell.Width = "20";
            //objRow.Cells.Add(objCell);

            //显示MLFB
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "MLFB-Z=<br>Options";//"产品名称";
            objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
            objCell.Attributes.Add("nowrap", "nowrap");
            objCell.Align = "left";
            objCell.Width = "50%";
            objRow.Cells.Add(objCell);

            //显示数量
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "Quantity";// "台数";
            objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
            objCell.Attributes.Add("nowrap", "nowrap");
            objCell.Width = "15%";
            objRow.Cells.Add(objCell);

            //折扣率
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "Discount <br>Factor";//"折扣率";
            objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
            objCell.Attributes.Add("nowrap", "nowrap");
            objCell.Width = "15%";
            objRow.Cells.Add(objCell);

            //价格
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "LP <br>(w/o VAT)";//"客户价（不含税）";
            objCell.Align = "Center";
            objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
            objCell.Attributes.Add("nowrap", "nowrap");
            objRow.Cells.Add(objCell);

            ////价格
            //objCell = new HtmlTableCell();
            //objCell.InnerHtml = "K-Price <br>(w/o VAT)";//"客户价（不含税）";
            //objCell.Align = "Center";
            //objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
            //objCell.Attributes.Add("nowrap", "nowrap");
            //objRow.Cells.Add(objCell);

            //成本价
            if (objLoginUserInfo.funBln_Limited("3004", objLoginUserInfo.SystemLimited))
            {
                objCell = new HtmlTableCell();
                objCell.InnerHtml = "TP <br>(w/o VAT)";//成本价（不含税）
                objCell.Align = "Center";
                objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
                objCell.Attributes.Add("nowrap", "nowrap");
                objRow.Cells.Add(objCell);
            }

            objTable.Rows.Add(objRow);
            #endregion

            //decimal TotalPrice = 0;
            decimal TotalLP = 0;
            //decimal TotalKP = 0;
            decimal TotalTP = 0;
            for (int i = 0; i < lstclsRowData.Count; i++)
            {
                #region "循环画产品信息"
                #region "MLFB行"
                objRow = new HtmlTableRow();

                ////用来重新选件的
                //objBtn = new Button();
                //objBtn.Text = "Modify";
                //objBtn.ID = "btn" + i.ToString();
                //objBtn.CssClass = "idio_FlatBtnOnMouseOut";
                //objBtn.Click += new EventHandler(objBtn_Click);

                objCell = new HtmlTableCell();
                objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #EEEEEE;");
                objCell.Attributes.Add("nowrap", "nowrap");
                //objCell.Controls.Add(objBtn);

                objBtn = new Button();
                objBtn.Text = "Delete";
                objBtn.ID = "btnDel" + i.ToString();
                objBtn.CssClass = "idio_FlatBtnOnMouseOut";
                objBtn.Click += new EventHandler(objBtnDel_Click);
                objCell.Controls.Add(objBtn);

                objRow.Cells.Add(objCell);

                ////用来缩放内容的
                //objCell = new HtmlTableCell();
                //objCell.InnerHtml = "<a href='javascript:ExpandOptionsTable(\"" + i.ToString() + "\")'><img src='UI/Images/09r003.gif' border='0' /></a>";
                //objCell.Attributes.Add("style", "padding:4px; ");
                //objRow.Cells.Add(objCell);

                //显示MLFB+Options
                objCell = new HtmlTableCell();
                //objCell.InnerHtml = lstclsRowData[i].lstProduct[0].ProductText + "(" + (lstclsRowData[i].lstProduct.Count - 1).ToString() + "种选件)";
                string strInnerHTML = "";
                strInnerHTML = lstclsRowData[i].lstProduct[0].ProductText;
                //if (strInnerHTML.Length == 12)
                //{
                //    //strInnerHTML = strInnerHTML.funString_StringStuff(4, 0, " ", true);
                //    strInnerHTML = strInnerHTML.funString_StringStuff(7, 0, "-", true);
                //}
                if (lstclsRowData[i].lstProduct.Count > 1)
                {
                    strInnerHTML = strInnerHTML + "-Z<br>";
                    #region "循环全部的Options"
                    for (int x = 1; x < lstclsRowData[i].lstProduct.Count; x++)
                    {
                        //循环这个选件一台中添加几个
                        try
                        {
                            if (lstclsRowData[i].QTY == 0)
                            {
                                lstclsRowData[i].QTY = 1;
                            }
                        }
                        catch (Exception)
                        {


                        }
                        for (int y = 0; y < (lstclsRowData[i].lstProduct[x].ProductCount / lstclsRowData[i].QTY); y++)
                        {
                            strInnerHTML = strInnerHTML + "+" + lstclsRowData[i].lstProduct[x].ProductText;
                        }
                    }
                    #endregion
                    strInnerHTML = strInnerHTML.Replace("-Z<br>+", "-Z<br>");
                }
                objCell.InnerHtml = strInnerHTML;
                objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #EEEEEE;");
                if (!lstclsRowData[i].IsValid)
                {
                    //objCell.Attributes.Add("class", "FontRed");
                    for (int j = 0; j < lstclsRowData[i].lstVItem.Count; j++)
                    {
                        objCell.InnerHtml = objCell.InnerHtml.Replace(lstclsRowData[i].lstVItem[j].ToString(), "<span style='color:#ff0000'>" + lstclsRowData[i].lstVItem[j].ToString() + "</span>");
                    }
                    //如果不是选项错误，那么就是MLFB号错误，那么全部标红
                    if (lstclsRowData[i].lstVItem.Count == 0)
                    {
                        objCell.InnerHtml = "<span style='color:#ff0000'>" + objCell.InnerHtml + "</span>";
                    }
                }
                objRow.Cells.Add(objCell);

                //显示数量
                objCell = new HtmlTableCell();
                objCell.InnerHtml = lstclsRowData[i].QTY.ToString();
                objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #EEEEEE;");
                if (!lstclsRowData[i].IsValid)
                {
                    objCell.Attributes.Add("class", "FontRed");
                }
                objRow.Cells.Add(objCell);

                //折扣率
                objCell = new HtmlTableCell();
                //objCell.InnerHtml = (lstclsRowData[i].DiscountFactor * decimal.Parse("100")).ToString() + "%";
                objCell.InnerHtml = (lstclsRowData[i].LPFactor * decimal.Parse("100")).ToString("F2") + "%";
                objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #EEEEEE;");
                if (!lstclsRowData[i].IsValid)
                {
                    objCell.Attributes.Add("class", "FontRed");
                }
                objRow.Cells.Add(objCell);

                //客户价
                objCell = new HtmlTableCell();
                ////double RowTotalPrice = (((double)lstclsRowData[i].TotalPrice * (double)lstclsRowData[i].DiscountFactor) / 100d) * (double)lstclsRowData[i].QTY;
                //decimal RowTotalPrice = lstclsRowData[i].TotalPrice * lstclsRowData[i].DiscountFactor / decimal.Parse("100");

                //计算KP的公式为：LP* 折扣factor*(1-bonus%) * 数量=K-price 2010-11-11 by Cummins Wang Zhuo(Wang Yilin)
                //decimal RowTotalLP = lstclsRowData[i].TotalLP * lstclsRowData[i].DiscountFactor;
                decimal RowTotalLP = lstclsRowData[i].TotalDisLP;//不用除了，已经是不含税的价格 / decimal.Parse("1.17");// lstclsRowData[i].TotalLP;//不用 / decimal.Parse("100");了，是因为粘贴的就是58%得到的数就是0.58
                //TotalPrice = TotalPrice + RowTotalLP;
                TotalLP = TotalLP + RowTotalLP;
                objCell.InnerHtml = RowTotalLP.ToString("n");
                objCell.Attributes.Add("nowrap", "nowrap");
                objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #EEEEEE;");
                if (!lstclsRowData[i].IsValid)
                {
                    objCell.Attributes.Add("class", "FontRed");

                }
                objCell.Align = "right";
                objRow.Cells.Add(objCell);

                ////客户价
                //objCell = new HtmlTableCell();
                //////double RowTotalPrice = (((double)lstclsRowData[i].TotalPrice * (double)lstclsRowData[i].DiscountFactor) / 100d) * (double)lstclsRowData[i].QTY;
                ////decimal RowTotalPrice = lstclsRowData[i].TotalPrice * lstclsRowData[i].DiscountFactor / decimal.Parse("100");

                ////计算KP的公式为：LP* 折扣factor*(1-bonus%) * 数量=K-price 2010-11-11 by Cummins Wang Zhuo(Wang Yilin)
                ////decimal RowTotalLP = lstclsRowData[i].TotalLP * lstclsRowData[i].DiscountFactor;
                //decimal BonusRate = txtBonusRate.Text.funDec_StringToDecimal(0) / decimal.Parse("100");
                //decimal RowTotalKP = lstclsRowData[i].TotalLP * lstclsRowData[i].DiscountFactor * (1 - BonusRate);//不用 / decimal.Parse("100");了，是因为粘贴的就是58%得到的数就是0.58
                ////TotalPrice = TotalPrice + RowTotalLP;
                //TotalKP = TotalKP + RowTotalKP;
                //objCell.InnerHtml = RowTotalKP.ToString("n");
                //objCell.Attributes.Add("nowrap", "nowrap");
                //objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #EEEEEE;");
                //if (!lstclsRowData[i].IsValid)
                //{
                //    objCell.Attributes.Add("class", "FontRed");
                //}
                //objCell.Align = "right";
                //objRow.Cells.Add(objCell);

                //成本价
                if (objLoginUserInfo.funBln_Limited("3004", objLoginUserInfo.SystemLimited))
                {
                    objCell = new HtmlTableCell();
                    //decimal RowTotalTP = lstclsRowData[i].TotalDisTP / decimal.Parse("1.17") * lstclsRowData[i].Factor; ;// lstclsRowData[i].TotalTP;// *lstclsRowData[i].DiscountFactor;// / decimal.Parse("100");
                    decimal RowTotalTP = lstclsRowData[i].TotalTP;// lstclsRowData[i].TotalTP;// *lstclsRowData[i].DiscountFactor;// / decimal.Parse("100");
                    TotalTP = TotalTP + RowTotalTP;
                    objCell.InnerHtml = RowTotalTP.ToString("n");
                    objCell.Attributes.Add("nowrap", "nowrap");
                    objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #EEEEEE;");
                    if (!lstclsRowData[i].IsValid)
                    {
                        objCell.Attributes.Add("class", "FontRed");
                    }
                    objCell.Align = "right";
                    objRow.Cells.Add(objCell);
                }

                objTable.Rows.Add(objRow);
                #endregion

                #endregion
            }

            #region "总计"
            objRow = new HtmlTableRow();

            objCell = new HtmlTableCell();
            objCell.ColSpan = 5;
            objCell.Attributes.Add("nowrap", "nowrap");
            objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
            objCell.Align = "right";
            objCell.InnerHtml = "<strong>Total:</strong>" + TotalLP.ToString("n");// TotalPrice.ToString("n"); //lstclsRowData.Sum(q => q.TotalPrice); 
            objRow.Cells.Add(objCell);

            //objCell = new HtmlTableCell();
            //objCell.Attributes.Add("nowrap", "nowrap");
            //objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
            //objCell.Align = "right";
            //objCell.InnerHtml = "&nbsp;";
            //objRow.Cells.Add(objCell);

            //objCell = new HtmlTableCell();
            //objCell.Attributes.Add("nowrap", "nowrap");
            //objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
            //objCell.Align = "right";
            //objCell.InnerHtml = TotalKP.ToString("n");
            //objRow.Cells.Add(objCell);

            if (objLoginUserInfo.funBln_Limited("3004", objLoginUserInfo.SystemLimited))
            {
                objCell = new HtmlTableCell();
                objCell.Attributes.Add("nowrap", "nowrap");
                objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
                objCell.Align = "right";
                objCell.InnerHtml = TotalTP.ToString("n");
                objRow.Cells.Add(objCell);
            }

            objTable.Rows.Add(objRow);

            //lblTotalPrice.Text = TotalPrice.ToString("n");
            #endregion

            divList.Controls.Add(objTable);
        }

        /// <summary>
        /// 删除一个选件产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void objBtnDel_Click(object sender, EventArgs e)
        {
            int intIndex = 0;
            intIndex = ((Button)sender).ID.Substring(6).funInt_StringToInt(-1);

            if (intIndex > -1)
            {
                try
                {
                    List<ClassLibrary.DriveProductRowData> lstclsRowData = new List<ClassLibrary.DriveProductRowData>();
                    if (Session["DriveProductList"] == null)
                    {
                        Session["DriveProductList"] = new List<ClassLibrary.ProductRowData>();
                    }
                    lstclsRowData = (List<ClassLibrary.DriveProductRowData>)Session["DriveProductList"];

                    lstclsRowData.Remove(lstclsRowData[intIndex]);

                    Session["DriveProductList"] = lstclsRowData;

                    subDisplay_ListTable();//刷新列表
                }
                catch
                {
                }
            }
        }

        protected void txtBonusRate_TextChanged(object sender, EventArgs e)
        {
            //因为改变了Bonus，所以，需要重新计算KP的值的显示
            subDisplay_ListTable();
        }

        /// <summary>
        /// 修改Excel内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModify_Click(object sender, EventArgs e)
        {
            //tableList.Visible = false;
            //tablePaste.Visible = true;
            tableList.Attributes.Add("style", "display:none;");
            tablePaste.Attributes.Add("style", "display:block;");
        }

        /// <summary>
        /// 重置报价单（不保存信息，只看看值）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuickOrderEntry.aspx");
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            HtmlTable objTable = (HtmlTable)divList.Controls[0];
            sb.Append("MLFB,");
            sb.Append("Qty,");
            sb.Append("LPrice,");
            if (objLoginUserInfo.funBln_Limited("3004", objLoginUserInfo.SystemLimited))
            {
                sb.Append("TPrice");
            }
            sb.Append("\r\n");
            for (int i = 0; i < objTable.Rows.Count - 1; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                HtmlTableRow row = objTable.Rows[i];
                sb.Append(row.Cells[1].InnerText.funString_RemoveHTML() + ",");
                sb.Append(row.Cells[2].InnerText.Replace(",", "") + ",");
                sb.Append(row.Cells[4].InnerText.Replace(",", "") + ",");
                if (objLoginUserInfo.funBln_Limited("3004", objLoginUserInfo.SystemLimited))
                {
                    sb.Append(row.Cells[5].InnerText.Replace(",", ""));
                }
                sb.Append("\r\n");
            }
            string temp = string.Format("attachment;filename={0}", "ExportData.csv");
            Response.ClearHeaders();
            Response.AppendHeader("Content-disposition", temp);
            Response.Write(sb.ToString());
            Response.End();
        }

    }

    public class classDriveQItems
    {
        public string MLFB = "";
        public int Qty = 0;
        public decimal Discount = 0;
    }
}