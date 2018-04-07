using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

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
    }
}
