using System.Text;
using ExpiryFood.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ExpiryFood.Notification.NotificationProvider
{
    public class TelegramNotificationProvider : INotificationProvider
    {
        private readonly TelegramBotClient client;
        private readonly List<ChatId> chatsId;
        public TelegramNotificationProvider(TelegramBotClient telegramBotClient, List<ChatId> chats)
        {
            client = telegramBotClient;
            chatsId = chats;
        }
        public async Task SendNoticationAsync(List<Product> products)
        {
            var message = BuildNotificationMessage(products);

            foreach (var chatId in chatsId)
            {
                await client.SendMessage(
                    chatId: chatId,
                    text: message
                    );
            }
        }
        private string BuildNotificationMessage(List<Product> products)
        {
            var sb = new StringBuilder();
            sb.AppendLine("⚠️ *Срочно! Продукты скоро испортятся*");

            foreach (var product in products)
            {
                var daysLeft = (product.ExpireAt - DateTime.Now).Days;
                sb.AppendLine($"\n📦 {product.Name} - осталось {daysLeft} дн.");
            }

            return sb.ToString();
        }
    }
}
