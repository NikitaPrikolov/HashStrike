using HashStrike.Bot.Interfaces;
using HashStrike.Bot.Models;
using HashStrike.Bot.Utilites;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace HashStrike.Bot.Services
{
    public class BotService : IBotService
    {
        public static long chatId;

        public async void StartReceiving(CancellationToken cancellationToken)
        {
            CommonVariables.botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, cancellationToken: cancellationToken);
        }

        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message?.Text != null)
            {
                chatId = update.Message.Chat.Id;
                

                if (update.Message.Text == "/start")
                {
                    CommonVariables.userId = update.Message.From.Id;
                    await TaskService.StartNewTask(chatId);
                }
                else if (update.Message.From.Id == CommonVariables.userId)
                {
                    IUserInputService _userInputService = new UserInputService();
                    await _userInputService.HandleUserInput(update);
                }
            }
            else if (update.Type == UpdateType.CallbackQuery) // Обработка нажатий кнопок
            {
                var callbackQuery = update.CallbackQuery;
                chatId = callbackQuery.Message.Chat.Id;

                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id); // Подтверждаем нажатие кнопки

                if (CommonVariables.hashType == null) // Если тип хеша еще не выбран
                {
                    CommonVariables.hashType = callbackQuery.Data; // Сохраняем тип хеша
                    await botClient.SendTextMessageAsync(chatId, "Введите хеш:");
                }
                else if (string.IsNullOrEmpty(CommonVariables.hash)) // Если хеш еще не введен
                {
                    CommonVariables.hash = callbackQuery.Data; // Это может быть "Да" или "Нет", но мы ожидаем текстовое сообщение
                    await MessageService.AskMinLength(chatId);
                }
                else if (CommonVariables.minLength == 0) // Если минимальная длина еще не введена
                {
                    if (int.TryParse(callbackQuery.Data, out CommonVariables.minLength) && CommonVariables.minLength > 0)
                    {
                        await MessageService.AskMaxLength(chatId);
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(chatId, "Ошибка ввода. Введите минимальную длину хеша:");
                    }
                }
                else if (CommonVariables.hasCapitalLetters == null) // Ответ на вопрос о заглавных буквах
                {
                    CommonVariables.hasCapitalLetters = callbackQuery.Data.ToLower() == "да";
                    await MessageService.AskHasSmallLetters(chatId);
                }
                else if (CommonVariables.hasSmallLetters == null) // Ответ на вопрос о строчных буквах
                {
                    CommonVariables.hasSmallLetters = callbackQuery.Data.ToLower() == "да";
                    await MessageService.AskHasNumbers(chatId);
                }
                else if (CommonVariables.hasNumbers == null) // Ответ на вопрос о цифрах
                {
                    CommonVariables.hasNumbers = callbackQuery.Data.ToLower() == "да";
                    await MessageService.AskHasSpecialCharacters(chatId);
                }
                else if (CommonVariables.hasSpecialCharacters == null) // Ответ на вопрос о специальных символах
                {
                    CommonVariables.activeHostCount = await ApiClient.GetActiveHostCountAsync();
                    if (CommonVariables.activeHostCount == 0)
                    {
                        await MessageService.NoAvaibleHosts(chatId);
                    }
                    else
                    {

                        CommonVariables.hasSpecialCharacters = callbackQuery.Data.ToLower() == "да";
                        ApiClient apiClient = new ApiClient();
                        await apiClient.PostDataAsync(chatId);
                    }
                }
                else if (callbackQuery.Data == "Новое задание") // Обработка кнопки "Новое задание"
                {
                    await TaskService.StartNewTask(chatId);
                }
            }
        }

        private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Ошибка: {exception.Message}");
        }
    }

}
