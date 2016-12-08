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

namespace CHub
{
    public partial class ProductsQuotation : ClassLibrary.Page
    {
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
                    ((CHub.Main)Master).TitleName = "ProductsQuotation(Select)";
                }
                else
                {
                    ((CHub.Main)Master).TitleName = "ProductsQuotation(Paste)";
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

            #region "粘贴Excel设置"
            //if (IsPostBack == true)
            //    return;

            //#region "将Excel模板文件拷贝一份到自己的文件夹里面"
            //string _Targ_file = "test.xml";
            //string file_name = Server.MapPath("Document/Excel/" + _Targ_file);

            ////先创建文件夹，然后判断文件是否存在
            //string strDirPath = Server.MapPath("Temp/ExcelExport/" + objLoginUserInfo.ID.Replace("-", "") + "/");
            //if (!Directory.Exists(strDirPath))
            //{
            //    Directory.CreateDirectory(strDirPath);
            //}
            //string strNewFile = strDirPath + _Targ_file;
            //subDeleteFile_TempFile(strNewFile);//如果已经存在test.xml文件，那么先删除
            //File.Copy(file_name, strNewFile);
            //#endregion

            ////取得Excel内容------------------------------------------
            //XmlDocument xml = new XmlDocument();
            //xml.Load(strNewFile);
            //if (xml != null)
            //    _hf_ExcelXmlData.Value = xml.OuterXml;

            ////取得Excel设置------------------------------------------
            //XmlNode node = get_ExcelSetting(_Targ_file);
            //if (node != null)
            //    _hf_ExcelSetting.Value = node.OuterXml;
            #endregion
        }


        /// <summary>
        /// 删除临时文件
        /// </summary>
        /// <param name="fileName"></param>
        private void subDeleteFile_TempFile(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            if (file.Exists)
            {
                try
                {
                    file.Delete();
                }
                catch
                {

                }
                finally
                {
                    file = null;
                }
            }
        }

        /// <summary>
        /// 粘贴Excel设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private XmlNode get_ExcelSetting(string key)
        {
            //string file = HttpContext.Current.Server.MapPath("Document/Excel/Excels.xml");
            string file = Server.MapPath("Document/Excel/Excels.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            //-------------------------------------------------------
            if (doc != null)
            {
                XmlNode root = doc.DocumentElement;
                if (root != null)
                {
                    string query = string.Format("sheet[@filename='{0}']", key);
                    XmlNode node = root.SelectSingleNode(query);
                    if (node != null)
                        return node;
                }
            }

            //-------------------------------------------------------
            return null;
        }

        /// <summary>
        /// 粘贴提交按钮被点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPSubmit_Click(object sender, EventArgs e)
        {
            List<IdioSoft.Public.cMaterial> objSubmitData = objLoginUserInfo.PasteData;// _hf_ExcelXmlData.Value;

            try
            {
                List<ClassLibrary.ProductRowData> lstclsRowData = new List<ClassLibrary.ProductRowData>();
                DataSet ds = new DataSet();

                #region "开始循环判断"
                #region "先得到数组"
                List<classQItems> lstTable = new List<classQItems>();
                for (int i = 0; i < objSubmitData.Count; i++)
                {
                    classQItems item = new classQItems();
                    item.MLFB = objSubmitData[i].MLFB;
                    item.SPR = objSubmitData[i].SPR;
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
                    item.Voltage = objSubmitData[i].Voltage.funDec_StringToDecimal(0);
                    item.V70Index = objSubmitData[i].V70Index;
                    if (item.V70Index == null)
                    {
                        item.V70Index = "";
                    }

                    lstTable.Add(item);
                }
                #endregion

                string strSQL = "";
                //因为有列头，所以从2行开始取
                for (int i = 0; i < lstTable.Count; i++)
                {
                    if (lstTable[i].Qty == 0)
                    {
                        lstTable[i].Qty = 1;
                    }
                    //如果有文字，那么证明是有效的文本
                    string strProduct = lstTable[i].MLFB;
                    if (strProduct.Length > 0)
                    {
                        #region "填写MLFB"
                        ClassLibrary.ProductRowData itemclsRowData = new ClassLibrary.ProductRowData();

                        #region "处理数据"
                        #region "将每行的数据拆分出来放到数组中 第一列是产品，第二列是SPR，第三列是数量，第四列是折扣"
                        int intQTY = lstTable[i].Qty; //aryRowText[1].funInt_StringToInt(1);
                        itemclsRowData.QTY = intQTY;

                        itemclsRowData.LPFactor = lstTable[i].Discount; //折扣率
                        itemclsRowData.Voltage = lstTable[i].Voltage;
                        itemclsRowData.V70Index = lstTable[i].V70Index;
                        #endregion

                        itemclsRowData.lstProduct = new List<CHub.ClassLibrary.ProductRowItem>();
                        itemclsRowData.OptionsText = "";
                        #region "然后将第一列再分拆"
                        #region "将MLFB拆分出来"
                        //数据举例
                        //aryProduct[0] 1LG0253-4AA70-Z
                        //aryProduct[1] K11+K45	
                        string[] aryProduct = Regex.Split(strProduct, "-Z=", RegexOptions.IgnoreCase);
                        if (aryProduct[aryProduct.Length - 1].Trim().Length == 0)
                        {
                            //删除Item aryProduct[aryProduct.Length - 1]
                            List<string> lst = new List<string>(aryProduct);
                            lst.RemoveAt(lst.Count - 1);
                            aryProduct = lst.ToArray();
                            //aryProduct.ToList().RemoveAt(aryProduct.Length - 1);
                            //aryProduct = aryProduct.ToArray();
                        }

                        //将MLFB号放置到Item类中
                        ClassLibrary.ProductRowItem itemclsProductItem = new CHub.ClassLibrary.ProductRowItem();
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
                                    if (itemclsRowData.lstProduct[x].ProductText.ToUpper() == aryOptions[j].ToUpper().Trim())
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
                                    itemclsProductItem = new CHub.ClassLibrary.ProductRowItem();
                                    itemclsProductItem.ProductText = aryOptions[j].Trim();
                                    itemclsProductItem.ProductCount = intQTY;
                                    //itemclsProductItem.LPFactor = itemclsRowData.LPFactor;
                                    //将Item类添加到行类中
                                    itemclsRowData.lstProduct.Add(itemclsProductItem);
                                }
                            }
                        }
                        #endregion
                        #endregion
                        #endregion

                        itemclsRowData = funMLFBData_RowData(itemclsRowData, false, "");//开始在数据库判断产品是否有效

                        #region "V70"
                        //每台点击都收费
                        string V70Index = itemclsRowData.V70Index;
                        if (V70Index.Length > 0)
                        {
                            decimal V70Price = objClassDbAccess.funString_SQLExecuteScalar("SELECT V70Price FROM CHub_Basic_V70 WHERE (V70Index = 1)").funDec_StringToDecimal(0);
                            itemclsRowData.TotalLP += V70Price;
                            itemclsRowData.TotalTP += V70Price;
                        }
                        #endregion
                        lstclsRowData.Add(itemclsRowData);//将行的数据保存到list中
                        #endregion
                    }
                    else
                    {
                        string strSPR = lstTable[i].SPR;
                        if (strSPR.Length > 0)
                        {
                            #region "填写SPR"
                            strSQL = "SELECT ID, MLFB, Options, Voltage, Quantity, OthersPerUnit, OthersPerItem FROM CHub_Info_SPR WHERE (SPRNo = '" + strSPR + "') AND (IsDel = 0) ORDER BY OthersPerItem DESC";
                            DataSet dsSPR = new DataSet();
                            dsSPR = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
                            int intSPRCount = dsSPR.Tables[0].Rows.Count;
                            if (intSPRCount == 0)
                            {
                                #region "错误，SPR在数据库中不存在"
                                ClassLibrary.ProductRowData itemclsRowData = new ClassLibrary.ProductRowData();
                                itemclsRowData.MLFB = lstTable[i].MLFB;
                                itemclsRowData.SPR = lstTable[i].SPR;
                                itemclsRowData.QTY = lstTable[i].Qty;
                                itemclsRowData.LPFactor = lstTable[i].Discount;
                                itemclsRowData.lstProduct[0].ProductText = lstTable[i].MLFB + lstTable[i].SPR;
                                itemclsRowData.lstProduct[0].ProductCount = lstTable[i].Qty;
                                itemclsRowData.lstProduct[0].ItemLP = decimal.Parse("0");
                                itemclsRowData.lstProduct[0].ItemTP = decimal.Parse("0");
                                itemclsRowData.lstProduct[0].IsRight = false;
                                itemclsRowData.lstProduct[0].ErrorInfo = "错误的SPR号！";
                                //将此数据设置为无效，继续循环
                                itemclsRowData.IsValid = false;
                                continue;
                                #endregion
                            }
                            else
                            {
                                for (int intSPRIndex = 0; intSPRIndex < intSPRCount; intSPRIndex++)
                                {
                                    string SPRMLFBID = dsSPR.Tables[0].Rows[intSPRIndex]["ID"].ToString();
                                    strProduct = dsSPR.Tables[0].Rows[intSPRIndex]["MLFB"].ToString() + "-Z=" + dsSPR.Tables[0].Rows[intSPRIndex]["Options"].ToString();

                                    //得到SPR号填写的电压，在下面的switch语句中使用
                                    decimal intVoltage = dsSPR.Tables[0].Rows[intSPRIndex]["Voltage"].ToString().funDec_StringToDecimal(0);
                                    int intQTY = dsSPR.Tables[0].Rows[intSPRIndex]["Quantity"].ToString().funInt_StringToInt(1);

                                    #region "每个MLFB号进行判断"
                                    ClassLibrary.ProductRowData itemclsRowData = new ClassLibrary.ProductRowData();
                                    itemclsRowData.MLFB = dsSPR.Tables[0].Rows[intSPRIndex]["MLFB"].ToString();
                                    itemclsRowData.OptionsText = dsSPR.Tables[0].Rows[intSPRIndex]["Options"].ToString();
                                    itemclsRowData.SPR = strSPR;
                                    itemclsRowData.Voltage = intVoltage;
                                    itemclsRowData.QTY = intQTY;

                                    #region "设计费"
                                    if (dsSPR.Tables[0].Rows[intSPRIndex]["OthersPerUnit"].ToString() != "")
                                    {
                                        //每台电机都收费
                                        itemclsRowData.SPROthersPerUnit = dsSPR.Tables[0].Rows[intSPRIndex]["OthersPerUnit"].ToString().funDec_StringToDecimal(0);
                                    }
                                    if (dsSPR.Tables[0].Rows[intSPRIndex]["OthersPerItem"].ToString() != "")
                                    {
                                        ////一个SPR只收一次费
                                        //if (intSPRIndex == 0)
                                        //{
                                            //itemclsRowData.SPROthersPerUnit = dsSPR.Tables[0].Rows[intSPRIndex]["OthersPerItem"].ToString().funDec_StringToDecimal(0);
                                            itemclsRowData.SPROthersPerItem = dsSPR.Tables[0].Rows[intSPRIndex]["OthersPerItem"].ToString().funDec_StringToDecimal(0);
                                        //}
                                    }
                                    #endregion

                                    #region "处理数据"
                                    #region "将每行的数据拆分出来放到数组中 第一列是产品，第二列是数量，第三列是折扣"
                                    ////if (lstTable[i].Qty == 0)
                                    ////{
                                    ////    lstTable[i].Qty = 1;
                                    ////}
                                    //int intQTY = lstTable[i].Qty; //aryRowText[1].funInt_StringToInt(1);
                                    //itemclsRowData.QTY = intQTY;

                                    itemclsRowData.LPFactor = lstTable[i].Discount; //折扣率
                                    #endregion

                                    itemclsRowData.lstProduct = new List<CHub.ClassLibrary.ProductRowItem>();
                                    itemclsRowData.OptionsText = "";
                                    #region "然后将第一列再分拆"
                                    #region "将MLFB拆分出来"
                                    //数据举例
                                    //aryProduct[0] 1LG0253-4AA70-Z
                                    //aryProduct[1] K11+K45	
                                    string[] aryProduct = Regex.Split(strProduct, "-Z=", RegexOptions.IgnoreCase);

                                    //将MLFB号放置到Item类中
                                    //clsProductItem itemclsProductItem = new clsProductItem();
                                    ClassLibrary.ProductRowItem itemclsProductItem = new CHub.ClassLibrary.ProductRowItem();
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
                                                itemclsProductItem = new CHub.ClassLibrary.ProductRowItem();
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

                                    itemclsRowData = funMLFBData_RowData(itemclsRowData, true, SPRMLFBID);//开始在数据库判断产品是否有效
                                    lstclsRowData.Add(itemclsRowData);//将行的数据保存到list中
                                    #endregion
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region "错误，MLFB和SPR两者必须填写一个"
                            ClassLibrary.ProductRowData itemclsRowData = new ClassLibrary.ProductRowData();
                            itemclsRowData.MLFB = lstTable[i].MLFB;
                            itemclsRowData.SPR = lstTable[i].SPR;
                            itemclsRowData.QTY = lstTable[i].Qty;
                            itemclsRowData.LPFactor = lstTable[i].Discount;
                            itemclsRowData.lstProduct[0].ProductText = lstTable[i].MLFB + lstTable[i].SPR;
                            itemclsRowData.lstProduct[0].ProductCount = lstTable[i].Qty;
                            itemclsRowData.lstProduct[0].ItemLP = decimal.Parse("0");
                            itemclsRowData.lstProduct[0].ItemTP = decimal.Parse("0");
                            itemclsRowData.lstProduct[0].IsRight = false;
                            itemclsRowData.lstProduct[0].ErrorInfo = "MLFB和SPR两者必须填写一个！";
                            //将此数据设置为无效，继续循环
                            itemclsRowData.IsValid = false;
                            continue;
                            #endregion
                        }
                    }
                }
                #endregion

                //将列表显示出来，用来给用户最后确定
                tablePaste.Attributes.Add("style", "display:none;");
                tableList.Attributes.Add("style", "display:block;");

                //将值默认保存到Session中
                Session["ProductList"] = lstclsRowData;
                subDisplay_ListTable();

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Source + "<br>" + ex.Message;
            }
        }

