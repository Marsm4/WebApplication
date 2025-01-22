using Microsoft.AspNetCore.Mvc;
using WebApplication.Requests;

namespace WebApplication.Interfaces
{
    public interface IUsersLoginsService
    {
        Task<IActionResult> GetAllUsersAsync();
        Task<IActionResult> CreateNewUserAndLoginAsync(CreateNewUserAndLogin newUser);
    }
}
