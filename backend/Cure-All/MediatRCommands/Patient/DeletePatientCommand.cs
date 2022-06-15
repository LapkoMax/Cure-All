using Cure_All.DataAccess.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Patient
{
    public class DeletePatientCommand : IRequest<bool>
    {
        public Guid? patientId { get; set; }

        public string? userId { get; set; }
    }

    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, bool>
    {
        private readonly IRepositoryManager _repository;

        public DeletePatientCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePatientCommand command, CancellationToken cancellationToken)
        {
            var patient = command.patientId != null ? await _repository.Patient.GetPatientByPatientIdAsync((Guid)command.patientId, true) : await _repository.Patient.GetPatientByUserIdAsync(command.userId, true);

            _repository.Patient.DeletePatient(patient);

            await _repository.SaveAsync();

            patient = command.patientId != null ? await _repository.Patient.GetPatientByPatientIdAsync((Guid)command.patientId) : await _repository.Patient.GetPatientByUserIdAsync(command.userId);

            return patient == null;
        }
    }
}
