using CsvHelper;
using CsvHelper.Configuration;
using MonthlyReport.BLL.Constants;
using MonthlyReport.BLL.Enums;
using MonthlyReport.BLL.Extensions;
using MonthlyReport.BLL.Interfaces;
using MonthlyReport.BLL.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Globalization;
using System.Text.Json;
using System.Xml;

namespace MonthlyReport.BLL.Services
{
    public class ExportService(IHtmlToPdfConverter htmlToPdfConverter, ITemplateService templateService) : IExportService
    {
        private static (Stream stream, string fileName, string mimeType) ExportToExcel(ExportModel exportModel)
        {
            var isXlsx = exportModel.ExportType == ExportType.Xlsx;

            using IWorkbook workbook = isXlsx ? new XSSFWorkbook() : new HSSFWorkbook();

            var sheet = workbook.CreateSheet(ExportConstants.SheetName);

            sheet.AddHeader(out var rowIndex, exportModel);

            sheet.AddRows(rowIndex, exportModel);

            sheet.AutoSizeColumns();

            var stream = workbook.GetMemoryStream();

            return (stream, exportModel.FileName, isXlsx ? ExportConstants.XlsxMimeType : ExportConstants.XlsxMimeType);
        }

        private static async Task<(Stream stream, string fileName, string mimeType)> ExportToCsv(ExportModel exportModel)
        {
            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, leaveOpen: true))
            {
                using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false
                });

                await csv.AddHeader(exportModel);

                await csv.AddRows(exportModel);
            }

            stream.GoToStart();

            return (stream, exportModel.FileName, ExportConstants.CsvMimeType);
        }

        private async Task<(Stream stream, string fileName, string mimeType)> ExportToPdf(ExportModel exportModel)
        {
            var pdfStream = await htmlToPdfConverter.GetPdf(exportModel, ExportConstants.TemplatePath);

            return (pdfStream, exportModel.FileName, ExportConstants.PdfMimeType);
        }

        private async Task<(Stream stream, string fileName, string mimeType)> ExportToHtml(ExportModel exportModel)
        {
            var html = await templateService.RenderAsync(ExportConstants.TemplatePath, exportModel);

            var htmlStream = html.ToStream();

            return (htmlStream, exportModel.FileName, ExportConstants.HtmlMimeType);
        }

        private static (Stream stream, string fileName, string mimeType) ExportToJson(ExportModel exportModel)
        {
            var stream = new MemoryStream();

            using (var writer = new Utf8JsonWriter(stream))
            {
                exportModel.Data.WriteTo(writer);
            }

            stream.GoToStart();

            return (stream, exportModel.FileName, ExportConstants.JsonMimeType);
        }

        private static async Task<(Stream stream, string fileName, string mimeType)> ExportToXml(ExportModel exportModel)
        {
            var stream = new MemoryStream();

            var xmlDocument = exportModel.Data.ToXmlDocument();

            using (var writer = XmlWriter.Create(stream))
            {
                await xmlDocument.WriteToAsync(writer, CancellationToken.None);
            }

            stream.GoToStart();

            return (stream, exportModel.FileName, ExportConstants.XmlMimeType);
        }

        private static (Stream stream, string fileName, string mimeType) ExportToTxt(ExportModel exportModel)
        {
            var txt = exportModel.ToTxt();

            var txtStream = txt.ToStream();

            return (txtStream, exportModel.FileName, ExportConstants.TxtMimeType);
        }

        public async Task<(Stream stream, string fileName, string mimeType)> Export(ExportModel exportModel) => exportModel.ExportType switch
        {
            ExportType.Xls => ExportToExcel(exportModel),
            ExportType.Xlsx => ExportToExcel(exportModel),
            ExportType.Csv => await ExportToCsv(exportModel),
            ExportType.Pdf => await ExportToPdf(exportModel),
            ExportType.Html => await ExportToHtml(exportModel),
            ExportType.Json => ExportToJson(exportModel),
            ExportType.Xml => await ExportToXml(exportModel),
            ExportType.Txt => ExportToTxt(exportModel),
            _ => throw new NotSupportedException()
        };
    }
}
