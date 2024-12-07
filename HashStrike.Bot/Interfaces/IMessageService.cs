using Telegram.Bot.Types.ReplyMarkups;

namespace HashStrike.Bot.Interfaces
{
    public interface IMessageService
    {
        Task SendTextMessageAsync(long chatId, string message, InlineKeyboardMarkup? inlineKeyboard);
    }
}
