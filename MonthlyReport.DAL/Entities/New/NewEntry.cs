using MonthlyReport.DAL.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MonthlyReport.DAL.Entities.New
{
    public class NewEntry
    {
        [Required]
        public required string Task { get; set; }

        public DateTimeOffset DateFrom { get; set; }

        [DateGreaterThan(nameof(DateFrom))]
        public DateTimeOffset DateTo { get; set; }
    }
}
