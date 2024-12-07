using HashStrike.Bot.Models;
using HashStrike.Bot.Services;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;
using Telegram.Bot;

namespace HashStrike.Bot.Utilites
{
    public class ApiClient
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public async Task PostDataAsync(long chatId)
        {
            var json = new
            {
                HashType = CommonVariables.hashType,
                Hash = CommonVariables.hash,
                MinLineLength = CommonVariables.minLength,
                MaxLineLength = CommonVariables.maxLength,
                HasCapitalLetters = CommonVariables.hasCapitalLetters,
                HasSmallLetters = CommonVariables.hasSmallLetters,
                HasNumbers = CommonVariables.hasNumbers,
                HasSpecialCharacters = CommonVariables.hasSpecialCharacters
            };

            var response = await httpClient.PostAsync("http://localhost:18865/api/tasks/create",
                new StringContent(JsonSerializer.Serialize(json), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                await CommonVariables.botClient.SendTextMessageAsync(chatId, "Задание создано.", replyMarkup: MessageService.GetNewTaskKeyboard());
            }
            else
            {
                await CommonVariables.botClient.SendTextMessageAsync(chatId, "Ошибка при добавлении задания.");
            }
        }
        public static async Task<int> GetActiveHostCountAsync()
        {
            var response = await httpClient.GetAsync("http://localhost:18865/api/hosts/active-hosts");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var activeHosts = JArray.Parse(json); // Разбираем JSON как массив

            return activeHosts.Count; // Возвращаем количество активных хостов
        }
        public static async Task<string> GetAnswersAsync()
        {
            var response = await httpClient.GetAsync("http://localhost:18865/api/tasks/answers");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var answers = JArray.Parse(json); // Разбираем JSON как массив

            // Объединяем элементы массива в одну строку, разделяя их переносом строки
            return string.Join(Environment.NewLine, answers);
        }

    }

}
