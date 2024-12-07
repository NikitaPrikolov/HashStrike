using HashStrike.Bot.Models;
using HashStrike.Bot.Utilites;

namespace HashStrike.Bot.Services
{
    public class TaskService
    {
        public static async Task StartNewTask(long chatId)
        {
            CommonVariables.hashType = null;
            CommonVariables.hash = null;
            CommonVariables.minLength = 0;
            CommonVariables.maxLength = 0;
            CommonVariables.hasCapitalLetters = null;
            CommonVariables.hasSmallLetters = null;
            CommonVariables.hasNumbers = null;
            CommonVariables.hasSpecialCharacters = null;

            CommonVariables.activeHostCount = await ApiClient.GetActiveHostCountAsync();
            if (CommonVariables.activeHostCount == 0)
            {
                await MessageService.NoAvaibleHosts(chatId);
            }
            else
            {

                await MessageService.SendAvaibleHosts(chatId);
                await MessageService.AskHashType(chatId);
            }
        }

    }

}
