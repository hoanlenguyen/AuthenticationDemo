using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationDemo.Roles.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AuthenticationDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        IConfiguration configuration;
        readonly ILogger<ConfigurationController> _log;

        public ConfigurationController(IConfiguration configuration, ILogger<ConfigurationController> log)
        {
            this.configuration = configuration;
            _log = log;
        }

        [Authorize(Permissions.Configurations.View)]
        [HttpGet]
        [Route("GetJWTSettings")]
        public IActionResult GetJWTSettings()
        {
            _log.LogInformation("Hello, world!");
            //var settings = configuration.GetSection("JWTSettings");
            var settings = configuration["JWTSettings:Audience"];
            return Ok(settings);
        }
    }
}