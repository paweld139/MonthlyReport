using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MonthlyReport.BLL.Enums;
using MonthlyReport.BLL.Interfaces;
using MonthlyReport.BLL.Models.Filter;
using MonthlyReport.DAL;
using MonthlyReport.DAL.Entities;
using MonthlyReport.DAL.Entities.New;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MonthlyReport.BLL.Services
{
    public class EntryService(MonthlyReportContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager) : IEntryService
    {
        static EntryService() => jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        private static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private HttpContext HttpContext => httpContextAccessor.HttpContext!;

        private string UserId => userManager.GetUserId(HttpContext.User)!;

        [SuppressMessage("Performance", "CA1862:Use the 'StringComparison' method overloads to perform case-insensitive string comparisons", Justification = "EF Core cannot use it")]
        public Task<List<Entry>> Get(string filter)
        {
            var entryFilter = JsonSerializer.Deserialize<EntryFilter>(filter, jsonSerializerOptions)!;

            var query = context.Entries
                .Where(e =>
                    (entryFilter.Task == null || e.Task.ToLower().Contains(entryFilter.Task.ToLower())) &&
                    (!entryFilter.DateFrom.HasValue || e.DateFrom >= entryFilter.DateFrom) &&
                    (!entryFilter.DateTo.HasValue || e.DateTo <= entryFilter.DateTo) &&
                    (!entryFilter.HoursFrom.HasValue || e.Hours >= entryFilter.HoursFrom) &&
                    (!entryFilter.HoursTo.HasValue || e.Hours <= entryFilter.HoursTo) &&
                    e.UserId == UserId
                );

            var sort = entryFilter.Sort;

            if (sort.PropertyName == null || sort.Direction == null)
            {
                query = query.OrderByDescending(e => e.DateTo);
            }
            else
            {
                query = sort.Direction switch
                {
                    SortDirection.Asc => query.OrderBy(e => EF.Property<object>(e, sort.PropertyName)),
                    SortDirection.Desc => query.OrderByDescending(e => EF.Property<object>(e, sort.PropertyName)),
                    _ => query
                };
            }

            return query.ToListAsync();
        }

        public Task Add(NewEntry newEntry)
        {
            var entry = mapper.Map<Entry>(newEntry);

            entry.UserId = UserId;

            context.Entries.Add(entry);

            return context.SaveChangesAsync();
        }

        public Task Delete(int id) => context.Entries
            .Where(e =>
                e.Id == id &&
                e.UserId == UserId
            )
            .ExecuteDeleteAsync();

        public Task Update(Entry entry)
        {
            entry.SetDatesAndHours();

            entry.UserId = UserId;

            context.Entries.Update(entry);

            return context.SaveChangesAsync();
        }
    }
}
