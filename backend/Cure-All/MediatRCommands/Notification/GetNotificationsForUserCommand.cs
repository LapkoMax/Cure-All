using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Notification
{
    public class GetNotificationsForUserCommand : IRequest<IEnumerable<NotificationDto>>
    {
        public string userId { get; set; }
    }

    public class GetNotificationsForUserCommandHandler : IRequestHandler<GetNotificationsForUserCommand, IEnumerable<NotificationDto>>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetNotificationsForUserCommandHandler(IRepositoryManager repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<NotificationDto>> Handle(GetNotificationsForUserCommand command, CancellationToken cancellationToken)
        {
            if (command.userId != _httpContextAccessor.HttpContext.User.FindFirst("id").Value)
                return new List<NotificationDto>();

            var notifications = await _repository.Notification.GetNotificationsForUserAsync(command.userId);

            notifications = notifications.Where(notif => notif.ShowFrom <= DateTime.UtcNow);

            var notificationsToReturn = _mapper.Map<IEnumerable<NotificationDto>>(notifications);

            return notificationsToReturn;
        }
    }
}
