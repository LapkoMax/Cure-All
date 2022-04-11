using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.Models.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Patient
{
    public class CreatePatientCommand : IRequest<Guid>
    {
        public PatientForCreationDto patient { get; set; }

        public string userId { get; set; }
    }

    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Guid>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public CreatePatientCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
        {
            var patientEntity = _mapper.Map<Models.Entities.Patient>(command.patient);
            patientEntity.UserId = command.userId;
            _repository.Patient.CreatePatient(patientEntity);
            await _repository.SaveAsync();
            return patientEntity.Id;
        }
    }
}
