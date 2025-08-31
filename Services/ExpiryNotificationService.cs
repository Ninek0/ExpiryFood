
using ExpiryFood.Models;
using ExpiryFood.Notification.NotificationProvider;

namespace ExpiryFood.Services
{
    public class ExpiryNotificationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly List<INotificationProvider> _notificationProvider;
        public ExpiryNotificationService(IServiceProvider serviceProvider, List<INotificationProvider> notificationProvider)
        {
            _serviceProvider = serviceProvider;
            _notificationProvider = notificationProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var now = DateTime.Now;
            var nextRun = new DateTime(now.Year, now.Month, now.Day, 12, 0, 0);
            if (now > nextRun)
                nextRun = nextRun.AddDays(1);
            await Task.Delay(nextRun - now, stoppingToken);
            while (!stoppingToken.IsCancellationRequested) 
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var productService = scope.ServiceProvider.GetService<ProductService>();
                    var expiryProducts = await productService!.GetExpiryProducts(3);
                    if (expiryProducts != null) await NotifyExpiryProduct(expiryProducts.ToList());
                }
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
        private Task NotifyExpiryProduct(List<Product> products)
        {
            return Task.Run( () =>
            {
                foreach (var provider in _notificationProvider)
                {
                    provider.SendNoticationAsync(products);
                }
            });
        }
    }
}