        /// <summary>
        /// 返回每行MLFB+Options的判断List对象
        /// </summary>
        /// <param name="itemRowData">保存了提交MLFB等信息的对象</param>
        /// <param name="IsSPRData">是否是SPR数据，False：直接提交的MLFB+Options；True：提交的SPR号</param>
        /// <param name="SPRMLFBID">如果是SPR数据，CHub_Info_SPR数据库表的ID；否则，为''</param>
        /// <returns></returns>
        private ClassLibrary.ProductRowData funMLFBData_RowData(ClassLibrary.ProductRowData itemRowData, bool IsSPRData, string SPRMLFBID)
        {
            string strSQL = "";
            DataSet ds = new DataSet();
            ClassLibrary.ProductRowData itemclsRowData = new ClassLibrary.ProductRowData();
            itemclsRowData = itemRowData;

            string strFirstChar = "";

            #region "开始在数据库判断产品是否有效"
            string strCheckMLFB = "";
            //得到用来做判断的MLFB号
            strCheckMLFB = itemclsRowData.lstProduct[0].ProductText;
            decimal intVoltage = itemclsRowData.Voltage;

            #region "判断选件是否都是数据库中允许的"
            //string strOptionsText = itemclsRowData.OptionsText.Replace(" ", "").Replace(",", "','");//加上空格的去掉是为了防止加上,号时加空格也包括进去，这样IN的时候怕找不到这个选件
            #region "判断MLFB号，并赋值"
            itemclsRowData.IsValid = true;

            itemclsRowData.SPRMLFB = strCheckMLFB;
            #region "开始在数据库判断产品是否有效"
            strSQL = "SELECT CurrentPrice FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB = '" + strCheckMLFB + "')";
            string strCountProduct = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
            decimal CurrentPrice = 0;
            if (strCountProduct.Length > 0)
            {
                #region "MLFB的表价和成本价"
                //得到表价
                //itemclsRowData.lstProduct[0].LPFactor = itemclsRowData.LPFactor;//LP折扣
                CurrentPrice = strCountProduct.funDec_StringToDecimal(0);
                itemclsRowData.lstProduct[0].ItemLP = CurrentPrice;//LP
                itemclsRowData.TotalLP = CurrentPrice * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的表价
                itemclsRowData.TotalDisLP = (CurrentPrice * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                itemclsRowData.lstProduct[0].IsRight = true;

                //得到成本价
                CurrentPrice = objClassDbAccess.funString_SQLExecuteScalar("SELECT Factor FROM CHub_Info_TPFactor where MLFB='" + strCheckMLFB + "'").funDec_StringToDecimal(1);
                //itemclsRowData.lstProduct[0].TPFactor = CurrentPrice;
                itemclsRowData.TPFactor = CurrentPrice;
                CurrentPrice = itemclsRowData.lstProduct[0].ItemLP * CurrentPrice;//TP=LP*Factor
                itemclsRowData.lstProduct[0].ItemTP = CurrentPrice;
                itemclsRowData.TotalTP = CurrentPrice * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的成本价
                itemclsRowData.TotalDisTP = (CurrentPrice * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                #endregion

                #region "继续计算配件的价格"
                int intInputOptionCount = itemclsRowData.lstProduct.Count;
                if (intInputOptionCount > 1)
                {
                    for (int intOptionIndex = 1; intOptionIndex < intInputOptionCount; intOptionIndex++)
                    {
                        //得到选件名称
                        string strTempOptionValue = itemclsRowData.lstProduct[intOptionIndex].ProductText;
                        #region "得到选件的表价、成本价，并判断选件是否合格"
                        string strMLFBPercentage = objClassDbAccess.funString_SQLExecuteScalar("SELECT MLFBPercentage FROM CHub_Info_LPSpecialOptions WHERE OptionValue='" + strTempOptionValue.funString_SQLToString() + "'");
                        bool blnIsSpecialOption=false;
                        if (strMLFBPercentage.Trim().Length > 0)
                        {
                            blnIsSpecialOption = true;
                            if (strTempOptionValue.ToUpper() == "L27")
                            {
                                if (strCheckMLFB.Substring(0, 4).ToUpper() != "1LA8" && strCheckMLFB.Substring(0, 4).ToUpper() != "1PQ8")
                                {
                                    blnIsSpecialOption = false;
                                }
                            }
                        }
                        if (blnIsSpecialOption)
                        {
                            #region "是特殊选件，直接用MLFB的价格乘以百分比"
                            decimal decMLFBPercentage = decimal.Parse(strMLFBPercentage);
                            #region "得到选件的表价"
                            itemclsRowData.lstProduct[intOptionIndex].ItemLP = 0;//itemclsRowData.lstProduct[0].ItemLP * decMLFBPercentage;
                            //itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                            //itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                            itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                            itemclsRowData.lstProduct[intOptionIndex].IsSpecialOption = true;
                            itemclsRowData.lstProduct[intOptionIndex].SpecialOptionPercentage = decMLFBPercentage;
                            #endregion
                            #region "得到选件的成本价"
                            itemclsRowData.lstProduct[intOptionIndex].ItemTP = 0;//itemclsRowData.lstProduct[0].ItemTP * decMLFBPercentage;
                            //itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                            ////itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                            #endregion
                            #endregion
                        }
                        else
                        {
                            #region "不是特殊选件，那么从数据库查找"
                            strSQL = "SELECT OptionValue, CurrentPrice FROM CHub_Info_LP WHERE (PriceType = 'Options') AND (MLFB = '" + strCheckMLFB + "') AND (OptionValue = '" + strTempOptionValue + "')";
                            ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                #region "得到选件的表价"
                                itemclsRowData.lstProduct[intOptionIndex].ItemLP = ds.Tables[0].Rows[0]["CurrentPrice"].ToString().funDec_StringToDecimal(0);//得到各个配件在数据库中的价格
                                itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                                #endregion
                                #region "得到选件的成本价"
                                itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.TPFactor;//得到各个配件在数据库中的价格
                                itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + (itemclsRowData.lstProduct[intOptionIndex].ItemTP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                                #endregion
                            }
                            else
                            {
                                if (IsSPRData)
                                {
                                    #region "数据库没有，那么用SPR中用户自己填写的价格"
                                    string strSPROptionPrice = objClassDbAccess.funString_SQLExecuteScalar("SELECT CurrentPrice FROM CHub_Info_SPROptions WHERE (SPRID = '" + SPRMLFBID + "') AND (IsDel = 0) AND (OptionValue='" + strTempOptionValue.funString_SQLToString() + "')");
                                    if (strSPROptionPrice.Trim().Length > 0)
                                    {
                                        #region "得到选件的表价"
                                        itemclsRowData.lstProduct[intOptionIndex].ItemLP = strSPROptionPrice.funDec_StringToDecimal(0);//得到各个配件在数据库中的价格
                                        itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                        itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                        itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                                        #endregion

                                        #region "根据特殊情况进行判断"
                                        string strSelectMLFB = strCheckMLFB.ToUpper().Substring(0, 4);
                                        decimal decMLFBPercentage = 1;
                                        switch (strSelectMLFB)
                                        {
                                            case "1LA8":
                                            case "1PQ8":
                                            case "1LL8":
                                                decMLFBPercentage = decimal.Parse("1.525");
                                                break;
                                            case "1LA4":
                                            case "1MS4":
                                            case "1PQ4":
                                                #region "1LA4"
                                                switch (strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1))
                                                {
                                                    case "M":
                                                        decMLFBPercentage = decimal.Parse("3.8125");
                                                        break;
                                                    case "N":
                                                    case "W":
                                                        decMLFBPercentage = decimal.Parse("2.6");
                                                        break;
                                                    default:
                                                        //没找到产品信息，将此数据设置为无效
                                                        itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                        itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = strSelectMLFB + "找不到对应的TP规则！";
                                                        itemclsRowData.IsValid = false;
                                                        break;
                                                }
                                                #endregion
                                                break;
                                            case "1RA1"://这个地方改成以1R开头就行了：2013-10-28
                                            case "1RQ1":
                                            case "1RN1":
                                                decMLFBPercentage = decimal.Parse("2.6");
                                                break;
                                            default:
                                                //没找到产品信息，将此数据设置为无效
                                                itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "找不到对应的TP规则！";
                                                itemclsRowData.IsValid = false;
                                                //给个默认值，防止下面计算出错
                                                decMLFBPercentage = decimal.Parse("1");
                                                break;
                                        }
                                        #endregion
                                        decMLFBPercentage = decMLFBPercentage / decimal.Parse("1.17");
                                        #region "得到选件的成本价"
                                        //itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * decMLFBPercentage * itemclsRowData.TPFactor;//得到各个配件在数据库中的价格
                                        itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * decMLFBPercentage;//得到各个配件在数据库中的价格
                                        itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                        itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + (itemclsRowData.lstProduct[intOptionIndex].ItemTP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                                        #endregion
                                    }
                                    else
                                    {
                                        itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                        //将此数据设置为无效，继续循环
                                        itemclsRowData.IsValid = false;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    //没找到选件信息，将此数据设置为无效
                                    itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                    if (itemclsRowData.lstProduct[intOptionIndex].ProductText.Length > 0)
                                    {
                                        strFirstChar = itemclsRowData.lstProduct[intOptionIndex].ProductText.Substring(0, 1).ToUpper();
                                    }
                                    if (strFirstChar == "J" || strFirstChar == "X")
                                    {
                                        itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "此选件请单独询价！";
                                    }
                                    else
                                    {
                                        itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "没找到对应的选件LP！";
                                    }
                                    itemclsRowData.IsValid = false;
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                }
                else
                {
                    itemclsRowData.lstProduct[0].IsRight = true;
                    itemclsRowData.IsValid = true;//只有电机一个，所以肯定是正确的了
                }
                #endregion
            }
            else
            {
                #region "MLFB号数据库中不存在，那么判断特殊情况"
                //decimal intVoltage = itemclsRowData.Voltage;
                string strSelectChar = strCheckMLFB.ToUpper().Substring(0, 4);
                switch (strCheckMLFB.Substring(strCheckMLFB.Length - 2, 1))
                {
                    case "9":
                        #region "倒数第二位是9"
                        #region "根据特殊情况进行判断"
                        switch (strSelectChar)
                        {
                            case "1LA8":
                            case "1PQ8":
                            case "1LL8":
                                #region "1LA8/1PQ8/1LL8"
                                string strCheckMLFB2 = "";
                                if (strSelectChar == "1LA8")
                                {
                                    if (strCheckMLFB.Substring(strCheckMLFB.Length - 1, 1) == "1")
                                    {
                                        strCheckMLFB = strCheckMLFB.Substring(0, strCheckMLFB.Length - 1) + "4";
                                        strCheckMLFB2 = strCheckMLFB.Substring(0, strCheckMLFB.Length - 1) + "8";//或者8
                                    }
                                }
                                strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strCheckMLFB.funString_StringStuff(strCheckMLFB.Length - 2, 1, "4", false) + "', '" + strCheckMLFB.funString_StringStuff(strCheckMLFB.Length - 2, 1, "6", false) + "', '" + strCheckMLFB.funString_StringStuff(strCheckMLFB.Length - 2, 1, "7", false) + "', '" + strCheckMLFB.funString_StringStuff(strCheckMLFB.Length - 2, 1, "8", false) + "', '" + strCheckMLFB.funString_StringStuff(strCheckMLFB.Length - 2, 1, "0", false) + "')) ORDER BY MLFB";
                                strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                if (strCheckMLFB.Length == 0 && strCheckMLFB2.Length > 0)
                                {
                                    strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strCheckMLFB2.funString_StringStuff(strCheckMLFB2.Length - 2, 1, "4", false) + "', '" + strCheckMLFB2.funString_StringStuff(strCheckMLFB2.Length - 2, 1, "6", false) + "', '" + strCheckMLFB2.funString_StringStuff(strCheckMLFB2.Length - 2, 1, "7", false) + "', '" + strCheckMLFB2.funString_StringStuff(strCheckMLFB2.Length - 2, 1, "8", false) + "', '" + strCheckMLFB2.funString_StringStuff(strCheckMLFB2.Length - 2, 1, "0", false) + "')) ORDER BY MLFB";
                                    strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                }
                                #endregion
                                break;
                            case "1LA4":
                            case "1MS4":
                            case "1PQ4":
                                #region "1LA4"
                                string strSPRMLFB = strCheckMLFB;

                                switch (strSPRMLFB.Substring(strSPRMLFB.Length - 3, 1).ToUpper())
                                {
                                    case "M":
                                        strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 2, 1, "0", false) + "')) ORDER BY MLFB";
                                        strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                        break;
                                    case "N":
                                        if (strSelectChar == "1PQ4")
                                        {
                                            strSPRMLFB = strCheckMLFB.funString_StringStuff(0, 4, "1LA4", false);
                                        }
                                        if (intVoltage == 0)
                                        {
                                            //没找到电压信息，将此数据设置为无效，继续循环
                                            itemclsRowData.lstProduct[0].IsRight = false;
                                            itemclsRowData.lstProduct[0].ErrorInfo = "请填写电压！";
                                            itemclsRowData.IsValid = false;
                                        }
                                        else
                                        {
                                            if (intVoltage <= decimal.Parse("6.6"))
                                            {
                                                strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 2, 1, "6", false) + "')) ORDER BY MLFB";
                                            }
                                            else
                                            {
                                                strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 2, 1, "8", false) + "')) ORDER BY MLFB";
                                            }
                                            strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                            if (strCheckMLFB.Length == 0)
                                            {
                                                //没找到电压信息，将此数据设置为无效，继续循环
                                                itemclsRowData.lstProduct[0].IsRight = false;
                                                itemclsRowData.lstProduct[0].ErrorInfo = "请填写正确的电压！";
                                                itemclsRowData.IsValid = false;
                                            }
                                        }
                                        break;
                                    case "W":
                                        if (strSelectChar == "1PQ4")
                                        {
                                            strSPRMLFB = strCheckMLFB.funString_StringStuff(0, 4, "1LA4", false);
                                        }
                                        if (intVoltage == 0)
                                        {
                                            //没找到电压信息，将此数据设置为无效，继续循环
                                            itemclsRowData.lstProduct[0].IsRight = false;
                                            itemclsRowData.lstProduct[0].ErrorInfo = "请填写电压！";
                                            itemclsRowData.IsValid = false;
                                        }
                                        else
                                        {
                                            if (intVoltage <= decimal.Parse("6.6"))
                                            {
                                                strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 3, 1, "N", false).funString_StringStuff(strSPRMLFB.Length - 2, 1, "6", false) + "')) ORDER BY MLFB";
                                            }
                                            else
                                            {
                                                strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 3, 1, "N", false).funString_StringStuff(strSPRMLFB.Length - 2, 1, "8", false) + "')) ORDER BY MLFB";
                                            }
                                            strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                            if (strCheckMLFB.Length == 0)
                                            {
                                                //没找到电压信息，将此数据设置为无效，继续循环
                                                itemclsRowData.lstProduct[0].IsRight = false;
                                                itemclsRowData.lstProduct[0].ErrorInfo = "请填写正确的电压！";
                                                itemclsRowData.IsValid = false;
                                            }
                                        }
                                        break;
                                    case "V":
                                        strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 2, 1, "2", false) + "', '" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 2, 1, "3", false) + "', '" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 2, 1, "4", false) + "')) ORDER BY MLFB";
                                        strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                        break;
                                    default:
                                        //没找到产品信息，将此数据设置为无效，继续循环
                                        itemclsRowData.lstProduct[0].IsRight = false;
                                        itemclsRowData.lstProduct[0].ErrorInfo = "找不到对应的MLFB号！";
                                        itemclsRowData.IsValid = false;
                                        break;
                                }
                                #endregion
                                break;
                            case "1RA1":
                            case "1RQ1":
                            case "1RN1":
                                #region "1RA1/1RQ1/1RN1"
                                if (intVoltage == 0)
                                {
                                    //没找到电压信息，将此数据设置为无效，继续循环
                                    itemclsRowData.lstProduct[0].IsRight = false;
                                    itemclsRowData.lstProduct[0].ErrorInfo = "请填写电压！";
                                    itemclsRowData.IsValid = false;
                                }
                                else
                                {
                                    if (intVoltage <= decimal.Parse("6.6"))
                                    {
                                        strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strCheckMLFB.funString_StringStuff(strCheckMLFB.Length - 2, 1, "6", false) + "')) ORDER BY MLFB";
                                    }
                                    else
                                    {
                                        strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strCheckMLFB.funString_StringStuff(strCheckMLFB.Length - 2, 1, "8", false) + "')) ORDER BY MLFB";
                                    }
                                    strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                }
                                #endregion
                                break;
                            default:
                                //没找到产品信息，将此数据设置为无效，继续循环
                                itemclsRowData.lstProduct[0].IsRight = false;
                                itemclsRowData.lstProduct[0].ErrorInfo = "找不到对应的MLFB号！";
                                itemclsRowData.IsValid = false;
                                break;
                        }
                        #endregion

                        //strCheckMLFB = itemclsRowData.SPRMLFB;
                        strSQL = "SELECT CurrentPrice FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB = '" + strCheckMLFB + "')";
                        strCountProduct = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                        if (strCountProduct.Length > 0)
                        {
                            #region "MLFB的表价和成本价"
                            //得到表价
                            //itemclsRowData.lstProduct[0].LPFactor = itemclsRowData.LPFactor;//LP折扣
                            CurrentPrice = strCountProduct.funDec_StringToDecimal(0);
                            //if (strSelectChar == "1PQ4" && (strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "N" || strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "W"))
                            //{
                            //    CurrentPrice = CurrentPrice + decimal.Parse("24364.01");// * decimal.Parse("1.1")
                            //}
                            itemclsRowData.lstProduct[0].ItemLP = CurrentPrice;//LP
                            itemclsRowData.TotalLP = CurrentPrice * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的表价
                            itemclsRowData.TotalDisLP = (CurrentPrice * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                            itemclsRowData.lstProduct[0].IsRight = true;

                            //得到成本价
                            CurrentPrice = objClassDbAccess.funString_SQLExecuteScalar("SELECT Factor FROM CHub_Info_TPFactor where MLFB='" + strCheckMLFB + "'").funDec_StringToDecimal(1);
                            //itemclsRowData.lstProduct[0].TPFactor = CurrentPrice;
                            itemclsRowData.TPFactor = CurrentPrice;
                            CurrentPrice = itemclsRowData.lstProduct[0].ItemLP * CurrentPrice;//TP=LP*Factor
                            if (strSelectChar == "1PQ4" && (strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "N" || strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "W"))
                            {
                                CurrentPrice = CurrentPrice + decimal.Parse("24364.01");// * decimal.Parse("1.1")
                            }
                            itemclsRowData.lstProduct[0].ItemTP = CurrentPrice;
                            itemclsRowData.TotalTP = CurrentPrice * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的成本价
                            itemclsRowData.TotalDisTP = (CurrentPrice * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                            #endregion

                            #region "继续计算配件的价格"
                            int intInputOptionCount = itemclsRowData.lstProduct.Count;
                            if (intInputOptionCount > 1)
                            {
                                for (int intOptionIndex = 1; intOptionIndex < intInputOptionCount; intOptionIndex++)
                                {
                                    //得到选件名称
                                    string strTempOptionValue = itemclsRowData.lstProduct[intOptionIndex].ProductText;
                                    #region "得到选件的表价、成本价，并判断选件是否合格"
                                    string strMLFBPercentage = objClassDbAccess.funString_SQLExecuteScalar("SELECT MLFBPercentage FROM CHub_Info_LPSpecialOptions WHERE OptionValue='" + strTempOptionValue.funString_SQLToString() + "'");
                                    bool blnIsSpecialOption = false;
                                    if (strMLFBPercentage.Trim().Length > 0)
                                    {
                                        blnIsSpecialOption = true;
                                        if (strTempOptionValue.ToUpper() == "L27")
                                        {
                                            if (strCheckMLFB.Substring(0, 4).ToUpper() != "1LA8" && strCheckMLFB.Substring(0, 4).ToUpper() != "1PQ8")
                                            {
                                                blnIsSpecialOption = false;
                                            }
                                        }
                                    }
                                    if (blnIsSpecialOption)
                                    {
                                        #region "是特殊选件，直接用MLFB的价格乘以百分比"
                                        decimal decMLFBPercentage = decimal.Parse(strMLFBPercentage);
                                        #region "得到选件的表价"
                                        itemclsRowData.lstProduct[intOptionIndex].ItemLP = 0;//itemclsRowData.lstProduct[0].ItemLP * decMLFBPercentage;
                                        //itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                        //itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                        itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                                        itemclsRowData.lstProduct[intOptionIndex].IsSpecialOption = true;
                                        itemclsRowData.lstProduct[intOptionIndex].SpecialOptionPercentage = decMLFBPercentage;
                                        #endregion
                                        #region "得到选件的成本价"
                                        itemclsRowData.lstProduct[intOptionIndex].ItemTP = 0;//itemclsRowData.lstProduct[0].ItemTP * decMLFBPercentage;
                                        //itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                        ////itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                                        #endregion
                                        #endregion
                                    }
                                    else
                                    {
                                        #region "不是特殊选件，那么从数据库查找"
                                        strSQL = "SELECT OptionValue, CurrentPrice FROM CHub_Info_LP WHERE (PriceType = 'Options') AND (MLFB = '" + strCheckMLFB + "') AND (OptionValue = '" + strTempOptionValue + "')";
                                        ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
                                        if (ds.Tables[0].Rows.Count > 0)
                                        {
                                            #region "得到选件的表价"
                                            itemclsRowData.lstProduct[intOptionIndex].ItemLP = ds.Tables[0].Rows[0]["CurrentPrice"].ToString().funDec_StringToDecimal(0);//得到各个配件在数据库中的价格
                                            itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                            itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                            itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                                            #endregion
                                            #region "得到选件的成本价"
                                            itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.TPFactor;//得到各个配件在数据库中的价格
                                            itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                            itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + (itemclsRowData.lstProduct[intOptionIndex].ItemTP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                                            #endregion
                                        }
                                        else
                                        {
                                            #region "数据库没有"
                                            if (IsSPRData)
                                            {
                                                #region "数据库没有，那么用SPR中用户自己填写的价格"
                                                string strSPROptionPrice = objClassDbAccess.funString_SQLExecuteScalar("SELECT CurrentPrice FROM CHub_Info_SPROptions WHERE (SPRID = '" + SPRMLFBID + "') AND (IsDel = 0) AND (OptionValue='" + strTempOptionValue.funString_SQLToString() + "')");
                                                if (strSPROptionPrice.Trim().Length > 0)
                                                {
                                                    #region "得到选件的表价"
                                                    itemclsRowData.lstProduct[intOptionIndex].ItemLP = strSPROptionPrice.funDec_StringToDecimal(0);//得到各个配件在数据库中的价格
                                                    itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                                    itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                                    itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                                                    #endregion

                                                    #region "根据特殊情况进行判断"
                                                    string strSelectMLFB = strCheckMLFB.ToUpper().Substring(0, 4);
                                                    decimal decMLFBPercentage = 1;
                                                    switch (strSelectMLFB)
                                                    {
                                                        case "1LA8":
                                                        case "1PQ8":
                                                        case "1LL8":
                                                            decMLFBPercentage = decimal.Parse("1.525");
                                                            break;
                                                        case "1LA4":
                                                        case "1MS4":
                                                        case "1PQ4":
                                                            #region "1LA4"
                                                            switch (strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1))
                                                            {
                                                                case "M":
                                                                    decMLFBPercentage = decimal.Parse("3.8125");
                                                                    break;
                                                                case "N":
                                                                case "W":
                                                                    decMLFBPercentage = decimal.Parse("2.6");
                                                                    break;
                                                                default:
                                                                    //没找到产品信息，将此数据设置为无效
                                                                    itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                                    itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = strSelectMLFB + "找不到对应的TP规则！";
                                                                    itemclsRowData.IsValid = false;
                                                                    break;
                                                            }
                                                            #endregion
                                                            break;
                                                        case "1RA1"://这个地方改成以1R开头就行了：2013-10-28
                                                        case "1RQ1":
                                                        case "1RN1":
                                                            decMLFBPercentage = decimal.Parse("2.6");
                                                            break;
                                                        default:
                                                            //没找到产品信息，将此数据设置为无效
                                                            itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                            itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "找不到对应的TP规则！";
                                                            itemclsRowData.IsValid = false;
                                                            //给个默认值，防止下面计算出错
                                                            decMLFBPercentage = decimal.Parse("1");
                                                            break;
                                                    }
                                                    #endregion
                                                    decMLFBPercentage = decMLFBPercentage / decimal.Parse("1.17");
                                                    #region "得到选件的成本价"
                                                    //itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * decMLFBPercentage * itemclsRowData.TPFactor;//得到各个配件在数据库中的价格
                                                    itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * decMLFBPercentage;//得到各个配件在数据库中的价格
                                                    itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                                    itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + (itemclsRowData.lstProduct[intOptionIndex].ItemTP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                                                    #endregion
                                                }
                                                else
                                                {
                                                    itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                    //将此数据设置为无效，继续循环
                                                    itemclsRowData.IsValid = false;
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                //没找到选件信息，将此数据设置为无效
                                                itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                if (itemclsRowData.lstProduct[intOptionIndex].ProductText.Length > 0)
                                                {
                                                    strFirstChar = itemclsRowData.lstProduct[intOptionIndex].ProductText.Substring(0, 1).ToUpper();
                                                }
                                                if (strFirstChar == "J" || strFirstChar == "X")
                                                {
                                                    itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "此选件请单独询价！";
                                                }
                                                else
                                                {
                                                    itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "没找到对应的选件LP！";
                                                }
                                                itemclsRowData.IsValid = false;
                                            }
                                            #endregion
                                        }
                                        #endregion
                                    }
                                    #endregion

                                }
                            }
                            else
                            {
                                itemclsRowData.lstProduct[0].IsRight = true;
                                itemclsRowData.IsValid = true;//只有电机一个，所以肯定是正确的了
                            }
                            #endregion
                        }
                        else
                        {
                            if (itemclsRowData.lstProduct[0].IsRight == true)
                            {
                                //没找到产品信息，将此数据设置为无效，继续循环
                                itemclsRowData.lstProduct[0].IsRight = false;
                                itemclsRowData.lstProduct[0].ErrorInfo = "找不到MLFB号的LP！";
                                itemclsRowData.IsValid = false;
                            }
                            else
                            {
                                if (itemclsRowData.lstProduct[0].ErrorInfo == null || itemclsRowData.lstProduct[0].ErrorInfo == "")
                                {
                                    //没找到产品信息，将此数据设置为无效，继续循环
                                    itemclsRowData.lstProduct[0].IsRight = false;
                                    itemclsRowData.lstProduct[0].ErrorInfo = "找不到MLFB号的LP！";
                                    itemclsRowData.IsValid = false;
                                }
                            }
                        }
                        #endregion
                        break;
                    case "7":
                    case "3":
                    case "4":
                    case "5":
                    case "0":
                    case "1":
                    case "2":
                        #region "倒数第二位是7、3、4、5"
                        #region "根据特殊情况进行判断"
                        switch (strSelectChar)
                        {
                            case "1LA4":
                            case "1MS4":
                            case "1PQ4":
                                #region "1LA4"
                                string strSPRMLFB = strCheckMLFB;
                                if (strSelectChar == "1PQ4")
                                {
                                    strSPRMLFB = strCheckMLFB.funString_StringStuff(0, 4, "1LA4", false);
                                }
                                switch (strSPRMLFB.Substring(strSPRMLFB.Length - 3, 1).ToUpper())
                                {
                                    case "N":
                                    case "W":
                                        strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 3, 1, "N", false).funString_StringStuff(strSPRMLFB.Length - 2, 1, "6", false) + "')) ORDER BY MLFB";
                                        strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                        break;
                                    default:
                                        //没找到产品信息，将此数据设置为无效，继续循环
                                        itemclsRowData.lstProduct[0].IsRight = false;
                                        itemclsRowData.lstProduct[0].ErrorInfo = "找不到对应的MLFB号！";
                                        itemclsRowData.IsValid = false;
                                        break;
                                }
                                #endregion
                                break;
                            case "1RA1":
                            case "1RQ1":
                            case "1RN1":
                                #region "1RA1/1RQ1/1RN1"
                                strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strCheckMLFB.funString_StringStuff(strCheckMLFB.Length - 2, 1, "6", false) + "')) ORDER BY MLFB";
                                strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                #endregion
                                break;
                            case "1LA8":
                            case "1PQ8":
                            case "1LL8":
                                #region "1LA8/1PQ8/1LL8"
                                if (strCheckMLFB.Substring(strCheckMLFB.Length - 2, 1) == "5")
                                {
                                    strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strCheckMLFB.funString_StringStuff(strCheckMLFB.Length - 2, 1, "6", false) + "', '" + strCheckMLFB.funString_StringStuff(strCheckMLFB.Length - 2, 1, "8", false) + "')) ORDER BY MLFB";
                                    strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                }
                                else
                                {
                                    //没找到产品信息，将此数据设置为无效，继续循环
                                    itemclsRowData.lstProduct[0].IsRight = false;
                                    itemclsRowData.lstProduct[0].ErrorInfo = "找不到对应的MLFB号！";
                                    itemclsRowData.IsValid = false;
                                }
                                #endregion
                                break;
                            default:
                                //没找到产品信息，将此数据设置为无效，继续循环
                                itemclsRowData.lstProduct[0].IsRight = false;
                                itemclsRowData.lstProduct[0].ErrorInfo = "找不到对应的MLFB号！";
                                itemclsRowData.IsValid = false;
                                break;
                        }
                        #endregion

                        //strCheckMLFB = itemclsRowData.SPRMLFB;
                        strSQL = "SELECT CurrentPrice FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB = '" + strCheckMLFB + "')";
                        strCountProduct = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                        if (strCountProduct.Length > 0)
                        {
                            #region "MLFB的表价和成本价"
                            //得到表价
                            //itemclsRowData.lstProduct[0].LPFactor = itemclsRowData.LPFactor;//LP折扣
                            CurrentPrice = strCountProduct.funDec_StringToDecimal(0);
                            //if (strSelectChar == "1PQ4" && (strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "N" || strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "W"))
                            //{
                            //    CurrentPrice = CurrentPrice + decimal.Parse("24364.01");// * decimal.Parse("1.1")
                            //}
                            itemclsRowData.lstProduct[0].ItemLP = CurrentPrice;//LP
                            itemclsRowData.TotalLP = CurrentPrice * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的表价
                            itemclsRowData.TotalDisLP = (CurrentPrice * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                            itemclsRowData.lstProduct[0].IsRight = true;

                            //得到成本价
                            CurrentPrice = objClassDbAccess.funString_SQLExecuteScalar("SELECT Factor FROM CHub_Info_TPFactor where MLFB='" + strCheckMLFB + "'").funDec_StringToDecimal(1);
                            //itemclsRowData.lstProduct[0].TPFactor = CurrentPrice;
                            itemclsRowData.TPFactor = CurrentPrice;
                            CurrentPrice = itemclsRowData.lstProduct[0].ItemLP * CurrentPrice;//TP=LP*Factor
                            if (strSelectChar == "1PQ4" && (strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "N" || strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "W"))
                            {
                                CurrentPrice = CurrentPrice + decimal.Parse("24364.01");// * decimal.Parse("1.1")
                            }
                            itemclsRowData.lstProduct[0].ItemTP = CurrentPrice;
                            itemclsRowData.TotalTP = CurrentPrice * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的成本价
                            itemclsRowData.TotalDisTP = (CurrentPrice * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                            #endregion

                            #region "继续计算配件的价格"
                            int intInputOptionCount = itemclsRowData.lstProduct.Count;
                            if (intInputOptionCount > 1)
                            {
                                for (int intOptionIndex = 1; intOptionIndex < intInputOptionCount; intOptionIndex++)
                                {
                                    //得到选件名称
                                    string strTempOptionValue = itemclsRowData.lstProduct[intOptionIndex].ProductText;
                                    #region "得到选件的表价、成本价，并判断选件是否合格"
                                    string strMLFBPercentage = objClassDbAccess.funString_SQLExecuteScalar("SELECT MLFBPercentage FROM CHub_Info_LPSpecialOptions WHERE OptionValue='" + strTempOptionValue.funString_SQLToString() + "'");
                                    bool blnIsSpecialOption = false;
                                    if (strMLFBPercentage.Trim().Length > 0)
                                    {
                                        blnIsSpecialOption = true;
                                        if (strTempOptionValue.ToUpper() == "L27")
                                        {
                                            if (strCheckMLFB.Substring(0, 4).ToUpper() != "1LA8" && strCheckMLFB.Substring(0, 4).ToUpper() != "1PQ8")
                                            {
                                                blnIsSpecialOption = false;
                                            }
                                        }
                                    }
                                    if (blnIsSpecialOption)
                                    {
                                        #region "是特殊选件，直接用MLFB的价格乘以百分比"
                                        decimal decMLFBPercentage = decimal.Parse(strMLFBPercentage);
                                        #region "得到选件的表价"
                                        itemclsRowData.lstProduct[intOptionIndex].ItemLP = 0;//itemclsRowData.lstProduct[0].ItemLP * decMLFBPercentage;
                                        //itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                        //itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                        itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                                        itemclsRowData.lstProduct[intOptionIndex].IsSpecialOption = true;
                                        itemclsRowData.lstProduct[intOptionIndex].SpecialOptionPercentage = decMLFBPercentage;
                                        #endregion
                                        #region "得到选件的成本价"
                                        itemclsRowData.lstProduct[intOptionIndex].ItemTP = 0;//itemclsRowData.lstProduct[0].ItemTP * decMLFBPercentage;
                                        //itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                        ////itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                                        #endregion
                                        #endregion
                                    }
                                    else
                                    {
                                        #region "不是特殊选件，那么从数据库查找"
                                        strSQL = "SELECT OptionValue, CurrentPrice FROM CHub_Info_LP WHERE (PriceType = 'Options') AND (MLFB = '" + strCheckMLFB + "') AND (OptionValue = '" + strTempOptionValue + "')";
                                        ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
                                        if (ds.Tables[0].Rows.Count > 0)
                                        {
                                            #region "得到选件的表价"
                                            itemclsRowData.lstProduct[intOptionIndex].ItemLP = ds.Tables[0].Rows[0]["CurrentPrice"].ToString().funDec_StringToDecimal(0);//得到各个配件在数据库中的价格
                                            itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                            itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                            itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                                            #endregion
                                            #region "得到选件的成本价"
                                            itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.TPFactor;//得到各个配件在数据库中的价格
                                            itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                            itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + (itemclsRowData.lstProduct[intOptionIndex].ItemTP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                                            #endregion
                                        }
                                        else
                                        {
                                            #region "数据库没有"
                                            if (IsSPRData)
                                            {
                                                #region "数据库没有，那么用SPR中用户自己填写的价格"
                                                string strSPROptionPrice = objClassDbAccess.funString_SQLExecuteScalar("SELECT CurrentPrice FROM CHub_Info_SPROptions WHERE (SPRID = '" + SPRMLFBID + "') AND (IsDel = 0) AND (OptionValue='" + strTempOptionValue.funString_SQLToString() + "')");
                                                if (strSPROptionPrice.Trim().Length > 0)
                                                {
                                                    #region "得到选件的表价"
                                                    itemclsRowData.lstProduct[intOptionIndex].ItemLP = strSPROptionPrice.funDec_StringToDecimal(0);//得到各个配件在数据库中的价格
                                                    itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                                    itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                                    itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                                                    #endregion

                                                    #region "根据特殊情况进行判断"
                                                    string strSelectMLFB = strCheckMLFB.ToUpper().Substring(0, 4);
                                                    decimal decMLFBPercentage = 1;
                                                    switch (strSelectMLFB)
                                                    {
                                                        case "1LA8":
                                                        case "1PQ8":
                                                        case "1LL8":
                                                            decMLFBPercentage = decimal.Parse("1.525");
                                                            break;
                                                        case "1LA4":
                                                        case "1MS4":
                                                        case "1PQ4":
                                                            #region "1LA4"
                                                            switch (strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1))
                                                            {
                                                                case "M":
                                                                    decMLFBPercentage = decimal.Parse("3.8125");
                                                                    break;
                                                                case "N":
                                                                case "W":
                                                                    decMLFBPercentage = decimal.Parse("2.6");
                                                                    break;
                                                                default:
                                                                    //没找到产品信息，将此数据设置为无效
                                                                    itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                                    itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = strSelectMLFB + "找不到对应的TP规则！";
                                                                    itemclsRowData.IsValid = false;
                                                                    break;
                                                            }
                                                            #endregion
                                                            break;
                                                        case "1RA1"://这个地方改成以1R开头就行了：2013-10-28
                                                        case "1RQ1":
                                                        case "1RN1":
                                                            decMLFBPercentage = decimal.Parse("2.6");
                                                            break;
                                                        default:
                                                            //没找到产品信息，将此数据设置为无效
                                                            itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                            itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "找不到对应的TP规则！";
                                                            itemclsRowData.IsValid = false;
                                                            //给个默认值，防止下面计算出错
                                                            decMLFBPercentage = decimal.Parse("1");
                                                            break;
                                                    }
                                                    #endregion
                                                    decMLFBPercentage = decMLFBPercentage / decimal.Parse("1.17");
                                                    #region "得到选件的成本价"
                                                    //itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * decMLFBPercentage * itemclsRowData.TPFactor;//得到各个配件在数据库中的价格
                                                    itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * decMLFBPercentage;//得到各个配件在数据库中的价格
                                                    itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                                    itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + (itemclsRowData.lstProduct[intOptionIndex].ItemTP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                                                    #endregion
                                                }
                                                else
                                                {
                                                    itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                    //将此数据设置为无效，继续循环
                                                    itemclsRowData.IsValid = false;
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                //没找到选件信息，将此数据设置为无效
                                                itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                if (itemclsRowData.lstProduct[intOptionIndex].ProductText.Length > 0)
                                                {
                                                    strFirstChar = itemclsRowData.lstProduct[intOptionIndex].ProductText.Substring(0, 1).ToUpper();
                                                }
                                                if (strFirstChar == "J" || strFirstChar == "X")
                                                {
                                                    itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "此选件请单独询价！";
                                                }
                                                else
                                                {
                                                    itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "没找到对应的选件LP！";
                                                }
                                                itemclsRowData.IsValid = false;
                                            }
                                            #endregion
                                        }
                                        #endregion
                                    }
                                    #endregion

                                }
                            }
                            else
                            {
                                itemclsRowData.lstProduct[0].IsRight = true;
                                itemclsRowData.IsValid = true;//只有电机一个，所以肯定是正确的了
                            }
                            #endregion
                        }
                        else
                        {
                            if (itemclsRowData.lstProduct[0].IsRight == true)
                            {
                                //没找到产品信息，将此数据设置为无效，继续循环
                                itemclsRowData.lstProduct[0].IsRight = false;
                                itemclsRowData.lstProduct[0].ErrorInfo = "找不到MLFB号的LP！";
                                itemclsRowData.IsValid = false;
                            }
                            else
                            {
                                if (itemclsRowData.lstProduct[0].ErrorInfo == null || itemclsRowData.lstProduct[0].ErrorInfo == "")
                                {
                                    //没找到产品信息，将此数据设置为无效，继续循环
                                    itemclsRowData.lstProduct[0].IsRight = false;
                                    itemclsRowData.lstProduct[0].ErrorInfo = "找不到MLFB号的LP！";
                                    itemclsRowData.IsValid = false;
                                }
                            }
                        }
                        #endregion
                        break;
                    case "8":
                        #region "倒数第二位是8"
                        #region "根据特殊情况进行判断"
                        switch (strSelectChar)
                        {
                            case "1LA8":
                            case "1PQ8":
                            case "1LL8":
                                #region "1LA8/1PQ8/1LL8"
                                strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strCheckMLFB.funString_StringStuff(strCheckMLFB.Length - 2, 1, "8", false) + "')) ORDER BY MLFB";
                                strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                //if (strCheckMLFB.Length == 0)
                                //{
                                //    itemclsRowData.lstProduct[0].IsRight = false;
                                //    itemclsRowData.lstProduct[0].ErrorInfo = "找不到对应的MLFB号！";
                                //    itemclsRowData.IsValid = false;
                                //}
                                #endregion
                                break;
                            case "1LA4":
                            case "1MS4":
                            case "1PQ4":
                                #region "1LA4"
                                string strSPRMLFB = strCheckMLFB;
                                switch (strSPRMLFB.Substring(strSPRMLFB.Length - 3, 1).ToUpper())
                                {
                                    case "M":
                                        strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 2, 1, "8", false) + "')) ORDER BY MLFB";
                                        strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                        break;
                                    case "N":
                                        if (strSelectChar == "1PQ4")
                                        {
                                            strSPRMLFB = strCheckMLFB.funString_StringStuff(0, 4, "1LA4", false);
                                        }
                                        strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 2, 1, "8", false) + "')) ORDER BY MLFB";
                                        strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                        break;
                                    case "W":
                                        if (strSelectChar == "1PQ4")
                                        {
                                            strSPRMLFB = strCheckMLFB.funString_StringStuff(0, 4, "1LA4", false);
                                        }
                                        strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 3, 1, "N", false).funString_StringStuff(strSPRMLFB.Length - 2, 1, "8", false) + "')) ORDER BY MLFB";
                                        strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                        break;
                                    default:
                                        //没找到产品信息，将此数据设置为无效，继续循环
                                        itemclsRowData.lstProduct[0].IsRight = false;
                                        itemclsRowData.lstProduct[0].ErrorInfo = "找不到对应的MLFB号！";
                                        itemclsRowData.IsValid = false;
                                        break;
                                }
                                #endregion
                                break;
                            case "1RA1":
                            case "1RQ1":
                            case "1RN1":
                                #region "1RA1/1RQ1/1RN1"
                                strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strCheckMLFB.funString_StringStuff(strCheckMLFB.Length - 2, 1, "8", false) + "')) ORDER BY MLFB";
                                strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                //if (strCheckMLFB.Length == 0)
                                //{
                                //    itemclsRowData.lstProduct[0].IsRight = false;
                                //    itemclsRowData.lstProduct[0].ErrorInfo = "找不到对应的MLFB号！";
                                //    itemclsRowData.IsValid = false;
                                //}
                                #endregion
                                break;
                            default:
                                //没找到产品信息，将此数据设置为无效，继续循环
                                itemclsRowData.lstProduct[0].IsRight = false;
                                itemclsRowData.lstProduct[0].ErrorInfo = "找不到对应的MLFB号！";
                                itemclsRowData.IsValid = false;
                                break;
                        }
                        #endregion

                        //strCheckMLFB = itemclsRowData.SPRMLFB;
                        strSQL = "SELECT CurrentPrice FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB = '" + strCheckMLFB + "')";
                        strCountProduct = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                        if (strCountProduct.Length > 0)
                        {
                            #region "MLFB的表价和成本价"
                            //得到表价
                            //itemclsRowData.lstProduct[0].LPFactor = itemclsRowData.LPFactor;//LP折扣
                            CurrentPrice = strCountProduct.funDec_StringToDecimal(0);
                            //if (strSelectChar == "1PQ4" && (strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "N" || strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "W"))
                            //{
                            //    CurrentPrice = CurrentPrice + decimal.Parse("24364.01");// * decimal.Parse("1.1")
                            //}
                            itemclsRowData.lstProduct[0].ItemLP = CurrentPrice;//LP
                            itemclsRowData.TotalLP = CurrentPrice * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的表价
                            itemclsRowData.TotalDisLP = (CurrentPrice * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                            itemclsRowData.lstProduct[0].IsRight = true;

                            //得到成本价
                            CurrentPrice = objClassDbAccess.funString_SQLExecuteScalar("SELECT Factor FROM CHub_Info_TPFactor where MLFB='" + strCheckMLFB + "'").funDec_StringToDecimal(1);
                            //itemclsRowData.lstProduct[0].TPFactor = CurrentPrice;
                            itemclsRowData.TPFactor = CurrentPrice;
                            CurrentPrice = itemclsRowData.lstProduct[0].ItemLP * CurrentPrice;//TP=LP*Factor
                            if (strSelectChar == "1PQ4" && (strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "N" || strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "W"))
                            {
                                CurrentPrice = CurrentPrice + decimal.Parse("24364.01");// * decimal.Parse("1.1")
                            }
                            itemclsRowData.lstProduct[0].ItemTP = CurrentPrice;
                            itemclsRowData.TotalTP = CurrentPrice * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的成本价
                            itemclsRowData.TotalDisTP = (CurrentPrice * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                            #endregion

                            #region "继续计算配件的价格"
                            int intInputOptionCount = itemclsRowData.lstProduct.Count;
                            if (intInputOptionCount > 1)
                            {
                                for (int intOptionIndex = 1; intOptionIndex < intInputOptionCount; intOptionIndex++)
                                {
                                    //得到选件名称
                                    string strTempOptionValue = itemclsRowData.lstProduct[intOptionIndex].ProductText;
                                    #region "得到选件的表价、成本价，并判断选件是否合格"
                                    string strMLFBPercentage = objClassDbAccess.funString_SQLExecuteScalar("SELECT MLFBPercentage FROM CHub_Info_LPSpecialOptions WHERE OptionValue='" + strTempOptionValue.funString_SQLToString() + "'");
                                    bool blnIsSpecialOption = false;
                                    if (strMLFBPercentage.Trim().Length > 0)
                                    {
                                        blnIsSpecialOption = true;
                                        if (strTempOptionValue.ToUpper() == "L27")
                                        {
                                            if (strCheckMLFB.Substring(0, 4).ToUpper() != "1LA8" && strCheckMLFB.Substring(0, 4).ToUpper() != "1PQ8")
                                            {
                                                blnIsSpecialOption = false;
                                            }
                                        }
                                    }
                                    if (blnIsSpecialOption)
                                    {
                                        #region "是特殊选件，直接用MLFB的价格乘以百分比"
                                        decimal decMLFBPercentage = decimal.Parse(strMLFBPercentage);
                                        #region "得到选件的表价"
                                        itemclsRowData.lstProduct[intOptionIndex].ItemLP = 0;//itemclsRowData.lstProduct[0].ItemLP * decMLFBPercentage;
                                        //itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                        //itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                        itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                                        itemclsRowData.lstProduct[intOptionIndex].IsSpecialOption = true;
                                        #endregion
                                        #region "得到选件的成本价"
                                        itemclsRowData.lstProduct[intOptionIndex].ItemTP = 0;//itemclsRowData.lstProduct[0].ItemTP * decMLFBPercentage;
                                        //itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                        ////itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                                        #endregion
                                        #endregion
                                    }
                                    else
                                    {
                                        #region "不是特殊选件，那么从数据库查找"
                                        strSQL = "SELECT OptionValue, CurrentPrice FROM CHub_Info_LP WHERE (PriceType = 'Options') AND (MLFB = '" + strCheckMLFB + "') AND (OptionValue = '" + strTempOptionValue + "')";
                                        ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
                                        if (ds.Tables[0].Rows.Count > 0)
                                        {
                                            #region "得到选件的表价"
                                            itemclsRowData.lstProduct[intOptionIndex].ItemLP = ds.Tables[0].Rows[0]["CurrentPrice"].ToString().funDec_StringToDecimal(0);//得到各个配件在数据库中的价格
                                            itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                            itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                            itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                                            #endregion
                                            #region "得到选件的成本价"
                                            itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.TPFactor;//得到各个配件在数据库中的价格
                                            itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                            itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + (itemclsRowData.lstProduct[intOptionIndex].ItemTP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                                            #endregion
                                        }
                                        else
                                        {
                                            #region "数据库没有"
                                            if (IsSPRData)
                                            {
                                                #region "数据库没有，那么用SPR中用户自己填写的价格"
                                                string strSPROptionPrice = objClassDbAccess.funString_SQLExecuteScalar("SELECT CurrentPrice FROM CHub_Info_SPROptions WHERE (SPRID = '" + SPRMLFBID + "') AND (IsDel = 0) AND (OptionValue='" + strTempOptionValue.funString_SQLToString() + "')");
                                                if (strSPROptionPrice.Trim().Length > 0)
                                                {
                                                    #region "得到选件的表价"
                                                    itemclsRowData.lstProduct[intOptionIndex].ItemLP = strSPROptionPrice.funDec_StringToDecimal(0);//得到各个配件在数据库中的价格
                                                    itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                                    itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                                    itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                                                    #endregion

                                                    #region "根据特殊情况进行判断"
                                                    string strSelectMLFB = strCheckMLFB.ToUpper().Substring(0, 4);
                                                    decimal decMLFBPercentage = 1;
                                                    switch (strSelectMLFB)
                                                    {
                                                        case "1LA8":
                                                        case "1PQ8":
                                                        case "1LL8":
                                                            decMLFBPercentage = decimal.Parse("1.525");
                                                            break;
                                                        case "1LA4":
                                                        case "1MS4":
                                                        case "1PQ4":
                                                            #region "1LA4"
                                                            switch (strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1))
                                                            {
                                                                case "M":
                                                                    decMLFBPercentage = decimal.Parse("3.8125");
                                                                    break;
                                                                case "N":
                                                                case "W":
                                                                    decMLFBPercentage = decimal.Parse("2.6");
                                                                    break;
                                                                default:
                                                                    //没找到产品信息，将此数据设置为无效
                                                                    itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                                    itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = strSelectMLFB + "找不到对应的TP规则！";
                                                                    itemclsRowData.IsValid = false;
                                                                    break;
                                                            }
                                                            #endregion
                                                            break;
                                                        case "1RA1"://这个地方改成以1R开头就行了：2013-10-28
                                                        case "1RQ1":
                                                        case "1RN1":
                                                            decMLFBPercentage = decimal.Parse("2.6");
                                                            break;
                                                        default:
                                                            //没找到产品信息，将此数据设置为无效
                                                            itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                            itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "找不到对应的TP规则！";
                                                            itemclsRowData.IsValid = false;
                                                            //给个默认值，防止下面计算出错
                                                            decMLFBPercentage = decimal.Parse("1");
                                                            break;
                                                    }
                                                    #endregion
                                                    decMLFBPercentage = decMLFBPercentage / decimal.Parse("1.17");
                                                    #region "得到选件的成本价"
                                                    //itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * decMLFBPercentage * itemclsRowData.TPFactor;//得到各个配件在数据库中的价格
                                                    itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * decMLFBPercentage;//得到各个配件在数据库中的价格
                                                    itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                                    itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + (itemclsRowData.lstProduct[intOptionIndex].ItemTP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                                                    #endregion
                                                }
                                                else
                                                {
                                                    itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                    //将此数据设置为无效，继续循环
                                                    itemclsRowData.IsValid = false;
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                //没找到选件信息，将此数据设置为无效
                                                itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                if (itemclsRowData.lstProduct[intOptionIndex].ProductText.Length > 0)
                                                {
                                                    strFirstChar = itemclsRowData.lstProduct[intOptionIndex].ProductText.Substring(0, 1).ToUpper();
                                                }
                                                if (strFirstChar == "J" || strFirstChar == "X")
                                                {
                                                    itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "此选件请单独询价！";
                                                }
                                                else
                                                {
                                                    itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "没找到对应的选件LP！";
                                                }
                                                itemclsRowData.IsValid = false;
                                            }
                                            #endregion
                                        }
                                        #endregion
                                    }
                                    #endregion

                                }
                            }
                            else
                            {
                                itemclsRowData.lstProduct[0].IsRight = true;
                                itemclsRowData.IsValid = true;//只有电机一个，所以肯定是正确的了
                            }
                            #endregion
                        }
                        else
                        {
                            if (itemclsRowData.lstProduct[0].IsRight == true)
                            {
                                //没找到产品信息，将此数据设置为无效，继续循环
                                itemclsRowData.lstProduct[0].IsRight = false;
                                itemclsRowData.lstProduct[0].ErrorInfo = "找不到MLFB号的LP！";
                                itemclsRowData.IsValid = false;
                            }
                            else
                            {
                                if (itemclsRowData.lstProduct[0].ErrorInfo == null || itemclsRowData.lstProduct[0].ErrorInfo == "")
                                {
                                    //没找到产品信息，将此数据设置为无效，继续循环
                                    itemclsRowData.lstProduct[0].IsRight = false;
                                    itemclsRowData.lstProduct[0].ErrorInfo = "找不到MLFB号的LP！";
                                    itemclsRowData.IsValid = false;
                                }
                            }
                        }
                        #endregion
                        break;
                    case "6":
                        #region "倒数第二位是6"
                        #region "根据特殊情况进行判断"
                        switch (strSelectChar)
                        {
                            case "1LA8":
                            case "1PQ8":
                            case "1LL8":
                                #region "1LA8/1PQ8/1LL8"
                                strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strCheckMLFB.funString_StringStuff(strCheckMLFB.Length - 2, 1, "0", false) + "')) ORDER BY MLFB";
                                strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                #endregion
                                break;
                            case "1LA4":
                            case "1MS4":
                            case "1PQ4":
                                #region "1LA4"
                                string strSPRMLFB = strCheckMLFB;
                                switch (strSPRMLFB.Substring(strSPRMLFB.Length - 3, 1).ToUpper())
                                {
                                    case "M":
                                        strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 2, 1, "0", false) + "')) ORDER BY MLFB";
                                        strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                        break;
                                    case "N":
                                        if (strSelectChar == "1PQ4")
                                        {
                                            strSPRMLFB = strCheckMLFB.funString_StringStuff(0, 4, "1LA4", false);
                                        }
                                        strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 2, 1, "0", false) + "')) ORDER BY MLFB";
                                        strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                        break;
                                    case "W":
                                        if (strSelectChar == "1PQ4")
                                        {
                                            strSPRMLFB = strCheckMLFB.funString_StringStuff(0, 4, "1LA4", false);
                                        }
                                        strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strSPRMLFB.funString_StringStuff(strSPRMLFB.Length - 3, 1, "N", false).funString_StringStuff(strSPRMLFB.Length - 2, 1, "6", false) + "')) ORDER BY MLFB";
                                        strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                        break;
                                    default:
                                        //没找到产品信息，将此数据设置为无效，继续循环
                                        itemclsRowData.lstProduct[0].IsRight = false;
                                        itemclsRowData.lstProduct[0].ErrorInfo = "找不到对应的MLFB号！";
                                        itemclsRowData.IsValid = false;
                                        break;
                                }
                                #endregion
                                break;
                            case "1RA1":
                            case "1RQ1":
                            case "1RN1":
                                #region "1RA1/1RQ1/1RN1"
                                strSQL = "SELECT MLFB FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB IN ('" + strCheckMLFB.funString_StringStuff(strCheckMLFB.Length - 2, 1, "0", false) + "')) ORDER BY MLFB";
                                strCheckMLFB = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                                #endregion
                                break;
                            default:
                                //没找到产品信息，将此数据设置为无效，继续循环
                                itemclsRowData.lstProduct[0].IsRight = false;
                                itemclsRowData.lstProduct[0].ErrorInfo = "找不到对应的MLFB号！";
                                itemclsRowData.IsValid = false;
                                break;
                        }
                        #endregion

                        //strCheckMLFB = itemclsRowData.SPRMLFB;
                        strSQL = "SELECT CurrentPrice FROM CHub_Info_LP WHERE (PriceType = 'MLFB') AND (MLFB = '" + strCheckMLFB + "')";
                        strCountProduct = objClassDbAccess.funString_SQLExecuteScalar(strSQL);
                        if (strCountProduct.Length > 0)
                        {
                            #region "MLFB的表价和成本价"
                            //得到表价
                            //itemclsRowData.lstProduct[0].LPFactor = itemclsRowData.LPFactor;//LP折扣
                            CurrentPrice = strCountProduct.funDec_StringToDecimal(0);
                            //if (strSelectChar == "1PQ4" && (strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "N" || strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "W"))
                            //{
                            //    CurrentPrice = CurrentPrice + decimal.Parse("24364.01");// * decimal.Parse("1.1")
                            //}
                            itemclsRowData.lstProduct[0].ItemLP = CurrentPrice;//LP
                            itemclsRowData.TotalLP = CurrentPrice * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的表价
                            itemclsRowData.TotalDisLP = (CurrentPrice * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                            itemclsRowData.lstProduct[0].IsRight = true;

                            //得到成本价
                            CurrentPrice = objClassDbAccess.funString_SQLExecuteScalar("SELECT Factor FROM CHub_Info_TPFactor where MLFB='" + strCheckMLFB + "'").funDec_StringToDecimal(1);
                            //itemclsRowData.lstProduct[0].TPFactor = CurrentPrice;
                            itemclsRowData.TPFactor = CurrentPrice;
                            CurrentPrice = itemclsRowData.lstProduct[0].ItemLP * CurrentPrice;//TP=LP*Factor
                            if (strSelectChar == "1PQ4" && (strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "N" || strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1) == "W"))
                            {
                                CurrentPrice = CurrentPrice + decimal.Parse("24364.01");// * decimal.Parse("1.1")
                            }
                            itemclsRowData.lstProduct[0].ItemTP = CurrentPrice;
                            itemclsRowData.TotalTP = CurrentPrice * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的成本价
                            itemclsRowData.TotalDisTP = (CurrentPrice * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[0].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                            #endregion

                            #region "继续计算配件的价格"
                            int intInputOptionCount = itemclsRowData.lstProduct.Count;
                            if (intInputOptionCount > 1)
                            {
                                for (int intOptionIndex = 1; intOptionIndex < intInputOptionCount; intOptionIndex++)
                                {
                                    //得到选件名称
                                    string strTempOptionValue = itemclsRowData.lstProduct[intOptionIndex].ProductText;
                                    #region "得到选件的表价、成本价，并判断选件是否合格"
                                    string strMLFBPercentage = objClassDbAccess.funString_SQLExecuteScalar("SELECT MLFBPercentage FROM CHub_Info_LPSpecialOptions WHERE OptionValue='" + strTempOptionValue.funString_SQLToString() + "'");
                                    bool blnIsSpecialOption = false;
                                    if (strMLFBPercentage.Trim().Length > 0)
                                    {
                                        blnIsSpecialOption = true;
                                        if (strTempOptionValue.ToUpper() == "L27")
                                        {
                                            if (strCheckMLFB.Substring(0, 4).ToUpper() != "1LA8" && strCheckMLFB.Substring(0, 4).ToUpper() != "1PQ8")
                                            {
                                                blnIsSpecialOption = false;
                                            }
                                        }
                                    }
                                    if (blnIsSpecialOption)
                                    {
                                        #region "是特殊选件，直接用MLFB的价格乘以百分比"
                                        decimal decMLFBPercentage = decimal.Parse(strMLFBPercentage);
                                        #region "得到选件的表价"
                                        itemclsRowData.lstProduct[intOptionIndex].ItemLP = 0;//itemclsRowData.lstProduct[0].ItemLP * decMLFBPercentage;
                                        //itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                        //itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                        itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                                        itemclsRowData.lstProduct[intOptionIndex].IsSpecialOption = true;
                                        #endregion
                                        #region "得到选件的成本价"
                                        itemclsRowData.lstProduct[intOptionIndex].ItemTP = 0;//itemclsRowData.lstProduct[0].ItemTP * decMLFBPercentage;
                                        //itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                        ////itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                                        #endregion
                                        #endregion
                                    }
                                    else
                                    {
                                        #region "不是特殊选件，那么从数据库查找"
                                        strSQL = "SELECT OptionValue, CurrentPrice FROM CHub_Info_LP WHERE (PriceType = 'Options') AND (MLFB = '" + strCheckMLFB + "') AND (OptionValue = '" + strTempOptionValue + "')";
                                        ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
                                        if (ds.Tables[0].Rows.Count > 0)
                                        {
                                            #region "得到选件的表价"
                                            itemclsRowData.lstProduct[intOptionIndex].ItemLP = ds.Tables[0].Rows[0]["CurrentPrice"].ToString().funDec_StringToDecimal(0);//得到各个配件在数据库中的价格
                                            itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                            itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                            itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                                            #endregion
                                            #region "得到选件的成本价"
                                            itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.TPFactor;//得到各个配件在数据库中的价格
                                            itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                            itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + (itemclsRowData.lstProduct[intOptionIndex].ItemTP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                                            #endregion
                                        }
                                        else
                                        {
                                            #region "数据库没有"
                                            if (IsSPRData)
                                            {
                                                #region "数据库没有，那么用SPR中用户自己填写的价格"
                                                string strSPROptionPrice = objClassDbAccess.funString_SQLExecuteScalar("SELECT CurrentPrice FROM CHub_Info_SPROptions WHERE (SPRID = '" + SPRMLFBID + "') AND (IsDel = 0) AND (OptionValue='" + strTempOptionValue.funString_SQLToString() + "')");
                                                if (strSPROptionPrice.Trim().Length > 0)
                                                {
                                                    #region "得到选件的表价"
                                                    itemclsRowData.lstProduct[intOptionIndex].ItemLP = strSPROptionPrice.funDec_StringToDecimal(0);//得到各个配件在数据库中的价格
                                                    itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                                    itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                                                    itemclsRowData.lstProduct[intOptionIndex].IsRight = true;
                                                    #endregion

                                                    #region "根据特殊情况进行判断"
                                                    string strSelectMLFB = strCheckMLFB.ToUpper().Substring(0, 4);
                                                    decimal decMLFBPercentage = 1;
                                                    switch (strSelectMLFB)
                                                    {
                                                        case "1LA8":
                                                        case "1PQ8":
                                                        case "1LL8":
                                                            decMLFBPercentage = decimal.Parse("1.525");
                                                            break;
                                                        case "1LA4":
                                                        case "1MS4":
                                                        case "1PQ4":
                                                            #region "1LA4"
                                                            switch (strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1))
                                                            {
                                                                case "M":
                                                                    decMLFBPercentage = decimal.Parse("3.8125");
                                                                    break;
                                                                case "N":
                                                                case "W":
                                                                    decMLFBPercentage = decimal.Parse("2.6");
                                                                    break;
                                                                default:
                                                                    //没找到产品信息，将此数据设置为无效
                                                                    itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                                    itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = strSelectMLFB + "找不到对应的TP规则！";
                                                                    itemclsRowData.IsValid = false;
                                                                    break;
                                                            }
                                                            #endregion
                                                            break;
                                                        case "1RA1"://这个地方改成以1R开头就行了：2013-10-28
                                                        case "1RQ1":
                                                        case "1RN1":
                                                            decMLFBPercentage = decimal.Parse("2.6");
                                                            break;
                                                        default:
                                                            //没找到产品信息，将此数据设置为无效
                                                            itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                            itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "找不到对应的TP规则！";
                                                            itemclsRowData.IsValid = false;
                                                            //给个默认值，防止下面计算出错
                                                            decMLFBPercentage = decimal.Parse("1");
                                                            break;
                                                    }
                                                    #endregion
                                                    decMLFBPercentage = decMLFBPercentage / decimal.Parse("1.17");
                                                    #region "得到选件的成本价"
                                                    //itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * decMLFBPercentage * itemclsRowData.TPFactor;//得到各个配件在数据库中的价格
                                                    itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.lstProduct[intOptionIndex].ItemLP * decMLFBPercentage;//得到各个配件在数据库中的价格
                                                    itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                                                    itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + (itemclsRowData.lstProduct[intOptionIndex].ItemTP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                                                    #endregion
                                                }
                                                else
                                                {
                                                    itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                    //将此数据设置为无效，继续循环
                                                    itemclsRowData.IsValid = false;
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                //没找到选件信息，将此数据设置为无效
                                                itemclsRowData.lstProduct[intOptionIndex].IsRight = false;
                                                if (itemclsRowData.lstProduct[intOptionIndex].ProductText.Length > 0)
                                                {
                                                    strFirstChar = itemclsRowData.lstProduct[intOptionIndex].ProductText.Substring(0, 1).ToUpper();
                                                }
                                                if (strFirstChar == "J" || strFirstChar == "X")
                                                {
                                                    itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "此选件请单独询价！";
                                                }
                                                else
                                                {
                                                    itemclsRowData.lstProduct[intOptionIndex].ErrorInfo = "没找到对应的选件LP！";
                                                }
                                                itemclsRowData.IsValid = false;
                                            }
                                            #endregion
                                        }
                                        #endregion
                                    }
                                    #endregion

                                }
                            }
                            else
                            {
                                itemclsRowData.lstProduct[0].IsRight = true;
                                itemclsRowData.IsValid = true;//只有电机一个，所以肯定是正确的了
                            }
                            #endregion
                        }
                        else
                        {
                            if (itemclsRowData.lstProduct[0].IsRight == true)
                            {
                                //没找到产品信息，将此数据设置为无效，继续循环
                                itemclsRowData.lstProduct[0].IsRight = false;
                                itemclsRowData.lstProduct[0].ErrorInfo = "找不到MLFB号的LP！";
                                itemclsRowData.IsValid = false;
                            }
                            else
                            {
                                if (itemclsRowData.lstProduct[0].ErrorInfo == null || itemclsRowData.lstProduct[0].ErrorInfo == "")
                                {
                                    //没找到产品信息，将此数据设置为无效，继续循环
                                    itemclsRowData.lstProduct[0].IsRight = false;
                                    itemclsRowData.lstProduct[0].ErrorInfo = "找不到MLFB号的LP！";
                                    itemclsRowData.IsValid = false;
                                }
                            }
                        }
                        #endregion
                        break;
                    default:
                        //没找到产品信息，将此数据设置为无效，继续循环
                        itemclsRowData.lstProduct[0].IsRight = false;
                        itemclsRowData.lstProduct[0].ErrorInfo = "错误的MLFB号！";
                        itemclsRowData.IsValid = false;
                        break;
                }
                #endregion
            }
            #endregion

            #region "设计费"
            if (IsSPRData)
            {
                #region "根据特殊情况进行判断"
                string strSelectMLFB = strCheckMLFB.ToUpper().Substring(0, 4);
                decimal decMLFBPercentage = 1;
                switch (strSelectMLFB)
                {
                    case "1LA8":
                    case "1PQ8":
                    case "1LL8":
                        decMLFBPercentage = decimal.Parse("1.525");
                        break;
                    case "1LA4":
                    case "1MS4":
                    case "1PQ4":
                        #region "1LA4"
                        switch (strCheckMLFB.Substring(strCheckMLFB.Length - 3, 1))
                        {
                            case "M":
                                decMLFBPercentage = decimal.Parse("3.8125");
                                break;
                            case "N":
                            case "W":
                                decMLFBPercentage = decimal.Parse("2.6");
                                break;
                            default:
                                //没找到产品信息，将此数据设置为无效
                                itemclsRowData.lstProduct[0].IsRight = false;
                                itemclsRowData.lstProduct[0].ErrorInfo = strSelectMLFB + "找不到对应的TP规则！";
                                itemclsRowData.IsValid = false;
                                break;
                        }
                        #endregion
                        break;
                    case "1RA1"://这个地方改成以1R开头就行了：2013-10-28
                    case "1RQ1":
                    case "1RN1":
                        decMLFBPercentage = decimal.Parse("2.6");
                        break;
                    default:
                        //没找到产品信息，将此数据设置为无效
                        itemclsRowData.lstProduct[0].IsRight = false;
                        itemclsRowData.lstProduct[0].ErrorInfo = "找不到对应的TP规则！";
                        itemclsRowData.IsValid = false;
                        //给个默认值，防止下面计算出错
                        decMLFBPercentage = decimal.Parse("1");
                        break;
                }
                #endregion
                decMLFBPercentage = decMLFBPercentage / decimal.Parse("1.17");
                #region "得到选件的成本价"
                itemclsRowData.SPROthersPerItem = itemclsRowData.SPROthersPerItem * decMLFBPercentage;
                itemclsRowData.SPROthersPerUnit = itemclsRowData.SPROthersPerUnit * decMLFBPercentage * decimal.Parse(itemclsRowData.QTY.ToString());
                #endregion
            }
            #endregion
            #endregion
            #endregion
            #region "如果是特殊选件，那么价格是全部价格(MLFB+Options)乘以百分比"
            int intOptionsCount = itemclsRowData.lstProduct.Count;
            if (intOptionsCount > 1)
            {
                for (int intOptionIndex = 1; intOptionIndex < intOptionsCount; intOptionIndex++)
                {
                    if (itemclsRowData.lstProduct[intOptionIndex].IsSpecialOption == true)
                    {
                        #region "是特殊选件，直接用全部价格(MLFB+Options)乘以百分比"
                        decimal decMLFBPercentage = itemclsRowData.lstProduct[intOptionIndex].SpecialOptionPercentage;
                        #region "得到选件的表价"
                        itemclsRowData.lstProduct[intOptionIndex].ItemLP = itemclsRowData.TotalLP * decMLFBPercentage;
                        itemclsRowData.TotalLP = itemclsRowData.TotalLP + itemclsRowData.lstProduct[intOptionIndex].ItemLP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                        itemclsRowData.TotalDisLP = itemclsRowData.TotalDisLP + (itemclsRowData.lstProduct[intOptionIndex].ItemLP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的表价(折扣后)
                        #endregion
                        #region "得到选件的成本价"
                        itemclsRowData.lstProduct[intOptionIndex].ItemTP = itemclsRowData.TotalTP * decMLFBPercentage;
                        itemclsRowData.TotalTP = itemclsRowData.TotalTP + itemclsRowData.lstProduct[intOptionIndex].ItemTP * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//得到总价格
                        itemclsRowData.TotalDisTP = itemclsRowData.TotalDisTP + (itemclsRowData.lstProduct[intOptionIndex].ItemTP * itemclsRowData.LPFactor) * decimal.Parse(itemclsRowData.lstProduct[intOptionIndex].ProductCount.ToString());//全部MLFB和选件的成本价(折扣后)
                        #endregion
                        #endregion
                    }
                }
            }
            #endregion

            //lstclsRowData.Add(itemclsRowData);//将行的数据保存到list中
            #endregion

            return itemclsRowData;
        }

        /// <summary>
        /// 显示选择的结果列表
        /// </summary>
        private void subDisplay_ListTable()
        {
            //为了保存值，将类保存到了Session中
            List<ClassLibrary.ProductRowData> lstclsRowData = new List<CHub.ClassLibrary.ProductRowData>();
            if (Session["ProductList"] == null)
            {
                Session["ProductList"] = new List<ClassLibrary.ProductRowData>();
            }
            lstclsRowData = (List<ClassLibrary.ProductRowData>)Session["ProductList"];

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

            //显示MLFB
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "MLFB-Z=<br>Options";//"产品名称";
            objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
            objCell.Attributes.Add("nowrap", "nowrap");
            objCell.Align = "left";
            objCell.Width = "50%";
            objRow.Cells.Add(objCell);
            //显示SPR
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "SPR";//"SPR";
            objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
            objCell.Attributes.Add("nowrap", "nowrap");
            objCell.Align = "left";
            objCell.Width = "20%";
            objRow.Cells.Add(objCell);

            //显示数量
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "Quantity";// "台数";
            objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
            objCell.Attributes.Add("nowrap", "nowrap");
            objCell.Width = "15%";
            objRow.Cells.Add(objCell);

            if (objLoginUserInfo.funBln_Limited("3001", objLoginUserInfo.SystemLimited))
            {
                //价格
                objCell = new HtmlTableCell();
                objCell.InnerHtml = "LP <br>(w/o VAT)";//"客户价（不含税）";
                objCell.Align = "Center";
                objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
                objCell.Attributes.Add("nowrap", "nowrap");
                objRow.Cells.Add(objCell);
            }

            //折扣率
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "Discount <br>Factor";//"折扣率";
            objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
            objCell.Attributes.Add("nowrap", "nowrap");
            objCell.Width = "5%";
            objRow.Cells.Add(objCell);

            //价格
            objCell = new HtmlTableCell();
            objCell.InnerHtml = "K-Price <br>(w/o VAT)";//"客户价（不含税）";
            objCell.Align = "Center";
            objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
            objCell.Attributes.Add("nowrap", "nowrap");
            objRow.Cells.Add(objCell);

            //成本价
            if (objLoginUserInfo.funBln_Limited("3002", objLoginUserInfo.SystemLimited))
            {
                objCell = new HtmlTableCell();
                objCell.InnerHtml = "TP <br>(w/o VAT)";//成本价（不含税）
                objCell.Align = "Center";
                objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
                objCell.Attributes.Add("nowrap", "nowrap");
                objRow.Cells.Add(objCell);

                objCell = new HtmlTableCell();
                objCell.InnerHtml = "Gross<br>Margin";
                objCell.Align = "Center";
                objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
                objCell.Attributes.Add("nowrap", "nowrap");
                objRow.Cells.Add(objCell);
            }

            //objCell = new HtmlTableCell();
            //objCell.InnerHtml = "Others<br>Per Unit";
            //objCell.Align = "Center";
            //objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
            //objCell.Attributes.Add("nowrap", "nowrap");
            //objRow.Cells.Add(objCell);

            //objCell = new HtmlTableCell();
            //objCell.InnerHtml = "Others<br>Per Item";
            //objCell.Align = "Center";
            //objCell.Attributes.Add("style", "padding:4px; border-top:1px solid #CCCCCC; border-bottom:1px solid #CCCCCC; background-color:#EEEEEE;");
            //objCell.Attributes.Add("nowrap", "nowrap");
            //objRow.Cells.Add(objCell);

            objTable.Rows.Add(objRow);
            #endregion

            #region "得到GrossMargin，在下面的循环产品中使用"
            string GrossMargin = "";
            string strSQL = "SELECT QuotationFunction FROM CHub_Management_Function WHERE (IsDel = 0) AND (QuotationText = '@QuotationFunction1@')";
            GrossMargin = objClassDbAccess.funString_SQLExecuteScalar(strSQL).ToLower();
            #endregion
            #region "得到KPDiscount，在下面的循环产品中使用"
            string KPDiscount = "";
            strSQL = "SELECT QuotationFunction FROM CHub_Management_Function WHERE (IsDel = 0) AND (QuotationText = '@KPDiscount@')";
            KPDiscount = objClassDbAccess.funString_SQLExecuteScalar(strSQL).ToLower();
            #endregion

            //decimal TotalPrice = 0;
            decimal TotalLP = 0;
            decimal TotalKP = 0;
            decimal TotalTP = 0;
            decimal TotalSPROthersPerUnit = 0;
            //decimal TotalFAGR = 0;
            decimal TotalSPROthersPerItem = 0;

            decimal BonusRate = txtBonusRate.Text.funDec_StringToDecimal(0) / decimal.Parse("100");
            GrossMargin = GrossMargin.Replace("bonus", BonusRate.ToString());
            for (int i = 0; i < lstclsRowData.Count; i++)
            {
                #region "循环画产品信息"
                #region "MLFB行"
                objRow = new HtmlTableRow();

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

                //显示MLFB+Options
                objCell = new HtmlTableCell();
                //objCell.InnerHtml = lstclsRowData[i].lstProduct[0].ProductText + "(" + (lstclsRowData[i].lstProduct.Count - 1).ToString() + "种选件)";
                string strInnerHTML = "";
                strInnerHTML = lstclsRowData[i].lstProduct[0].ProductText;
                //如果MLFB有错误，那么红色显示
                if (lstclsRowData[i].lstProduct[0].IsRight == false)
                {
                    strInnerHTML = "<span class='FontRed' title='" + lstclsRowData[i].lstProduct[0].ErrorInfo + "'>" + strInnerHTML + "</span>";
                }
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
                        catch (Exception ex)
                        {
                        }
                        for (int y = 0; y < (lstclsRowData[i].lstProduct[x].ProductCount / lstclsRowData[i].QTY); y++)
                        {
                            if (lstclsRowData[i].lstProduct[x].IsRight)
                            {
                                strInnerHTML = strInnerHTML + "+" + lstclsRowData[i].lstProduct[x].ProductText;
                            }
                            else
                            {
                                strInnerHTML = strInnerHTML + "+<span class='FontRed' title='" + lstclsRowData[i].lstProduct[x].ErrorInfo + "'>" + lstclsRowData[i].lstProduct[x].ProductText + "</span>";
                            }
                        }
                    }
                    #endregion
                    strInnerHTML = strInnerHTML.Replace("-Z<br>+", "-Z<br>");
                }
                objCell.InnerHtml = strInnerHTML;
                objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #EEEEEE;");
                //if (!lstclsRowData[i].IsValid)
                //{
                //    objCell.Attributes.Add("class", "FontRed");
                //}

                objRow.Cells.Add(objCell);

                //SPR
                objCell = new HtmlTableCell();
                objCell.InnerHtml = lstclsRowData[i].SPR.funString_ValidEmptyString("&nbsp;");
                objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #EEEEEE;");
                if (!lstclsRowData[i].IsValid)
                {
                    objCell.Attributes.Add("class", "FontRed");
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

                if (objLoginUserInfo.funBln_Limited("3001", objLoginUserInfo.SystemLimited))
                {
                    //LP
                    objCell = new HtmlTableCell();
                    //decimal RowTotalLP = lstclsRowData[i].TotalLP + lstclsRowData[i].SPROthersPerUnit + lstclsRowData[i].SPROthersPerItem;// lstclsRowData[i].TotalLP;//不用 / decimal.Parse("100");了，是因为粘贴的就是58%得到的数就是0.58
                    decimal RowTotalLP = lstclsRowData[i].TotalDisLP + lstclsRowData[i].SPROthersPerUnit + lstclsRowData[i].SPROthersPerItem;// lstclsRowData[i].TotalLP;//不用 / decimal.Parse("100");了，是因为粘贴的就是58%得到的数就是0.58
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
                }

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

                //KP
                objCell = new HtmlTableCell();
                string tmpKPDiscount = KPDiscount.ToLower();
                try
                {
                    //tmpKPDiscount = tmpKPDiscount.Replace("lp", lstclsRowData[i].TotalLP.ToString());
                    //tmpKPDiscount = tmpKPDiscount.Replace("tp", lstclsRowData[i].TotalTP.ToString());
                    tmpKPDiscount = tmpKPDiscount.Replace("lp", (lstclsRowData[i].TotalDisLP + lstclsRowData[i].SPROthersPerUnit + lstclsRowData[i].SPROthersPerItem).ToString());
                    tmpKPDiscount = tmpKPDiscount.Replace("tp", (lstclsRowData[i].TotalDisTP + lstclsRowData[i].SPROthersPerUnit + lstclsRowData[i].SPROthersPerItem).ToString());
                    tmpKPDiscount = ClassLibrary.CalculateExpression.Calculate(tmpKPDiscount).ToString();
                }
                catch
                {
                    tmpKPDiscount = "";
                }
                objCell.InnerHtml = tmpKPDiscount;
                decimal RowTotalKP = decimal.Parse(tmpKPDiscount);//lstclsRowData[i].TotalTP / decimal.Parse("0.8");//lstclsRowData[i].TotalLP * lstclsRowData[i].LPFactor;//不用 / decimal.Parse("100");了，是因为粘贴的就是58%得到的数就是0.58
                TotalKP = TotalKP + RowTotalKP;
                objCell.InnerHtml = RowTotalKP.ToString("n");
                objCell.Attributes.Add("nowrap", "nowrap");
                objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #EEEEEE;");
                if (!lstclsRowData[i].IsValid)
                {
                    objCell.Attributes.Add("class", "FontRed");
                }
                objCell.Align = "right";
                objRow.Cells.Add(objCell);

                //成本价
                if (objLoginUserInfo.funBln_Limited("3002", objLoginUserInfo.SystemLimited))
                {
                    objCell = new HtmlTableCell();
                    ////decimal RowTotalTP = lstclsRowData[i].TotalDisTP / decimal.Parse("1.17") + lstclsRowData[i].SPROthersPerUnit + lstclsRowData[i].SPROthersPerItem;// *lstclsRowData[i].Factor; ;// lstclsRowData[i].TotalTP;// *lstclsRowData[i].DiscountFactor;// / decimal.Parse("100");
                    //decimal RowTotalTP = lstclsRowData[i].TotalTP + lstclsRowData[i].SPROthersPerUnit + lstclsRowData[i].SPROthersPerItem;// *lstclsRowData[i].Factor; ;// lstclsRowData[i].TotalTP;// *lstclsRowData[i].DiscountFactor;// / decimal.Parse("100");
                    decimal RowTotalTP = lstclsRowData[i].TotalDisTP + lstclsRowData[i].SPROthersPerUnit + lstclsRowData[i].SPROthersPerItem;// *lstclsRowData[i].Factor; ;// lstclsRowData[i].TotalTP;// *lstclsRowData[i].DiscountFactor;// / decimal.Parse("100");
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

                    //GrossMargin
                    objCell = new HtmlTableCell();
                    string tmpGrossMargin = GrossMargin.ToLower();
                    try
                    {
                        tmpGrossMargin = tmpGrossMargin.Replace("kp", RowTotalKP.ToString());
                        tmpGrossMargin = tmpGrossMargin.Replace("tp", RowTotalTP.ToString());
                        //GrossMargin = GrossMargin.Replace("bonus", BonusRate.ToString());
                        tmpGrossMargin = ClassLibrary.CalculateExpression.Calculate(tmpGrossMargin).ToString();
                        tmpGrossMargin = (decimal.Parse(tmpGrossMargin) * decimal.Parse("100")).ToString("F2") + "%";
                    }
                    catch
                    {
                        tmpGrossMargin = "";
                    }
                    objCell.InnerHtml = tmpGrossMargin;
                    objCell.Attributes.Add("nowrap", "nowrap");
                    objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #EEEEEE;");
                    if (!lstclsRowData[i].IsValid)
                    {
                        objCell.Attributes.Add("class", "FontRed");
                    }
                    objCell.Align = "right";
                    objRow.Cells.Add(objCell);
                }

                ////SPROthersPerUnit
                //objCell = new HtmlTableCell();
                //decimal RowTotalSPROthersPerUnit = lstclsRowData[i].SPROthersPerUnit;// *lstclsRowData[i].DiscountFactor;// / decimal.Parse("100");
                //TotalSPROthersPerUnit = TotalSPROthersPerUnit + RowTotalSPROthersPerUnit;
                //objCell.InnerHtml = RowTotalSPROthersPerUnit.ToString();// RowTotalKG.ToString("n");
                //objCell.Attributes.Add("nowrap", "nowrap");
                //objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #EEEEEE;");
                //if (!lstclsRowData[i].IsValid)
                //{
                //    objCell.Attributes.Add("class", "FontRed");
                //}
                //objCell.Align = "right";
                //objRow.Cells.Add(objCell);
                ////SPROthersPerItem
                //objCell = new HtmlTableCell();
                //decimal RowTotalSPROthersPerItem = lstclsRowData[i].SPROthersPerItem;// *lstclsRowData[i].DiscountFactor;// / decimal.Parse("100");
                //TotalSPROthersPerItem = TotalSPROthersPerItem + RowTotalSPROthersPerItem;
                //objCell.InnerHtml = RowTotalSPROthersPerItem.ToString("F0");// RowTotalHS.ToString("n");
                //objCell.Attributes.Add("nowrap", "nowrap");
                //objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #EEEEEE;");
                //if (!lstclsRowData[i].IsValid)
                //{
                //    objCell.Attributes.Add("class", "FontRed");
                //}
                //objCell.Align = "right";
                //objRow.Cells.Add(objCell);                   

                objTable.Rows.Add(objRow);
                #endregion

                #endregion
            }

            #region "总计"
            objRow = new HtmlTableRow();

            objCell = new HtmlTableCell();
            objCell.ColSpan = 4;
            objCell.Attributes.Add("nowrap", "nowrap");
            objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
            objCell.Align = "right";
            objCell.InnerHtml = "<strong>Total:</strong>";// TotalPrice.ToString("n"); //lstclsRowData.Sum(q => q.TotalPrice); 
            objRow.Cells.Add(objCell);

            if (objLoginUserInfo.funBln_Limited("3001", objLoginUserInfo.SystemLimited))
            {
                objCell = new HtmlTableCell();
                objCell.Attributes.Add("nowrap", "nowrap");
                objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
                objCell.Align = "right";
                objCell.InnerHtml = TotalLP.ToString("n");
                objRow.Cells.Add(objCell);
            }

            objCell = new HtmlTableCell();
            objCell.Attributes.Add("nowrap", "nowrap");
            objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
            objCell.Align = "right";
            objCell.InnerHtml = "&nbsp;";// TotalPrice.ToString("n"); //lstclsRowData.Sum(q => q.TotalPrice); 
            objRow.Cells.Add(objCell);

            objCell = new HtmlTableCell();
            objCell.Attributes.Add("nowrap", "nowrap");
            objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
            objCell.Align = "right";
            objCell.InnerHtml = TotalKP.ToString("n");
            objRow.Cells.Add(objCell);

            if (objLoginUserInfo.funBln_Limited("3002", objLoginUserInfo.SystemLimited))
            {
                objCell = new HtmlTableCell();
                objCell.Attributes.Add("nowrap", "nowrap");
                objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
                objCell.Align = "right";
                objCell.InnerHtml = TotalTP.ToString("n");
                objRow.Cells.Add(objCell);

                objCell = new HtmlTableCell();
                objCell.Attributes.Add("nowrap", "nowrap");
                objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
                objCell.Align = "right";
                string tmpGrossMargin = GrossMargin.ToLower();
                try
                {
                    tmpGrossMargin = tmpGrossMargin.Replace("kp", TotalKP.ToString());
                    tmpGrossMargin = tmpGrossMargin.Replace("tp", TotalTP.ToString());
                    //GrossMargin = GrossMargin.Replace("bonus", BonusRate.ToString());
                    tmpGrossMargin = ClassLibrary.CalculateExpression.Calculate(tmpGrossMargin).ToString();
                    tmpGrossMargin = (decimal.Parse(tmpGrossMargin) * decimal.Parse("100")).ToString("F2") + "%";
                }
                catch
                {
                    tmpGrossMargin = "";
                }
                objCell.InnerHtml = tmpGrossMargin;
                objRow.Cells.Add(objCell);
            }

            //objCell = new HtmlTableCell();
            //objCell.Attributes.Add("nowrap", "nowrap");
            //objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
            //objCell.Align = "right";
            //objCell.InnerHtml = TotalSPROthersPerUnit.ToString("n");
            //objRow.Cells.Add(objCell);

            //objCell = new HtmlTableCell();
            //objCell.Attributes.Add("nowrap", "nowrap");
            //objCell.Attributes.Add("style", "padding:4px; border-bottom:1px solid #CCCCCC;");
            //objCell.Align = "right";
            //objCell.InnerHtml = TotalSPROthersPerItem.ToString("F0");
            //objRow.Cells.Add(objCell);

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
                    List<ClassLibrary.ProductRowData> lstclsRowData = new List<CHub.ClassLibrary.ProductRowData>();
                    if (Session["ProductList"] == null)
                    {
                        Session["ProductList"] = new List<ClassLibrary.ProductRowData>();
                    }
                    lstclsRowData = (List<ClassLibrary.ProductRowData>)Session["ProductList"];

                    lstclsRowData.Remove(lstclsRowData[intIndex]);

                    Session["ProductList"] = lstclsRowData;

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
            Session["ProductList"] = null;
            Response.Redirect("ProductsQuotation.aspx?type=Paste");
        }

        #region"建立Excel文件，其实不用判断Encoding的，因为用Owc导出Excel可直接另存为Excel"
        /// <summary>
        /// 创建Excel文件
        /// </summary>
        /// <param name="swExcel"></param>
        /// <returns></returns>
        private string funCreateExcelFile(StringWriter swExcel, string FileName)
        {
            //先删除文件，怕有重复的文件
            string strFileName = "";
            string strFullFileName = "";
            string strDirPath = Server.MapPath("Temp/ExcelExport/" + objLoginUserInfo.ID.Replace("-", "") + "/");
            if (!Directory.Exists(strDirPath))
            {
                Directory.CreateDirectory(strDirPath);
            }


            Encoding objEncoding;
            objEncoding = System.Text.Encoding.GetEncoding("GB2312");//导出文件导式用GB2312
            strFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + FileName + ".xls";
            //strFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + FileName + ".htm";

            //strFullFileName = strDirPath + "\\" + strFileName;
            strFullFileName = strDirPath + strFileName;
            subDeleteFile_TempFile(strFullFileName);
            FileStream objFileStream = new FileStream(strFullFileName, FileMode.Create);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, objEncoding);
            objStreamWriter.Write(swExcel.ToString());
            objStreamWriter.Close();
            swExcel.Close();
            return strFileName;
        }
        #endregion

        /// <summary>
        /// 导出选件详细列表
        /// </summary>
        /// <param name="BriefID">保存数据的BriefID</param>
        /// <param name="IsDisplayTPAndMargin">是否显示TP和Margin的信息，True：显示；False：不显示</param>
        private void subExport_HTMLProductList(string BriefID, bool IsDisplayTPAndMargin)
        {
            string strHTML = "";
            strHTML = "Document/Export/ExportProductList.htm".funString_FileContent(System.Text.Encoding.GetEncoding("GB2312"));

            //为了保存值，将类保存到了Session中
            List<ClassLibrary.ProductRowData> lstclsRowData = new List<ClassLibrary.ProductRowData>();
            if (Session["ProductList"] == null)
            {
                Session["ProductList"] = new List<ClassLibrary.ProductRowData>();
            }
            lstclsRowData = (List<ClassLibrary.ProductRowData>)Session["ProductList"];

            #region "替换文字"
            #region "报价单信息"
            strHTML = strHTML.Replace("@Customer@", txtCustomer.Value.funString_SQLToString());
            strHTML = strHTML.Replace("@Channel@", txtChannel.Value.funString_SQLToString());
            strHTML = strHTML.Replace("@Project@", txtProject.Value.funString_SQLToString());
            strHTML = strHTML.Replace("@QuotationNo@", txtQuotationNo.Value.funString_SQLToString());
            strHTML = strHTML.Replace("@IsGM@", "");//cboIsGM.funComboBox_SelectedValue().funString_SQLToString());

            strHTML = strHTML.Replace("@SalesRegion@", objLoginUserInfo.RegionName);
            strHTML = strHTML.Replace("@BUResponsible@", objLoginUserInfo.LoginName);

            strHTML = strHTML.Replace("@QuotationDate@", DateTime.Now.ToString("yyyy-MM-dd"));
            #endregion

            #region "得到报价单的列数，并将文字中的列先替换掉"
            int intColumn = 11;
            if (objLoginUserInfo.funBln_Limited("3002", objLoginUserInfo.SystemLimited))
            {
                if (IsDisplayTPAndMargin)
                {
                    intColumn = 11;
                }
                else
                {
                    intColumn = 9;
                }
            }
            else
            {
                intColumn = 9;
            }
            if (objLoginUserInfo.funBln_Limited("3001", objLoginUserInfo.SystemLimited))
            {

            }
            else
            {
                intColumn = intColumn - 1;
            }
            strHTML = strHTML.Replace("@TotalColumn@", intColumn.ToString());
            //这个替换一定要放在后面，因为里面本身就有
            int LeftColumn1 = intColumn - 3;
            int LeftColumn2 = 1;
            int RightColumn2 = intColumn - 3;
            //int SpaceColumn = intColumn - 3;
            if (intColumn == 9)
            {
                LeftColumn2 = 1;
                RightColumn2 = intColumn - 6;
                //SpaceColumn = intColumn - 3;
            }
            else
            {
                LeftColumn2 = 2;
                RightColumn2 = intColumn - 7;
                //SpaceColumn = intColumn - 4;
            }
            strHTML = strHTML.Replace("@LeftColumn1@", LeftColumn1.ToString());
            strHTML = strHTML.Replace("@LeftColumn2@", LeftColumn2.ToString());
            strHTML = strHTML.Replace("@RightColumn2@", RightColumn2.ToString());
            //strHTML = strHTML.Replace("@SpaceColumn@", SpaceColumn.ToString());
            #endregion

            string KPDiscount = "";
            string strSQL = "SELECT QuotationFunction FROM CHub_Management_Function WHERE (IsDel = 0) AND (QuotationText = '@KPDiscount@')";
            KPDiscount = objClassDbAccess.funString_SQLExecuteScalar(strSQL).ToLower();
            #region "价格"
            //得到填充的奖金百分比
            decimal BonusRate = txtBonusRate.Text.funDec_StringToDecimal(0);
            BonusRate = BonusRate / decimal.Parse("100");//百分比先除以100，得到数值

            //strHTML = strHTML.Replace("@TotalPrice@", lblTotalPrice.Text);
            //string TotalLP = lstclsRowData.Sum(q => q.TotalLP).ToString("n");
            //string TotalTP = lstclsRowData.Sum(q => q.TotalTP).ToString("n");
            decimal decKP = 0;
            decimal decTP = 0;
            decimal decSPROthersPerUnit = 0;
            //decimal decFAGR = 0;
            decimal decSPROthersPerItem = 0;
            for (int i = 0; i < lstclsRowData.Count; i++)
            {
                string tmpKPDiscount = KPDiscount.ToLower();
                try
                {
                    //tmpKPDiscount = tmpKPDiscount.Replace("lp", lstclsRowData[i].TotalLP.ToString());
                    //tmpKPDiscount = tmpKPDiscount.Replace("tp", lstclsRowData[i].TotalTP.ToString());
                    tmpKPDiscount = tmpKPDiscount.Replace("lp", (lstclsRowData[i].TotalDisLP + lstclsRowData[i].SPROthersPerUnit + lstclsRowData[i].SPROthersPerItem).ToString());
                    tmpKPDiscount = tmpKPDiscount.Replace("tp", (lstclsRowData[i].TotalDisTP + lstclsRowData[i].SPROthersPerUnit + lstclsRowData[i].SPROthersPerItem).ToString());
                    tmpKPDiscount = ClassLibrary.CalculateExpression.Calculate(tmpKPDiscount).ToString();
                }
                catch
                {
                    tmpKPDiscount = "0";
                }
                decKP += decimal.Parse(tmpKPDiscount);//lstclsRowData[i].TotalTP / decimal.Parse("0.8");//lstclsRowData[i].TotalLP * lstclsRowData[i].LPFactor;

                ////decKP += lstclsRowData[i].TotalLP * lstclsRowData[i].DiscountFactor * (1 - BonusRate);
                //decTP += lstclsRowData[i].TotalTP;// * lstclsRowData[i].DiscountFactor;
                decTP += lstclsRowData[i].TotalDisTP + lstclsRowData[i].SPROthersPerUnit + lstclsRowData[i].SPROthersPerItem;// *lstclsRowData[i].Factor; ;// lstclsRowData[i].TotalTP;// *lstclsRowData[i].DiscountFactor;// / decimal.Parse("100");
                decSPROthersPerUnit += lstclsRowData[i].SPROthersPerUnit;
                //decFAGR += lstclsRowData[i].FAGR;
                decSPROthersPerItem += lstclsRowData[i].SPROthersPerItem;
            }
            string TotalKP = decKP.ToString("n");// lstclsRowData.Sum(q => q.TotalLP).ToString();//.ToString("n");
            string TotalTP = decTP.ToString("n");// lstclsRowData.Sum(q => q.TotalTP).ToString();//.ToString("n");
            string TotalSPROthersPerUnit = decSPROthersPerUnit.ToString("");
            //string TotalFAGR = decFAGR.ToString("");
            string TotalHS = decSPROthersPerItem.ToString("");

            strHTML = strHTML.Replace("@TotalKP@", TotalKP);
            if (!objLoginUserInfo.funBln_Limited("3002", objLoginUserInfo.SystemLimited))
            {
                TotalTP = " - ";
            }
            if (IsDisplayTPAndMargin)
            {
                strHTML = strHTML.Replace("@TotalTP@", "<tr><td colspan='" + LeftColumn2.ToString() + "'>&nbsp;</td><td>TP</td><td> " + TotalTP + " </td><td colspan='" + RightColumn2.ToString() + "'>&nbsp;</td></tr>");
            }
            else
            {
                strHTML = strHTML.Replace("@TotalTP@", "");
            }

            string GrossMargin = "";
            strSQL = "SELECT QuotationFunction FROM CHub_Management_Function WHERE (IsDel = 0) AND (QuotationText = '@QuotationFunction1@')";
            GrossMargin = objClassDbAccess.funString_SQLExecuteScalar(strSQL).ToLower();
            string tmpGrossMargin = GrossMargin.ToLower();
            try
            {
                tmpGrossMargin = tmpGrossMargin.Replace("kp", decKP.ToString());
                tmpGrossMargin = tmpGrossMargin.Replace("tp", decTP.ToString());
                tmpGrossMargin = tmpGrossMargin.Replace("bonus", BonusRate.ToString());
                tmpGrossMargin = ClassLibrary.CalculateExpression.Calculate(tmpGrossMargin).ToString();
            }
            catch
            {
                tmpGrossMargin = "0";
            }
            
            if (IsDisplayTPAndMargin)
            {
                string strGrossMargin = (decimal.Parse(tmpGrossMargin) * decimal.Parse("100")).ToString("F2") + "% ";//tmpGrossMargin.ToString();
                if (!objLoginUserInfo.funBln_Limited("3002", objLoginUserInfo.SystemLimited))
                {
                    strGrossMargin = " - ";
                }
                strHTML = strHTML.Replace("@GrossMargin@", "<tr><td colspan='" + LeftColumn2.ToString() + "'>&nbsp;</td><td>Gross Margin</td><td> " + strGrossMargin + "</td><td colspan='" + RightColumn2.ToString() + "'>&nbsp;</td></tr>");
                strHTML = strHTML.Replace("@BonusRate@", "<tr><td colspan='" + LeftColumn2.ToString() + "'>&nbsp;</td><td>Bonus Rate</td><td> " + (BonusRate * decimal.Parse("100")).ToString("F2") + "% </td><td colspan='" + RightColumn2.ToString() + "'>&nbsp;</td></tr>");
            }
            else
            {
                strHTML = strHTML.Replace("@GrossMargin@", "");
                strHTML = strHTML.Replace("@BonusRate@", "");
            }
            #endregion

            #region Product List"
            string strTable = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" style=\"border-bottom: 1px solid #000;\">";

            strTable += "<tr><td colspan='" + intColumn.ToString() + "'><strong>Options List</strong></td></tr>";

            #region "列头行"
            //objRow = new HtmlTableRow();
            strTable += "<tr>";

            //显示MLFB
            strTable += "<td width='30%' style='border-bottom:1px solid #000;'><strong>MLFB-Z=Options</strong></td>";//"产品名称";
            strTable += "<td width='30%' style='border-bottom:1px solid #000;'><strong>SPR</strong></td>";//"产品名称";

            //显示数量
            strTable += "<td align='center' width='15%' style='border-bottom:1px solid #000;'><strong>Quantity</strong></td>";//"需求数量";

            if (objLoginUserInfo.funBln_Limited("3001", objLoginUserInfo.SystemLimited))
            {
                strTable += "<td align='center' style='border-bottom:1px solid #000;'><strong>LP (w/o VAT)</strong></td>";//"价格";

            }
            //折扣率
            strTable += "<td align='center' width='15%' style='border-bottom:1px solid #000;'><strong>Discount Factor</strong></td>";//"折扣率";

            //价格
            strTable += "<td align='center' style='border-bottom:1px solid #000;'><strong>K-Price (w/o VAT)</strong></td>";//"价格";

            //成本价
            if (IsDisplayTPAndMargin)
            {
                if (objLoginUserInfo.funBln_Limited("3002", objLoginUserInfo.SystemLimited))
                {
                    strTable += "<td align='center' style='border-bottom:1px solid #000;'><strong>TP (w/o VAT)</strong></td>";//"价格";

                    strTable += "<td align='center' style='border-bottom:1px solid #000;'><strong>Gross Margin</strong></td>";//"毛利";
                }
            }

            strTable += "<td align='center' style='border-bottom:1px solid #000;'><strong>Others Per Unit</strong></td>";//"";
            //strTable += "<td align='center' style='border-bottom:1px solid #000;'><strong>FAGR</strong></td>";//"";
            strTable += "<td align='center' style='border-bottom:1px solid #000;'><strong>Others Per Item</strong></td>";//"";
            strTable += "</tr>";
            #endregion

            decimal RowTotalLP = 0;
            decimal RowTotalKP = 0;
            decimal RowTotalTP = 0;
            decimal RowTotalSPROthersPerUnit = 0;
            //decimal RowTotalFAGR = 0;
            decimal RowTotalSPROthersPerItem = 0;

            for (int i = 0; i < lstclsRowData.Count; i++)
            {
                #region "循环画产品信息"
                //objRow = new HtmlTableRow();
                strTable += "<tr>";

                //显示MLFB+Options
                //objCell = new HtmlTableCell();
                //objCell.InnerHtml = "<p>" + ds.Tables[0].Rows[i]["MLFBOptions"].ToString() + "</p>";
                //objCell.Attributes.Add("style", "padding:4px; ");
                //objRow.Cells.Add(objCell);
                string strInnerHTML = "";
                strInnerHTML = lstclsRowData[i].lstProduct[0].ProductText;
                switch (strInnerHTML.Length)
                {
                    case 16:
                        break;
                    default://12
                        strInnerHTML = strInnerHTML.funString_StringStuff(7, 0, "-", true);
                        break;
                }
                if (lstclsRowData[i].lstProduct.Count > 1)
                {
                    strInnerHTML = strInnerHTML + "-Z=";
                    #region "循环全部的Options"
                    for (int x = 1; x < lstclsRowData[i].lstProduct.Count; x++)
                    {
                        //循环这个选件一台中添加几个
                        for (int y = 0; y < (lstclsRowData[i].lstProduct[x].ProductCount / lstclsRowData[i].QTY); y++)
                        {
                            strInnerHTML = strInnerHTML + "+" + lstclsRowData[i].lstProduct[x].ProductText;
                        }
                    }
                    #endregion
                    strInnerHTML = strInnerHTML.Replace("-Z=+", "-Z=");
                }
                strTable += "<td style='border-top:1px solid #000; border-right:1px solid #000;'>" + strInnerHTML + "</td>";//ds.Tables[0].Rows[i]["MLFBOptions"].ToString()

                strTable += "<td style='border-top:1px solid #000; border-right:1px solid #000;'>" + lstclsRowData[i].SPR + "</td>";//ds.Tables[0].Rows[i]["QTY"].ToString()
                //显示数量
                //objCell = new HtmlTableCell();
                //objCell.InnerHtml = "<p>" + ds.Tables[0].Rows[i]["QTY"].ToString() + "</p>";
                //objCell.Attributes.Add("style", "padding:4px; ");
                //objRow.Cells.Add(objCell);
                strTable += "<td align='center' style='border-top:1px solid #000; border-right:1px solid #000;'>" + lstclsRowData[i].QTY + "</td>";//ds.Tables[0].Rows[i]["QTY"].ToString()

                if (objLoginUserInfo.funBln_Limited("3001", objLoginUserInfo.SystemLimited))
                {
                    //RowTotalLP = lstclsRowData[i].TotalLP;
                    RowTotalLP = lstclsRowData[i].TotalDisLP + lstclsRowData[i].SPROthersPerUnit + lstclsRowData[i].SPROthersPerItem;
                    strTable += "<td align='right' style='border-top:1px solid #000; border-right:1px solid #000;'>" + RowTotalLP.ToString("n") + "</td>";//ds.Tables[0].Rows[i]["KP"].ToString()
                }

                //折扣率
                //objCell = new HtmlTableCell();
                //objCell.InnerHtml = "<p>" + (ds.Tables[0].Rows[i]["DiscountFactor"].ToString().funDec_StringToDecimal(0) * 100) + "%</p>";
                //objCell.Attributes.Add("style", "padding:4px; ");
                //objRow.Cells.Add(objCell);
                strTable += "<td align='center' style='border-top:1px solid #000; border-right:1px solid #000;'>" + (lstclsRowData[i].LPFactor * decimal.Parse("100")).ToString("F2") + "%</td>";//(ds.Tables[0].Rows[i]["DiscountFactor"].ToString().funDec_StringToDecimal(0) * 100).ToString("F0")

                //客户价
                ////objCell = new HtmlTableCell();
                ////objCell.InnerHtml = "<p>" + ds.Tables[0].Rows[i]["KP"].ToString() + "</p>";
                ////objCell.Attributes.Add("style", "padding:4px; ");
                ////objCell.Align = "right";
                ////objRow.Cells.Add(objCell);
                ////计算KP的公式为：LP* 折扣factor*(1-bonus%) * 数量=K-price 2010-11-11 by Cummins Wang Zhuo(Wang Yilin)
                //RowTotalKP = RowTotalLP * lstclsRowData[i].DiscountFactor * (1 - BonusRate);
                string tmpKPDiscount = KPDiscount.ToLower();
                try
                {
                    //tmpKPDiscount = tmpKPDiscount.Replace("lp", lstclsRowData[i].TotalLP.ToString());
                    //tmpKPDiscount = tmpKPDiscount.Replace("tp", lstclsRowData[i].TotalTP.ToString());
                    tmpKPDiscount = tmpKPDiscount.Replace("lp", (lstclsRowData[i].TotalDisLP + lstclsRowData[i].SPROthersPerUnit + lstclsRowData[i].SPROthersPerItem).ToString());
                    tmpKPDiscount = tmpKPDiscount.Replace("tp", (lstclsRowData[i].TotalDisTP + lstclsRowData[i].SPROthersPerUnit + lstclsRowData[i].SPROthersPerItem).ToString());
                    tmpKPDiscount = ClassLibrary.CalculateExpression.Calculate(tmpKPDiscount).ToString();
                }
                catch
                {
                    tmpKPDiscount = "0";
                }

                RowTotalKP = decimal.Parse(tmpKPDiscount);//lstclsRowData[i].TotalTP / decimal.Parse("0.8");//RowTotalLP * lstclsRowData[i].LPFactor;
                strTable += "<td align='right' style='border-top:1px solid #000; border-right:1px solid #000;'>" + RowTotalKP.ToString("n") + "</td>";//ds.Tables[0].Rows[i]["KP"].ToString()

                //成本价
                if (IsDisplayTPAndMargin)
                {
                    if (objLoginUserInfo.funBln_Limited("3002", objLoginUserInfo.SystemLimited))
                    {
                        //objCell = new HtmlTableCell();
                        //objCell.InnerHtml = "<p>" + ds.Tables[0].Rows[i]["TP"].ToString() + "</p>";
                        //objCell.Attributes.Add("style", "padding:4px; ");
                        //objCell.Align = "right";
                        //objRow.Cells.Add(objCell);
                        //RowTotalTP = lstclsRowData[i].TotalTP;
                        RowTotalTP = lstclsRowData[i].TotalDisTP + lstclsRowData[i].SPROthersPerUnit + lstclsRowData[i].SPROthersPerItem;
                        strTable += "<td align='right' style='border-top:1px solid #000; border-right:1px solid #000;'>" + RowTotalTP.ToString("n") + "</td>";//ds.Tables[0].Rows[i]["TP"].ToString()

                        tmpGrossMargin = GrossMargin.ToLower();
                        try
                        {
                            tmpGrossMargin = tmpGrossMargin.Replace("kp", RowTotalKP.ToString());
                            tmpGrossMargin = tmpGrossMargin.Replace("tp", RowTotalTP.ToString());
                            tmpGrossMargin = tmpGrossMargin.Replace("bonus", BonusRate.ToString());
                            tmpGrossMargin = ClassLibrary.CalculateExpression.Calculate(tmpGrossMargin).ToString();
                            tmpGrossMargin = (decimal.Parse(tmpGrossMargin) * decimal.Parse("100")).ToString("F2") + "%";
                        }
                        catch
                        {
                            tmpGrossMargin = "";
                        }
                        strTable += "<td align='right' style='border-top:1px solid #000; border-right:1px solid #000;'>" + tmpGrossMargin + "</td>";

                    }
                }

                RowTotalSPROthersPerUnit = lstclsRowData[i].SPROthersPerUnit;
                strTable += "<td align='right' style='border-top:1px solid #000; border-right:1px solid #000;'>" + RowTotalSPROthersPerUnit.ToString("") + "</td>";
                //RowTotalFAGR = lstclsRowData[i].FAGR;
                //strTable += "<td align='right' style='border-top:1px solid #000; border-right:1px solid #000;'>" + RowTotalFAGR.ToString("") + "</td>";
                RowTotalSPROthersPerItem = lstclsRowData[i].SPROthersPerItem;
                strTable += "<td align='right' style='border-top:1px solid #000; border-right:1px solid #000;'>" + RowTotalSPROthersPerItem.ToString("") + "</td>";

                //objTable.Rows.Add(objRow);
                strTable += "</tr>";
                #endregion
            }

            strTable += "</table>";
            #endregion
            strHTML = strHTML.Replace("@ProductList@", strTable);
            #endregion


            //将内容导出
            StringBuilder sbExport = new StringBuilder();
            sbExport.Append(strHTML);

            string strFileName = "";
            StringWriter swExcel = new StringWriter(sbExport);
            strFileName = funCreateExcelFile(swExcel, "PO");
            swExcel.Close();

            //string strDirPath = "Temp/ExcelExport/" + objLoginUserInfo.ID.Replace("-", "");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "NewPO", "window.open('" + strDirPath + "/" + strFileName + "');", true);
            ////Response.Redirect("" + strDirPath + "/" + strFileName + "");
            string strJs = "window.open('FileDownload.aspx?File=" + System.Web.HttpContext.Current.Server.UrlEncode(strFileName) + "&date=" + DateTime.Now.ToString("yyyyMMddHHmmss") + "','_blank','width=350px,height=150px,status=no,toolbar=no,menubar=no,location=no');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenExcelJsSucceed2", strJs, true);
        }


        protected void btnExport_Click(object sender, EventArgs e)
        {
            string BriefID = Guid.NewGuid().ToString();
            subExport_HTMLProductList(BriefID, true);

            #region "Cancel"
            //StringBuilder sb = new StringBuilder();
            //HtmlTable objTable = (HtmlTable)divList.Controls[0];
            //sb.Append("MLFB,");
            //sb.Append("Qty,");
            //sb.Append("LPrice,");
            //sb.Append("TPrice");
            //sb.Append("\r\n");
            //for (int i = 0; i < objTable.Rows.Count - 1; i++)
            //{
            //    if (i == 0)
            //    {
            //        continue;
            //    }
            //    HtmlTableRow row = objTable.Rows[i];
            //    sb.Append(row.Cells[1].InnerText.funString_RemoveHTML() + ",");
            //    sb.Append(row.Cells[2].InnerText.Replace(",", "") + ",");
            //    sb.Append(row.Cells[4].InnerText.Replace(",", "") + ",");
            //    sb.Append(row.Cells[5].InnerText.Replace(",", ""));
            //    sb.Append("\r\n");
            //}
            //string temp = string.Format("attachment;filename={0}", "ExportData.csv");
            //Response.ClearHeaders();
            //Response.AppendHeader("Content-disposition", temp);
            //Response.Write(sb.ToString());
            //Response.End();
            #endregion
        }
    }

    public class classQItems
    {
        public string MLFB = "";
        public string SPR = "";
        public int Qty = 0;
        public decimal Discount = 0;
        public decimal Voltage = 0;
        public string V70Index = "";
    }
}
