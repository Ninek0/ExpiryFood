using ExpiryFood.Models;

namespace ExpiryFood.Notification.NotificationProvider
{
    public interface INotificationProvider
    {
        public Task SendNoticationAsync(List<Product> products);
    }
}
