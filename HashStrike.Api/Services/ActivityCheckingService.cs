using HashStrike.Api.Models.Data;

namespace HashStrike.Api.Services
{
    public class ActivityCheckingService
    {
        private readonly ApplicationContext _db;

        public ActivityCheckingService(ApplicationContext db)
        {
            _db = db;
        }
        public List<Models.Host> GetActiveHosts()
        {
            var currentTime = DateTime.Now;
            var thresholdTime = currentTime.AddSeconds(-12);

            return _db.Hosts
                .Where(h => h.LastRequestTime >= thresholdTime)
                .ToList();
        }
    }
}
