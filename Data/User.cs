using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Base;
using Data.Enums;

namespace Data
{
    public class User : BaseEntity
    {
        public string Login { get; set; }

        public string City { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
