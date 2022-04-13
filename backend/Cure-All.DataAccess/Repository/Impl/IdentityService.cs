using AutoMapper;
using Cure_All.BusinessLogic.Options;
using Cure_All.Models.DTO;
using Cure_All.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository.Impl
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JWTSettings _jwtSettings;

        public IdentityService(UserManager<User> userManager, IMapper mapper, JWTSettings jwtSettings)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
        }

        public async Task<User> GetUserAsync(string userLogin)
        {
            return userLogin.Contains("@") ? await _userManager.FindByEmailAsync(userLogin) : await _userManager.FindByNameAsync(userLogin);
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<AuthenticationResultDto> RegisterAsync(UserRegistrationDto registrationDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registrationDto.Email);

            if (existingUser != null)
                return new AuthenticationResultDto
                {
                    ErrorMessages = new List<string> { "Данная электронная почта уже зарегистрирована, попробуйте другую!" }
                };

            existingUser = await _userManager.FindByNameAsync(registrationDto.UserName);

            if (existingUser != null)
                return new AuthenticationResultDto
                {
                    ErrorMessages = new List<string> { "Данное имя пользователя уже зарегистрировано!" }
                };

            var newUser = _mapper.Map<User>(registrationDto);

            if (registrationDto.Password != registrationDto.ConfirmPassword)
                return new AuthenticationResultDto
                {
                    ErrorMessages = new List<string> { "Пароль подтверждён неверно!" }
                };

            var createdUser = await _userManager.CreateAsync(newUser, registrationDto.Password);

            if (!createdUser.Succeeded)
                return new AuthenticationResultDto
                {
                    ErrorMessages = createdUser.Errors.Select(err => err.Description)
                };

            if (!string.IsNullOrEmpty(newUser.Type))
            {
                var result = await _userManager.AddToRoleAsync(newUser, newUser.Type);
                if(!result.Succeeded)
                    return new AuthenticationResultDto
                    {
                        ErrorMessages = result.Errors.Select(err => err.Description)
                    };
            }

            return await GenerateAuthResultForUser(newUser);
        }

        public async Task<AuthenticationResultDto> LoginAsync(UserLoginDto loginDto)
        {
            var user = loginDto.Login.Contains("@") ? await _userManager.FindByEmailAsync(loginDto.Login) : await _userManager.FindByNameAsync(loginDto.Login);

            if(user == null)
                return new AuthenticationResultDto
                {
                    ErrorMessages = new List<string> { "Неверные данные пользователя!" }
                };

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if(!userHasValidPassword)
                return new AuthenticationResultDto
                {
                    ErrorMessages = new List<string> { "Неверные данные пользователя!" }
                };

            return await GenerateAuthResultForUser(user);
        }

        public async Task<IEnumerable<string>> EditUserAsync(UserForEditingDto user)
        {
            var userEntity = await GetUserAsync(user.OldUserName);

            var result = new IdentityResult();

            if (!string.IsNullOrEmpty(user.NewPassword) && !string.IsNullOrEmpty(user.OldPassword) && !string.IsNullOrEmpty(user.ConfirmPassword))
            {
                result = await _userManager.ChangePasswordAsync(userEntity, user.OldPassword, user.NewPassword);

                if (!result.Succeeded) return result.Errors.Select(err => err.Description);
            }

            if (user.Email != userEntity.Email)
            {
                result = await _userManager.SetEmailAsync(userEntity, user.Email);
                if (!result.Succeeded) return result.Errors.Select(err => err.Description);
            }

            if (user.PhoneNumber != userEntity.PhoneNumber)
            {
                result = await _userManager.SetPhoneNumberAsync(userEntity, user.PhoneNumber);
                if (!result.Succeeded) return result.Errors.Select(err => err.Description);
            }

            if (user.UserName != userEntity.UserName)
            {
                result = await _userManager.SetUserNameAsync(userEntity, user.UserName);
                if (!result.Succeeded) return result.Errors.Select(err => err.Description);
            }

            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.DateOfBurth = user.DateOfBurth;
            userEntity.ZipCode = user.ZipCode;
            userEntity.Country = user.Country;
            userEntity.City = user.City;

            result = await _userManager.UpdateAsync(userEntity);

            if (!result.Succeeded) return result.Errors.Select(err => err.Description);
            else return new List<string>();
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var result = false;
            foreach(var role in userRoles)
            {
                result = (await _userManager.RemoveFromRoleAsync(user, role)).Succeeded;
                if (!result) return false;
            }
            result = (await _userManager.DeleteAsync(user)).Succeeded;
            return result;
        }

        private async Task<AuthenticationResultDto> GenerateAuthResultForUser(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id)
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (string role in userRoles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResultDto
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
