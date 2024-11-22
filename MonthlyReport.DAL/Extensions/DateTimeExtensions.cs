namespace MonthlyReport.DAL.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTimeOffset TruncateSeconds(this DateTimeOffset dateTime)
        {
            return new DateTimeOffset(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, dateTime.Offset);
        }
    }
}
