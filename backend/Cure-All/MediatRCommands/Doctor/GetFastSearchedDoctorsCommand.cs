using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.Models.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Doctor
{
    public class GetFastSearchedDoctorsCommand : IRequest<IEnumerable<DoctorDto>>
    {
        public string searchTerm { get; set; }
    }

    public class GetFastSearchedDoctorsCommandHandler : IRequestHandler<GetFastSearchedDoctorsCommand, IEnumerable<DoctorDto>>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public GetFastSearchedDoctorsCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DoctorDto>> Handle(GetFastSearchedDoctorsCommand command, CancellationToken cancellationToken)
        {
            var doctorEntities = await _repository.Doctor.GetFastSearchedDoctorsAsync(command.searchTerm);

            var doctorsToReturn = _mapper.Map<IEnumerable<DoctorDto>>(doctorEntities);

            foreach (var doctorToReturn in doctorsToReturn)
            {
                doctorToReturn.DoctorsScedule = _mapper.Map<IEnumerable<DoctorsSceduleDto>>(doctorEntities.Where(doc => doc.Id.Equals(doctorToReturn.Id)).SingleOrDefault().DoctorsScedule);

                doctorToReturn.DoctorDayOffs = _mapper.Map<IEnumerable<DoctorDayOffsDto>>(doctorEntities.Where(doc => doc.Id.Equals(doctorToReturn.Id)).SingleOrDefault().DoctorDayOffs);
            }

            return doctorsToReturn;
        }
    }
}
