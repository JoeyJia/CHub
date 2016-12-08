using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;

[assembly: TagPrefix("IdioSoft.DateTimePicker", "IdioSoft")]
namespace IdioSoft.DateTimePicker
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:DateTimePicker runat=server></{0}:DateTimePicker>")]
    public class DateTimePicker : WebControl, IPostBackDataHandler
    {
        Button objButton;
        TextBox objText;
        private ArrayList _aryWorkHours = new ArrayList();
        #region "公用属性"
        /// <summary>   
        /// 设置或取得Text
        /// </summary>   
        [Bindable(true),
            Category("IDIO_Property"),
            DefaultValue(""),Description("设置或取得Text")]
        public string Text
        {
            get
            {
                if (ViewState["Text"] != null)
                {
                    return ViewState["Text"].ToString();
                }
                else
                {
                    return null;
                }
            }

            set
            {
                ViewState["Text"] = value;
            }
        }
        /// <summary>   
        /// 设置或取得文本控件是否可修改
        /// </summary>   
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue(""), Description("设置或取得文本控件是否可修改")]
        public bool ReadOnly
        {
            get
            {
                if (ViewState["ReadOnly"] != null)
                {
                    return bool.Parse(ViewState["ReadOnly"].ToString());
                }
                else
                {
                    return false;
                }
            }

            set
            {
                ViewState["ReadOnly"] = value;
            }
        }
        /// <summary>   
        /// 设置或取得文本控件的宽度,默认为160
        /// </summary>   
        [Bindable(true),
            Category("IDIO_Property"),
            DefaultValue(""),Description("设置或取得文本控件的宽度,默认为160")]
        public string TimeWidth
        {
            get
            {
                if (ViewState["TimeWidth"] != null)
                {
                    return ViewState["TimeWidth"].ToString();
                }
                else
                {
                    return null;
                }
            }

            set
            {
                ViewState["TimeWidth"] = value;
            }
        }
        /// <summary>   
        /// 设置或取得控件是否是短日期,格式yyyy-MM-dd
        /// </summary>   
        [Bindable(true),
            Category("IDIO_Property"),
            DefaultValue(""),Description("设置或取得控件是否是短日期,格式yyyy-MM-dd")]
        public Boolean blnIsShortTime
        {
            get
            {
                if (ViewState["blnIsShortTime"] != null)
                {
                    return bool.Parse(ViewState["blnIsShortTime"].ToString());
                }
                else
                {
                    return false;
                }
            }

            set
            {
                ViewState["blnIsShortTime"] = value;
            }
        }
        /// <summary>   
        /// 设置或取得控件文本框的Javascript事件,如onblur等
        /// </summary>  
        [Bindable(true),
            Category("IDIO_Property"),
            DefaultValue(""),Description("设置或取得控件文本框的Javascript事件")]
        public string JsEvent
        {
            get
            {
                if (ViewState["JsEvent"] != null)
                {
                    return ViewState["JsEvent"].ToString();
                }
                else
                {
                    return "";
                }
            }

            set
            {
                ViewState["JsEvent"] = value;
            }
        }
        /// <summary>   
        /// 设置或取得控件文本框的JsEvent事件对应的脚本操作
        /// </summary>  
        [Bindable(true),
            Category("IDIO_Property"),
          DefaultValue(""), Description("设置或取得控件文本框的JsEvent事件对应的脚本操作")]
        public string JsFunction
        {
            get
            {
                if (ViewState["JsFunction"] != null)
                {
                    return ViewState["JsFunction"].ToString();
                }
                else
                {
                    return "";
                }
            }

            set
            {
                ViewState["JsFunction"] = value;
            }
        }
        #endregion
        #region"特别时间显示属性"
        /// <summary>   
        /// 过滤剩下的小时点
        /// </summary>  
        [Bindable(true),
        Category("IDIO_Property"), DefaultValue(""), Description("过滤只用显示的小时点")]
        public string aryWorkHours
        {
            get
            {
                if (ViewState["aryWorkHours"] != null)
                {
                    return ViewState["aryWorkHours"].ToString();
                }
                else
                {
                    return "";
                }
            }

            set
            {
                ViewState["aryWorkHours"] = value;
            }
        }
        /// <summary>   
        /// 过滤剩下的分钟点
        /// </summary>  
        [Bindable(true),
        Category("IDIO_Property"), DefaultValue(""), Description("过滤只用显示的分钟点")]
        public string aryWorkMinute
        {
            get
            {
                if (ViewState["aryWorkMinute"] != null)
                {
                    return ViewState["aryWorkMinute"].ToString();
                }
                else
                {
                    return "";
                }
            }

            set
            {
                ViewState["aryWorkMinute"] = value;
            }
        }
        /// <summary>   
        /// 过滤剩下的分钟点
        /// </summary>  
        [Bindable(true),
       Category("IDIO_Property"), DefaultValue(""), Description("过滤只用显示的秒钟点")]
        public string aryWorkSecond
        {
            get
            {
                if (ViewState["aryWorkSecond"] != null)
                {
                    return ViewState["aryWorkSecond"].ToString();
                }
                else
                {
                    return "";
                }
            }

            set
            {
                ViewState["aryWorkSecond"] = value;
            }
        }
        #endregion
        #region "设置默认要载入的YYYY,MM,HH,mm ss"
        /// <summary>   
        /// 设置或取得默认的时间点,当用户点击选取时间时,自动跳转到这个时间点
        /// </summary> 
        [Bindable(true),
        Category("IDIO_Property"), DefaultValue(""), Description("设置或取得默认的时间点")]
        public string defaultDateTime
        {
            get
            {
                if (ViewState["defaultDateTime"] != null)
                {
                    return ViewState["defaultDateTime"].ToString();
                }
                else
                {
                    return "";
                }
            }

            set
            {
                ViewState["defaultDateTime"] = value;
            }
        }
        #endregion
        #region"重画控件"
        protected override void CreateChildControls()
        {
            objText = new TextBox();
            objText.ID = this.ID;

            Table objTable = new Table();
            TableRow objRow = new TableRow();
            TableCell objCell;
            objTable.CellPadding = 0;
            objTable.CellSpacing = 0;

            if (this.Text != "" && this.Text != null)
            {
                objText.Attributes.Add("Value", this.Text);
            }
            if (JsEvent != "")
            {
                objText.Attributes.Add(JsEvent, JsFunction);
            }
            objText.Width = new Unit(TimeWidth);
            if (Enabled)
            {
                objText.Attributes.Add("class", "idio_inputBoxNormalStyle");
            }
            else
            {
                objText.Attributes.Add("class", "idio_inputBoxDisabledStyle");
            }
            if (ReadOnly)
            {
                objText.Attributes.Add("readonly", "true");
            }
            objText.Enabled = Enabled;
            objCell = new TableCell();
            objCell.Controls.Add(objText);
            objRow.Cells.Add(objCell);

            objButton = new Button();
            objButton.ID = "btnTime" + this.UniqueID;
            objButton.Attributes.Add("class", "idio_DateTimePickerBtnOnMouseOut");
            objButton.Enabled = Enabled;
            if (Enabled)
            {
                if (blnIsShortTime)
                {
                    objButton.Attributes.Add("Onclick", "Idio_DateTimePicker_funParametersDetailSet(this,'" + blnIsShortTime + "','" + aryWorkHours + "','" + aryWorkMinute + "','" + aryWorkSecond + "','" + defaultDateTime + "');Idio_DateTimePicker_fPopCalendar(this,'" + this.UniqueID + "','PopCal',event); return false;");
                }
                else
                {
                    objButton.Attributes.Add("Onclick", "Idio_DateTimePicker_funParametersDetailSet(this,'" + blnIsShortTime + "','" + aryWorkHours + "','" + aryWorkMinute + "','" + aryWorkSecond + "','" + defaultDateTime + "');Idio_DateTimePicker_fPopCalendar(this,'" + this.UniqueID + "','PopCal',event); return false;");
                }
                objButton.Attributes.Add("onmouseover", "this.className='idio_DateTimePickerBtnOnMouseOver';");
                objButton.Attributes.Add("onmouseout", "this.className='idio_DateTimePickerBtnOnMouseOut';");
            }
            objCell = new TableCell();
            objCell.Controls.Add(objButton);
            objRow.Cells.Add(objCell);
            objTable.Rows.Add(objRow);
            this.Controls.Add(objTable);
        }
        #endregion
        #region IPostBackDataHandler 成员
        public event EventHandler TextChanged;

        /// <summary>
        /// 当由类实现时，为 ASP.NET 服务器控件处理回发数据。
        /// </summary>
        /// <param name="postDataKey">控件的主要标识符</param>
        /// <param name="postCollection">所有传入名称值的集合</param>
        /// <returns>如果服务器控件的状态在回发发生后更改，则为 true；否则为 false。</returns>
        public virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            String presentValue = Text;
            String postedValue = postCollection[postDataKey];

            if (presentValue == null || !presentValue.Equals(postedValue))
            {
                Text = postedValue;
                return true;
            }

            return false;
        }
        /// <summary>
        /// 当由类实现时，用信号要求服务器控件对象通知 ASP.NET 应用程序该控件的状态已更改。
        /// </summary>
        public virtual void RaisePostDataChangedEvent()
        {
            OnTextChanged(EventArgs.Empty);
        }


        protected virtual void OnTextChanged(EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }
        #endregion
    }
}