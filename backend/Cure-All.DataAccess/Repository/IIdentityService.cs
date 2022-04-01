using Cure_All.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository
{
    public interface IIdentityService
    {
        Task<AuthenticationResultDto> RegisterAsync(UserRegistrationDto registrationDto);

        Task<AuthenticationResultDto> LoginAsync(UserLoginDto loginDto);
    }
}
