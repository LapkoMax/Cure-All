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
    public class GetNotificationCommand : IRequest<NotificationDto>
    {
        public Guid notificationId { get; set; }
    }

    public class GetNotificationCommandHandler : IRequestHandler<GetNotificationCommand, NotificationDto>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetNotificationCommandHandler(IRepositoryManager repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<NotificationDto> Handle(GetNotificationCommand command, CancellationToken cancellationToken)
        {
            var notificationToRead = await _repository.Notification.GetNotificationAsync(command.notificationId, true);

            if (notificationToRead.UserId != _httpContextAccessor.HttpContext.User.FindFirst("id").Value || notificationToRead.ShowFrom > DateTime.UtcNow)
                return null;

            if (!notificationToRead.Readed)
            {
                notificationToRead.Readed = true;
                await _repository.SaveAsync();
            }

            var notification = await _repository.Notification.GetNotificationAsync(command.notificationId);

            var notificationToReturn = _mapper.Map<NotificationDto>(notification);

            return notificationToReturn;
        }
    }
}
