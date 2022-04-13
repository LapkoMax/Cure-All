using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.Models.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Illness
{
    public class CreateNewIllnessCommand : IRequest<Guid>
    {
        public IllnessForCreationDto illness { get; set; }
    }

    public class CreateNewIllnessCommandHandler : IRequestHandler<CreateNewIllnessCommand, Guid>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public CreateNewIllnessCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateNewIllnessCommand command, CancellationToken cancellationToken)
        {
            var illnessEntity = _mapper.Map<Models.Entities.Illness>(command.illness);

            _repository.Illness.CreateIllness(illnessEntity);

            await _repository.SaveAsync();

            return illnessEntity.Id;
        }
    }
}
