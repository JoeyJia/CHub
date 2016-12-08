using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Reflection;

[assembly: TagPrefix("IdioSoft.CustomPageEx", "IdioSoft")]
namespace IdioSoft.CustomPageEx
{
    [ToolboxData("<{0}:CustomPageEx runat=server></{0}:CustomPageEx>")]
    public class CustomPageEx : CompositeControl
    {
        #region "����"
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue("")]
        public string GridID
        {
            get
            {
                if (ViewState["GridID"] != null)
                {
                    return ViewState["GridID"].ToString();
                }
                else
                {
                    return null;
                }
            }

            set
            {
                ViewState["GridID"] = value;
            }
        }
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue("")]
        public string SQL
        {
            get
            {
                if (ViewState["SQL"] != null)
                {
                    return ViewState["SQL"].ToString();
                }
                else
                {
                    return null;
                }
            }

            set
            {
                ViewState["SQL"] = value;
            }
        }
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue("")]
        public string CustomControlID
        {
            get
            {
                if (ViewState["CustomControlID"] != null)
                {
                    return ViewState["CustomControlID"].ToString();
                }
                else
                {
                    return null;
                }
            }

            set
            {
                ViewState["CustomControlID"] = value;
            }
        }
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue("")]
        public int PageSize
        {
            get
            {
                if (ViewState["PageSize"] != null)
                {
                    return int.Parse(ViewState["PageSize"].ToString());
                }
                else
                {
                    return 1;
                }
            }

            set
            {
                ViewState["PageSize"] = value;
            }
        }
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue("")]
        public int StartRecord
        {
            get
            {
                return CurrentPage * PageSize;
            }
            set
            {
                ViewState["StartRecord"] = value;
            }
        }
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue("")]
        public int CustomPage
        {
            get
            {
                if (ViewState["CustomPage"] != null)
                {
                    return int.Parse(ViewState["CustomPage"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["CustomPage"] = value;
            }
        }
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue("")]
        public int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] != null)
                {
                    return int.Parse(ViewState["CurrentPage"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue("")]
        public int RecordCount
        {
            get
            {
                if (ViewState["RecordCount"] != null)
                {
                    return int.Parse(ViewState["RecordCount"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["RecordCount"] = value;
            }
        }
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue("")]
        public int PageCount
        {
            get
            {

                ViewState["PageCount"] = RecordCount / PageSize;
                if (RecordCount % PageSize != 0)
                {
                    ViewState["PageCount"] = (int)ViewState["PageCount"] + 1;
                }
                return (int)ViewState["PageCount"] - 1;
            }
        }
        #endregion
        #region "�ػ��ӿؼ�"
        public void refreshControl()
        {
            CreateChildControls();
        }
        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            HtmlTable objTable = new HtmlTable();
            HtmlTableRow objRow;
            HtmlTableCell objCell;
            Button objButton;
            objTable.CellPadding = 0;
            objTable.CellSpacing = 0;
            //�Ȼ��ĸ���ť
            objRow = new HtmlTableRow();
            
            objButton = new Button();
            objButton.Attributes.Add("class", "idio_FlatBtnOnMouseOut");
            //objButton.Attributes.Add("style", "margin:4px 4px 4px 4px;padding:2px 2px 2px 2px");
            objButton.Attributes.Add("style", "margin:4px 4px 4px 4px;");
            if (CurrentPage == 0)
            {
                objButton.Enabled = false;
            }
            else
            {
                objButton.Enabled = true;
            }
            objButton.Click += new EventHandler(objButton_OnClick);
            objButton.Text = "��ҳ";
            objCell = new HtmlTableCell();
            objCell.Controls.Add(objButton);
            objRow.Cells.Add(objCell);

            objButton = new Button();
            objButton.Attributes.Add("class", "idio_FlatBtnOnMouseOut");
            //objButton.Attributes.Add("style", "margin:4px 4px 4px 4px;padding:2px 2px 2px 2px");
            objButton.Attributes.Add("style", "margin:4px 4px 4px 4px;");
            if (CurrentPage == 0)
            {
                objButton.Enabled = false;
            }
            else
            {
                objButton.Enabled = true;
            }
            objButton.Text = "��һҳ";
            objButton.Click += new EventHandler(objButton_OnClick);
            objCell = new HtmlTableCell();
            objCell.Controls.Add(objButton);
            objRow.Cells.Add(objCell);

            objButton = new Button();
            objButton.Attributes.Add("class", "idio_FlatBtnOnMouseOut");
            //objButton.Attributes.Add("style", "margin:4px 4px 4px 4px;padding:2px 2px 2px 2px");
            objButton.Attributes.Add("style", "margin:4px 4px 4px 4px;");
            if (CurrentPage >= PageCount || RecordCount <= PageSize)
            {
                objButton.Enabled = false;
            }
            else
            {
                objButton.Enabled = true;
            }
            objButton.Text = "��һҳ";
            objButton.Click += new EventHandler(objButton_OnClick);
            objCell = new HtmlTableCell();
            objCell.Controls.Add(objButton);
            objRow.Cells.Add(objCell);

            objButton = new Button();
            objButton.Attributes.Add("class", "idio_FlatBtnOnMouseOut");
            //objButton.Attributes.Add("style", "margin:4px 4px 4px 4px;padding:2px 2px 2px 2px");
            objButton.Attributes.Add("style", "margin:4px 4px 4px 4px;");
            if (CurrentPage >= PageCount)
            {
                objButton.Enabled = false;
            }
            else
            {
                objButton.Enabled = true;
            }
            objButton.Text = "ĩҳ";
            objButton.Click += new EventHandler(objButton_OnClick);
            objCell = new HtmlTableCell();
            objCell.Controls.Add(objButton);
            objRow.Cells.Add(objCell);

            #region "��¼����"
            Label lblRecord = new Label();
            lblRecord.Text = "��¼����:" + RecordCount.ToString();
            objCell = new HtmlTableCell();
            objCell.Controls.Add(lblRecord);
            objRow.Cells.Add(objCell);
            #endregion

            objTable.Rows.Add(objRow);
            this.Controls.Add(objTable);
        }
        #endregion

        #region "��ť����¼�"
        protected void objButton_OnClick(object sender, EventArgs e)
        {
            switch (((Button)sender).Text.ToLower())
            {
                case "��ҳ":
                    CurrentPage = 0;
                    break;
                case "��һҳ":
                    if (CurrentPage > 0)
                    {
                        CurrentPage--;
                    }
                    else
                    {
                        CurrentPage = 0;
                    }
                    break;
                case "��һҳ":
                    if (CurrentPage <= PageCount)
                    {
                        CurrentPage++;
                    }
                    else
                    {
                        CurrentPage = PageCount;
                    }
                    break;
                case "ĩҳ":
                    int i = RecordCount;

                    CurrentPage = PageCount;
                    break;
            }

            subReLoadGridView();
        }
        private void subReLoadGridView()
        {
            //�ش�������ҳ��
            try
            {
                string strSQL = SQL;
                object o;
                Type t;
                if (CustomControlID==null || CustomControlID == "")
                {
                    t = this.Parent.Page.GetType();
                    o = this.Parent.Page;
                }
                else
                {
                    o = funControl_FindEx(this.Page, CustomControlID);
                    t = o.GetType();
                }
                object[] objPar = new object[0];
                MethodInfo objMI ;
                objMI = t.GetMethod("GetSortDirection");
                objMI.Invoke(o, objPar);
                objMI = t.GetMethod("subgrdMain_Load");
                objMI.Invoke(o, objPar);
                //CreateChildControls();
            }
            catch
            {

            }
        }
        #endregion

        #region "�ݹ�ؼ��еĿؼ�"
        private System.Web.UI.Control _objFindContorl;
        /// <summary>
        /// �ݹ�ؼ��еĿؼ�
        /// </summary>
        /// <param name="objControl"></param>
        /// <param name="strControlName"></param>
        /// <returns></returns>
        public System.Web.UI.Control funControl_FindEx(System.Web.UI.Control objControl, string strControlName)
        {
            System.Web.UI.Control objCon = null;

            for (int i = 0; i < objControl.Controls.Count; i++)
            {
                objCon = objControl.Controls[i];
                if (objCon.ID == strControlName)
                {
                    _objFindContorl = objCon;
                    break;
                }
                if (objCon.Controls.Count > 0 && _objFindContorl == null)
                {
                    funControl_FindEx(objCon, strControlName);
                }
            }
            return _objFindContorl;
        }
        #endregion
    }
}
