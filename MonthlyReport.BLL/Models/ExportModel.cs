using MonthlyReport.BLL.Enums;
using System.Text.Json.Nodes;

namespace MonthlyReport.BLL.Models
{
    public class ExportModel
    {
        public required JsonArray Data { get; set; }

        public required ExportColumn[] Columns { get; set; }

        public ExportType ExportType { get; set; }

        public string FileName => $"export.{ExportType.ToString().ToLower()}";
    }
}
