using MonthlyReport.BLL.Models;
using NPOI.SS.UserModel;

namespace MonthlyReport.BLL.Extensions
{
    public static class SheetExtensions
    {
        public static void AddRows(this ISheet sheet, int rowIndex, ExportModel exportModel)
        {
            foreach (var item in exportModel.Data)
            {
                if (item == null)
                    continue;

                var row = sheet.CreateRow(rowIndex);

                var colIndex = 0;

                foreach (var column in exportModel.Columns)
                {
                    var cell = row.CreateCell(colIndex);

                    var property = item.AsObject().AsEnumerable().Single(p => p.Key == column.Property);

                    cell.SetCellValue(property.Value);

                    colIndex++;
                }

                rowIndex++;
            }
        }

        public static void AutoSizeColumns(this ISheet sheet)
        {
            for (var i = 0; i < sheet.GetRow(0).PhysicalNumberOfCells; i++)
            {
                sheet.AutoSizeColumn(i);
            }
        }

        public static void AddHeader(this ISheet sheet, out int rowIndex, ExportModel exportModel)
        {
            rowIndex = 0;

            var headerRow = sheet.CreateRow(rowIndex);

            var colIndex = 0;

            var columnNames = exportModel.Columns.Select(c => c.Name);

            foreach (var column in columnNames)
            {
                headerRow.CreateCell(colIndex).SetCellValue(column);

                colIndex++;
            }

            rowIndex++;
        }
    }
}
