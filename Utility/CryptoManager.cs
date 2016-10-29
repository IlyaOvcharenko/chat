using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class CryptoManager : ICryptoManager
    {
        public string GetHash(string input)
        {
            var md5Hasher = MD5.Create();
            var hash = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            var sb = new StringBuilder();

            foreach (var b in hash)
            {
                sb.Append(b.ToString());
            }

            return sb.ToString();
        }

        public bool VarifyHash(string hash, string input)
        {
            var inputHash = GetHash(input);
            return 0 == hash.CompareTo(inputHash);
        }
    }
}
