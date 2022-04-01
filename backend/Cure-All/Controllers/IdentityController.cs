using Cure_All.BusinessLogic.Extensions;
using Cure_All.DataAccess.Repository;
using Cure_All.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cure_All.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(UserRegistrationDto registrationDto)
        {
            var authResult = await _identityService.RegisterAsync(registrationDto);

            if (!authResult.Success)
                return BadRequest(new { Errors = authResult.ErrorMessages });

            return Ok(new { Token = authResult.Token });
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Register(UserLoginDto loginDto)
        {
            var authResult = await _identityService.LoginAsync(loginDto);

            if (!authResult.Success)
                return BadRequest(new { Errors = authResult.ErrorMessages });

            return Ok(new { Token = authResult.Token });
        }

        [HttpPost("/activeUserId")]
        [Authorize]
        public IActionResult GetActiveUserId()
        {
            return Ok(new { UserId = HttpContext.GetUserId() });
        }
    }
}
