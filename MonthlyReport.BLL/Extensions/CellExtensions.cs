using NPOI.SS.UserModel;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MonthlyReport.BLL.Extensions
{
    public static class CellExtensions
    {
        public static void SetCellValue(this ICell cell, JsonNode? value)
        {
            switch (value?.GetValueKind())
            {
                case JsonValueKind.Number:
                    cell.SetCellValue(value.GetValue<double>());
                    break;
                case JsonValueKind.String:
                    var stringValue = value.GetValue<string>();

                    if (DateTime.TryParse(stringValue, out DateTime dateValue))
                        cell.SetCellValue(dateValue.ToString(CultureInfo.CurrentUICulture));
                    else
                        cell.SetCellValue(stringValue);
                    break;
                case JsonValueKind.True:
                    cell.SetCellValue(true);
                    break;
                case JsonValueKind.False:
                    cell.SetCellValue(false);
                    break;
                case JsonValueKind.Null:
                    cell.SetCellValue("");
                    break;
                default:
                    cell.SetCellValue(value?.GetValue<string>());
                    break;
            }
        }
    }
}
