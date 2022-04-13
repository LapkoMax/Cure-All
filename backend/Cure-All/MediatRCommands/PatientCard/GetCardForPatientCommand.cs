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
    public class GetCardForPatientCommand : IRequest<PatientCardDto>
    {
        public Guid patientId { get; set; }
    }

    public class GetCardForPatientCommandHandler : IRequestHandler<GetCardForPatientCommand, PatientCardDto> 
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public GetCardForPatientCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PatientCardDto> Handle(GetCardForPatientCommand command, CancellationToken cancellationToken)
        {
            var patientCard = await _repository.PatientCard.GetPatientCardForPatientAsync(command.patientId);
            
            if (patientCard == null)
                return null;

            var patientCardToReturn = _mapper.Map<PatientCardDto>(patientCard);

            return patientCardToReturn;
        }
    }

}
