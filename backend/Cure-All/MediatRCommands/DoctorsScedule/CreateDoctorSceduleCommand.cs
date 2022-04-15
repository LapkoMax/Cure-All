using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.Models.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.DoctorsScedule
{
    public class CreateDoctorSceduleCommand : IRequest<IEnumerable<Guid>>
    {
        public IEnumerable<DoctorSceduleForCreationDto> doctorScedules { get; set; }
    }

    public class CreateDoctorSceduleCommandHandler : IRequestHandler<CreateDoctorSceduleCommand, IEnumerable<Guid>>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public CreateDoctorSceduleCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Guid>> Handle(CreateDoctorSceduleCommand command, CancellationToken cancellationToken)
        {
            var doctorScedules = _mapper.Map<IEnumerable<Models.Entities.DoctorsScedule>>(command.doctorScedules);

            foreach(var docSced in doctorScedules)
                _repository.DoctorsScedule.CreateDoctorsScedule(docSced);

            await _repository.SaveAsync();

            var docScedIds = doctorScedules.Select(docSced => docSced.Id);

            return docScedIds;
        }
    }
}
