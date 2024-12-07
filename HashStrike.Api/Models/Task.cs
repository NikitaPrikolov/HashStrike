using HashStrike.Common.Models;

namespace HashStrike.Api.Models
{
    public class Task
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

        public Task() { }
        public Task(string htype, string hash, int minlength, int maxlength, bool hasCapitalLetters,
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
        public TaskModel ToDto()
        {
            return new TaskModel()
            {
                Id = this.Id,
                HashType = this.HashType,
                Hash = this.Hash,
                MinLineLength = this.MinLineLength,
                MaxLineLength = this.MaxLineLength,
                HasCapitalLetters = this.HasCapitalLetters,
                HasSmallLetters = this.HasSmallLetters,
                HasNumbers = this.HasNumbers,
                HasSpecialCharacters = this.HasSpecialCharacters
            };
        }
    }
}
