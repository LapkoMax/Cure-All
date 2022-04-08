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
    public class EditDoctorCommand : IRequest<Guid>
    {
        public Guid doctorId { get; set; }

        public DoctorForEditingDto doctor { get; set; }
    }

    public class EditDoctorCommandHandler : IRequestHandler<EditDoctorCommand, Guid>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public EditDoctorCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(EditDoctorCommand command, CancellationToken cancellationToken)
        {
            var doctor = await _repository.Doctor.GetDoctorByDoctorIdAsync(command.doctorId, true);

            _mapper.Map(command.doctor, doctor);

            await _repository.SaveAsync();

            return doctor.Id;
        }
    }

}
