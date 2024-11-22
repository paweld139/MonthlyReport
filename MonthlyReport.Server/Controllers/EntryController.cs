using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonthlyReport.BLL.Interfaces;
using MonthlyReport.DAL.Entities;
using MonthlyReport.DAL.Entities.New;

namespace MonthlyReport.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EntryController(IEntryService entryService) : ControllerBase
    {
        [HttpGet("{filter}")]
        public async Task<IActionResult> Get(string filter)
        {
            var result = await entryService.Get(filter);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewEntry entry)
        {
            await entryService.Add(entry);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await entryService.Delete(id);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Entry entry)
        {
            await entryService.Update(entry);

            return Ok();
        }
    }
}
