using Cure_All.DataAccess.Repository;
using Cure_All.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cure_All.Controllers
{
    [Route("api/specializations")]
    [ApiController]
    [Authorize]
    public class SpecializationsController : ControllerBase
    {
        private readonly IRepositoryManager _reposirory;

        public SpecializationsController(IRepositoryManager reposirory)
        {
            _reposirory = reposirory;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Specialization>> GetSpecializations()
        {
            return await _reposirory.Specialization.GetAllSpecializationsAsync();
        }
    }
}
