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
    public class CreateDoctorCommand : IRequest<Guid>
    {
        public DoctorForCreationDto doctor { get; set; }

        public string userId { get; set; }
    }

    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Guid>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public CreateDoctorCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateDoctorCommand command, CancellationToken cancellationToken)
        {
            var doctorEntity = _mapper.Map<Models.Entities.Doctor>(command.doctor);
            doctorEntity.UserId = command.userId;
            _repository.Doctor.CreateDoctor(doctorEntity);
            await _repository.SaveAsync();
            return doctorEntity.Id;
        }
    }
}
