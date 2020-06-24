using AuthenticationDemo.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//dotnet add package JWT -v 3.0.0-beta4
//dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
//dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
namespace AuthenticationDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JWTSettings _options;
        private readonly ILogger<AccountController> logger;

        public AccountController(
          UserManager<IdentityUser> userManager,
          SignInManager<IdentityUser> signInManager,
          IOptions<JWTSettings> optionsAccessor,
          ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _options = optionsAccessor.Value;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] Credentials Credentials)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Credentials.Email, Email = Credentials.Email };
                var result = await _userManager.CreateAsync(user, Credentials.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return new JsonResult(new Dictionary<string, object>
                      {
                        { "access_token", GetAccessToken(Credentials.Email) },
                        { "id_token", GetIdToken(user) }
                      });
                }
                return Errors(result);
            }
            return Error("Unexpected error");
        }

        [HttpPost("Signin")]
        public async Task<IActionResult> SignIn([FromBody] Credentials Credentials)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Credentials.Email, Credentials.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(Credentials.Email);
                    logger.LogInformation("User {0} has logged", user.UserName);
                    return new JsonResult(new Dictionary<string, object>
                    {
                        { "access_token", GetAccessToken(Credentials.Email) },
                        { "id_token", GetIdToken(user) }
                    });
                }
                return new JsonResult("Unable to sign in") { StatusCode = 401 };
            }
            return Error("Unexpected error");
        }

        [HttpGet("Signout")]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPut("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser(string userEmail, string roleName)
        {

            var user = await _userManager.FindByEmailAsync(userEmail);

            await _userManager.AddToRoleAsync(user, roleName);
            //var testUser = new IdentityUser("TestUser");
            //await _userManager.CreateAsync(testUser);
            //await _userManager.AddToRoleAsync(testUser, "Administrators");
            return Ok();
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> AddRoleToUser(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            return Ok(user);
        }

        private string GetIdToken(IdentityUser user)
        {
            var payload = new Dictionary<string, object>
          {
            { "id", user.Id },
            { "sub", user.Email },
            { "email", user.Email },
            { "emailConfirmed", user.EmailConfirmed },
          };
            return GetToken(payload);
        }

        private string GetAccessToken(string Email)
        {
            var payload = new Dictionary<string, object>
              {
                { "sub", Email },
                { "email", Email }
              };
            return GetToken(payload);
        }

        private string GetToken(Dictionary<string, object> payload)
        {
            var secret = _options.SecretKey;

            payload.Add("iss", _options.Issuer);
            payload.Add("aud", _options.Audience);
            payload.Add("nbf", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("iat", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("exp", ConvertToUnixTimestamp(DateTime.Now.AddDays(7)));
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, secret);
        }

        private JsonResult Errors(IdentityResult result)
        {
            var items = result.Errors
                .Select(x => x.Description)
                .ToArray();
            return new JsonResult(items) { StatusCode = 400 };
        }

        private JsonResult Error(string message)
        {
            return new JsonResult(message) { StatusCode = 400 };
        }

        private static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}