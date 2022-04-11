using Cure_All.DataAccess.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.PatientCard
{
    public class CreatePatientCardForPatientCommand : IRequest<Guid>
    {
        public Guid patientId { get; set; }
    }

    public class CreatePatientCardForPatientCommandHandler : IRequestHandler<CreatePatientCardForPatientCommand, Guid>
    {
        private readonly IRepositoryManager _repository;

        public CreatePatientCardForPatientCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreatePatientCardForPatientCommand command, CancellationToken cancellationToken)
        {
            var patientCardEntity = new Models.Entities.PatientCard { };
            _repository.PatientCard.CreatePatientCardForPatient(command.patientId, patientCardEntity);

            await _repository.SaveAsync();

            return patientCardEntity.Id;
        }
    }
}
