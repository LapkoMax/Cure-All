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

        Task<AuthenticationResultDto> RegisterAsync(UserRegistrationDto registrationDto);

        Task<AuthenticationResultDto> LoginAsync(UserLoginDto loginDto);

        Task<IEnumerable<string>> EditUserAsync(UserForEditingDto user);

        Task<bool> DeleteUserAsync(User user);
    }
}
