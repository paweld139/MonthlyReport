using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonthlyReport.DAL.Entities;

namespace MonthlyReport.DAL
{
    public class MonthlyReportContext(DbContextOptions<MonthlyReportContext> options) : IdentityDbContext(options)
    {
        public DbSet<Entry> Entries { get; set; }
    }
}
