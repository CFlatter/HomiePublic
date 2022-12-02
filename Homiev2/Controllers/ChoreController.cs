using Homiev2.Shared.Dto;
using Homiev2.Shared.Interfaces.Services;
using Homiev2.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Homiev2.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChoreController : Controller
    {
        private readonly IChoreService _choreService;

        public ChoreController(IChoreService choreService)
        {
            _choreService = choreService;
        }

        [HttpGet]
        public async Task<IActionResult> Chores()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var chores = await _choreService.GetChoresAsync(userId);
          
            if (chores == null)
            {
                return NotFound();
            }

            return Ok(chores);
        }

        [HttpPost]
        public async Task<IActionResult> SimpleChore([FromBody] SimpleChoreDto json)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var result = await _choreService.CreateChoreAsync(userId, json);
                return Created("", new                 {
                    ChoreId = result.ChoreId,
                    TaskName = result.TaskName,
                    StartDate = result.NextDueDate
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }

        }

        [HttpPost]
        public async Task<IActionResult> AdvancedChore([FromBody] AdvancedChoreDto json)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var result = await _choreService.CreateChoreAsync(userId, json);
                return Created("", new AdvancedChoreDto
                {
                    ChoreId = result.ChoreId,
                    TaskName = result.TaskName,
                    StartDate = result.NextDueDate
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }

        [HttpPost]
        public async Task<IActionResult> CompleteChore(CompletedChoreDto json)
        {
            try
            {
                var result = await _choreService.CompleteChoreAsync(json);
                return Ok(new CompletedChoreResponseDto
                {
                    ChoreId = result.ChoreId
                });
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Chore(DeleteChoreDto json)
        {
            try
            {
                await _choreService.DeleteChoreAsync(json.ChoreId);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);  
            }
        }


    }
}
