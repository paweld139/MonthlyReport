namespace MonthlyReport.BLL.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime TruncateSeconds(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, dateTime.Kind);
        }
    }
}
