using MonthlyReport.BLL.Models;

namespace MonthlyReport.BLL.Interfaces
{
    public interface IExportService
    {
        Task<(Stream stream, string fileName, string mimeType)> Export(ExportModel exportModel);
    }
}