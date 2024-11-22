using CsvHelper;
using MonthlyReport.BLL.Models;
using System.Dynamic;

namespace MonthlyReport.BLL.Extensions
{
    public static class CsvWriterExtensions
    {
        public static Task AddRows(this CsvWriter csv, ExportModel exportModel)
        {
            var records = new List<object>();

            foreach (var record in exportModel.Data)
            {
                var recordDictionary = new ExpandoObject() as IDictionary<string, object?>;

                if (record == null)
                    continue;

                foreach (var column in exportModel.Columns)
                {
                    var property = record.AsObject().AsEnumerable().Single(p => p.Key == column.Property);

                    recordDictionary[property.Key] = property.GetValue();
                }

                records.Add(recordDictionary);
            }

            return csv.WriteRecordsAsync(records);
        }

        public static Task AddHeader(this CsvWriter csv, ExportModel exportModel)
        {
            var columnNames = exportModel.Columns
                .Select(c => c.Name)
                .ToArray();

            csv.WriteField(columnNames);

            return csv.NextRecordAsync();
        }
    }
}
