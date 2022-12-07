using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Drawing;

namespace Domain.Exporters
{
    public class ExcelExporter : IDisposable
    {
        private MemoryStream stream;
        private XSSFWorkbook workbook;
        private ISheet currentSheet;
        private IRow headerRow;
        private IRow currentRow;
        private XSSFCellStyle headerStyle;
        private XSSFCellStyle defaultStyle;
        private XSSFCellStyle numericStyle;

        public void CreateDocument()
        {
            if (HasOpenDocument())
            {
                throw new ArgumentException("Um documento já está aberto");
            }

            workbook = new XSSFWorkbook();

            var headerFont = (XSSFFont)workbook.CreateFont();
            headerFont.FontName = "Calibri";
            headerFont.IsBold = true;
            headerFont.FontHeightInPoints = 12;
            headerFont.SetColor(new XSSFColor(Color.FromArgb(68, 84, 106)));

            headerStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            headerStyle.SetBlackBorders();
            headerStyle.SetFont(headerFont);
            headerStyle.SetFillForegroundColor(new XSSFColor(Color.FromArgb(242, 242, 242)));
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.Alignment = HorizontalAlignment.Center;

            var defaultFont = workbook.CreateFont();
            defaultFont.FontName = "Calibri";
            defaultFont.FontHeightInPoints = 11;

            defaultStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            defaultStyle.SetBlackBorders();
            defaultStyle.SetFont(defaultFont);

            numericStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            numericStyle.SetBlackBorders();
            numericStyle.SetFont(defaultFont);
            numericStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00");
        }

        public MemoryStream EndDocument()
        {
            if (!HasOpenDocument())
            {
                throw new ArgumentException("Um documento deve estar aberto antes de usar esta operação");
            }

            if (currentSheet != null)
            {
                currentSheet.AutoSizeColumns();
            }

            stream = new MemoryStream();
            workbook.Write(stream, leaveOpen: true);
            workbook.Close();
            workbook = null;
            return stream;
        }

        public void CreateSheet(string name)
        {
            if (!HasOpenDocument())
            {
                throw new ArgumentException("Um documento deve estar aberto antes de usar esta operação");
            }

            if (currentSheet != null)
            {
                currentSheet.AutoSizeColumns();
            }

            currentSheet = workbook.CreateSheet(name);
            headerRow = currentSheet.CreateRow(0);
        }

        public void AddHeaderColumns(string[] names)
        {
            if (!HasOpenDocument())
            {
                throw new ArgumentException("Um documento deve estar aberto antes de usar esta operação");
            }

            if (currentSheet == null)
            {
                throw new ArgumentException("\r\nUma folha deve ser criada antes de usar esta operação");
            }

            foreach (var name in names)
            {
                var cell = headerRow.CreateCell();
                cell.SetCellValue(name);
                cell.CellStyle = headerStyle;
            }
        }

        public void AddHeaderColumn(string name)
        {
            if (!HasOpenDocument())
            {
                throw new ArgumentException("Um documento deve estar aberto antes de usar esta operação");
            }

            if (currentSheet == null)
            {
                throw new ArgumentException("Uma folha deve ser criada antes de usar esta operação");
            }

            var cell = headerRow.CreateCell();
            cell.SetCellValue(name);
            cell.CellStyle = headerStyle;
        }

        public void AddRow()
        {
            if (!HasOpenDocument())
            {
                throw new ArgumentException("Um documento deve estar aberto antes de usar esta operação");
            }

            if (currentSheet == null)
            {
                throw new ArgumentException("Uma folha deve ser criada antes de usar esta operação");
            }

            currentRow = currentSheet.CreateRow();
        }

        public void AddRowValue(string value)
        {
            ValidateRow();
            var cell = currentRow.CreateCell();
            cell.CellStyle = defaultStyle;
            cell.SetCellType(CellType.String);
            cell.SetCellValue(value);
        }

        public void AddRowValue(int value)
        {
            AddRowValue(value.ToString());
        }

        public void AddRowValue(decimal value)
        {
            ValidateRow();
            var cell = currentRow.CreateCell();
            cell.CellStyle = numericStyle;
            cell.SetCellValue((double)value);
        }

        public void AddRowValue(bool value)
        {
            ValidateRow();
            var cell = currentRow.CreateCell();
            cell.CellStyle = defaultStyle;
            cell.SetCellValue(value ? "Sim" : "Não");
        }

        private void ValidateRow()
        {
            if (!HasOpenDocument())
            {
                throw new ArgumentException("Um documento deve estar aberto antes de usar esta operação");
            }

            if (currentSheet == null)
            {
                throw new ArgumentException("Uma folha deve ser criada antes de usar esta operação");
            }

            if (currentRow == null)
            {
                throw new ArgumentException("Uma linha deve ser adicionada antes de usar esta operação");
            }
        }

        private bool HasOpenDocument()
        {
            return workbook != null;
        }

        public void Dispose()
        {
            workbook?.Close();
            stream?.Dispose();
        }
    }

    public static class XSSFExtensions
    {
        public static IRow CreateRow(this ISheet sheet)
        {
            return sheet.CreateRow(sheet.LastRowNum + 1);
        }

        public static ICell CreateCell(this IRow row)
        {
            return row.CreateCell(row.LastCellNum == -1 ? 0 : row.LastCellNum);
        }

        public static void AutoSizeColumns(this ISheet sheet)
        {
            var header = sheet.GetRow(0);
            if (header != null)
            {
                foreach (var cell in header.Cells)
                {
                    sheet.AutoSizeColumn(cell.ColumnIndex);
                }
            }
        }

        public static void SetBlackBorders(this ICellStyle style)
        {
            style.BorderBottom = BorderStyle.Thin;
            style.BottomBorderColor = IndexedColors.Black.Index;

            style.BorderTop = BorderStyle.Thin;
            style.TopBorderColor = IndexedColors.Black.Index;

            style.BorderRight = BorderStyle.Thin;
            style.RightBorderColor = IndexedColors.Black.Index;

            style.BorderLeft = BorderStyle.Thin;
            style.LeftBorderColor = IndexedColors.Black.Index;
        }
    }
}
