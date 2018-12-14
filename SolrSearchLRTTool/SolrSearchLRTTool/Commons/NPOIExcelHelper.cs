using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Reflection;
using System.Drawing;
using NPOI.HSSF.Util;

namespace SolrSearchLRTTool
{
    public class ExportObj
    {
        /// <summary>
        /// 键值
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public Type DataType { get; set; }
    }
    public class NPOIExcelHelper
    {
        #region 读取excel
        /// <summary>
        /// 根据文件路径和sheet名称读取----table的结构默认第一行为ColumnHeader
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string excelPath, string sheetName)
        {
            return ExcelToDataTable(excelPath, sheetName, true);
        }
        /// <summary>
        /// 根据文件路径和sheet名称读取----可设置是否sheet的第一行为ColumnHeader
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="sheetName"></param>
        /// <param name="firstRowAsHeader"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string excelPath, string sheetName, bool firstRowAsHeader)
        {
            using (FileStream fileStream = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = null;
                IFormulaEvaluator evaluator = null;

                if (excelPath.EndsWith(".xlsx"))
                {
                    workbook = new XSSFWorkbook(fileStream);
                    evaluator = new XSSFFormulaEvaluator(workbook);
                }
                else
                {
                    workbook = new HSSFWorkbook(fileStream);
                    evaluator = new HSSFFormulaEvaluator(workbook);
                }

                ISheet sheet = workbook.GetSheet(sheetName);
                return ExcelToDataTable(sheet, evaluator, firstRowAsHeader);
            }
        }

        private static DataTable ExcelToDataTable(ISheet sheet, IFormulaEvaluator evaluator, bool firstRowAsHeader)
        {
            if (firstRowAsHeader)
            {
                return ExcelToDataTableFirstRowAsHeader(sheet, evaluator);
            }
            else
            {
                return ExcelToDataTable(sheet, evaluator);
            }
        }

        private static DataTable ExcelToDataTableFirstRowAsHeader(ISheet sheet, IFormulaEvaluator evaluator)
        {
            using (DataTable dt = new DataTable())
            {
                IRow firstRow = sheet.GetRow(0);
                int cellCount = GetCellCount(sheet);

                for (int i = 0; i < cellCount; i++)
                {
                    if (firstRow.GetCell(i) != null)
                    {
                        var CellValue = firstRow.GetCell(i).StringCellValue;
                        dt.Columns.Add(string.IsNullOrEmpty(CellValue) ? string.Format("F{0}", i + 1) : CellValue, typeof(string));
                    }
                    else
                    {
                        dt.Columns.Add(string.Format("F{0}", i + 1), typeof(string));
                    }
                }

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i); 
                    if (row != null && !row.ZeroHeight)
                    {
                        DataRow dr = dt.NewRow();
                        FillDataRowByHSSFRow(row, evaluator, ref dr, sheet);
                        dt.Rows.Add(dr);
                    }
                }

