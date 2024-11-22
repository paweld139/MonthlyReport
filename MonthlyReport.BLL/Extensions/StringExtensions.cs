using System.Text;

namespace MonthlyReport.BLL.Extensions
{
    public static class StringExtensions
    {
        public static string CapitalizeFirstLetter(this string input) => $"{char.ToUpper(input[0])}{input[1..]}";

        public static MemoryStream ToStream(this string input) => new(Encoding.UTF8.GetBytes(input));
    }
}
