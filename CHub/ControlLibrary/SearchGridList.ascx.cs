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
using IdioSoft.ClassCommon;
using System.ComponentModel;
using System.Collections.Generic;

namespace CHub.ControlLibrary
{
    public partial class SearchGridList : ClassLibrary.UserControl
    {
        #region "定义公用属性、事件"
        public delegate void delgbtnAddnewClick(object sender, EventArgs e);
        public event delgbtnAddnewClick btnAddnewClick;

        public delegate void delgbtnModifyClick(object sender, EventArgs e);
        public event delgbtnModifyClick btnModifyClick;

        public delegate void delgbtnDeleteClick(object sender, EventArgs e);
        public event delgbtnDeleteClick btnDeleteClick;

        public delegate void delgbtnDetailClick(object sender, EventArgs e);
        public event delgbtnDetailClick btnDetailClick;

        public delegate void delgbtnStopClick(object sender, EventArgs e);
        public event delgbtnStopClick btnStopClick;

        public delegate void delgbtnOther1Click(object sender, EventArgs e);
        public event delgbtnOther1Click btnOther1Click;

        public delegate void delgbtnOther2Click(object sender, EventArgs e);
        public event delgbtnOther2Click btnOther2Click;

        public delegate void delgbtnOther3Click(object sender, EventArgs e);
        public event delgbtnOther3Click btnOther3Click;

        public delegate void delgbtnOther4Click(object sender, EventArgs e);
        public event delgbtnOther4Click btnOther4Click;


        public delegate void delgbtnOther5Click(object sender, EventArgs e);
        public event delgbtnOther5Click btnOther5Click;

        public delegate void delgbtnOther6Click(object sender, EventArgs e);
        public event delgbtnOther6Click btnOther6Click;


        public delegate void delgbtnOther7Click(object sender, EventArgs e);
        public event delgbtnOther7Click btnOther7Click;

        public delegate void delgbtnOther8Click(object sender, EventArgs e);
        public event delgbtnOther8Click btnOther8Click;

        public delegate void delgGridFulled();
        public event delgGridFulled gridFulled;

        public delegate void delgridRowSelected(object sender, EventArgs e);
        public event delgridRowSelected grdRowSelected;

        public delegate void delgrdMainRowDataBound(object sender, GridViewRowEventArgs e);
        public event delgrdMainRowDataBound grdRowDataBound;
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CustomPageEx1.GridID = "grdMain";
            CustomPageEx1.CustomControlID = this.ID;
            //objLoginUserInfo.AdvancedSearchAbUrl = this.Page.Request.Url.AbsolutePath;
        }

