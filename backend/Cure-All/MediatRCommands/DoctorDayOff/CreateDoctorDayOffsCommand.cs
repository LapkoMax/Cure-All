using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.Models.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.DoctorDayOff
{
    public class CreateDoctorDayOffsCommand : IRequest<IEnumerable<Guid>>
    {
        public IEnumerable<DoctorDayOffForCreationDto> doctorDayOffs { get; set; }
    }

    public class CreateDoctorDayOffsCommandHandler : IRequestHandler<CreateDoctorDayOffsCommand, IEnumerable<Guid>>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public CreateDoctorDayOffsCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Guid>> Handle(CreateDoctorDayOffsCommand command, CancellationToken cancellationToken)
        {
            var doctorDayOffs = _mapper.Map<IEnumerable<Models.Entities.DoctorDayOffs>>(command.doctorDayOffs);

            foreach (var docSced in doctorDayOffs)
                _repository.DoctorDayOff.CreateDoctorDayOffs(docSced);

            await _repository.SaveAsync();

            var docDayOffIds = doctorDayOffs.Select(docDayOff => docDayOff.Id);

            return docDayOffIds;
        }
    }
}
