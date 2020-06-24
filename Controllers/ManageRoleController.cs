using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManageRoleController : ControllerBase
    {
        private readonly IManageRoleService _manageRole;
        public ManageRoleController(IManageRoleService manageRole)
        {
            _manageRole = manageRole;
        }

        [HttpPost]
        [Route("Create")]
        public async Task CreateRole(string roleName)
        {
            await _manageRole.CreateRole(roleName);
        }
    }
}