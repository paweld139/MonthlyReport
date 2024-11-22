using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonthlyReport.BLL.Interfaces;
using MonthlyReport.BLL.Models;

namespace MonthlyReport.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExportController(IExportService exportService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExportModel exportModel)
        {
            var (stream, fileName, mimeType) = await exportService.Export(exportModel);

            return File(stream, mimeType, fileName);
        }
    }
}
