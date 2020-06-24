using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationDemo.Services
{
    public interface IManageRoleService
    {
        Task CreateRole(string roleName);
    }
}
