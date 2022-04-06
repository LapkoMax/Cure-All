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
    public class GetPatientCommand : IRequest<PatientDto>
    {
        public Guid patientId { get; set; }
    }

    public class GetPatientCommandHandler : IRequestHandler<GetPatientCommand, PatientDto>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public GetPatientCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PatientDto> Handle(GetPatientCommand command, CancellationToken cancellationToken)
        {
            var patientEntity = await _repository.Patient.GetPatientByPatientIdAsync(command.patientId);

            if (patientEntity == null)
                return null;

            var patientToReturn = _mapper.Map<PatientDto>(patientEntity);
            return patientToReturn;
        }
    }
}
