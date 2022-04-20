using Cure_All.DataAccess.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Notification
{
    public class DeleteNotificationCommand : IRequest<bool>
    {
        public Guid notificationId { get; set; }
    }

    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, bool>
    {
        private readonly IRepositoryManager _repository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteNotificationCommandHandler(IRepositoryManager repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteNotificationCommand command, CancellationToken cancellationToken)
        {
            var notification = await _repository.Notification.GetNotificationAsync(command.notificationId, true);

            if (notification == null || notification.UserId != _httpContextAccessor.HttpContext.User.FindFirst("id").Value)
                return false;

            _repository.Notification.DeleteNotification(notification);

            await _repository.SaveAsync();

            notification = await _repository.Notification.GetNotificationAsync(command.notificationId);

            return notification == null;
        }
    }
}
