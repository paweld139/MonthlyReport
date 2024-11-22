namespace MonthlyReport.BLL.Models.Filter
{
    public class EntryFilter
    {
        public string? Task { get; set; }

        public DateTimeOffset? DateFrom { get; set; }

        public DateTimeOffset? DateTo { get; set; }

        public double? HoursFrom { get; set; }

        public double? HoursTo { get; set; }

        public required Sort Sort { get; set; }
    }
}