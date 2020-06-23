using AuthenticationDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationDemo.Services
{
   public interface IAccountService
    {
        string Login(UserModel login);
    }
}
