using NPOI.SS.UserModel;

namespace MonthlyReport.BLL.Extensions
{
    public static class WorkbookExtensions
    {
        public static MemoryStream GetMemoryStream(this IWorkbook workbook)
        {
            var stream = new MemoryStream();

            workbook.Write(stream, true);

            stream.GoToStart();

            return stream;
        }
    }
}
