using System;
using System.Security.Cryptography;
using System.Text;

namespace HashStrike.Client.Hashing
{
    public class Md5Hasher : IHasher
    {
        public string ComputeHash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
