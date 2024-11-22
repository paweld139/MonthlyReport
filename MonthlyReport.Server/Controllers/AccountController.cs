using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MonthlyReport.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController(IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        [HttpGet("UserName")]
        public IActionResult GetUserName() => Ok(httpContextAccessor.HttpContext!.User.Identity!.Name);
    }
}
