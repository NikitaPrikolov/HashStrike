using HashStrike.Bot.Interfaces;
using HashStrike.Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HashStrike.Bot.Services
{
    public class UserInputService : IUserInputService
    {
        public async Task HandleUserInput(Update update)
        {
            var chatId = update.Message.Chat.Id;

            if (CommonVariables.hashType == null) // Если тип хеша еще не выбран
            {
                CommonVariables.hashType = update.Message.Text.ToUpper(); // Сохраняем тип хеша
                await CommonVariables.botClient.SendTextMessageAsync(chatId, "Введите хеш:");
            }
            else if (string.IsNullOrEmpty(CommonVariables.hash)) // Если хеш еще не введен
            {
                CommonVariables.hash = update.Message.Text; // Сохраняем хеш
                await MessageService.AskMinLength(chatId);
            }
            else if (CommonVariables.minLength == 0) // Если минимальная длина еще не введена
            {
                if (int.TryParse(update.Message.Text, out CommonVariables.minLength) && CommonVariables.minLength > 0)
                {
                    await MessageService.AskMaxLength(chatId);
                }
                else
                {
                    await CommonVariables.botClient.SendTextMessageAsync(chatId, "Ошибка ввода. Введите минимальную длину хеша:");
                }
            }
            else if (CommonVariables.maxLength == 0) // Если максимальная длина еще не введена
            {
                if (int.TryParse(update.Message.Text, out CommonVariables.maxLength) && CommonVariables.maxLength > CommonVariables.minLength && CommonVariables.maxLength <= 32)
                {
                    await MessageService.AskHasCapitalLetters(chatId);
                }
                else
                {
                    await CommonVariables.botClient.SendTextMessageAsync(chatId, "Неправильный ввод. Введите максимальную длину хеша:");
                }
            }
        }
    }
}
