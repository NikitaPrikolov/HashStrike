using HashStrike.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
    public static string hostName;

    static async Task Main(string[] args)
    {
        Console.SetWindowSize(120, 30);
        AsciiArt.PaintArt();
        hostName = NameGetter.GetHostName();
        while (true)
        {
            await TasksGetter.GetNewTask();
            Thread.Sleep(5000);
        }
    }

}