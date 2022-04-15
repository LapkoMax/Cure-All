using Cure_All.Models.DTO;
using System;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Cure_All.DataAccess.Repository;
using AutoMapper;
using Cure_All.BusinessLogic.RequestFeatures;

namespace Cure_All.MediatRCommands.Doctor
{
    public class GetDoctorsCommand : IRequest<IEnumerable<DoctorDto>>
    {
        public DoctorParameters doctorParameters { get; set; }
    }

    public class GetDoctorsCommandHandler : IRequestHandler<GetDoctorsCommand, IEnumerable<DoctorDto>>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public GetDoctorsCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DoctorDto>> Handle(GetDoctorsCommand command, CancellationToken cancellationToken)
        {
            var doctorEntities = await _repository.Doctor.GetAllDoctorsAsync(command.doctorParameters);

            var doctorsToReturn = _mapper.Map<IEnumerable<DoctorDto>>(doctorEntities);

            foreach(var doctorToReturn in doctorsToReturn)
            {
                doctorToReturn.DoctorsScedule = _mapper.Map<IEnumerable<DoctorsSceduleDto>>(doctorEntities.Where(doc => doc.Id.Equals(doctorToReturn.Id)).SingleOrDefault().DoctorsScedule);

                doctorToReturn.DoctorDayOffs = _mapper.Map<IEnumerable<DoctorDayOffsDto>>(doctorEntities.Where(doc => doc.Id.Equals(doctorToReturn.Id)).SingleOrDefault().DoctorDayOffs);
            }

            return doctorsToReturn;
        }
    }
}
