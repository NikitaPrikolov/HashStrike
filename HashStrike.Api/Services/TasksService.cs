using HashStrike.Api.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace HashStrike.Api.Services
{
    public class TasksService
    {
        private readonly ApplicationContext _db;
        private readonly ActivityCheckingService _activityCheckingService;
        private readonly AnswerService _answerService;
        private static List<Models.Host> _activeHosts;
        private static int _currentDistributedTaskId;
        
        public TasksService(ApplicationContext db)
        {
            _db = db;
            _activityCheckingService = new ActivityCheckingService(db);
            _answerService = new AnswerService(db);
        }

        public void DistributeTasks()
        {
            // Получаем первое задание из таблицы tasks, если оно существует
            var firstTask = _db.Tasks.OrderBy(t => t.Id).FirstOrDefault();
            if (firstTask == null || firstTask.Id == _currentDistributedTaskId) return; // Если нет задач, ничего не делаем.

            // Получаем активные хосты
            _activeHosts = _activityCheckingService.GetActiveHosts();
            if (_activeHosts.Count == 0) return; // Если нет активных хостов, ничего не делаем.

            string symbols = GetSymbols(firstTask);

            List<(string from, string to)> ranges = new List<(string from, string to)>();
            BigInteger totalCombinations = 0;

            // Подсчет общего количества комбинаций от MinLineLength до MaxLineLength
            for (int length = firstTask.MinLineLength; length <= firstTask.MaxLineLength; length++)
            {
                totalCombinations += BigInteger.Pow(symbols.Length, length);
            }

            // Вычисление диапазона для каждого хоста
            for (int clientId = 0; clientId < _activeHosts.Count; clientId++)
            {
                BigInteger startRange = (totalCombinations / _activeHosts.Count) * clientId;
                BigInteger endRange = (clientId == _activeHosts.Count - 1)
                    ? totalCombinations - 1
                    : (totalCombinations / _activeHosts.Count) * (clientId + 1) - 1;

                // Генерация значений для диапазона
                string fromValue = GenerateValue(symbols, firstTask.MinLineLength, startRange);
                string toValue = GenerateValue(symbols, firstTask.MinLineLength, endRange);
                ranges.Add((fromValue, toValue));
            }

            // Сохранение задания для активных хостов
            for (int i = 0; i < ranges.Count; i++)
            {
                _activeHosts[i].Task = $"{firstTask.HashType} - {firstTask.Hash}\n" +
                                       $"HasCapitalLetters: {firstTask.HasCapitalLetters}\n" +
                                       $"HasSmallLetters: {firstTask.HasSmallLetters}\n" +
                                       $"HasNumbers: {firstTask.HasNumbers}\n" +
                                       $"HasSpecialCharacters: {firstTask.HasSpecialCharacters}\n" +
                                       $"From: {ranges[i].from}\n" +
                                       $"To: {ranges[i].to}";

                _db.Update(_activeHosts[i]);
            }

            _db.SaveChanges();
            _currentDistributedTaskId = firstTask.Id;
        }

        private string GetSymbols(Models.Task task)
        {
            string symbols = "";
            if (task.HasCapitalLetters) symbols += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (task.HasSmallLetters) symbols += "abcdefghijklmnopqrstuvwxyz";
            if (task.HasNumbers) symbols += "0123456789";
            if (task.HasSpecialCharacters) symbols += "!@#$%^&*()-_=+[]{};:'\",.<>?/\\|";
            return symbols;
        }

        private string GenerateValue(string symbols, int minLength, BigInteger index)
        {
            int currentLength = minLength;
            BigInteger baseCombinationCount = 0;

            while (baseCombinationCount <= index)
            {
                baseCombinationCount += BigInteger.Pow(symbols.Length, currentLength);
                currentLength++;
            }

            currentLength--;
            char[] result = new char[currentLength];
            index -= baseCombinationCount - BigInteger.Pow(symbols.Length, currentLength);
            for (int i = 0; i < currentLength; i++)
            {

                result[currentLength - 1 - i] = symbols[(int)(index % symbols.Length)];
                index /= symbols.Length;
            }

            return new string(result);
        }
        public IActionResult CreateAnswer(string name, string answer)
        {
            string hash = _db.Tasks.OrderBy(t => t.Id).FirstOrDefault().Hash;
            var host = _db.Hosts.FirstOrDefault(h => h.Name == name);
            if (host == null) return new NotFoundResult();

            var hostToChange = _activeHosts.FirstOrDefault(h => h.Name == name);
            if (hostToChange != null)
                hostToChange.Answer = answer;

            if (answer == "false")
            {
                HandleFalseAnswer(host, hash);
            }
            else
            {
                string correctAnswer = answer.ToString();
                _answerService.AnswerHandler(true, _activeHosts, hash, correctAnswer);
            }
            _db.Update(host);
            _db.SaveChanges();
            return new OkResult();
        }
        private void HandleFalseAnswer(Models.Host host, string hash)
        {

            if (_activeHosts.All(h => h.Answer == "false"))
            {
                _answerService.AnswerHandler(false, _activeHosts, hash);
            }
            // Получаем актуальный список активных хостов
            var currentActiveHosts = _activityCheckingService.GetActiveHosts();

            // Находим неактивные хосты из текущего списка активных хостов
            var inactiveHost = _activeHosts.FirstOrDefault(h =>
                !currentActiveHosts.Any(activeHost => activeHost.Id == h.Id) &&
                IsHostActive(h) == false);

            if (inactiveHost != null)
            {
                // Передаем задание неактивного хоста активному хосту
                host.Task = inactiveHost.Task;

                // Очищаем поля Task и Answer у неактивного хоста
                inactiveHost.Task = null;
                inactiveHost.Answer = null;
                host.Answer = null;

                // Обновляем состояние хостов в базе данных
                _db.Entry(host).State = EntityState.Modified;
                _db.Entry(inactiveHost).State = EntityState.Modified;
                _db.SaveChanges();

                // Удаляем неактивного хоста из списка активных хостов
                _activeHosts.Remove(inactiveHost);
                return;
            }
        }

        private bool IsHostActive(Models.Host host)
        {
            return (DateTime.Now - host.LastRequestTime).TotalSeconds < 12;
        }
    }

}
