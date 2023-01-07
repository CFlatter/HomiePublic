using Homiev2.Shared.Dto;
using Homiev2.Shared.Enums;
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
                return NoContent();
            }

            return Ok(chores);
        }

        [HttpGet]
        [Route("{choreId:guid}")]
        public async Task<IActionResult> Chore(Guid choreId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                dynamic chore = await _choreService.GetChoreByIdAsync(userId, choreId);

                if (chore == null)
                {
                    return NoContent();
                }

                if (chore.FrequencyTypeId == FrequencyType.Simple)
                {
                    SimpleChoreDto returnObject = new()
                    {
                        ChoreId = chore.ChoreId,
                        TaskName = chore.TaskName,
                        Points = chore.Points,
                        TimeSpan = chore.Schedule.TimeSpan,
                        Multiplier = chore.Schedule.Multiplier
                    };

                    return Ok(returnObject);
                }
                else if (chore.FrequencyTypeId == FrequencyType.Advanced)
                {
                    AdvancedChoreDto returnObject = new()
                    {
                        ChoreId = chore.ChoreId,
                        TaskName = chore.TaskName,
                        Points = chore.Points,
                        DOfWeek = chore.Schedule.DOfWeek,
                        DOfMonth = chore.Schedule.DOfMonth,
                        FirstDOfMonth = chore.Schedule.FirstDOfMonth,
                        LastDOfMonth = chore.Schedule.LastDOfMonth
                    };

                    return Ok(returnObject);
                }

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (UnauthorizedAccessException)
            {

                return Unauthorized();
            }            

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
            if (!json.IsValid)
            {
                return BadRequest(json.ValidationMessage);
            }

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

        [HttpPatch]
        public async Task<IActionResult> UpdateSimpleChore([FromBody] UpdateSimpleChoreDto json)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var result = await _choreService.UpdateChoreAsync(userId, json);
                return Created("", new
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

        [HttpPatch]
        public async Task<IActionResult> UpdateAdvancedChore([FromBody] UpdateAdvancedChoreDto json)
        {

            if (!json.IsValid)
            {
                return BadRequest(json.ValidationMessage);
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var result = await _choreService.UpdateChoreAsync(userId, json);
                return Created("", new
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
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var result = await _choreService.CompleteChoreAsync(userId, json);
                return Ok(new CompletedChoreResponseDto
                {
                    ChoreId = result.ChoreId
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpDelete]
        [Route("{choreId:guid}")]
        public async Task<IActionResult> DeleteChore(Guid choreId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                await _choreService.DeleteChoreAsync(userId, choreId);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);  
            }
        }


    }
}
