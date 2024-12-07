namespace HashStrike.Common.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string HashType { get; set; }
        public string Hash { get; set; }
        public int MinLineLength { get; set; }
        public int MaxLineLength { get; set; }
        public bool HasCapitalLetters { get; set; }
        public bool HasSmallLetters { get; set; }
        public bool HasNumbers { get; set; }
        public bool HasSpecialCharacters { get; set; }
        public TaskModel(string htype, string hash, int minlength, int maxlength, bool hasCapitalLetters,
            bool hasSmallLetters, bool hasNumbers, bool hasSpecialCharacters)
        {
            HashType = htype;
            Hash = hash;
            MinLineLength = minlength;
            MaxLineLength = maxlength;
            HasCapitalLetters = hasCapitalLetters;
            HasSmallLetters = hasSmallLetters;
            HasNumbers = hasNumbers;
            HasSpecialCharacters = hasSpecialCharacters;
        }
        public TaskModel() { }
    }
}
