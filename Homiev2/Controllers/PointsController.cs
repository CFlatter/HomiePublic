using Homiev2.Shared.Dto;
using Homiev2.Shared.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Homiev2.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PointsController : Controller
    {
        private readonly IChoreLogService _choreLogService;

        public PointsController(IChoreLogService choreLogService)
        {
            _choreLogService = choreLogService;
        }

        [HttpGet]
        public async Task<IActionResult> Points([FromQuery] PointsDto pointsDto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var choreLogs = await _choreLogService.GetChoreLogsAsync(pointsDto.StartDate, pointsDto.EndDate);
            if (choreLogs == null)
            {
                return NoContent();
            }

            return Ok(choreLogs);
        }
    }
}
