using HashStrike.Bot.Interfaces;
using HashStrike.Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace HashStrike.Bot.Services
{
    public class MessageService : IMessageService
    {

        public async Task SendTextMessageAsync(long chatId, string message, InlineKeyboardMarkup? inlineKeyboard)
        {
            await CommonVariables.botClient.SendTextMessageAsync(chatId, message, replyMarkup: inlineKeyboard = null);
        }

        public static async Task AskHashType(long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
            new[] { InlineKeyboardButton.WithCallbackData("SHA"), InlineKeyboardButton.WithCallbackData("MD5") }
        });

            await CommonVariables.botClient.SendTextMessageAsync(chatId, "Выберите тип хеша:", replyMarkup: inlineKeyboard);
        }

        public static async Task AskMinLength(long chatId)
        {
            await CommonVariables.botClient.SendTextMessageAsync(chatId, "Введите минимальную длину хеша:");
        }

        public static async Task AskMaxLength(long chatId)
        {
            await CommonVariables.botClient.SendTextMessageAsync(chatId, "Введите максимальную длину хеша:");
        }

        public static async Task AskHasCapitalLetters(long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
            new[] { InlineKeyboardButton.WithCallbackData("Да"), InlineKeyboardButton.WithCallbackData("Нет") }
        });

            await CommonVariables.botClient.SendTextMessageAsync(chatId, "Содержит ли хеш большие буквы?", replyMarkup: inlineKeyboard);
        }

        public static async Task AskHasSmallLetters(long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
            new[] { InlineKeyboardButton.WithCallbackData("Да"), InlineKeyboardButton.WithCallbackData("Нет") }
        });

            await CommonVariables.botClient.SendTextMessageAsync(chatId, "Содержит ли хеш маленькие буквы?", replyMarkup: inlineKeyboard);
        }

        public static async Task AskHasNumbers(long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
            new[] { InlineKeyboardButton.WithCallbackData("Да"), InlineKeyboardButton.WithCallbackData("Нет") }
        });

            await CommonVariables.botClient.SendTextMessageAsync(chatId, "Содержит ли хеш цифры?", replyMarkup: inlineKeyboard);
        }

        public static async Task AskHasSpecialCharacters(long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
            new[] { InlineKeyboardButton.WithCallbackData("Да"), InlineKeyboardButton.WithCallbackData("Нет") }
        });

            await CommonVariables.botClient.SendTextMessageAsync(chatId, "Содержит ли хеш специальные символы?", replyMarkup: inlineKeyboard);
        }
        public static IReplyMarkup GetNewTaskKeyboard()
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
            new[] { InlineKeyboardButton.WithCallbackData("Новое задание") }
        });

            return inlineKeyboard;
        }
        public static async Task NoAvaibleHosts(long chatId)
        {
            await CommonVariables.botClient.SendTextMessageAsync(chatId, "В системе нет активных хостов!");
        }

        public static async Task SendAvaibleHosts(long chatId)
        {
            await CommonVariables.botClient.SendTextMessageAsync(chatId, $"Количество активных хостов: {CommonVariables.activeHostCount}");
        }
        public static async Task SendAnswer(string answer)
        {
            await CommonVariables.botClient.SendTextMessageAsync(BotService.chatId, answer);
        }
    }
}
