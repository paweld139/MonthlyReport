using Microsoft.Extensions.Hosting;
using PuppeteerSharp;

namespace MonthlyReport.BLL.Extensions
{
    public static class PuppeteerExtensions
    {
        public static string? ExecutablePath { get; private set; }

        public static async Task PreparePuppeteerAsync(this IHostEnvironment hostingEnvironment)
        {
            var downloadPath = Path.Join(hostingEnvironment.ContentRootPath, "puppeteer");

            var browserOptions = new BrowserFetcherOptions
            {
                Path = downloadPath
            };

            var browserFetcher = new BrowserFetcher(browserOptions);

            var installedBrowser = await browserFetcher.DownloadAsync();

            ExecutablePath = installedBrowser.GetExecutablePath();
        }
    }
}
