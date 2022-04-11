using Cure_All.DataAccess.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.PatientCard
{
    public class DeletePatientCardCommand : IRequest<bool>
    {
        public Guid patientId { get; set; }
    }

    public class DeletePatientCardCommandHandler : IRequestHandler<DeletePatientCardCommand, bool>
    {
        private readonly IRepositoryManager _repository;

        public DeletePatientCardCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePatientCardCommand command, CancellationToken cancellationToken)
        {
            var card = await _repository.PatientCard.GetPatientCardForPatientAsync(command.patientId);

            if (card == null) return false;

            _repository.PatientCard.DeletePatientCard(card);

            await _repository.SaveAsync();

            card = await _repository.PatientCard.GetPatientCardForPatientAsync(command.patientId);

            return card == null;
        }
    }
}
