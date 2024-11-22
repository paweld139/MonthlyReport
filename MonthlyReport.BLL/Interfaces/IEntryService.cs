using MonthlyReport.DAL.Entities;
using MonthlyReport.DAL.Entities.New;

namespace MonthlyReport.BLL.Interfaces
{
    public interface IEntryService
    {
        Task Add(NewEntry newEntry);
        Task Delete(int id);
        Task<List<Entry>> Get(string filter);
        Task Update(Entry entry);
    }
}