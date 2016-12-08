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
        #region "��������"
        /// <summary>   
        /// ���û�ȡ��Text
        /// </summary>   
        [Bindable(true),
            Category("IDIO_Property"),
            DefaultValue(""),Description("���û�ȡ��Text")]
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
        /// ���û�ȡ���ı��ؼ��Ƿ���޸�
        /// </summary>   
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue(""), Description("���û�ȡ���ı��ؼ��Ƿ���޸�")]
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
        /// ���û�ȡ���ı��ؼ��Ŀ��,Ĭ��Ϊ160
        /// </summary>   
        [Bindable(true),
            Category("IDIO_Property"),
            DefaultValue(""),Description("���û�ȡ���ı��ؼ��Ŀ��,Ĭ��Ϊ160")]
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
        /// ���û�ȡ�ÿؼ��Ƿ��Ƕ�����,��ʽyyyy-MM-dd
        /// </summary>   
        [Bindable(true),
            Category("IDIO_Property"),
            DefaultValue(""),Description("���û�ȡ�ÿؼ��Ƿ��Ƕ�����,��ʽyyyy-MM-dd")]
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
        /// ���û�ȡ�ÿؼ��ı����Javascript�¼�,��onblur��
        /// </summary>  
        [Bindable(true),
            Category("IDIO_Property"),
            DefaultValue(""),Description("���û�ȡ�ÿؼ��ı����Javascript�¼�")]
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
        /// ���û�ȡ�ÿؼ��ı����JsEvent�¼���Ӧ�Ľű�����
        /// </summary>  
        [Bindable(true),
            Category("IDIO_Property"),
          DefaultValue(""), Description("���û�ȡ�ÿؼ��ı����JsEvent�¼���Ӧ�Ľű�����")]
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
        #region"�ر�ʱ����ʾ����"
        /// <summary>   
        /// ����ʣ�µ�Сʱ��
        /// </summary>  
        [Bindable(true),
        Category("IDIO_Property"), DefaultValue(""), Description("����ֻ����ʾ��Сʱ��")]
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
        /// ����ʣ�µķ��ӵ�
        /// </summary>  
        [Bindable(true),
        Category("IDIO_Property"), DefaultValue(""), Description("����ֻ����ʾ�ķ��ӵ�")]
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
        /// ����ʣ�µķ��ӵ�
        /// </summary>  
        [Bindable(true),
       Category("IDIO_Property"), DefaultValue(""), Description("����ֻ����ʾ�����ӵ�")]
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
        #region "����Ĭ��Ҫ�����YYYY,MM,HH,mm ss"
        /// <summary>   
        /// ���û�ȡ��Ĭ�ϵ�ʱ���,���û����ѡȡʱ��ʱ,�Զ���ת�����ʱ���
        /// </summary> 
        [Bindable(true),
        Category("IDIO_Property"), DefaultValue(""), Description("���û�ȡ��Ĭ�ϵ�ʱ���")]
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
        #region"�ػ��ؼ�"
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
        #region IPostBackDataHandler ��Ա
        public event EventHandler TextChanged;

        /// <summary>
        /// ������ʵ��ʱ��Ϊ ASP.NET �������ؼ�����ط����ݡ�
        /// </summary>
        /// <param name="postDataKey">�ؼ�����Ҫ��ʶ��</param>
        /// <param name="postCollection">���д�������ֵ�ļ���</param>
        /// <returns>����������ؼ���״̬�ڻط���������ģ���Ϊ true������Ϊ false��</returns>
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
        /// ������ʵ��ʱ�����ź�Ҫ��������ؼ�����֪ͨ ASP.NET Ӧ�ó���ÿؼ���״̬�Ѹ��ġ�
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