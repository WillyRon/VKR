using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Forest_fire_control.Data.Models;
using Forest_fire_control.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace Forest_fire_control.BI.ServiceInterfaces
{
    public interface IUserService
    {
        Task<AuthenticationResult> Login(LoginModel model);

        Task<AuthenticationResult> CreateUser(UserModel userMode, Guid regionId);

        Task<User> GetUser(string email);
    }
}
