using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Base;

namespace Data
{
    public class UserActivity : BaseEntity
    {
        public int UserId { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime Date { get; set; }

        public int MessageCount { get; set; }

        public string ClientIpAddress { get; set; }
        public virtual User User { get; set; }
    }
}
