using Homiev2.Shared.Dto;
using Homiev2.Shared.Interfaces.Repositories;
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
    public class HouseholdMemberController : Controller
    {
        private readonly IHouseholdMemberService _householdMemberService;
        private readonly IAuthUsersRepository _authUsersRepository;

        public HouseholdMemberController(IHouseholdMemberService householdMemberService, IAuthUsersRepository authUsersRepository)
        {
            _householdMemberService = householdMemberService;
            _authUsersRepository = authUsersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> HouseholdMembers()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _householdMemberService.GetHouseholdMembersAsync(username);
            if (result != null)
            {
                return Ok(result);
            };
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> HouseholdMember([FromBody] HouseholdMemberDto json)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var result = await _householdMemberService.CreateHouseholdMemberAsync(username, json.MemberName);
                return Created("", result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }


            return BadRequest(error: "Incorrect data format");


        }

        [HttpPost]
        public async Task<IActionResult> JoinHousehold([FromBody] JoinHouseholdDto dto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var friendlyName = _authUsersRepository.GetUserAsync(username).Result.FriendlyName;

            try
            {
                var result = await _householdMemberService.JoinHouseholdAsync(username, dto.ShareCode, friendlyName);
                if (result != null)
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHouseholdMember([FromBody] HouseholdMemberDto json)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var result = await _householdMemberService.DeleteHouseholdMemberAsync(username, json.MemberName);

                if (result != null)
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return BadRequest();
        }
    }
}
