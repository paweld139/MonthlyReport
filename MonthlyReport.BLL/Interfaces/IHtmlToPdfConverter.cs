namespace MonthlyReport.BLL.Interfaces
{
    public interface IHtmlToPdfConverter
    {
        Task<Stream> GetPdf<T>(T model, string templatePath);
    }
}