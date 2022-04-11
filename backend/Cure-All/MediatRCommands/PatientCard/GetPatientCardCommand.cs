using Cure_All.DataAccess.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.PatientCard
{
    public class GetPatientCardCommand : IRequest<Models.Entities.PatientCard>
    {
        public Guid patientCardId { get; set; }
    }

    public class GetPatientCardCommandHandler : IRequestHandler<GetPatientCardCommand, Models.Entities.PatientCard>
    {
        private readonly IRepositoryManager _repository;

        public GetPatientCardCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<Models.Entities.PatientCard> Handle(GetPatientCardCommand command, CancellationToken cancellationToken)
        {
            var patientEntity = await _repository.PatientCard.GetPatientCardByIdAsync(command.patientCardId);

            if (patientEntity == null)
                return null;
            return patientEntity;
        }
    }
}
