using System.IO;

namespace HashStrike.Client
{
    public static class NameGetter
    {
        private static ServerClient serverClient = new ServerClient();
        public static string GetHostName()
        {
            const string fileName = "hostname.txt";
            if (File.Exists(fileName))
            {
                return File.ReadAllText(fileName);
            }

            var response = serverClient.GetResponseAsync("register").Result;
            File.WriteAllText(fileName, response);
            return response;
        }
    }
}
