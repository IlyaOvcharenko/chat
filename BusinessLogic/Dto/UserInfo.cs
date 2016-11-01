using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dto
{
    public class UserInfo
    {
        public string Login { get; set; }

        public string City { get; set; }

        public DateTime LastActivityDateTime { get; set; }

        public int MessageCount { get; set; }

        public double AverageMessageCount { get; set; }

        public string ClientIpAddress { get; set; }
    }
}
