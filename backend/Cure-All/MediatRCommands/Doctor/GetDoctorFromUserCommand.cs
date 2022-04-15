using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.Models.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Doctor
{
    public class GetDoctorFromUserCommand : IRequest<DoctorDto>
    {
        public string userId { get; set; }
    }

    public class GetDoctorFromUserCommandHandler : IRequestHandler<GetDoctorFromUserCommand, DoctorDto>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public GetDoctorFromUserCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<DoctorDto> Handle(GetDoctorFromUserCommand command, CancellationToken cancellationToken)
        {
            var doctorEntity = await _repository.Doctor.GetDoctorByUserIdAsync(command.userId);

            if (doctorEntity == null)
                return null;

            var doctorToReturn = _mapper.Map<DoctorDto>(doctorEntity);

            doctorToReturn.DoctorsScedule = _mapper.Map<IEnumerable<DoctorsSceduleDto>>(doctorEntity.DoctorsScedule);

            doctorToReturn.DoctorDayOffs = _mapper.Map<IEnumerable<DoctorDayOffsDto>>(doctorEntity.DoctorDayOffs);

            return doctorToReturn;
        }
    }
}
