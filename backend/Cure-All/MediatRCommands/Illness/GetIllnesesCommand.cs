using Cure_All.DataAccess.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Illness
{
    public class GetIllnesesCommand : IRequest<IEnumerable<Models.Entities.Illness>>
    {
    }

    public class GetIllnesesCommandHandler : IRequestHandler<GetIllnesesCommand, IEnumerable<Models.Entities.Illness>> 
    {
        private readonly IRepositoryManager _repository;

        public GetIllnesesCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Models.Entities.Illness>> Handle(GetIllnesesCommand command, CancellationToken cancellationToken)
        {
            return await _repository.Illness.GetAllIllnesesAsync();
        }
    }
}
