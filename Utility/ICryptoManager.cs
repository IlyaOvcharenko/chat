using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public interface ICryptoManager
    {
        string GetHash(string input);

        bool VarifyHash(string hash, string input);
    }
}