                dt.TableName = sheet.SheetName;
                return dt;
            }
        }

        private static DataTable ExcelToDataTable(ISheet sheet, IFormulaEvaluator evaluator)
        {
            using (DataTable dt = new DataTable())
            {
                if (sheet.LastRowNum != 0)
                {
                    int cellCount = GetCellCount(sheet);

                    for (int i = 0; i < cellCount; i++)
                    {
                        dt.Columns.Add(string.Format("F{0}", i), typeof(string));
                    }

                    for (int i = 0; i < sheet.FirstRowNum; ++i)
                    {
                        DataRow dr = dt.NewRow();
                        dt.Rows.Add(dr);
                    }

                    for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        //skip hidden rows, shiqin, 20141121
                        if (row != null && !row.ZeroHeight)
                        {
                            DataRow dr = dt.NewRow();
                            FillDataRowByHSSFRow(row, evaluator, ref dr, sheet);
                            dt.Rows.Add(dr);
                        }
                    }
                }

                dt.TableName = sheet.SheetName;
                return dt;
            }
        }

        private static object GetCellValue(ISheet sheet, ICell cell, IFormulaEvaluator evaluator)
        {
            if (cell.CellType != CellType.Blank)
            {
                switch (cell.CellType)
                {
                    case CellType.Boolean:
                        return cell.BooleanCellValue;

                    case CellType.Numeric:
                        if (DateUtil.IsCellDateFormatted(cell))
                        {
                            return cell.DateCellValue;
                        }
                        else
                        {
                            if (cell.CellStyle.DataFormat == 9)
                            {
                                return string.Format("{0}%", cell.NumericCellValue * 100);
                            }
                            else
                            {
                                return cell.NumericCellValue;
                            }
                        }

                    case CellType.String:
                        return cell.StringCellValue;

                    case CellType.Error:
                        return cell.ErrorCellValue;

                    case CellType.Formula:
                        cell = evaluator.EvaluateInCell(cell);
                        return cell.ToString();
                    default:
                        return DBNull.Value;
                }
            }
            else
            {
                if (cell.IsMergedCell && IsCellMergedToLastRow(sheet, cell))
                {
                    int ilastrow = cell.RowIndex - 1;
                    int icellnum = cell.ColumnIndex;
                    ICell lastCell = cell.Sheet.GetRow(ilastrow).GetCell(icellnum);
                    return GetCellValue(sheet, lastCell, evaluator);
                }
                else
                {
                    return DBNull.Value;
                }
            }
        }

        private static bool IsCellMergedToLastRow(ISheet sheet, ICell cell)
        {
            bool result = false;
            for (int i = 0; i < sheet.NumMergedRegions; i++)
            {
                var region = sheet.GetMergedRegion(i);
                if (region.IsInRange(cell.RowIndex, cell.ColumnIndex))
                {
                    if (region.IsInRange(cell.RowIndex - 1, cell.ColumnIndex))
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        private static void FillDataRowByHSSFRow(IRow row, IFormulaEvaluator evaluator, ref DataRow dr, ISheet sheet)
        {

            if (row != null)
            {
                for (int j = 0; j < dr.Table.Columns.Count; j++)
                {
                    ICell cell = row.GetCell(j);

                    if (cell != null)
                    {
                        dr[j] = GetCellValue(sheet, cell, evaluator);
                    }
                }
            }
        }

        private static int GetCellCount(ISheet sheet)
        {
            int firstRowNum = sheet.FirstRowNum;

            int cellCount = 0;

            for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; ++i)
            {
                IRow row = sheet.GetRow(i);

                if (row != null && row.LastCellNum > cellCount)
                {
                    cellCount = row.LastCellNum;
                }
            }

            return cellCount;
        }
         
        #endregion

        #region 导出Excel

        /// <summary>
        /// 导出文件 
        /// </summary>
        /// <param name="list">数据集合List</param>
        /// <param name="head">列集合</param>
        /// <param name="title">导出文件名称</param>
        /// <param name="rootpath">导出路径</param>
        /// <returns></returns>
        public static string ExportListToExcelNew(IQueryable list, List<ExportObj> head, string title, string rootpath)
        {
            try
            { 
                string FileName = title + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
                string strPath = Path.Combine(rootpath, FileName);
                if (Directory.Exists(rootpath) == false)
                {
                    Directory.CreateDirectory(rootpath);
                }

                //文件流对象
                using (FileStream filestream = new FileStream(strPath, FileMode.Create, FileAccess.Write))
                {
                    Type type = list.ElementType;
                    PropertyInfo[] properties = type.GetProperties();
                    Int32 i = 0;
                    Int32 j = 0;
                    //打开Excel对象
                    XSSFWorkbook workbook = new XSSFWorkbook();

                    IDataFormat format = workbook.CreateDataFormat();
                    //set int format
                    ICellStyle cellStyleInt = workbook.CreateCellStyle();
                    cellStyleInt.DataFormat = format.GetFormat("#,##0");
                    cellStyleInt.WrapText = true;
                    //set decimal format
                    ICellStyle cellStyleDecimal = workbook.CreateCellStyle();
                    cellStyleDecimal.DataFormat = format.GetFormat("#,##0.0000");
                    //cellStyleDecimal.DataFormat = format.GetFormat("#,##0");
                    cellStyleDecimal.WrapText = true;
                    //set float format
                    ICellStyle cellStylefloat = workbook.CreateCellStyle();
                    cellStylefloat.DataFormat = HSSFDataFormat.GetBuiltinFormat("0%");
                    cellStylefloat.WrapText = true;
                    //set double format
                    ICellStyle cellStyledouble = workbook.CreateCellStyle();
                    cellStyledouble.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
                    cellStyledouble.WrapText = true;

                    ICellStyle cellStyleWrapText = workbook.CreateCellStyle();
                    cellStyleWrapText.WrapText = true;

                    //创建一个字体样式对象
                    IFont font = workbook.CreateFont();
                    font.Boldweight = (short)FontBoldWeight.Bold;

                    ICellStyle cellStyleWrapTextTitle = workbook.CreateCellStyle();
                    cellStyleWrapTextTitle.WrapText = true;
                    cellStyleWrapTextTitle.VerticalAlignment = VerticalAlignment.Center;
                    cellStyleWrapTextTitle.SetFont(font);

                    //Excel的Sheet对象
                    ISheet sheet = workbook.CreateSheet(title);
                    sheet.SetColumnWidth(0, 30 * 256);
                    sheet.SetColumnWidth(1, 15 * 256);
                    sheet.SetColumnWidth(2, 15 * 256);
                    sheet.SetColumnWidth(3, 22 * 256);

                    //生成sheet第一行列名 
                    IRow headerRow = sheet.CreateRow(0);

                    foreach (var item in head)
                    {
                        if (type.GetProperty(item.key) != null)
                        {
                            ICell cell = headerRow.CreateCell(j);
                            cell.CellStyle = cellStyleWrapTextTitle;
                            cell.SetCellValue(item.Name);
                            j++;
                        }
                    }

                    //生成sheet数据部分
                    j = 1;

                    foreach (var obj in list)
                    {
                        //Writelog(string.Format("【创建行开始】"));
                        IRow dataRow = sheet.CreateRow(j);
                        //Writelog(string.Format("【创建行结束】"));
                        i = 0;
                        foreach (var item in head)
                        {
                            //Writelog(string.Format("【列开始】行数{0},列名{1}", j, item.key));
                            PropertyInfo column = type.GetProperty(item.key);
                            if (column != null)
                            {

                                ICell cell = dataRow.CreateCell(i);
                                Type cellType = item.DataType;

                                if (column.GetValue(obj, null) != null)
                                {
                                    //整数123,456
                                    if (cellType == typeof(int))
                                    {
                                        cell.SetCellValue((int)column.GetValue(obj, null));

                                        cell.CellStyle = cellStyleInt;
                                    }
                                    //金额123,456
                                    else if (cellType == typeof(decimal))
                                    {
                                        cell.SetCellValue(Convert.ToDouble(column.GetValue(obj, null)));

                                        cell.CellStyle = cellStyleDecimal;
                                    }
                                    else if (cellType == typeof(float))
                                    {
                                        cell.SetCellValue(Convert.ToDouble(column.GetValue(obj, null)));

                                        cell.CellStyle = cellStylefloat;
                                    }

                                    else if (cellType == typeof(double))
                                    {
                                        cell.SetCellValue(Convert.ToDouble(column.GetValue(obj, null)));

                                        cell.CellStyle = cellStyledouble;

                                    }
                                    else
                                    {
                                        cell.SetCellValue(column.GetValue(obj, null).ToString());
                                        cell.CellStyle.WrapText = true;
                                    }
                                }
                                i++;
                            }
                        }
                        j++;
                    }

                    //保存excel文档
                    sheet.ForceFormulaRecalculation = true;
                    workbook.Write(filestream);
                    workbook.Clear();

                }

                return rootpath + FileName;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        /// <summary>
        /// 导出文件 
        /// </summary>
        /// <param name="list">数据集合List</param>
        /// <param name="head">列集合</param>
        /// <param name="title">导出文件名称</param>
        /// <param name="rootpath">导出路径</param>
        /// <returns></returns>
        public static string ExportListToExcelNew(IQueryable list, List<ExportObj> head, string title, string rootpath, string filename)
        {
            try
            {
                string FileName = filename.ToString() + "_" + title + ".xlsx";
                string strPath = Path.Combine(rootpath, FileName);
                if (Directory.Exists(rootpath) == false)
                {
                    Directory.CreateDirectory(rootpath);
                }

                //文件流对象
                using (FileStream filestream = new FileStream(strPath, FileMode.Create, FileAccess.Write))
                {
                    Type type = list.ElementType;
                    PropertyInfo[] properties = type.GetProperties();
                    Int32 i = 0;
                    Int32 j = 0;
                    //打开Excel对象
                    XSSFWorkbook workbook = new XSSFWorkbook();

                    IDataFormat format = workbook.CreateDataFormat();
                    //set int format
                    ICellStyle cellStyleInt = workbook.CreateCellStyle();
                    cellStyleInt.DataFormat = format.GetFormat("#,##0");
                    cellStyleInt.WrapText = true;
                    //set decimal format
                    ICellStyle cellStyleDecimal = workbook.CreateCellStyle();
                    cellStyleDecimal.DataFormat = format.GetFormat("#,##0.0000");
                    cellStyleDecimal.WrapText = true;
                    //set float format
                    ICellStyle cellStylefloat = workbook.CreateCellStyle();
                    cellStylefloat.DataFormat = HSSFDataFormat.GetBuiltinFormat("0%");
                    cellStylefloat.WrapText = true;
                    //set double format
                    ICellStyle cellStyledouble = workbook.CreateCellStyle();
                    cellStyledouble.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
                    cellStyledouble.WrapText = true;

                    ICellStyle cellStyleWrapText = workbook.CreateCellStyle();
                    cellStyleWrapText.WrapText = true;

                    //创建一个字体样式对象
                    IFont font = workbook.CreateFont();
                    font.Boldweight = (short)FontBoldWeight.Bold;

                    ICellStyle cellStyleWrapTextTitle = workbook.CreateCellStyle();
                    cellStyleWrapTextTitle.WrapText = true;
                    cellStyleWrapTextTitle.VerticalAlignment = VerticalAlignment.Center;
                    cellStyleWrapTextTitle.SetFont(font);

                    //Excel的Sheet对象
                    ISheet sheet = workbook.CreateSheet(title);
                    sheet.SetColumnWidth(0, 30 * 256);
                    sheet.SetColumnWidth(1, 15 * 256);
                    sheet.SetColumnWidth(2, 15 * 256);
                    sheet.SetColumnWidth(3, 22 * 256);

                    //生成sheet第一行列名 
                    IRow headerRow = sheet.CreateRow(0);

                    foreach (var item in head)
                    {
                        if (type.GetProperty(item.key) != null)
                        {
                            ICell cell = headerRow.CreateCell(j);
                            cell.CellStyle = cellStyleWrapTextTitle;
                            cell.SetCellValue(item.Name);
                            j++;
                        }
                    }

                    //生成sheet数据部分
                    j = 1;

                    foreach (var obj in list)
                    {
                        //Writelog(string.Format("【创建行开始】"));
                        IRow dataRow = sheet.CreateRow(j);
                        //Writelog(string.Format("【创建行结束】"));
                        i = 0;
                        foreach (var item in head)
                        {
                            //Writelog(string.Format("【列开始】行数{0},列名{1}", j, item.key));
                            PropertyInfo column = type.GetProperty(item.key);
                            if (column != null)
                            {

                                ICell cell = dataRow.CreateCell(i);
                                Type cellType = item.DataType;

                                if (column.GetValue(obj, null) != null)
                                {
                                    //整数123,456
                                    if (cellType == typeof(int))
                                    {
                                        cell.SetCellValue((int)column.GetValue(obj, null));

                                        cell.CellStyle = cellStyleInt;
                                    }
                                    //金额123,456
                                    else if (cellType == typeof(decimal))
                                    {
                                        cell.SetCellValue(Convert.ToDouble(column.GetValue(obj, null)));

                                        cell.CellStyle = cellStyleDecimal;
                                    }
                                    else if (cellType == typeof(float))
                                    {
                                        cell.SetCellValue(Convert.ToDouble(column.GetValue(obj, null)));

                                        cell.CellStyle = cellStylefloat;
                                    }

                                    else if (cellType == typeof(double))
                                    {
                                        cell.SetCellValue(Convert.ToDouble(column.GetValue(obj, null)));

                                        cell.CellStyle = cellStyledouble;

                                    }
                                    else
                                    {
                                        cell.SetCellValue(column.GetValue(obj, null).ToString());
                                        cell.CellStyle.WrapText = true;
                                    }
                                }
                                i++;
                            }
                        }
                        j++;
                    }

                    //保存excel文档
                    sheet.ForceFormulaRecalculation = true;
                    workbook.Write(filestream);
                    workbook.Clear();
                }

                return rootpath + FileName;
            }
            catch (Exception ex)
            {
                return "";
            }

        }



        #endregion
    }

}
