using Telegram.Bot.Types;
using Telegram.Bot;

namespace HashStrike.Bot.Interfaces
{
    public interface IBotService
    {
        public async void StartReceiving(CancellationToken cancellationToken) { }
        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) { }
        private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken) { }
    }
}
