using System;
using System.Security.Cryptography;
using System.Text;

namespace HashStrike.Client.Hashing
{
    public class Sha256Hasher : IHasher
    {
        public string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
