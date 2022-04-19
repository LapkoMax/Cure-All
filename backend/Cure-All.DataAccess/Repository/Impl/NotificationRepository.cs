using Cure_All.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository.Impl
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(DataContext dataContext) : base(dataContext)
        { }

        public override IQueryable<Notification> FindAll(bool trackChanges = false) =>
            !trackChanges ?
            DataContext.Notifications.Include(notif => notif.User).AsNoTracking() :
            DataContext.Notifications.Include(notif => notif.User);

        public override IQueryable<Notification> FindByCondition(Expression<Func<Notification, bool>> expression, bool trackChanges = false) =>
            !trackChanges ?
            DataContext.Notifications.Include(notif => notif.User).Where(expression).AsNoTracking() :
            DataContext.Notifications.Include(notif => notif.User).Where(expression);

        public async Task<IEnumerable<Notification>> GetNotificationsForUserAsync(string userId, bool trackChanges = false)
        {
            return await FindByCondition(notif => notif.UserId == userId, trackChanges).ToListAsync();
        }

        public async Task<Notification> GetNotificationAsync(Guid notificationId, bool trackChanges = false)
        {
            return await FindByCondition(notif => notif.Id == notificationId, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<int> GetUnreadNotificationAmountAsync(string userId)
        {
            return await FindByCondition(notif => notif.UserId == userId && !notif.Readed && notif.ShowFrom <= DateTime.UtcNow).CountAsync();
        }

        public void CreateNotification(Notification notification) => Create(notification);

        public void DeleteNotification(Notification notification) => Delete(notification);
    }
}
