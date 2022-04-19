using Cure_All.DataAccess.Repository;
using Cure_All.MediatRCommands.Notification;
using Cure_All.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IRepositoryManager _repository;

        public NotificationsController(IMediator mediator, IRepositoryManager repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet("forUser/{userId}")]
        public async Task<IEnumerable<NotificationDto>> GetNotificationsForUser(string userId)
        {
            return await _mediator.Send(new GetNotificationsForUserCommand { userId = userId }, CancellationToken.None);
        }

        [HttpGet("{notificationId}")]
        public async Task<IActionResult> GetNotification(Guid notificationId)
        {
            var notification = await _mediator.Send(new GetNotificationCommand { notificationId = notificationId }, CancellationToken.None);

            if (notification == null)
                return NotFound();
            return Ok(notification);
        }

        [HttpGet("{userId}/unreaded")]
        public async Task<int> GetUnreadedNotificationAmount(string userId)
        {
            return await _repository.Notification.GetUnreadNotificationAmountAsync(userId);
        }

        [HttpPost("confirmNotification/{notificationId}")]
        public async Task<IActionResult> ConfirmAppointmentNotification(Guid notificationId)
        {
            var notificationForConfirm = await _mediator.Send(new GetNotificationCommand { notificationId = notificationId }, CancellationToken.None);

            if (notificationForConfirm == null)
                return NotFound();

            if (notificationForConfirm.AppointmentId != null)
            {
                var newNotificationId = await _mediator.Send(new CreateAppointmentConfirmNotificationCommand { notificationId = notificationForConfirm.Id }, CancellationToken.None);

                if (newNotificationId == Guid.Empty)
                    return BadRequest();

                return Ok(newNotificationId);
            }

            return BadRequest();
        }

        [HttpPost("rejectNotification/{notificationId}")]
        public async Task<IActionResult> RejectAppointmentNotification(Guid notificationId)
        {
            var notificationForReject = await _mediator.Send(new GetNotificationCommand { notificationId = notificationId }, CancellationToken.None);

            if (notificationForReject == null)
                return NotFound();

            if (notificationForReject.AppointmentId != null)
            {
                var newNotificationId = await _mediator.Send(new CreateAppointmentRejectNotificationCommand { notificationId = notificationForReject.Id }, CancellationToken.None);

                if (newNotificationId == Guid.Empty)
                    return BadRequest();

                return Ok(newNotificationId);
            }

            return BadRequest();
        }

        [HttpDelete("{notificationId}")]
        public async Task<IActionResult> DeleteNotification(Guid notificationId)
        {
            var result = await _mediator.Send(new DeleteNotificationCommand { notificationId = notificationId }, CancellationToken.None);

            if (result)
                return BadRequest();
            return NoContent();
        }
    }
}
