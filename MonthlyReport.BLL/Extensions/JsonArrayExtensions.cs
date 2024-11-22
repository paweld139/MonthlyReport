using MonthlyReport.BLL.Models;
using System.Text;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace MonthlyReport.BLL.Extensions
{
    public static class JsonArrayExtensions
    {
        public static XDocument ToXmlDocument(this JsonArray jsonArray, string rootElementName = "Root")
        {
            var root = new XElement(rootElementName);

            foreach (var jsonElement in jsonArray)
            {
                if (jsonElement is JsonObject jsonObject)
                {
                    var element = new XElement("Item");

                    foreach (var property in jsonObject)
                    {
                        element.Add(new XElement(property.Key, property.Value?.ToString()));
                    }

                    root.Add(element);
                }
            }

            return new XDocument(root);
        }

        public static string ToTxt(this ExportModel exportModel)
        {
            var result = new StringBuilder();

            if (exportModel.Data.Count > 0)
            {
                var columnWidths = GetColumnWidths(exportModel);

                AddColumns(exportModel, result, columnWidths);

                AddRows(exportModel, result, columnWidths);
            }

            return result.ToString();
        }

        private static void AddRows(ExportModel exportModel, StringBuilder result, int[] columnWidths)
        {
            foreach (var jsonElement in exportModel.Data)
            {
                var index = 0;

                foreach (var column in exportModel.Columns)
                {
                    if (jsonElement is JsonObject jsonObject)
                    {
                        var value = jsonObject[column.Property].GetStringValue() ?? string.Empty;

                        result.Append(value.PadRight(columnWidths[index] + 1));
                    }

                    index++;
                }

                result.AppendLine();
            }
        }

        private static void AddColumns(ExportModel exportModel, StringBuilder result, int[] columnWidths)
        {
            var index = 0;

            foreach (var column in exportModel.Columns)
            {
                result.Append(column.Name.PadRight(columnWidths[index] + 1));

                index++;
            }

            result.AppendLine();
        }

        private static int[] GetColumnWidths(ExportModel exportModel)
        {
            var columnWidths = new int[exportModel.Columns.Length];

            var index = 0;

            foreach (var column in exportModel.Columns)
            {
                foreach (var jsonElement in exportModel.Data)
                {
                    if (jsonElement is JsonObject jsonObject)
                    {
                        var value = jsonObject[column.Property].GetStringValue() ?? string.Empty;

                        columnWidths[index] = value.Length > columnWidths[index] ? value.Length : columnWidths[index];
                    }
                }

                index++;
            }

            return columnWidths;
        }
    }
}