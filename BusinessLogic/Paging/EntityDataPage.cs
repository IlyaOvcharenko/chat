using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Paging
{
    public class EntityDataPage<T>
    {
        public IList<T> List { get; set; }

        public int EntityCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int PageCount
        {
            get
            {
                return Convert.ToInt32(Math.Ceiling((double)EntityCount / PageSize));
            }
        }
    }
}
