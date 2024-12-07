using HashStrike.Api.Models.Data;

namespace HashStrike.Api.Services
{
    public class HostRegitrationService
    {
        private readonly ApplicationContext _db;
        public HostRegitrationService(ApplicationContext db)
        {
            _db = db;
        }
        public string CreateHostName()
        {
            string hostname = GenerateHostName();
            SaveHostName(hostname);
            return hostname;
        }
        private string GenerateHostName()
        {
            return "Host_" + Guid.NewGuid().ToString();
        }
        private void SaveHostName(string hostname)
        {
            Models.Host newHost = new Models.Host(hostname);

            _db.Hosts.Add(newHost);
            _db.SaveChanges(); 
        }
    }
}
