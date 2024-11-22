using Microsoft.AspNetCore.Identity;
using MonthlyReport.DAL.Entities.New;
using MonthlyReport.DAL.Extensions;

namespace MonthlyReport.DAL.Entities
{
    public class Entry : NewEntry
    {
        public int Id { get; set; }

        public double Hours { get; set; }

        public required string UserId { get; set; }

        public virtual IdentityUser? User { get; set; }

        public void SetDatesAndHours()
        {
            DateFrom = DateFrom.TruncateSeconds();

            DateTo = DateTo.TruncateSeconds();

            Hours = Math.Round((DateTo.TruncateSeconds() - DateFrom.TruncateSeconds()).TotalHours, 2);
        }
    }
}
