using Homiev2.Shared.Dto;
using Homiev2.Shared.Models;
using Homiev2.Shared.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Homiev2.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly UserManager<AuthUser> _userManager;
        private readonly IOptions<Token> _token;
        private readonly IOptions<Registration> _registrationOptions;

        public AccountController(SignInManager<AuthUser> signInManager, UserManager<AuthUser> userManager, IOptions<Token> token, IOptions<Registration> registrationOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _token = token;
            _registrationOptions = registrationOptions;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AuthUser authUser)
        {
            if (_registrationOptions.Value.AcceptingRegistration == false)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(authUser.FriendlyName) && !string.IsNullOrWhiteSpace(authUser.Email) && !string.IsNullOrWhiteSpace(authUser.Password))
            {
                authUser.UserName = authUser.Email;
                var result = await _userManager.CreateAsync(authUser, authUser.Password);

                if (result.Succeeded)
                {
                    return Created("", new
                    {
                        Email = authUser.Email,
                        Name = authUser.FriendlyName,
                        Result = "Created"
                    });
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                return BadRequest("Friendly name, Email & Password fields are all required to register");
            }

            return BadRequest();



        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto json)
        {
            if (!string.IsNullOrWhiteSpace(json.UserName) && !string.IsNullOrWhiteSpace(json.Password))
            {
                var user = await _userManager.FindByNameAsync(json.UserName);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, json.Password, false);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Value.Key));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

                        var token = new JwtSecurityToken(
                            _token.Value.Issuer,
                            _token.Value.Audience,
                            claims,
                            signingCredentials: creds,
                            expires: DateTime.UtcNow.AddDays(5));

                        return Ok(new JsonToken
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            Expiration = token.ValidTo
                        });
                    }
                    else if (!result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized);
                    }
                }


            }

            return BadRequest();
        }
    }
}
