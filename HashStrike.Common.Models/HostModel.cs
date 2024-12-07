namespace HashStrike.Common.Models
{
    public class HostModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastRequestTime { get; set; }
        public string? Task { get; set; }
        public string? Answer { get; set; }
        public HostModel(string name, DateTime lastRequestTime, string? task, string? answer)
        {
            Name = name;
            LastRequestTime = lastRequestTime;
            Task = task;
            Answer = answer;
        }
        public HostModel() { }
    }
}