        DataSet dsMain = new DataSet();
        /// <summary>
        /// 页面载入时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //tableMain.Width = (objLoginUserInfo.UserScreenWidth).ToString() + "px";
                //divGrid.Style.Remove("width");
                //divGrid.Style.Add("width", (objLoginUserInfo.UserScreenWidth - 10) + "px");
                //divGrid.Style.Remove("height");
                //divGrid.Style.Add("height", (objLoginUserInfo.UserScreenHeight - 10) + "px");
            }
            catch
            {
            }
            if (!IsPostBack)
            {
                try
                {
                    IdioSoft.ClassCommon.CommonFunction.subComboBox_LoadItemsByDBColumnName(cboField, FieldSQL, false);
                    CustomPageEx1.StartRecord = 0;
                    CustomPageEx1.CurrentPage = 0;
                    CustomPageEx1.RecordCount = MaxRecord;
                    cboField_SelectedIndexChanged(cboField, new EventArgs());
                    subgrdMain_Load();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 当载入Grid时
        /// </summary>
        public void subMLoad_SearchGrid()
        {

            IdioSoft.ClassCommon.CommonFunction.subComboBox_LoadItemsByDBColumnName(cboField, FieldSQL, false);
            CustomPageEx1.StartRecord = 0;
            CustomPageEx1.CurrentPage = 0;
            CustomPageEx1.RecordCount = MaxRecord;
            cboField_SelectedIndexChanged(cboField, new EventArgs());
            cboField.Enabled = false;
            grdMain.Enabled = false;
            btnSearch.Disabled = true;
            subgrdMain_Load();
            cboField.Enabled = true;
            grdMain.Enabled = true;
            btnSearch.Disabled = false;
        }

        #region "属性"
        /// <summary>
        /// 是否自动行绑定
        /// </summary>
        bool _IsAutoRowDatabound = true;
        public bool IsAutoRowDatabound
        {
            get
            {
                return _IsAutoRowDatabound;
            }
            set
            {
                _IsAutoRowDatabound = value;
            }
        }
        /// <summary>
        /// 是否需要Checkbox列
        /// </summary>
        bool _IsCheckCol = false;
        public bool IsCheckCol
        {
            set
            {
                _IsCheckCol = value;
            }
            get
            {
                return _IsCheckCol;
            }
        }
        /// <summary>
        /// 是否显示选择按钮列
        /// </summary>
        public bool _IsShowSelect = true;
        public bool IsShowSelect
        {
            set
            {
                _IsShowSelect = value;
            }
            get
            {
                return _IsShowSelect;
            }
        }

        string _FieldSQL = "";
        /// <summary>
        /// 绑定搜索下拉菜单的SQL
        /// </summary>
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue(""), Description("绑定搜索下拉菜单的SQL")]
        public string FieldSQL
        {
            get
            {
                return _FieldSQL;
            }
            set
            {
                _FieldSQL = value;
            }
        }
        string _SQL = "";
        /// <summary>
        /// 绑定搜索Grid的SQL
        /// </summary>
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue(""), Description("绑定搜索Grid的SQL")]
        public string SQL
        {
            get
            {
                return _SQL;
            }
            set
            {
                _SQL = value;
            }
        }
        /// <summary>
        /// 取得返回Grid的最大分页数
        /// </summary>
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue(""), Description("取得返回Grid的最大分页数")]
        public int MaxPage
        {
            get
            {
                string strPageSQL = "";
                int intCount = 0;
                strPageSQL = "select count(*) from (" + CurrentGridSQL + ") DERIVEDTBL";
                try
                {
                    intCount = int.Parse(objClassDbAccess.funString_SQLExecuteScalar(strPageSQL));
                    if (intCount % objLoginUserInfo.PageSize != 0)
                    {
                        MaxPage = intCount / objLoginUserInfo.PageSize + 1;
                    }
                    else
                    {
                        MaxPage = intCount / objLoginUserInfo.PageSize;
                    }
                }
                catch
                {
                }
                return MaxPage;
            }
            set
            {
                ViewState["MaxPage"] = value;
            }
        }
        /// <summary>
        /// 取得最大记录数
        /// </summary>
        private int MaxRecord
        {
            get
            {
                string strPageSQL = "";
                int intCount = 0;
                if (ViewState["CurrentGridSQL"] == null || ViewState["CurrentGridSQL"].ToString() == "")
                {
                    ViewState["CurrentGridSQL"] = CurrentGridSQL;
                }
                strPageSQL = "select count(*) from (" + ViewState["CurrentGridSQL"].ToString() + ") DERIVEDTBL";
                try
                {
                    intCount = int.Parse(objClassDbAccess.funString_SQLExecuteScalar(strPageSQL));
                }
                catch
                {
                }
                return intCount;
            }
        }
        /// <summary>
        /// 默认排序的列
        /// </summary>
        public string DefaultSortName
        {
            get
            {
                string strValue = "";
                if (ViewState["DefaultSortName"] == null)
                {
                    strValue = "[Create Date]";
                }
                else
                {
                    strValue = ViewState["DefaultSortName"].ToString();
                }
                return strValue;
            }
            set
            {
                ViewState["DefaultSortName"] = value;
            }
        }
        /// <summary>
        /// GRID绑定的SQL中的KEY列名字
        /// </summary>
        string _GridFieldKeyName = "";
        [Bindable(true),
        Category("IDIO_Property"),
       DefaultValue(""), Description("GRID绑定的SQL中的KEY列名字")]
        public string GridFieldKeyName
        {
            get
            {
                return _GridFieldKeyName;
            }
            set
            {
                _GridFieldKeyName = value;
            }
        }
        int _GridFieldKeyNameIndex = -1;
        /// <summary>
        /// 返回与取得KEY列的索引
        /// </summary>
        private int GridFieldKeyNameIndex
        {
            get
            {
                for (int i = 0; i < dsMain.Tables[0].Columns.Count; i++)
                {
                    if (dsMain.Tables[0].Columns[i].ColumnName.ToLower() == GridFieldKeyName.ToLower())
                    {
                        _GridFieldKeyNameIndex = i;
                        break;
                    }
                }
                return _GridFieldKeyNameIndex;
            }
        }
        private int GridFiledKeyIndex
        {
            get
            {
                if (ViewState["GridFiledKeyIndex"] == null)
                {
                    ViewState["GridFiledKeyIndex"] = "0";
                }
                return ViewState["GridFiledKeyIndex"].ToString().funInt_StringToInt(0);
            }
            set
            {
                ViewState["GridFiledKeyIndex"] = value;
            }
        }
        /// <summary>
        /// 当前Grid搜索的SQL
        /// </summary>
        private string CurrentGridSQL
        {
            get
            {
                string strIDsss = this.ID.ToString();
                //string DefaultSQL = "";
                string strSQL = "";
                //高级搜索,搜索菜单返回,设置默认的SQL
                strSQL = SQL;
                //try
                //{
                //    tc.ActiveTabIndex = objLoginUserInfo.TabAIndex;
                //}
                //catch
                //{
                //}

                string strConditions = "";
                string strValue = cboField.funComboBox_SelectedValue();
                #region "取得搜索条件"
                string strDataType = ViewState["ColumnDataType"] == null ? funDataType_Column(strValue) : ViewState["ColumnDataType"].ToString();
                object o = null;
                try
                {
                    o = DicFieldSearchSQL[strValue];
                }
                catch
                {
                }
                switch (strDataType)
                {
                    case "System.String":
                        if (o != null)
                        {
                            strConditions = "[" + strValue + "] ='" + cboSearch.funComboBox_SelectedValue() + "'";
                        }
                        else
                        {
                            strConditions = "[" + strValue + "] like '%" + txtSearch.Value.funString_SQLToString() + "%'";
                        }
                        break;
                    case "System.DateTime":
                        strConditions = "[" + strValue + "] >='" + DateTimePickerStartDate.Text.funDateTime_StringToDatetime().ToString("yyyy-MM-dd HH:mm:ss") + "' and [" + strValue + "] <='" + DateTimePickerEndDate.Text.funDateTime_StringToDatetime().ToString("yyyy-MM-dd HH:mm:ss") + "'";
                        break;
                    case "System.Int32":
                        if (txtSearch.Value.IndexOf("~") >= 0)
                        {
                            string[] aryInt;
                            try
                            {
                                aryInt = txtSearch.Value.Split('~');
                                strConditions = "[" + strValue + "] >=" + aryInt[0] + " and " + strValue + "<=" + aryInt[1];
                            }
                            catch (Exception ex)
                            {
                                //subMessage_Load("Search conditions is error!", ex.Message + "\n" + ex.Source);
                            }
                        }
                        else
                        {
                            strConditions = "[" + strValue + "] =" + txtSearch.Value;
                        }
                        break;
                    case "System.Decimal":
                        if (txtSearch.Value.IndexOf("~") >= 0)
                        {
                            string[] aryInt;
                            try
                            {
                                aryInt = txtSearch.Value.Split('~');
                                strConditions = "[" + strValue + "] >=" + aryInt[0] + " and [" + strValue + "] <=" + aryInt[1];
                            }
                            catch (Exception ex)
                            {
                                //subMessage_Load("Search conditions is error!", ex.Message + "\n" + ex.Source);
                            }
                        }
                        else
                        {
                            strConditions = "[" + strValue + "] =" + txtSearch.Value;
                        }
                        break;
                    case "System.Boolean":
                        strConditions = "[" + strValue + "] ='" + cboSearch.funComboBox_SelectedValue() + "'";
                        break;
                    default:
                        strConditions = "1=1";
                        break;
                }
                #endregion
                strSQL += " and " + strConditions;
                ViewState["CurrentGridSQL"] = strSQL;
                return ViewState["CurrentGridSQL"].ToString();
            }
            set
            {
                ViewState["CurrentGridSQL"] = value;
            }
        }
        /// <summary>
        /// 选中GRID的记录的ID
        /// </summary>
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue(""), Description("选中GRID的记录的ID")]
        public string GridSelectID
        {
            get
            {
                string SelectID = "";
                try
                {
                    if (IsCheckCol)
                    {
                        SelectID = grdMain.SelectedRow.Cells[2].Text.ToString();
                    }
                    else
                    {
                        SelectID = grdMain.SelectedRow.Cells[1].Text.ToString();
                    }
                }
                catch
                {
                }
                return SelectID;
            }
        }
        Dictionary<string, string> _DicFieldSearchSQL;
        /// <summary>
        /// 定义搜索下拉菜对应下拉菜单要载入的SQL
        /// </summary>
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue(""), Description("定义搜索下拉菜对应下拉菜单要载入的SQL")]
        public Dictionary<string, string> DicFieldSearchSQL
        {
            get
            {
                return _DicFieldSearchSQL;
            }
            set
            {
                _DicFieldSearchSQL = value;
            }
        }
        /// <summary>
        /// 搜索文本框的内容
        /// </summary>
        public string txtSearchValue
        {
            set
            {
                txtSearch.Value = value;
            }
            get
            {
                return txtSearch.Value;
            }
        }

        #region "Other1~8按钮的文字和JS"
        /// <summary>
        /// Other1的文字
        /// </summary>
        public string btnOther1Text
        {
            set
            {
                this.btnOther1.Text = value;
            }
        }
        /// <summary>
        /// btnOther1的JS定义
        /// </summary>
        public string btnOther1JsAttributes
        {
            set
            {
                btnOther1.Attributes.Add("onclick", value);
            }
        }
        /// <summary>
        /// Other2的文字
        /// </summary>
        public string btnOther2Text
        {
            set
            {
                this.btnOther2.Text = value;
            }
        }
        /// <summary>
        /// btnOther2的JS定义
        /// </summary>
        public string btnOther2JsAttributes
        {
            set
            {
                btnOther2.Attributes.Add("onclick", value);
            }
        }
        /// <summary>
        /// Other3的文字
        /// </summary>
        public string btnOther3Text
        {
            set
            {
                this.btnOther3.Text = value;
            }
        }
        /// <summary>
        /// btnOther3的JS定义
        /// </summary>
        public string btnOther3JsAttributes
        {
            set
            {
                btnOther3.Attributes.Add("onclick", value); ;
            }
        }
        /// <summary>
        /// Other4的文字
        /// </summary>
        public string btnOther4Text
        {
            set
            {
                this.btnOther4.Text = value;
            }
        }
        /// <summary>
        /// btnOther4的JS定义
        /// </summary>
        public string btnOther4JsAttributes
        {
            set
            {
                btnOther4.Attributes.Add("onclick", value); ;
            }
        }
        /// <summary>
        /// Other5的文字
        /// </summary>
        public string btnOther5Text
        {
            set
            {
                this.btnOther5.Text = value;
            }
        }
        /// <summary>
        /// btnOther5的JS定义
        /// </summary>
        public string btnOther5JsAttributes
        {
            set
            {
                btnOther5.Attributes.Add("onclick", value); ;
            }
        }
        /// <summary>
        /// Other6的文字
        /// </summary>
        public string btnOther6Text
        {
            set
            {
                this.btnOther6.Text = value;
            }
        }
        /// <summary>
        /// btnOther6的JS定义
        /// </summary>
        public string btnOther6JsAttributes
        {
            set
            {
                btnOther6.Attributes.Add("onclick", value); ;
            }
        }
        /// <summary>
        /// Other7的文字
        /// </summary>
        public string btnOther7Text
        {
            set
            {
                this.btnOther7.Text = value;
            }
        }
        /// <summary>
        /// btnOther7的JS定义
        /// </summary>
        public string btnOther7JsAttributes
        {
            set
            {
                btnOther7.Attributes.Add("onclick", value); ;
            }
        }
        /// <summary>
        /// Other8的文字
        /// </summary>
        public string btnOther8Text
        {
            set
            {
                this.btnOther8.Text = value;
            }
        }
        /// <summary>
        /// btnOther8的JS定义
        /// </summary>
        public string btnOther8JsAttributes
        {
            set
            {
                btnOther8.Attributes.Add("onclick", value); ;
            }
        }
        #endregion

        /// <summary>
        /// 取得Search按钮的Name，用于回调
        /// </summary>
        public string btnSearchClientName
        {
            get
            {
                return btnSearch.Name;
            }
        }
        /// <summary>
        /// btnDelete的JS定义
        /// </summary>
        public string btnDeleteJsAttributes
        {
            set
            {
                btnDelete.Attributes.Remove("onclick");
                btnDelete.Attributes.Add("onclick", value); ;
            }
        }
        public string GridTitle
        {
            set
            {
                tdlblTitle.InnerHtml = value;
                trlblTitle.Visible = true;
            }
        }
        #region "操作按钮是否显示"
        public bool IsModify
        {
            set
            {
                btnModify.Visible = value;
            }
        }
        public bool IsAddnew
        {
            set
            {
                btnAddnew.Visible = value;
            }
        }
        public bool IsDelete
        {
            set
            {
                btnDelete.Visible = value;
            }
        }
        public bool IsDetail
        {
            set
            {
                btnDetail.Visible = value;
            }
        }
        public bool IsStop
        {
            set
            {
                btnStop.Visible = value;
            }
        }
        public bool IsOther1
        {
            set
            {
                btnOther1.Visible = value;
            }
        }
        public bool IsOther2
        {
            set
            {
                btnOther2.Visible = value;
            }
        }
        public bool IsOperation
        {
            set
            {
                trOperation.Visible = value;
            }
        }
        public bool IsOther3
        {
            set
            {
                btnOther3.Visible = value;
            }
        }
        public bool IsOther4
        {
            set
            {
                btnOther4.Visible = value;
            }
        }
        public bool IsOther5
        {
            set
            {
                btnOther5.Visible = value;
            }
        }
        public bool IsOther6
        {
            set
            {
                btnOther6.Visible = value;
            }
        }
        public bool IsOther7
        {
            set
            {
                btnOther7.Visible = value;
            }
        }
        public bool IsOther8
        {
            set
            {
                btnOther8.Visible = value;
            }
        }
        #endregion
        public List<string> ListCheckIDs
        {
            get
            {
                List<string> lst = new List<string>();
                for (int i = 0; i < grdMain.Rows.Count; i++)
                {
                    CheckBox chk = null;
                    try
                    {
                        chk = (CheckBox)grdMain.Rows[i].Cells[1].FindControl("chkSelect");
                        if (chk.Checked)
                        {
                            lst.Add(grdMain.Rows[i].Cells[2].Text.ToString());
                        }
                    }
                    catch
                    {
                    }
                }
                return lst;
            }
        }
        #endregion
        #region "搜索下拉菜单"
        /// <summary>
        /// 搜索字段选择时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboField_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strValue = cboField.funComboBox_SelectedValue();
            ViewState["ColumnDataType"] = funDataType_Column(strValue);
            object o = null;
            try
            {
                o = DicFieldSearchSQL[strValue];
            }
            catch
            {
            }
            switch (ViewState["ColumnDataType"].ToString())
            {
                case "System.String":
                    if (o != null)
                    {
                        cboSearch.subComboBox_LoadItems(o.ToString(), 0, null);
                        tdsearchSelect.Visible = true;
                        tdsearchContent.Visible = false;
                        tdsearchDate1.Visible = false;
                        tdsearchDate2.Visible = false;
                    }
                    else
                    {
                        tdsearchSelect.Visible = false;
                        tdsearchContent.Visible = true;
                        tdsearchDate1.Visible = false;
                        tdsearchDate2.Visible = false;
                    }

                    break;
                case "System.DateTime":
                    tdsearchSelect.Visible = false;
                    tdsearchContent.Visible = false;
                    tdsearchDate1.Visible = true;
                    tdsearchDate2.Visible = true;
                    break;
                case "System.Int32":
                    tdsearchSelect.Visible = false;
                    tdsearchContent.Visible = true;
                    tdsearchDate1.Visible = false;
                    tdsearchDate2.Visible = false;
                    break;
                case "System.Decimal":
                    tdsearchSelect.Visible = false;
                    tdsearchContent.Visible = true;
                    tdsearchDate1.Visible = false;
                    tdsearchDate2.Visible = false;
                    break;
                case "System.Boolean":
                    cboSearch.subComboBox_LoadItems(true);
                    tdsearchSelect.Visible = true;
                    tdsearchContent.Visible = false;
                    tdsearchDate1.Visible = false;
                    tdsearchDate2.Visible = false;
                    break;
                default:
                    tdsearchSelect.Visible = false;
                    tdsearchContent.Visible = true;
                    tdsearchDate1.Visible = false;
                    tdsearchDate2.Visible = false;
                    break;
            }

        }
        /// <summary>
        /// 得到搜索字段的数据类型
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        private string funDataType_Column(string ColumnName)
        {
            string strDataType = "";
            string strSQL = "";
            strSQL = FieldSQL;
            DataSet ds = new DataSet();
            ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                if (ColumnName.ToLower() == ds.Tables[0].Columns[i].ColumnName.ToLower())
                {
                    strDataType = ds.Tables[0].Columns[i].DataType.ToString();
                    break;
                }
            }
            return strDataType;
        }
        #endregion
        #region "点击搜索按钮事件"
        /// <summary>
        /// 点击搜索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_OnServerClick(object sender, EventArgs e)
        {
            GridViewSortExpression = DefaultSortName;
            GridViewSortDirection = "ASC";
            //objLoginUserInfo.AdvancedSearchQuerySQL = "";
            //objLoginUserInfo.AdvancedSearchSQLFrom = "";
            CustomPageEx1.StartRecord = 0;
            CustomPageEx1.CurrentPage = 0;
            CustomPageEx1.RecordCount = MaxRecord;
            subgrdMain_Load();
            grdMain.SelectedIndex = -1;
        }
        #endregion
        #region "Group By属性"
        public void subGroupBy_Set(string strValue)
        {
            if (tdsearchContent.Visible)
            {
                txtSearch.Value = strValue;
            }
            if (tdsearchSelect.Visible)
            {
                cboSearch.subComboBox_SelectItemByValue(strValue);
            }
            if (tdsearchDate1.Visible && tdsearchDate2.Visible)
            {
                DateTimePickerStartDate.Text = strValue;
                DateTimePickerEndDate.Text = strValue;
            }
        }
        #endregion
        #region "载入数据到GRIDVIEW"
        /// <summary>
        /// 载入数据到GRIDVIEW
        /// </summary>
        /// <param name="strSQL"></param>
        public virtual void subgrdMain_Load()
        {

            if (!IsCheckCol)
            {
                try
                {
                    grdMain.Columns.RemoveAt(1);
                }
                catch
                {
                }
            }

            string strSQL = "";
            //if (ViewState["CurrentGridSQL"] == null || ViewState["CurrentGridSQL"].ToString() == "")
            //{
            ViewState["CurrentGridSQL"] = CurrentGridSQL;
            //}
            if (GridViewSortExpression.Substring(0, 1) != "[")
            {
                GridViewSortExpression = "[" + GridViewSortExpression + "]";
            }
            strSQL = ViewState["CurrentGridSQL"].ToString() + " order by " + GridViewSortExpression + " " + GetSortDirection();
            ExportExcel1.SQL = strSQL;
            CustomPageEx1.PageSize = objLoginUserInfo.PageSize;
            dsMain = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL, CustomPageEx1.StartRecord, objLoginUserInfo.PageSize);
            GridFiledKeyIndex = GridFieldKeyNameIndex;
            if (dsMain != null)
            {
                CustomPageEx1.SQL = strSQL;
                grdMain.DataSource = dsMain.Tables[0];
                grdMain.DataBind();
                CustomPageEx1.RecordCount = MaxRecord;
                CustomPageEx1.refreshControl();
                if (gridFulled != null)
                {
                    this.gridFulled();
                }
                //objLoginUserInfo.AdvancedSearchQuerySQL = "";
            }
        }
        #endregion
        #region "排序公用的一段代码"
        //Grid栏目排序时
        protected void grdMain_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewSortExpression = e.SortExpression;
            subgrdMain_Load();
            grdMain.SelectedIndex = -1;
        }
        //判断排序类别
        public string GridViewSortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }
        //判断排序列
        public string GridViewSortExpression
        {
            get
            {
                if (ViewState["SortExpression"] == null || ViewState["SortExpression"].ToString() == "")
                {
                    ViewState["SortExpression"] = DefaultSortName;
                }
                return ViewState["SortExpression"].ToString();
            }
            set { ViewState["SortExpression"] = value; }
        }
        //得到排序类别
        public string GetSortDirection()
        {
            switch (GridViewSortDirection)
            {
                case "ASC":
                    GridViewSortDirection = "DESC";
                    break;
                case "DESC":
                    GridViewSortDirection = "ASC";
                    break;
            }
            return GridViewSortDirection;
        }
        #endregion
        #region"做行数据绑定"
        public virtual void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            for (int i = 0; i <= e.Row.Cells.Count - 1; i++)
            {
                e.Row.Cells[i].Attributes.Add("class", "idio_gridBottom");
            }
            //}
            //用于不换行属性
            if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.Footer)
            {
                for (int i = 0; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].Attributes.Add("nowrap", "nowrap");
                    e.Row.Cells[i].Attributes.Add("style", "text-align:left;");

                    if (GridFiledKeyIndex >= 0)
                    {
                        if (IsCheckCol)
                        {
                            e.Row.Cells[GridFiledKeyIndex + 2].CssClass = "idio_hiddenItem";
                        }
                        else
                        {
                            e.Row.Cells[GridFiledKeyIndex + 1].CssClass = "idio_hiddenItem";
                        }
                    }

                }
                if (!IsShowSelect)
                {
                    e.Row.Cells[0].CssClass = "idio_hiddenItem";
                }
            }
            if (!IsAutoRowDatabound)
            {
                if (grdRowDataBound != null)
                {
                    this.grdRowDataBound(sender, e);
                }
            }
        }
        #endregion
        #region "点击操作按钮"
        protected void btnModify_OnClick(object sender, EventArgs e)
        {
            if (btnModifyClick != null)
            {
                this.btnModifyClick(sender, e);
            }
        }
        protected void btnAddnew_OnClick(object sender, EventArgs e)
        {
            if (btnAddnewClick != null)
            {
                this.btnAddnewClick(sender, e);
            }
        }
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            if (btnDeleteClick != null)
            {
                this.btnDeleteClick(sender, e);
            }
        }
        protected void btnDetail_OnClick(object sender, EventArgs e)
        {
            if (btnDetailClick != null)
            {
                this.btnDetailClick(sender, e);
            }
        }
        protected void btnStop_OnClick(object sender, EventArgs e)
        {
            if (btnStopClick != null)
            {
                this.btnStopClick(sender, e);
            }
        }
        protected void btnOther1_OnClick(object sender, EventArgs e)
        {
            if (btnOther1Click != null)
            {
                this.btnOther1Click(sender, e);
            }
        }
        protected void btnOther2_OnClick(object sender, EventArgs e)
        {
            if (btnOther2Click != null)
            {
                this.btnOther2Click(sender, e);
            }
        }
        protected void btnOther3_OnClick(object sender, EventArgs e)
        {
            if (btnOther3Click != null)
            {
                this.btnOther3Click(sender, e);
            }
        }
        protected void btnOther4_OnClick(object sender, EventArgs e)
        {
            if (btnOther4Click != null)
            {
                this.btnOther4Click(sender, e);
            }
        }
        protected void btnOther5_OnClick(object sender, EventArgs e)
        {
            if (btnOther5Click != null)
            {
                this.btnOther5Click(sender, e);
            }
        }
        protected void btnOther6_OnClick(object sender, EventArgs e)
        {
            if (btnOther6Click != null)
            {
                this.btnOther6Click(sender, e);
            }
        }
        protected void btnOther7_OnClick(object sender, EventArgs e)
        {
            if (btnOther7Click != null)
            {
                this.btnOther7Click(sender, e);
            }
        }
        protected void btnOther8_OnClick(object sender, EventArgs e)
        {
            if (btnOther8Click != null)
            {
                this.btnOther8Click(sender, e);
            }
        }
        #endregion

        #region "点击高级搜索"
        public void btnAdSearch_OnServerClick(object sender, EventArgs e)
        {
            ////objLoginUserInfo.AdvancedSearchSQLTo = SQL;
            //int intTabIndex = 0;
            //try
            //{
            //    //AjaxControlToolkit.TabContainer o = (AjaxControlToolkit.TabContainer)this.Parent.Parent.Parent;
            //    //intTabIndex = o.ActiveTabIndex;
            //}
            //catch
            //{
            //}
            ////objLoginUserInfo.TabAIndex = intTabIndex;
            ////objLoginUserInfo.AdvancedSearchUrl = this.Page.Request.Url.AbsoluteUri;
            ////objLoginUserInfo.AdvancedSearchAbUrl = this.Page.Request.Url.AbsolutePath;
            ////objLoginUserInfo.DicFieldSearchSQL = DicFieldSearchSQL;
            ////Response.Redirect("ControlLibrary/Public/AdvancedSearch.aspx");
        }
        #endregion

        /// <summary>
        /// Grid选择记录改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdRowSelected != null)
            {
                this.grdRowSelected(sender, e);
            }
        }
    }
}