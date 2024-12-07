using Telegram.Bot;

namespace HashStrike.Bot.Models
{
    public static class CommonVariables
    {
        public static readonly string botToken = "TOKEN"; // Укажите токен вашего бота
        public static readonly HttpClient httpClient = new HttpClient();
        public static readonly ITelegramBotClient botClient = new TelegramBotClient(botToken);
        public static int activeHostCount;
        public static long userId;
        public static string hashType;
        public static string hash;
        public static int minLength;
        public static int maxLength;
        public static bool? hasCapitalLetters = null;
        public static bool? hasSmallLetters = null;
        public static bool? hasNumbers = null;
        public static bool? hasSpecialCharacters = null;
    }
}
