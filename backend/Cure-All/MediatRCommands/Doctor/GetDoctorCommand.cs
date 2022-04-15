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
    public class GetDoctorCommand : IRequest<DoctorDto>
    {
        public Guid doctorId { get; set; }
    }

    public class GetDoctorCommandHandler : IRequestHandler<GetDoctorCommand, DoctorDto>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public GetDoctorCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<DoctorDto> Handle(GetDoctorCommand command, CancellationToken cancellationToken)
        {
            var doctorEntity = await _repository.Doctor.GetDoctorByDoctorIdAsync(command.doctorId);

            if (doctorEntity == null)
                return null;

            var doctorToReturn = _mapper.Map<DoctorDto>(doctorEntity);

            doctorToReturn.DoctorsScedule = _mapper.Map<IEnumerable<DoctorsSceduleDto>>(doctorEntity.DoctorsScedule);

            doctorToReturn.DoctorDayOffs = _mapper.Map<IEnumerable<DoctorDayOffsDto>>(doctorEntity.DoctorDayOffs);

            return doctorToReturn;
        }
    }
}
