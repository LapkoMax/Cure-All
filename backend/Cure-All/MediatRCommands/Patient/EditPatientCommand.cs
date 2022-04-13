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
    public class EditPatientCommand : IRequest<Guid>
    {
        public Guid patientId { get; set; }

        public PatientForEditingDto patient { get; set; }
    }

    public class EditPatientCommandHandler : IRequestHandler<EditPatientCommand, Guid>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public EditPatientCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(EditPatientCommand command, CancellationToken cancellationToken)
        {
            var patient = await _repository.Patient.GetPatientByPatientIdAsync(command.patientId, true);

            _mapper.Map(command.patient, patient);

            await _repository.SaveAsync();

            return patient.Id;
        }
    }
}
