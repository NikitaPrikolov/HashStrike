using HashStrike.Client.Hashing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HashStrike.Client
{
    public static class TasksGetter
    {
        private static ServerClient serverClient = new ServerClient();
        private static HashTask currentTask = null;
        private static CancellationTokenSource currentTaskCancellationSource;
        public static async Task GetNewTask()
        {
            var response = await serverClient.GetResponseAsync(Program.hostName);
            if (response != null)
            {
                var newTask = ExtractTaskData(response);

                if (currentTask == null || !AreTasksEqual(currentTask, newTask))
                {
                    // Отменяем текущее выполнение, если оно есть
                    if (currentTaskCancellationSource != null)
                    {
                        currentTaskCancellationSource.Cancel();
                    }

                    // Создаем новый CancellationTokenSource для новой задачи
                    currentTaskCancellationSource = new CancellationTokenSource();
                    var cancellationToken = currentTaskCancellationSource.Token;

                    currentTask = newTask;

                    // Запускаем брутфорс в фоновом режиме
                    _ = Task.Run(() => Bruteforcer.StartBruteForce(currentTask, cancellationToken));
                }
            }
        }

        private static HashTask ExtractTaskData(string response)
        {
            var newTask = new HashTask();
            string[] lines = response.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                if (line.StartsWith("MD5 -") || line.StartsWith("SHA -"))
                {
                    newTask.TargetHash = line.Split('-')[1].Trim();
                    newTask.HashStrategy = line.StartsWith("MD5") ? (IHasher)new Md5Hasher() : (IHasher)new Sha256Hasher();
                }
                else if (line.StartsWith("From:"))
                {
                    newTask.StartString = line.Split(':')[1].Trim();
                }
                else if (line.StartsWith("To:"))
                {
                    newTask.EndString = line.Split(':')[1].Trim();
                }
                else if (line.StartsWith("HasCapitalLetters:"))
                {
                    newTask.HasUpperCase = bool.Parse(line.Split(':')[1].Trim());
                }
                else if (line.StartsWith("HasSmallLetters:"))
                {
                    newTask.HasLowerCase = bool.Parse(line.Split(':')[1].Trim());
                }
                else if (line.StartsWith("HasNumbers:"))
                {
                    newTask.HasDigits = bool.Parse(line.Split(':')[1].Trim());
                }
                else if (line.StartsWith("HasSpecialCharacters:"))
                {
                    newTask.HasSymbols = bool.Parse(line.Split(':')[1].Trim());
                }
            }

            return newTask;
        }
        public static bool AreTasksEqual(HashTask task1, HashTask task2)
        {
            if (task1 == null || task2 == null) return false;
            return task1.TargetHash == task2.TargetHash &&
                   task1.EndString == task2.EndString;
        }

    }
}
