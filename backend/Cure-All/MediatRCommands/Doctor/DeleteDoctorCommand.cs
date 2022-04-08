using Cure_All.DataAccess.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Doctor
{
    public class DeleteDoctorCommand : IRequest<bool>
    {
        public Guid? doctorId { get; set; }

        public string? userId { get; set; }
    }

    public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, bool>
    {
        private readonly IRepositoryManager _repository;

        public DeleteDoctorCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteDoctorCommand command, CancellationToken cancellationToken)
        {
            var doctor = command.doctorId != null ? await _repository.Doctor.GetDoctorByDoctorIdAsync((Guid)command.doctorId, true) : await _repository.Doctor.GetDoctorByUserIdAsync(command.userId, true);

            _repository.Doctor.DeleteDoctor(doctor);

            await _repository.SaveAsync();

            doctor = command.doctorId != null ? await _repository.Doctor.GetDoctorByDoctorIdAsync((Guid)command.doctorId, true) : await _repository.Doctor.GetDoctorByUserIdAsync(command.userId, true);

            return doctor == null;
        }
    }
}
