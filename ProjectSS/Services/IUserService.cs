using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectSS.Models;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services
{
    public interface IUserService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<bool> Registration(RegistrationUser request);
        List<UserResponse> GetlistUsers();
        UserResponse DeleteUser(Guid id);
        UserResponse EditUser(EditUserRequest request);

    }
}