using HashStrike.Bot.Interfaces;
using HashStrike.Bot.Services;
using Telegram.Bot;

class Program
{
    static async Task Main(string[] args)
    {
        IBotService botService = new BotService();
        var cts = new CancellationTokenSource();
        botService.StartReceiving(cts.Token);

        Console.WriteLine("Бот запущен...");
        while (true)
        {
            AnswerService.GetAnswers();
            Thread.Sleep(10000);
        }
        Console.ReadLine();
        cts.Cancel();
    }
}
