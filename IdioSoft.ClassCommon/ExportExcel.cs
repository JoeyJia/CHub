using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
//using Microsoft.Office.Interop.Owc11;

namespace IdioSoft.ClassCommon
{
    /// <summary>
    /// 导出Excel类,这个方法类是产生临时Excel文件
    /// </summary>
    public class ExportExcel
    {
        #region"用csv方式导出Excel,没有好看的样式"
        /// <summary>   
        /// 用csv方式导出Excel，如果导出成功返回文件名，否则为空
        /// </summary>   
        /// <param name="dTable">DataTable数据源</param> 
        public string funString_ExcelExportCsv(DataTable dTable)
        {
            string strFileName = DateTime.Now.Ticks.ToString() + ".xls";
            StringBuilder sbContent = new StringBuilder();
            //写列头
            for (int i = 0; i <= dTable.Columns.Count - 1; i++)
            {
                sbContent.Append(dTable.Columns[i].ToString().ToLower().funString_ExcelCSV() + ",");
            }
            sbContent.Append("\r\n");
            //写内容
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
        #region "用html方式导出Excel"
        /// <summary>   
        /// 用html方式导出Excel，如果导出成功返回文件名，否则为空
        /// </summary>   
        /// <param name="dTable">DataTable数据源</param>
        public string funString_ExcelExportHtml(DataTable dTable)
        {
            string strFileName = DateTime.Now.Ticks.ToString() + ".xls";
            StringBuilder sbContent = new StringBuilder();

            //data += tb.TableName + "\n";
            sbContent.Append("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
            //写出列名
            sbContent.Append("<tr style=\"font-weight: bold; white-space: nowrap;\">");
            foreach (DataColumn column in dTable.Columns)
            {
                sbContent.Append("<td>" + column.ColumnName + "</td>");
            }
            sbContent.Append("</tr>");

            //写出数据
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
        #region "用Office web control导出Excel"
        /// <summary>   
        /// 用OWC方式导出Excel
        /// </summary>   
        /// <param name="dTable">DataTable数据源</param>
        /// <param name="eTitle">标题</param>
        /// <param name="tempPath">导出的产生临时文件的路径</param>
        private string funString_ExcelExportOfficewebControl(DataTable dTable, string eTitle, string tempPath)
        {
            string xlName = "";
            //SpreadsheetClass xlsSheet = new SpreadsheetClass();
            //int row = 1;//定义开始行
            //int col = dTable.Columns.Count;//定义列数
            ////画标头
            //Range rng;
            //rng = new Range();
            //rng = xlsSheet.Cells;
            //rng.Font.set_Name("Arial");
            //rng.Font.set_Size(10);
            //xlsSheet.ActiveCell.Cells[1, 1] = eTitle;
            //rng = xlsSheet.ActiveSheet.get_Range(xlsSheet.ActiveSheet.Cells[1, 1], xlsSheet.ActiveSheet.Cells[1, col]);
            //object o = new object();
            //rng.Merge(ref o);
            //rng.set_HorizontalAlignment(XlHAlign.xlHAlignCenter);//居中
            //rng.Font.set_Size(16);
            //rng.Font.set_Bold(true);
            //row++;
            ////画导出时间
            //xlsSheet.ActiveCell.Cells[row, 1] = "Date:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //rng = new Range();
            //rng = xlsSheet.ActiveSheet.get_Range(xlsSheet.ActiveSheet.Cells[row, 1], xlsSheet.ActiveSheet.Cells[row, 2]);
            //rng.Merge(ref o);
            //row++;
            ////画列头
            //for (int i = 0; i < dTable.Columns.Count; i++)
            //{
            //    xlsSheet.ActiveSheet.Cells[row, i + 1] = dTable.Columns[i].ColumnName.ToString();
            //}
            ////画行
            //row++;
            //for (int i = 0; i < dTable.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dTable.Columns.Count; j++)
            //    {
            //        xlsSheet.ActiveSheet.Cells[row, j + 1] = dTable.Rows[i][j].ToString();
            //    }
            //    row++;
            //}
            ////调整列宽
            //for (int i = 0; i < dTable.Columns.Count; i++)
            //{
            //    rng = new Range();
            //    rng = xlsSheet.ActiveSheet.get_Range(xlsSheet.ActiveSheet.Cells[1, i + 1], xlsSheet.ActiveSheet.Cells[1, i + 1]);
            //    rng.EntireColumn.AutoFit();
            //}
            ////画边
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
        //删除临时文件
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
        #region "创建临时文件"
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
