using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.MediatRCommands.Notification;
using Cure_All.Models.DTO;
using Cure_All.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cure_All.UnitTests
{
    public class NotificationMediatRTests
    {
        [Fact]
        public async Task GetNotificationsForUserCommand_ShouldReturnUserNotifications()
        {
            var command = new GetNotificationsForUserCommand()
            {
                userId = "178f5e28-e855-4190-b632-588c3c95547e"
            };

            var notifMock = new Mock<INotificationRepository>();
            notifMock.Setup(repo => repo.GetNotificationsForUserAsync(command.userId, false)).Returns(GetTestNotificationsForUser(command.userId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Notification).Returns(notifMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<NotificationDto>>(It.IsAny<IEnumerable<Notification>>())).Returns(GetTestNotificationsDtoForUser(command.userId));

            var httpMock = new Mock<IHttpContextAccessor>();
            httpMock.Setup(http => http.HttpContext.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] 
            {
                new Claim("id", "178f5e28-e855-4190-b632-588c3c95547e")
            })));

            var commandHandler = new GetNotificationsForUserCommandHandler(mockRepoManager.Object, mapperMock.Object, httpMock.Object);

            var notifications = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(notifications);
            Assert.IsAssignableFrom<IEnumerable<NotificationDto>>(notifications);
            Assert.Equal(GetTestNotificationsDtoForUser(command.userId).Count(), notifications.Count());
        }

        [Fact]
        public async Task GetNotificationCommand_ShouldReturnCorrectNotification()
        {
            var command = new GetNotificationCommand()
            {
                notificationId = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656")
            };

            var notifMock = new Mock<INotificationRepository>();
            notifMock.Setup(repo => repo.GetNotificationAsync(command.notificationId, true)).Returns(GetTestNotification(command.notificationId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Notification).Returns(notifMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<NotificationDto>(It.IsAny<Notification>())).Returns(GetTestNotificationDto(command.notificationId));

            var httpMock = new Mock<IHttpContextAccessor>();
            httpMock.Setup(http => http.HttpContext.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("id", "178f5e28-e855-4190-b632-588c3c95547e")
            })));

            var commandHandler = new GetNotificationCommandHandler(mockRepoManager.Object, mapperMock.Object, httpMock.Object);

            var notifDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(notifDto);
            Assert.IsAssignableFrom<NotificationDto>(notifDto);
            Assert.Equal(command.notificationId, notifDto.Id);
        }

        [Fact]
        public async Task GetNotificationCommand_ShouldReturnNull()
        {
            var command = new GetNotificationCommand()
            {
                notificationId = Guid.NewGuid()
            };

            var notifMock = new Mock<INotificationRepository>();
            notifMock.Setup(repo => repo.GetNotificationAsync(command.notificationId, true)).Returns(GetTestNotification(command.notificationId));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Notification).Returns(notifMock.Object);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<NotificationDto>(It.IsAny<Notification>())).Returns(GetTestNotificationDto(command.notificationId));

            var httpMock = new Mock<IHttpContextAccessor>();
            httpMock.Setup(http => http.HttpContext.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("id", "178f5e28-e855-4190-b632-588c3c95547e")
            })));

            var commandHandler = new GetNotificationCommandHandler(mockRepoManager.Object, mapperMock.Object, httpMock.Object);

            var notifDto = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(notifDto);
        }

        [Fact]
        public async Task CreateNotificationCommand_ShouldCreateNewNotification()
        {
            var command = new CreateNotificationCommand()
            {
                notification = new NotificationForCreationDto { }
            };

            var notifId = Guid.NewGuid();

            var notifMock = new Mock<INotificationRepository>();
            notifMock.Setup(repo => repo.CreateNotification(new Notification { Id = notifId }));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Notification).Returns(notifMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();
            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Notification>(It.IsAny<NotificationForCreationDto>())).Returns(new Notification { Id = notifId });

            var commandHandler = new CreateNotificationCommandHandler(mockRepoManager.Object, mapperMock.Object);

            var id = await commandHandler.Handle(command, CancellationToken.None);

            Assert.True(id == notifId);
        }

        [Fact]
        public async Task DeleteNotificationCommand_ShouldDeleteCorrectly()
        {
            var command = new DeleteNotificationCommand()
            {
                notificationId = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656")
            };

            var notifMock = new Mock<INotificationRepository>();
            notifMock.Setup(repo => repo.GetNotificationAsync(command.notificationId, true)).Returns(GetTestNotification(command.notificationId));
            notifMock.Setup(repo => repo.DeleteNotification(new Notification { Id = (Guid)command.notificationId }));

            var mockRepoManager = new Mock<IRepositoryManager>();
            mockRepoManager.Setup(repo => repo.Notification).Returns(notifMock.Object);
            mockRepoManager.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

            var httpMock = new Mock<IHttpContextAccessor>();
            httpMock.Setup(http => http.HttpContext.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("id", "178f5e28-e855-4190-b632-588c3c95547e")
            })));

            var commandHandler = new DeleteNotificationCommandHandler(mockRepoManager.Object, httpMock.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }

        private async Task<IEnumerable<Notification>> TestNotifications()
        {
            return new List<Notification>
            {
                new Notification
                {
                    Id = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656"),
                    UserId = new Guid("178f5e28-e855-4190-b632-588c3c95547e").ToString(),
                    ShowFrom = DateTime.UtcNow
                },
                new Notification
                {
                    Id = new Guid("a5ed51ce-ea0a-4c67-ab45-c392b9a75220"),
                    UserId = new Guid("2127fc08-eae4-4a9a-87b5-496a3a47507c").ToString(),
                    ShowFrom = DateTime.UtcNow
                },
                new Notification
                {
                    Id = new Guid("62b9f89e-0450-4285-a3d0-d2f1dd88ae89"),
                    UserId = new Guid("0a23d9ed-f162-4a3a-ae39-d51384e55db0").ToString(),
                    ShowFrom = DateTime.UtcNow
                }
            };
        }

        private async Task<Notification> GetTestNotification(Guid notifId)
        {
            var notifications = await TestNotifications();
            return notifications.Where(notif => notif.Id == notifId).FirstOrDefault();
        }

        private async Task<IEnumerable<Notification>> GetTestNotificationsForUser(string userId)
        {
            var notifications = await TestNotifications();
            return notifications.Where(notif => notif.UserId == userId);
        }

        private IEnumerable<NotificationDto> TestNotificationsDto()
        {
            return new List<NotificationDto>
            {
                new NotificationDto
                {
                    Id = new Guid("cef789eb-d5dd-4d72-84f4-5a73aee21656"),
                    UserId = new Guid("178f5e28-e855-4190-b632-588c3c95547e").ToString()
                },
                new NotificationDto
                {
                    Id = new Guid("a5ed51ce-ea0a-4c67-ab45-c392b9a75220"),
                    UserId = new Guid("2127fc08-eae4-4a9a-87b5-496a3a47507c").ToString()
                },
                new NotificationDto
                {
                    Id = new Guid("62b9f89e-0450-4285-a3d0-d2f1dd88ae89"),
                    UserId = new Guid("0a23d9ed-f162-4a3a-ae39-d51384e55db0").ToString()
                }
            };
        }

        private NotificationDto GetTestNotificationDto(Guid notifId)
        {
            var notifications = TestNotificationsDto();

            return notifications.Where(notif => notif.Id == notifId).SingleOrDefault();
        }

        private IEnumerable<NotificationDto> GetTestNotificationsDtoForUser(string userId)
        {
            var notifications = TestNotificationsDto();
            return notifications.Where(notif => notif.UserId == userId);
        }
    }
}
