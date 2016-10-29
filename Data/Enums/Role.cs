using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enums
{
    [Flags]
    public enum Role : byte
    {
        Admin = 0,
        User = 1
    }
}
