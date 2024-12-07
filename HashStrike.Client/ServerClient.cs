using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HashStrike.Client
{
    public class ServerClient
    {
        private readonly HttpClient _httpClient;
        private static string url = "http://localhost:18865/api/hosts/";

        public ServerClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetResponseAsync(string additionalUrl)
        {
            try
            {
                return await _httpClient.GetStringAsync(url+additionalUrl);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task SendResultAsync(string additionalUrl, string result)
        {
            string formatedString = $"\"{result}\"";
            var content = new StringContent(formatedString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url + additionalUrl, content);
        }
    }
}
