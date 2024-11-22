using MonthlyReport.BLL.Enums;

namespace MonthlyReport.BLL.Models
{
    public class Sort
    {
        public string? Property { get; set; }

        public string? PropertyName => Property?[0].ToString().ToUpper() + Property?[1..];

        public SortDirection? Direction { get; set; }
    }
}
