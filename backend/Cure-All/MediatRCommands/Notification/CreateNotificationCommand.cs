using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.Models.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Notification
{
    public class CreateNotificationCommand : IRequest<Guid>
    {
        public NotificationForCreationDto notification { get; set; }
    }

    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Guid>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public CreateNotificationCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateNotificationCommand command, CancellationToken cancellationToken)
        {
            var notification = _mapper.Map<Models.Entities.Notification>(command.notification);
            _repository.Notification.CreateNotification(notification);
            await _repository.SaveAsync();
            return notification.Id;
        }
    }
}
