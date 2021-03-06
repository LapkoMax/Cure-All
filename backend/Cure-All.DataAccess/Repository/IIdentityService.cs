using Cure_All.Models.DTO;
using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository
{
    public interface IIdentityService
    {
        Task<User> GetUserAsync(string userLogin);

        Task<User> GetUserByIdAsync(string userId);

        Task<AuthenticationResultDto> RegisterAsync(UserRegistrationDto registrationDto);

        Task<AuthenticationResultDto> LoginAsync(UserLoginDto loginDto);

        Task<bool> ConfirmUserEmail(User user, string token);

        Task<bool> SendResetPasswordEmail(string email);

        Task<bool> ResetPassword(User user, string token, string password);

        Task<IEnumerable<string>> EditUserAsync(UserForEditingDto user);

        Task<bool> DeleteUserAsync(User user);
    }
}
