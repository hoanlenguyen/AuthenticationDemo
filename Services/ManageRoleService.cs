using AuthenticationDemo.Roles;
using AuthenticationDemo.Roles.Permissions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationDemo.Services
{
    public class ManageRoleService:IManageRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageRoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateRole(string roleName)
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
            var role= await _roleManager.FindByNameAsync(roleName);

            await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Students.View));
            await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Configurations.Create));
        }
    }
}
