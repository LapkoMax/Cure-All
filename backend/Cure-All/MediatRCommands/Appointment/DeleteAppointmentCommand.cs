using Cure_All.DataAccess.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Appointment
{
    public class DeleteAppointmentCommand : IRequest<bool>
    {
        public Guid appointmentId { get; set; }
    }

    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, bool>
    {
        private readonly IRepositoryManager _repository;

        public DeleteAppointmentCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteAppointmentCommand command, CancellationToken cancellationToken)
        {
            var appointment = await _repository.Appointment.GetAppointmentAsync(command.appointmentId, true);

            _repository.Appointment.DeleteAppointment(appointment);

            await _repository.SaveAsync();

            appointment = await _repository.Appointment.GetAppointmentAsync(command.appointmentId, true);

            return appointment == null;
        }
    }
}
