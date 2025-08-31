using ExpiryFood.Notification.NotificationProvider;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ExpiryFood.Notification
{
    public static class NotificationProviderFactory
    {
        public static INotificationProvider CreateTelegramProvider(IConfiguration config)
        {
            var botToken = config["NotificationConfig:Telegram:BotToken"];
            var chatIds = config.GetSection("NotificationConfig:Telegram:ChatIds")
                               .Get<List<long>>();
            var chats = chatIds!.Select(static chatValue => new ChatId(chatValue)).ToList();
            return new TelegramNotificationProvider(
                new TelegramBotClient(botToken),
                chats
            );
        }

        //public static INotificationProvider CreateEmailProvider(IConfiguration config)
        //{
        //    var smtpServer = config["NotificationConfig:Email:SmtpServer"];
        //    var port = int.Parse(config["NotificationConfig:Email:Port"]);
        //    var fromAddress = config["NotificationConfig:Email:FromAddress"];

        //    return new EmailNotificationProvider(smtpServer, port, fromAddress);
        //}
    }
}
