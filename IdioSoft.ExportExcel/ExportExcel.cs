using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Reflection;
using System.Text;
using System.IO;
//using Microsoft.Office.Interop.Owc11;

[assembly: TagPrefix("IdioSoft.ExportExcel", "IdioSoft")]
namespace IdioSoft.ExportExcel
{
    [DefaultProperty("SQL")]
    [ToolboxData("<{0}:ExportExcel runat=server></{0}:ExportExcel>")]
    public class ExportExcel : CompositeControl
    {
        public enum enumExcelType
        {   
            Csv=0,
            Xls=1,
            Html=2
        }
        HtmlInputButton objButton;
        #region"��������"
        /// <summary>   
        /// ���û򷵻ش�������������SQL���
        /// </summary> 
        [Bindable(true),
            Category("IDIO_Property"),
            DefaultValue(""),Description("���û򷵻ش�������������SQL���")]
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
        /// <summary>   
        /// �������ĵ���Excel�ļ���ʱ�ļ���,���Ϊ��,Ĭ��Ϊ��Ŀ¼�µ�TempĿ¼,��TempĿ¼������,�Զ�����
        /// </summary> 
        [Bindable(true),
           Category("IDIO_Property"),
         DefaultValue(""), Description("�������ĵ���Excel�ļ���ʱ�ļ���,���Ϊ��,Ĭ��Ϊ��Ŀ¼�µ�TempĿ¼,��TempĿ¼������,�Զ�����")]
        private string TempFolder
        {
            get
            {
                string strAbsoluteDir = "";
                if (ViewState["TempFolder"] != null)
                {
                    strAbsoluteDir = System.Web.HttpContext.Current.Server.MapPath(ViewState["TempFolder"].ToString());
                    ViewState["TempFolder"]= strAbsoluteDir;
                    return ViewState["TempFolder"].ToString();
                }
                else
                {
                    //����Ҳ���TempĿ¼,��̬����Ŀ¼
                    strAbsoluteDir = System.Web.HttpContext.Current.Server.MapPath("Temp/ExcelExport");
                    if (!Directory.Exists(strAbsoluteDir))
                    {
                        Directory.CreateDirectory(strAbsoluteDir);
                    }
                    return strAbsoluteDir;
                }
            }
            set
            {
                ViewState["TempFolder"] = value;
            }
        }
        /// <summary>   
        /// �����ж�����CSV��ʽ����Excel����Excel��html��ʽ
        /// </summary> 
        [Bindable(true),
           Category("IDIO_Property"),
           DefaultValue(true),Description("�����ж�����CSV��ʽ����Excel����Excel��html��ʽ")]
        public enumExcelType ExcelType
        {
            get
            {
                if (ViewState["ExcelType"] != null)
                {
                    return (enumExcelType)ViewState["ExcelType"];
                }
                else
                {
                    return enumExcelType.Csv;
                }
            }
            set
            {
                ViewState["ExcelType"] = value;
            }
        }
        /// <summary>   
        /// ���÷��ص��ļ����ƣ�����û������˹̶����������û���������ִ��̣�û�еĻ���ʱ���ʽ����
        /// </summary>
        [Bindable(true),
           Category("IDIO_Property"),
           DefaultValue(""),Description("���÷��ص��ļ����ƣ�����û������˹̶����������û���������ִ��̣�û�еĻ���ʱ���ʽ����")]
        public string ExportExcelFileName
        {
            get
            {
                if (ViewState["ExportExcelFileName"] != null)
                {
                    return ViewState["ExportExcelFileName"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                ViewState["ExportExcelFileName"] = value;
            }
        }
        /// <summary>   
        /// ����OWC��HTML��Excelʱ���������ñ����
        /// </summary>
        [Bindable(true),
           Category("IDIO_Property"),
          DefaultValue(""), Description("����OWC��HTML��Excelʱ���������ñ����")]
        public string ExcelTitle
        {
            get
            {
                if (ViewState["ExcelTitle"] != null)
                {
                    return ViewState["ExcelTitle"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                ViewState["ExcelTitle"] = value;
            }
        }
        #endregion
        #region"���߼�������ʽ,��дCreateChildControls���������ӿؼ���ӵ����Ͽؼ���"
        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            //�Ȼ�Button
            objButton = new HtmlInputButton();
            objButton.ID = "btnExport";
            //objButton.Value = "4";
            objButton.Value = "����";
            //objButton.Attributes.Add("style", "font-family: Webdings; width:20px; height:20px; ");
            objButton.Attributes.Add("title", "����EXCEL");
            //objButton.Attributes.Add("style", "cursor:hand;font-family: Webdings;width:20px;height:20px");
            objButton.Attributes.Add("class", "idio_FlatBtnOnMouseOut");
            objButton.ServerClick += new EventHandler(btnExport_Click);
            this.Controls.Add(objButton);
        }
        #endregion
        #region "����ť����¼�"
        private void btnExport_Click(object sender, EventArgs e)
        {
            IdioSoft.ClassDbAccess.ClassDbAccess objClassDbAccess;
            objClassDbAccess = new IdioSoft.ClassDbAccess.ClassDbAccess();
            string strSQL = "";
            strSQL = SQL;
            DataSet ds = new DataSet();
            ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
            if (ds == null)
            {
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "noFindDataset", "alert('no find data!');", true);
                return;
            }
            string strFileName = "";
            switch (ExcelType)
            {
                case enumExcelType.Csv:
                    strFileName = funExcelExportCsv(ds.Tables[0]);
                    break;
                case enumExcelType.Xls:
                    strFileName = funExcelExportOWC(ds.Tables[0]);
                    break;
                case enumExcelType.Html:
                    strFileName = funExcelExportHTML(ds.Tables[0]);
                    break;
            }
            //���ļ�
            string strFullFileName = "";
            string strDirPath = TempFolder;
            strFullFileName = strDirPath + "\\" + strFileName;
            if (File.Exists(strFullFileName))
            {
                string strJs = "";
                string strVpath = "";
                strVpath = "Temp/ExcelExport/" + strFileName;//../../
                strJs = "window.open('" + strVpath +"');";
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "OpenExcelJsSucceed", strJs, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "OpenExcelJsError", "alert('Export Excel Error!')", true);
            }
        }
        #endregion
        #region"��csv��ʽ����Excel,û�кÿ�����ʽ"
        public string funExcelExportCsv(DataTable dTable)//���ص����ļ�����
        {
            StringBuilder sbContent = new StringBuilder();
            //д��ͷ
            //�ӵڶ��п�ʼ��������Ϊ��һ����ID��
            //for (int i = 0; i <= dTable.Columns.Count - 1; i++)
            for (int i = 1; i <= dTable.Columns.Count - 1; i++)
            {
                sbContent.Append(funTransferToCSVstring(dTable.Columns[i].ToString()).ToLower() + ",");
            }
            sbContent.Append("\r\n");
            //д����
            for (int i = 0; i <= dTable.Rows.Count - 1; i++)
            {
                //for (int j = 0; j <= dTable.Columns.Count - 1; j++)
                for (int j = 1; j <= dTable.Columns.Count - 1; j++)
                {
                    if (dTable.Rows[i].IsNull(j) == false)
                    {
                        sbContent.Append(funTransferToCSVstring(dTable.Rows[i][j].ToString()) + ",");
                    }
                    else
                    {
                        sbContent.Append("_" + ",");
                    }
                }
                sbContent.Append("\r\n");
            }
            string strFileName = "";
            StringWriter swExcel = new StringWriter(sbContent);
            strFileName = funCreateExcelFile(swExcel);
            swExcel.Close();
            return strFileName;
        }
        //�����������Ϊ�˷�ֹ������������","Ϊ�ָ�
        private string funTransferToCSVstring(string strContent)
        {
            if (strContent != "")
            {
                strContent = strContent.Replace("\r\n", "");
            }
            if (strContent != "")
            {
                strContent = strContent.Replace(",", "��");
            }
            if (strContent != "")
            {
                strContent = strContent.Replace("\"", "");
            }
            return strContent;
        }
                #endregion
        #region "��Office web control����Excel"
        private string funExcelExportOWC(DataTable dTable)
        {
            //SpreadsheetClass xlsSheet = new SpreadsheetClass();
            //int row = 1;//���忪ʼ��
            //int col = dTable.Columns.Count;//��������
            ////����ͷ
            //Range rng;
            //rng = new Range();
            //rng = xlsSheet.Cells;
            //rng.Font.set_Name("Arial");
            //rng.Font.set_Size(10);
            //if (ExcelTitle == "")
            //{
            //    xlsSheet.ActiveCell.Cells[1, 1] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //}
            //else
            //{
            //    xlsSheet.ActiveCell.Cells[1, 1] = ExcelTitle;
            //}
            //rng = xlsSheet.ActiveSheet.get_Range(xlsSheet.ActiveSheet.Cells[1, 1], xlsSheet.ActiveSheet.Cells[1, col]);
            //object o = new object();
            //rng.Merge(ref o);
            //rng.set_HorizontalAlignment(XlHAlign.xlHAlignCenter);//����
            //rng.Font.set_Size(16);
            //rng.Font.set_Bold(true);
            //row++;
            ////������ʱ��
            //xlsSheet.ActiveCell.Cells[row, 1] = "Date:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //rng = new Range();
            //rng = xlsSheet.ActiveSheet.get_Range(xlsSheet.ActiveSheet.Cells[row, 1], xlsSheet.ActiveSheet.Cells[row, 2]);
            //rng.Merge(ref o);
            //row++;
            ////����ͷ
            //for (int i = 0; i < dTable.Columns.Count; i++)
            //{
            //    xlsSheet.ActiveSheet.Cells[row, i + 1] = dTable.Columns[i].ColumnName.ToString();
            //}
            ////����
            //row++;
            //for (int i = 0; i < dTable.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dTable.Columns.Count; j++)
            //    {
            //        xlsSheet.ActiveSheet.Cells[row, j + 1] = dTable.Rows[i][j].ToString();
            //    }
            //    row++;
            //}
            ////�����п�
            //for (int i = 0; i < dTable.Columns.Count; i++)
            //{
            //    rng = new Range();
            //    rng = xlsSheet.ActiveSheet.get_Range(xlsSheet.ActiveSheet.Cells[1, i + 1], xlsSheet.ActiveSheet.Cells[1, i + 1]);
            //    rng.EntireColumn.AutoFit();
            //}
            ////����
            //xlsSheet.get_Range(xlsSheet.ActiveSheet.Cells[1, 1], xlsSheet.ActiveSheet.Cells[row, col]).Borders.set_LineStyle(XlLineStyle.xlContinuous);
            //xlsSheet.get_Range(xlsSheet.ActiveSheet.Cells[1, 1], xlsSheet.ActiveSheet.Cells[row, col]).Borders[XlBordersIndex.xlEdgeTop].set_Weight(XlBorderWeight.xlThin);
            //xlsSheet.get_Range(xlsSheet.ActiveSheet.Cells[1, 1], xlsSheet.ActiveSheet.Cells[row, col]).Borders[XlBordersIndex.xlEdgeTop].set_LineStyle(XlLineStyle.xlContinuous);
            //xlsSheet.get_Range(xlsSheet.ActiveSheet.Cells[1, 1], xlsSheet.ActiveSheet.Cells[row, col]).Borders[XlBordersIndex.xlEdgeBottom].set_Weight(XlBorderWeight.xlThin);
            //xlsSheet.get_Range(xlsSheet.ActiveSheet.Cells[1, 1], xlsSheet.ActiveSheet.Cells[row, col]).Borders[XlBordersIndex.xlEdgeBottom].set_LineStyle(XlLineStyle.xlContinuous);
            //xlsSheet.get_Range(xlsSheet.ActiveSheet.Cells[1, 1], xlsSheet.ActiveSheet.Cells[row, col]).Borders[XlBordersIndex.xlEdgeLeft].set_Weight(XlBorderWeight.xlThin);
            //xlsSheet.get_Range(xlsSheet.ActiveSheet.Cells[1, 1], xlsSheet.ActiveSheet.Cells[row, col]).Borders[XlBordersIndex.xlEdgeLeft].set_LineStyle(XlLineStyle.xlContinuous);
            //xlsSheet.get_Range(xlsSheet.ActiveSheet.Cells[1, 1], xlsSheet.ActiveSheet.Cells[row, col]).Borders[XlBordersIndex.xlEdgeRight].set_Weight(XlBorderWeight.xlThin);
            //xlsSheet.get_Range(xlsSheet.ActiveSheet.Cells[1, 1], xlsSheet.ActiveSheet.Cells[row, col]).Borders[XlBordersIndex.xlEdgeRight].set_LineStyle(XlLineStyle.xlContinuous);
            //string strFullFileName = "";
            string strFileName = "";
            //try
            //{
            //    string strDirPath = TempFolder;
            //    if (ExportExcelFileName == "")
            //    {
            //        strFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            //    }
            //    else
            //    {
            //        strFileName = ExportExcelFileName;
            //    }
            //    strFullFileName = strDirPath + "\\" + strFileName;
            //    xlsSheet.Export(strFullFileName, SheetExportActionEnum.ssExportActionNone, SheetExportFormat.ssExportHTML);
            //}
            //catch
            //{
            //}
            //finally
            //{

            //}
            return strFileName;
        }
        //ɾ����ʱ�ļ�
        private void subDeleteTempFile(string fileName)
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
        #endregion
        #region"��Html��������Excel,������ٶȱȽϿ�,��ʽҲ��"
        public string funExcelExportHTML(DataTable dTable)
        {
            StringBuilder sbContent = new StringBuilder();
            //д������
            //������
            sbContent.Append("<table border='0' cellspacing='0' cellpadding='0'>");
            if (ExcelTitle == "")
            {
                sbContent.Append("<tr style=\"font-weight: bold; white-space: nowrap;\">");
                sbContent.Append("<td colspan='" + dTable.Columns.Count + "'>" + ExcelTitle + "</td>");
                sbContent.Append("</tr>");
            }
            //д������
            sbContent.Append("<tr style=\"font-weight: bold; white-space: nowrap;\">");
            foreach (DataColumn column in dTable.Columns)
            {
                sbContent.Append("<td>" + column.ColumnName + "</td>");
            }
            sbContent.Append("</tr>");
            foreach (DataRow row in dTable.Rows)
            {
                sbContent.Append("<tr>");
                foreach (DataColumn column in dTable.Columns)
                {
                    switch (column.DataType.ToString().ToLower())
                    {
                        case "system.int32":
                            sbContent.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + row[column].ToString() + "</td>");
                            break;
                        default:
                            sbContent.Append("<td>" + row[column].ToString() + "</td>");
                            break;
                    }
                }
                sbContent.Append("</tr>");
            }
            sbContent.Append("</table>");
            string strFileName = "";
            StringWriter swExcel = new StringWriter(sbContent);
            strFileName = funCreateExcelFile(swExcel);
            swExcel.Close();
            return strFileName;
        }
        #endregion
        #region"����Excel�ļ�����ʵ�����ж�Encoding�ģ���Ϊ��Owc����Excel��ֱ�����ΪExcel"
        private string funCreateExcelFile(StringWriter swExcel)
        {
            //��ɾ���ļ��������ظ����ļ�
            string strFileName = "";
            string strFullFileName = "";
            string strDirPath = TempFolder;
            if(!Directory.Exists(strDirPath))
            {
                Directory.CreateDirectory(strDirPath);
            }


            Encoding objEncoding;
            objEncoding = System.Text.Encoding.GetEncoding("GB2312");
            switch (ExcelType)
            {
                case enumExcelType.Csv://CSV
                    objEncoding = System.Text.Encoding.GetEncoding("GB2312");//�����ļ���ʽ��GB2312
                    if (ExportExcelFileName == "")
                    {
                        strFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                    }
                    break;
                case enumExcelType.Xls://OWC
                    objEncoding = System.Text.Encoding.Default;
                    if (ExportExcelFileName == "")
                    {
                        strFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    }
                    break;
                case enumExcelType.Html://HTML
                    objEncoding = System.Text.Encoding.GetEncoding("GB2312");//�����ļ���ʽ��GB2312
                    if (ExportExcelFileName == "")
                    {
                        strFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    }
                    break;
            }
            strFullFileName = strDirPath + "\\" + strFileName;
            subDeleteTempFile(strFullFileName);
            FileStream objFileStream = new FileStream(strFullFileName, FileMode.Create);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, objEncoding);
            objStreamWriter.Write(swExcel.ToString());
            objStreamWriter.Close();
            swExcel.Close();
            return strFileName;
        }
        #endregion
    }
}
