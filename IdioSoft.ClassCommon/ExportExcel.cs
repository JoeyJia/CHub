using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
//using Microsoft.Office.Interop.Owc11;

namespace IdioSoft.ClassCommon
{
    /// <summary>
    /// ����Excel��,����������ǲ�����ʱExcel�ļ�
    /// </summary>
    public class ExportExcel
    {
        #region"��csv��ʽ����Excel,û�кÿ�����ʽ"
        /// <summary>   
        /// ��csv��ʽ����Excel����������ɹ������ļ���������Ϊ��
        /// </summary>   
        /// <param name="dTable">DataTable����Դ</param> 
        public string funString_ExcelExportCsv(DataTable dTable)
        {
            string strFileName = DateTime.Now.Ticks.ToString() + ".xls";
            StringBuilder sbContent = new StringBuilder();
            //д��ͷ
            for (int i = 0; i <= dTable.Columns.Count - 1; i++)
            {
                sbContent.Append(dTable.Columns[i].ToString().ToLower().funString_ExcelCSV() + ",");
            }
            sbContent.Append("\r\n");
            //д����
            for (int i = 0; i <= dTable.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dTable.Columns.Count - 1; j++)
                {
                    if (dTable.Rows[i].IsNull(j) == false)
                    {
                        sbContent.Append(dTable.Rows[i][j].ToString().funString_ExcelCSV() + ",");
                    }
                    else
                    {
                        sbContent.Append("_" + ",");
                    }
                }
                sbContent.Append("\r\n");
            }
            if (funBoolean_CreateFile(strFileName, sbContent.ToString()))
            {
                return strFileName;
            }
            else
            {
                return "";
            }
        }

        #endregion
        #region "��html��ʽ����Excel"
        /// <summary>   
        /// ��html��ʽ����Excel����������ɹ������ļ���������Ϊ��
        /// </summary>   
        /// <param name="dTable">DataTable����Դ</param>
        public string funString_ExcelExportHtml(DataTable dTable)
        {
            string strFileName = DateTime.Now.Ticks.ToString() + ".xls";
            StringBuilder sbContent = new StringBuilder();

            //data += tb.TableName + "\n";
            sbContent.Append("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
            //д������
            sbContent.Append("<tr style=\"font-weight: bold; white-space: nowrap;\">");
            foreach (DataColumn column in dTable.Columns)
            {
                sbContent.Append("<td>" + column.ColumnName + "</td>");
            }
            sbContent.Append("</tr>");

            //д������
            foreach (DataRow row in dTable.Rows)
            {
                sbContent.Append("<tr>");
                foreach (DataColumn column in dTable.Columns)
                {
                    switch (column.DataType.ToString().ToLower())
                    {
                        case "system.string":
                            sbContent.Append("<td>" + row[column].ToString() + "</td>");
                            break;
                        case "system.guid":
                            sbContent.Append("<td>" + row[column].ToString() + "</td>");
                            break;
                        case "system.datetime":
                            sbContent.Append("<td>" + row[column].ToString() + "</td>");
                            break;
                        case "system.decimal":
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

            if (funBoolean_CreateFile(strFileName, sbContent.ToString()))
            {
                return strFileName;
            }
            else
            {
                return "";
            }
        }
        #endregion
        #region "��Office web control����Excel"
        /// <summary>   
        /// ��OWC��ʽ����Excel
        /// </summary>   
        /// <param name="dTable">DataTable����Դ</param>
        /// <param name="eTitle">����</param>
        /// <param name="tempPath">�����Ĳ�����ʱ�ļ���·��</param>
        private string funString_ExcelExportOfficewebControl(DataTable dTable, string eTitle, string tempPath)
        {
            string xlName = "";
            //SpreadsheetClass xlsSheet = new SpreadsheetClass();
            //int row = 1;//���忪ʼ��
            //int col = dTable.Columns.Count;//��������
            ////����ͷ
            //Range rng;
            //rng = new Range();
            //rng = xlsSheet.Cells;
            //rng.Font.set_Name("Arial");
            //rng.Font.set_Size(10);
            //xlsSheet.ActiveCell.Cells[1, 1] = eTitle;
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
            //try
            //{
            //    xlName = DateTime.Now.Ticks.ToString() + ".xls";
                //string filename;
        //        filename = tempPath + xlName;
        //        xlsSheet.Export(filename, SheetExportActionEnum.ssExportActionNone, SheetExportFormat.ssExportHTML);
        //    }
        //    catch
        //    {

        //    }
        //    finally
        //    {

        //    }
            return xlName;
        }
        //ɾ����ʱ�ļ�
        private void deleteTempFile(string fileName)
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
        #region "������ʱ�ļ�"
        private bool funBoolean_CreateFile(string strFileName, string strContent)
        {
            bool blnCreate = false;
            string strTempDir = "";
            strTempDir = System.Web.HttpContext.Current.Server.MapPath("Temp/");
            if (!Directory.Exists(strTempDir))
            {
                Directory.CreateDirectory(strTempDir);
            }
            FileStream fs;
            if (strTempDir.Substring(strTempDir.Length - 1) == "/" || strTempDir.Substring(strTempDir.Length - 1) == "\\")
            {
                fs = new FileStream(strTempDir + strFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            }
            else
            {
                fs = new FileStream(strTempDir + "/" + strFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            }
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("GB2312"));
            try
            {
                sw.Write(strContent);
                blnCreate = true;
            }
            catch
            {
                blnCreate = false;
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
            return blnCreate;
        }
        #endregion
    }
}
