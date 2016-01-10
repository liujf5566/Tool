using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExportData.Helper
{
    public class ExcelCreate
    {
        static XSSFColor _scoreColor;
        static XSSFColor _winRateColor;
        static XSSFColor _bgColor1;
        static XSSFColor _bgColor2;
        static ExcelCreate()
        {
            _scoreColor = new XSSFColor();
            _scoreColor.SetRgb(new byte[] { 255, 153, 58 });
            _winRateColor = new XSSFColor();
            _winRateColor.SetRgb(new byte[] { 255, 0, 0 });

            _bgColor1 = new XSSFColor();
            _bgColor1.SetRgb(new byte[] { 112, 206, 188 });
            _bgColor2 = new XSSFColor();
            _bgColor2.SetRgb(new byte[] { 220, 230, 241 });

        }
        static IFont _normalFont;
        static IFont _scoreFont;
        static IFont _winRateFont;
        static ICellStyle _normalLeftCellStyle;
        static ICellStyle _normalCenterCellStyle;
        static ICellStyle _aquaCellStyle;
        static ICellStyle _greycellStyle;
        static ICellStyle _justifyLeftCellStyle;

        private static void CreateStyle(XSSFWorkbook workbook)
        {
            //获取字体
            _normalFont = GetFontStyle(workbook, "宋体", null, 14);
            _scoreFont = GetFontStyle(workbook, "宋体", _scoreColor, 24);
            _winRateFont = GetFontStyle(workbook, "宋体", _winRateColor, 14);
            //获取样式
            _normalLeftCellStyle = GetCellStyle(workbook, _normalFont, null, FillPattern.NoFill, null, HorizontalAlignment.Left, VerticalAlignment.Center);
            _normalCenterCellStyle = GetCellStyle(workbook, _normalFont, null, FillPattern.NoFill, null, HorizontalAlignment.Center, VerticalAlignment.Center);
            _aquaCellStyle = GetCellStyle(workbook, _normalFont, _bgColor1, FillPattern.SolidForeground, null, HorizontalAlignment.Left, VerticalAlignment.Center);
            _greycellStyle = GetCellStyle(workbook, _normalFont, _bgColor2, FillPattern.SolidForeground, null, HorizontalAlignment.Center, VerticalAlignment.Center);
            _justifyLeftCellStyle = GetCellStyle(workbook, _normalFont, null, FillPattern.NoFill, null, HorizontalAlignment.Left, VerticalAlignment.Justify);
        }
        /// <summary>
        /// 创建Excel
        /// </summary>
        /// <param name="pperformance">个人绩效内存数据</param>
        /// <param name="path">附件所在路径</param>
        /// <param name="scores">排序后的分数列表</param>
        public static void Create(PersonalPerformance pperformance, string path, List<float> scores)
        {
            try
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                CreateStyle(workbook);
                ISheet sheet = workbook.CreateSheet("Sheet1");
                //设置列宽
                sheet.SetColumnWidth(0, (int)(10 + 0.72) * 256);
                sheet.SetColumnWidth(1, (int)(15 + 0.72) * 256);
                sheet.SetColumnWidth(2, (int)(65 + 0.72) * 256);
                sheet.SetColumnWidth(3, (int)(10 + 0.72) * 256);
                sheet.SetColumnWidth(4, (int)(10 + 0.72) * 256);

                //创建行和列
                //行一
                IRow row = sheet.CreateRow(0);
                row.HeightInPoints = 30;
                ICell cell = row.CreateCell(0);
                XSSFRichTextString richText = new XSSFRichTextString("  个人绩效对账单");
                richText.ApplyFont(_normalFont);
                cell.CellStyle = _aquaCellStyle;
                cell.SetCellValue(richText);
                SetCellRangeAddress(sheet, 0, 0, 0, 1);
                cell = row.CreateCell(2);
                cell.CellStyle = _aquaCellStyle;
                cell = row.CreateCell(3);
                cell.CellStyle = _aquaCellStyle;
                cell.SetCellValue(pperformance.Department);
                SetCellRangeAddress(sheet, 0, 0, 3, 4);

                //行二
                row = sheet.CreateRow(1);
                row.HeightInPoints = 90;
                cell = row.CreateCell(0);
                string g = string.Empty;
                float score = pperformance.Person.Score;
                if (!string.IsNullOrEmpty(pperformance.Person.Grade))
                {
                    string[] gs = pperformance.Person.Grade.Split('-');
                    g = gs[0];
                }
                string str = string.Format("        {0}  {1}月绩效等级{2}，绩效分值{3}", pperformance.Person.Name, pperformance.Month, g, score);
                cell.CellStyle = _normalLeftCellStyle;
                cell.SetCellValue(str);
                SetCellRangeAddress(sheet, 1, 1, 0, 4);
                //行三
                row = sheet.CreateRow(2);
                row.HeightInPoints = 30;
                cell = row.CreateCell(0);
                int index = scores.FindIndex(sc => sc == score) + 1;
                float winRate = 0;
                if (index != 1)
                {
                    winRate = ((float)index / (float)(scores.Count)) * 100f;
                }
                str = string.Format("        你打败了{0}", pperformance.Department);
                richText = new XSSFRichTextString(str);
                richText.Append(string.Format("{0}%", winRate.ToString("f0")), _winRateFont as XSSFFont);
                richText.Append("的成员", _normalFont as XSSFFont);
                cell.SetCellValue(richText);
                cell.CellStyle = _aquaCellStyle;
                SetCellRangeAddress(sheet, 2, 2, 0, 4);

                //行四
                row = sheet.CreateRow(3);
                row.HeightInPoints = 30;
                cell = row.CreateCell(0);
                cell.SetCellValue("序号");
                cell.CellStyle = _greycellStyle;
                cell = row.CreateCell(1);
                cell.SetCellValue("任务名称");
                cell.CellStyle = _greycellStyle;
                cell = row.CreateCell(2);
                SetCellRangeAddress(sheet, 3, 3, 1, 2);
                cell.CellStyle = _greycellStyle;
                cell = row.CreateCell(3);
                cell.SetCellValue("任务得分");
                SetCellRangeAddress(sheet, 3, 3, 3, 4);
                cell.CellStyle = _greycellStyle;
                //添加任务
                int i = 1;
                int rowIndex = 4;
                foreach (var task in pperformance.Tasks)
                {
                    row = sheet.CreateRow(rowIndex);
                    row.HeightInPoints = 30;
                    cell = row.CreateCell(0);
                    cell.SetCellValue(i);
                    if (i % 2 == 0)
                    {
                        cell.CellStyle = _greycellStyle;
                    }
                    else
                    {
                        cell.CellStyle = _normalCenterCellStyle;
                    }
                    cell = row.CreateCell(1);
                    cell.SetCellValue(task.Name);
                    if (i % 2 == 0)
                    {
                        cell.CellStyle = _greycellStyle;
                    }
                    else
                    {
                        cell.CellStyle = _normalCenterCellStyle;
                    }
                    cell = row.CreateCell(2);
                    SetCellRangeAddress(sheet, rowIndex, rowIndex, 1, 2);
                    if (i % 2 == 0)
                    {
                        cell.CellStyle = _greycellStyle;
                    }
                    else
                    {
                        cell.CellStyle = _normalCenterCellStyle;
                    }
                    cell = row.CreateCell(3);
                    cell.SetCellValue(task.Score);
                    SetCellRangeAddress(sheet, rowIndex, rowIndex, 3, 4);
                    if (i % 2 == 0)
                    {
                        cell.CellStyle = _greycellStyle;
                    }
                    else
                    {
                        cell.CellStyle = _normalCenterCellStyle;
                    }
                    i++;
                    rowIndex++;
                }
                //简评
                row = sheet.CreateRow(rowIndex);
                row.HeightInPoints = 30;
                cell = row.CreateCell(0);
                cell.SetCellValue("      简评");
                cell.CellStyle = _aquaCellStyle;
                SetCellRangeAddress(sheet, rowIndex, rowIndex, 0, 4);

                row = sheet.CreateRow(++rowIndex);
                row.HeightInPoints = 30;
                cell = row.CreateCell(0);
                str = string.Format("      {0}月工作包数量{1}，平均分值{2}", pperformance.Month, pperformance.Person.WorkBagNum, pperformance.Person.AvgScore);
                cell.SetCellValue(str);
                cell.CellStyle = _normalLeftCellStyle;
                SetCellRangeAddress(sheet, rowIndex, rowIndex, 0, 4);

                row = sheet.CreateRow(++rowIndex);
                row.HeightInPoints = 30;
                cell = row.CreateCell(0);
                str = string.Format("      {0}月加减分值{1}，加减分值说明：{2}", pperformance.Month, pperformance.Person.AddSubScore, pperformance.Person.AddSubExplain);
                cell.SetCellValue(str);
                cell.CellStyle = _normalLeftCellStyle;
                SetCellRangeAddress(sheet, rowIndex, rowIndex, 0, 4);

                row = sheet.CreateRow(++rowIndex);
                row.HeightInPoints = 30;
                cell = row.CreateCell(0);
                str = string.Format("领导评语：{0}", pperformance.Person.LeadComment);
                cell.SetCellValue(str);
                cell.CellStyle = _normalLeftCellStyle;
                SetCellRangeAddress(sheet, rowIndex, rowIndex, 0, 4);

                row = sheet.CreateRow(++rowIndex);
                row.HeightInPoints = 40;
                cell = row.CreateCell(0);
                str = "说明：绩效分值参考工作包平均分值采取10分制：A级10分，B级8分；C级5分；D级0分；加减分项在此基础上执行。";
                cell.SetCellValue(str);
                cell.CellStyle = _justifyLeftCellStyle;
                SetCellRangeAddress(sheet, rowIndex, rowIndex, 0, 4);

                using (FileStream stream = File.OpenWrite(path))
                {
                    workbook.Write(stream);
                    stream.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception("创建Excel出错！");
            }
        }
        /// <summary>
        /// 获取字体样式
        /// </summary>
        /// <param name="hssfworkbook">Excel操作类</param>
        /// <param name="fontfamily">字体名</param>
        /// <param name="fontcolor">字体颜色</param>
        /// <param name="fontsize">字体大小</param>
        /// <param name="bold">是否加粗</param>
        /// <param name="typeOffset">上下标</param>
        /// <returns></returns>
        public static IFont GetFontStyle(XSSFWorkbook hssfworkbook, string fontfamily, XSSFColor fontcolor, int fontsize, FontBoldWeight bold = FontBoldWeight.Normal, FontSuperScript typeOffset = FontSuperScript.None)
        {
            XSSFFont xfont = hssfworkbook.CreateFont() as XSSFFont;
            if (string.IsNullOrEmpty(fontfamily))
            {
                xfont.FontName = fontfamily;
            }
            if (fontcolor != null)
            {
                xfont.SetColor(fontcolor);
            }
            xfont.IsItalic = false;
            xfont.FontHeightInPoints = (short)fontsize;
            xfont.Boldweight = (short)bold;
            xfont.TypeOffset = typeOffset;
            return xfont;
        }

        /// <summary>
        /// 获取单元格样式
        /// </summary>
        /// <param name="xssfworkbook">Excel操作类</param>
        /// <param name="font">单元格字体</param>
        /// <param name="fillForegroundColor">图案的颜色</param>
        /// <param name="fillPattern">图案样式</param>
        /// <param name="fillBackgroundColor">单元格背景</param>
        /// <param name="ha">垂直对齐方式</param>
        /// <param name="va">垂直对齐方式</param>
        /// <returns></returns>
        public static ICellStyle GetCellStyle(XSSFWorkbook xssfworkbook, IFont font, XSSFColor fillForegroundColor, FillPattern fillPattern, XSSFColor fillBackgroundColor, HorizontalAlignment ha, VerticalAlignment va)
        {
            XSSFCellStyle cellstyle = xssfworkbook.CreateCellStyle() as XSSFCellStyle;
            cellstyle.FillPattern = fillPattern;
            cellstyle.Alignment = ha;
            cellstyle.VerticalAlignment = va;
            if (fillForegroundColor != null)
            {
                cellstyle.SetFillForegroundColor(fillForegroundColor);
            }
            if (fillBackgroundColor != null)
            {
                cellstyle.SetFillBackgroundColor(fillBackgroundColor);
            }
            if (font != null)
            {
                cellstyle.SetFont(font);
            }
            return cellstyle;
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }
    }
}
