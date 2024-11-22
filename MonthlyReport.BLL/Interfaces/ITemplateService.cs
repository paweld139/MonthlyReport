namespace MonthlyReport.BLL.Interfaces
{
    public interface ITemplateService
    {
        Task<string> RenderAsync<TViewModel>(string templateFileName, TViewModel viewModel);
    }
}
