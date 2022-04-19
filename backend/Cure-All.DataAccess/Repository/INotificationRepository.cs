using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetNotificationsForUserAsync(string userId, bool trackChanges = false);

        Task<Notification> GetNotificationAsync(Guid notificationId, bool trackChanges = false);

        Task<int> GetUnreadNotificationAmountAsync(string userId);

        void CreateNotification(Notification notification);

        void DeleteNotification(Notification notification);
    }
}
