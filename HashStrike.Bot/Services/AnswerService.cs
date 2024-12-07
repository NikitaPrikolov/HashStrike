using HashStrike.Bot.Utilites;

namespace HashStrike.Bot.Services
{
    public class AnswerService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static string url = "http://localhost:18865/api/tasks/answers";

        public static async void GetAnswers()
        {
            
            try
            {
                var answer = await ApiClient.GetAnswersAsync();
                if (answer != null)
                {
                    MessageService.SendAnswer(answer);
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
