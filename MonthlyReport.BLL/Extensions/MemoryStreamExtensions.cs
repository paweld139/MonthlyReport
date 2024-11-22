namespace MonthlyReport.BLL.Extensions
{
    public static class MemoryStreamExtensions
    {
        public static void GoToStart(this MemoryStream stream) => stream.Position = 0;
    }
}
