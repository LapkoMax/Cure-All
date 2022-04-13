using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.Models.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.PatientCard
{
    public class GetPatientCardCommand : IRequest<PatientCardDto>
    {
        public Guid patientCardId { get; set; }
    }

    public class GetPatientCardCommandHandler : IRequestHandler<GetPatientCardCommand, PatientCardDto>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public GetPatientCardCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PatientCardDto> Handle(GetPatientCardCommand command, CancellationToken cancellationToken)
        {
            var patientCardEntity = await _repository.PatientCard.GetPatientCardByIdAsync(command.patientCardId);

            if (patientCardEntity == null)
                return null;

            var patientCard = _mapper.Map<PatientCardDto>(patientCardEntity);

            return patientCard;
        }
    }
}
