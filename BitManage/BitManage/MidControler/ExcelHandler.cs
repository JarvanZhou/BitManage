using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace BitManage.MidControler
{
    public class ExcelHandler
    {
        public static DataTable ReadExcelToDataTable(string path)
        {
            DataTable dt = new DataTable();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                IWorkbook workbook = new HSSFWorkbook(fs);
                ISheet sheet = workbook.GetSheetAt(0);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                int rfirst = sheet.FirstRowNum;
                int rlast = sheet.LastRowNum;
                IRow row = sheet.GetRow(rfirst);
                int cfirst = row.FirstCellNum;
                int clast = row.LastCellNum;
                for (int i = row.FirstCellNum; i < clast; i++)
                {
                    if (row.GetCell(i) != null)
                    {
                        dt.Columns.Add(row.GetCell(i).StringCellValue, typeof(string));
                    }
                    else
                    {
                        throw new Exception("获取Excel行1列" + (i + 1).ToString() + "数据失败");
                    }
                }
                rows.MoveNext();
                while (rows.MoveNext())
                {
                    IRow temprow = (HSSFRow)rows.Current;
                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < temprow.LastCellNum; i++)
                    {
                        ICell cell = temprow.GetCell(i);


                        if (cell == null)
                        {
                            dr[i] = "";
                        }
                        else
                        {
                            dr[i] = cell.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                }
                sheet = null;
                workbook = null;
                fs.Close();
            }
            return dt;
        }

        /// <summary>
        /// datatable写入excel
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="dt">表格</param>
        /// <param name="tableName">标题</param>
        /// <returns></returns>
        public static bool Write(string path, DataTable dt, string tableName = "")
        {
            List<DataTable> dtList = new List<DataTable>() { dt };
            List<string> tableNameList = new List<string>() { tableName };
            return Write(path, dtList, tableNameList);
        }
        /// <summary>
        /// datatable写入excel(多sheet)
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="dtList">表格</param>
        /// <param name="tableNameList">标题</param>
        /// <returns></returns>
        public static bool Write(string path, List<DataTable> dtList, List<string> tableNameList = null)
        {
            try
            {
                if (tableNameList != null && dtList.Count != tableNameList.Count) return false;

                HSSFWorkbook wb = new HSSFWorkbook();
                #region[字体]
                IFont columnNameFont = wb.CreateFont();
                columnNameFont.FontName = "黑体";
                columnNameFont.FontHeightInPoints = 11;
                IFont tableNameFont = wb.CreateFont();
                tableNameFont.FontName = "黑体";
                tableNameFont.FontHeightInPoints = 25;
                #endregion
                #region[单元格格式]
                //标题
                ICellStyle tableNameStyle = wb.CreateCellStyle();
                tableNameStyle.WrapText = true;
                tableNameStyle.Alignment = HorizontalAlignment.CenterSelection;
                tableNameStyle.VerticalAlignment = VerticalAlignment.Center;
                tableNameStyle.BorderBottom = BorderStyle.Thin;
                tableNameStyle.BorderLeft = BorderStyle.Thin;
                tableNameStyle.BorderRight = BorderStyle.Thin;
                tableNameStyle.BorderTop = BorderStyle.Thin;
                //tableNameStyle.FillBackgroundColor = HSSFColor.Grey25Percent.Index;
                //tableNameStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;
                //tableNameStyle.FillPattern = FillPattern.SolidForeground;
                tableNameStyle.SetFont(tableNameFont);
                //表头
                ICellStyle columnNameStyle = wb.CreateCellStyle();
                columnNameStyle.WrapText = true;
                columnNameStyle.Alignment = HorizontalAlignment.CenterSelection;
                columnNameStyle.VerticalAlignment = VerticalAlignment.Center;
                columnNameStyle.BorderBottom = BorderStyle.Thin;
                columnNameStyle.BorderLeft = BorderStyle.Thin;
                columnNameStyle.BorderRight = BorderStyle.Thin;
                columnNameStyle.BorderTop = BorderStyle.Thin;
                columnNameStyle.FillBackgroundColor = HSSFColor.Grey25Percent.Index;
                columnNameStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;
                columnNameStyle.FillPattern = FillPattern.SolidForeground;
                columnNameStyle.SetFont(columnNameFont);
                //奇数行
                ICellStyle style1 = wb.CreateCellStyle();
                style1.Alignment = HorizontalAlignment.CenterSelection;
                style1.VerticalAlignment = VerticalAlignment.Center;
                style1.BorderBottom = BorderStyle.Thin;
                style1.BorderLeft = BorderStyle.Thin;
                style1.BorderRight = BorderStyle.Thin;
                style1.BorderTop = BorderStyle.Thin;
                style1.FillBackgroundColor = HSSFColor.LightYellow.Index;
                style1.FillForegroundColor = HSSFColor.LightYellow.Index;
                //style1.FillBackgroundColor= HSSFColor.LightTurquoise.Index;
                //style1.FillForegroundColor= HSSFColor.LightTurquoise.Index;
                style1.FillPattern = FillPattern.SolidForeground;
                //偶数行
                ICellStyle style2 = wb.CreateCellStyle();
                style2.Alignment = HorizontalAlignment.CenterSelection;
                style2.VerticalAlignment = VerticalAlignment.Center;
                style2.BorderBottom = BorderStyle.Thin;
                style2.BorderLeft = BorderStyle.Thin;
                style2.BorderRight = BorderStyle.Thin;
                style2.BorderTop = BorderStyle.Thin;
                #endregion
                for (int i = 0; i < dtList.Count; i++)
                {
                    //sheet
                    ISheet sheet = wb.CreateSheet("sheet" + (i + 1));
                    int rowStartIndex = 0;
                    //标题
                    if (tableNameList != null && !string.IsNullOrEmpty(tableNameList[i]))
                    {
                        IRow tableNameRow = sheet.CreateRow(rowStartIndex);
                        tableNameRow.HeightInPoints = 50;
                        ICell cell = tableNameRow.CreateCell(0);
                        cell.SetCellValue(tableNameList[i]);
                        cell.CellStyle = tableNameStyle;
                        sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dtList[i].Columns.Count - 1));
                        rowStartIndex++;
                    }
                    //表头
                    IRow rowName = sheet.CreateRow(rowStartIndex);
                    rowName.HeightInPoints = 40;
                    for (int j = 0; j < dtList[i].Columns.Count; j++)
                    {
                        sheet.SetColumnWidth(j, 20 * 256);
                        ICell cell = rowName.CreateCell(j);
                        cell.SetCellValue(dtList[i].Columns[j].ColumnName);
                        cell.CellStyle = columnNameStyle;
                    }
                    //内容
                    for (int j = 0; j < dtList[i].Rows.Count; j++)
                    {
                        IRow row = sheet.CreateRow(j + rowStartIndex + 1);
                        row.HeightInPoints = 20;
                        for (int k = 0; k < dtList[i].Columns.Count; k++)
                        {
                            ICell cell = row.CreateCell(k);
                            int value = 0;
                            if (int.TryParse(dtList[i].Rows[j][k].ToString(), out value))
                            {
                                cell.SetCellValue(value);
                            }
                            else
                            {
                                cell.SetCellValue(dtList[i].Rows[j][k].ToString());
                            }
                            //奇偶行切换颜色
                            if (j % 2 == 1) cell.CellStyle = style1;
                            else cell.CellStyle = style2;
                        }
                    }
                }

                using (FileStream fs = new FileStream(path, FileMode.Create)) wb.Write(fs);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
