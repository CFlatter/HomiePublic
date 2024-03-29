﻿using Homiev2.Shared.Dto;
using Homiev2.Shared.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Homiev2.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HouseholdController : Controller
    {

        private readonly IHouseholdService _householdService;
        private readonly IHouseholdCreationService _householdCreationService;

        public HouseholdController(IHouseholdService householdService, IHouseholdCreationService householdCreationService)
        {
            _householdService = householdService;
            _householdCreationService = householdCreationService;
        }

        [HttpGet]
        public async Task<IActionResult> Household()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var household = await _householdService.ReturnHouseholdAsync(userId);

            if (household == null)
            {
                return NoContent();
            }

            HouseholdDTO returnObject = new()
            {
                HouseholdId = household.HouseholdId,
                HouseholdName = household.HouseholdName,
                ShareCode = household.ShareCode
            };
            return Ok(returnObject);

        }

        [HttpPost]
        public async Task<IActionResult> Household([FromBody] HouseholdDTO json)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                try
                {
                    var result = await _householdCreationService.CreateHouseholdAsync(userId, json.HouseholdName);
                    return Created("", result);
                }
                catch (ApplicationException e)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
                catch (ArgumentException e)
                {
                    return BadRequest(error: e.Message);
                }
                catch(Exception e)
                {
                    return BadRequest(e.Message);
                }


        }




    }
}
