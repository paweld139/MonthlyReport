using MonthlyReport.BLL.Extensions;
using MonthlyReport.BLL.Interfaces;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace MonthlyReport.BLL.Services
{
    public class HtmlToPdfConverter(ITemplateService templateService) : IHtmlToPdfConverter
    {
        public async Task<Stream> GetPdf<T>(T model, string templatePath)
        {
            var html = await templateService.RenderAsync(templatePath, model);

            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = PuppeteerExtensions.ExecutablePath,
                Args = ["--no-sandbox"]
            });

            await using var page = await browser.NewPageAsync();

            await page.EmulateMediaTypeAsync(MediaType.Screen);

            await page.SetContentAsync(html);

            return await page.PdfStreamAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true
            });
        }
    }
}
