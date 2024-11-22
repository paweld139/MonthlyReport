using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MonthlyReport.DAL.Entities;

namespace MonthlyReport.DAL
{
    public class MonthlyReportSeeder(MonthlyReportContext context, UserManager<IdentityUser> userManager)
    {
        private async Task Migrate()
        {
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

            var shouldMigrate = pendingMigrations.Any();

            if (shouldMigrate)
                await context.Database.MigrateAsync();
        }

        private const string Email = "paweldywan@paweldywan.com";

        private async Task SeedUsers()
        {
            var exists = await context.Users.AnyAsync();

            if (!exists)
            {
                var user = new IdentityUser(Email)
                {
                    Email = Email
                };

                await userManager.CreateAsync(user, "P@ssw0rd");
            }
        }

        private async Task SeedEntries()
        {
            var exists = await context.Entries.AnyAsync();

            if (!exists)
            {
                var userId = await context.Users
                    .Where(u => u.Email == Email)
                    .Select(u => u.Id)
                    .SingleAsync();

                var entries = new List<Entry>
                {
                    new()
                    {
                        Task = "Task 1",
                        DateFrom = new DateTime(2024, 9, 1, 8, 0, 0),
                        DateTo = new DateTime(2024, 9, 1, 16, 0, 0),
                        UserId = userId,
                        Hours = 8
                    },
                    new()
                    {
                        Task = "Task 2",
                        DateFrom = new DateTime(2024, 9, 2, 8, 0, 0),
                        DateTo = new DateTime(2024, 9, 2, 16, 0, 0),
                        UserId = userId,
                        Hours = 8
                    },
                    new()
                    {
                        Task = "Task 3",
                        DateFrom = new DateTime(2024, 9, 3, 8, 0, 0),
                        DateTo = new DateTime(2024, 9, 3, 16, 0, 0),
                        UserId = userId,
                        Hours = 8
                    },
                    new()
                    {
                        Task = "Task 4",
                        DateFrom = new DateTime(2024, 9, 4, 8, 0, 0),
                        DateTo = new DateTime(2024, 9, 4, 16, 0, 0),
                        UserId = userId,
                        Hours = 8
                    },
                    new()
                    {
                        Task = "Task 5",
                        DateFrom = new DateTime(2024, 9, 5, 8, 0, 0),
                        DateTo = new DateTime(2024, 9, 5, 16, 0, 0),
                        UserId = userId,
                        Hours = 8
                    },
                    new()
                    {
                        Task = "Task 6",
                        DateFrom = new DateTime(2024, 9, 6, 8, 0, 0),
                        DateTo = new DateTime(2024, 9, 6, 16, 0, 0),
                        UserId = userId,
                        Hours = 8
                    },
                    new()
                    {
                        Task = "Task 7",
                        DateFrom = new DateTime(2024, 9, 7, 8, 0, 0),
                        DateTo = new DateTime(2024, 9, 7, 16, 0, 0),
                        UserId = userId,
                        Hours = 8
                    },
                    new()
                    {
                        Task = "Task 8",
                        DateFrom = new DateTime(2024, 9, 8, 8, 0, 0),
                        DateTo = new DateTime(2024, 9, 8, 16, 0, 0),
                        UserId = userId,
                        Hours = 8
                    },
                    new()
                    {
                        Task = "Task 9",
                        DateFrom = new DateTime(2024, 9, 9, 8, 0, 0),
                        DateTo = new DateTime(2024, 9, 9, 16, 0, 0),
                        UserId = userId,
                        Hours = 8
                    },
                    new()
                    {
                        Task = "Task 10",
                        DateFrom = new DateTime(2024, 9, 10, 8, 0, 0),
                        DateTo = new DateTime(2024, 9, 10, 16, 0, 0),
                        UserId = userId,
                        Hours = 8
                    }
                };

                context.Entries.AddRange(entries);

                await context.SaveChangesAsync();
            }
        }

        public async Task Seed()
        {
            await Migrate();

            await SeedUsers();

            await SeedEntries();
        }
    }
}
