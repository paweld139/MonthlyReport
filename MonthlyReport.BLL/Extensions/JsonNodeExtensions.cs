using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MonthlyReport.BLL.Extensions
{
    public static class JsonNodeExtensions
    {
        public static object? GetValue(this KeyValuePair<string, JsonNode?> property)
        {
            var value = property.Value;

            var kind = value?.GetValueKind();

            return kind switch
            {
                JsonValueKind.Number => value?.GetValue<double>(),
                JsonValueKind.String when DateTime.TryParse(value?.GetValue<string>(), out DateTime dateValue) => dateValue,
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.Null => null,
                _ => value?.GetValue<string>()
            };
        }

        public static string? GetStringValue(this JsonNode? property)
        {
            var kind = property?.GetValueKind();

            return kind switch
            {
                JsonValueKind.String when DateTime.TryParse(property?.GetValue<string>(), out DateTime dateValue) => dateValue.ToString(CultureInfo.CurrentUICulture),
                JsonValueKind.Number => property?.GetValue<double>().ToString(CultureInfo.CurrentUICulture),
                _ => property?.GetValue<string>()
            };
        }

        public static string? GetStringValue(this KeyValuePair<string, JsonNode?> property) => property.Value.GetStringValue();
    }
}
