using HashStrike.Common.Models;

namespace HashStrike.Api.Models
{
    public class Host
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastRequestTime { get; set; }
        public string? Task {  get; set; }
        public string? Answer { get; set; }
        public Host() { }
        public Host(string name, DateTime lastRequestTime, string? task, string? answer)
        {
            Name = name;
            LastRequestTime = lastRequestTime;
            Task = task;
            Answer = answer;
        }
        public Host(string name)
        {
            Name = name;
            LastRequestTime = DateTime.Now;
            Task = null;
            Answer = null;
        }
        public HostModel ToDto()
        {
            return new HostModel 
            { 
                Id = this.Id, 
                Name = this.Name,
                LastRequestTime = this.LastRequestTime, 
                Task = this.Task, 
                Answer = this.Answer 
            };
        }

    }
}
