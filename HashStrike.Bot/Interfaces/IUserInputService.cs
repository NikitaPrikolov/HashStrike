using Telegram.Bot.Types;

namespace HashStrike.Bot.Interfaces
{
    public interface IUserInputService
    {
        Task HandleUserInput( Update update);
    }
}
