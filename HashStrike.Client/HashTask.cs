using HashStrike.Client.Hashing;

namespace HashStrike.Client
{
    public class HashTask
    {
        public string TargetHash { get; set; }
        public string StartString { get; set; }
        public string EndString { get; set; }
        public bool HasUpperCase { get; set; }
        public bool HasLowerCase { get; set; }
        public bool HasDigits { get; set; }
        public bool HasSymbols { get; set; }
        public IHasher HashStrategy { get; set; }
        
    }

}
