using AutoMapper;
using MonthlyReport.DAL.Entities;
using MonthlyReport.DAL.Entities.New;

namespace MonthlyReport.DAL
{
    public class MonthlyReportMappingProfile : Profile
    {
        public MonthlyReportMappingProfile()
        {
            CreateMap<NewEntry, Entry>()
                .AfterMap((src, dest) => dest.SetDatesAndHours());
        }
    }
}
