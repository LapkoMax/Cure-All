using Cure_All.DataAccess.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Illness
{
    public class GetIllnessCommand : IRequest<Models.Entities.Illness>
    {
        public Guid illnessId { get; set; }
    }

    public class GetIllnessCommandHandler : IRequestHandler<GetIllnessCommand, Models.Entities.Illness>
    {
        private readonly IRepositoryManager _repository;

        public GetIllnessCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<Models.Entities.Illness> Handle(GetIllnessCommand command, CancellationToken cancellationToken)
        {
            return await _repository.Illness.GetIllnessByIdAsync(command.illnessId);
        }
    }
}
