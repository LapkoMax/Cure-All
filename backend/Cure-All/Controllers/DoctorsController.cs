using AutoMapper;
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
    [Route("api/doctors")]
    [ApiController]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public DoctorsController(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<DoctorDto>> GetDoctors()
        {
            var doctorEntities = await _repositoryManager.Doctor.GetAllDoctorsAsync();

            var doctorsToReturn = _mapper.Map<IEnumerable<DoctorDto>>(doctorEntities);
            return doctorsToReturn;
        }

        [HttpGet("{doctorId}")]
        public async Task<ActionResult<DoctorDto>> GetDoctor(Guid doctorId)
        {
            var doctorEntity = await _repositoryManager.Doctor.GetDoctorByDoctorIdAsync(doctorId);

            if (doctorEntity == null)
                return NotFound();

            var doctorToReturn = _mapper.Map<DoctorDto>(doctorEntity);
            return Ok(doctorToReturn);
        }

        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<DoctorDto>> GetDoctorFromUser(string userId)
        {
            var doctorEntity = await _repositoryManager.Doctor.GetDoctorByUserIdAsync(userId);

            if (doctorEntity == null)
                return NotFound();

            var doctorToReturn = _mapper.Map<DoctorDto>(doctorEntity);
            return Ok(doctorToReturn);
        }
    }
}
