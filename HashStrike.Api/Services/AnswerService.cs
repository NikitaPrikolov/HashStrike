using HashStrike.Api.Models.Data;

namespace HashStrike.Api.Services
{
    public class AnswerService
    {
        private static List<string> answers = new List<string>();
        private readonly ApplicationContext _db;

        public AnswerService(ApplicationContext db)
        {
            _db = db;
        }
        public void AnswerHandler(bool IsAnswerFounded, List<Models.Host> _activeHosts, string hash, string correctAnswer = null)
        {
            var oldestTask = _db.Tasks.OrderBy(t => t.Id).FirstOrDefault();
            if (oldestTask != null)
            {
                _db.Tasks.Remove(oldestTask);
                _db.SaveChanges();
            }
            foreach (var host in _db.Hosts)
            {
                host.Task = null;
                host.Answer = null;
            }
            if (IsAnswerFounded)
            {
                answers.Add($"Удалось подобрать значение для хеша: {hash}, значение: {correctAnswer}");
            }
            else
            {
                answers.Add($"Не удалось подобрать значение для хеша: {hash}");
            }
        }
        public void CleanAnswers()
        {
            answers.Clear();
        }
        public List<string> GetAnswers()
        {
            return new List<string>(answers);
        }
    }

}
