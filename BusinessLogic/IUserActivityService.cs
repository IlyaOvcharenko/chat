using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Dto;
using BusinessLogic.Paging;
using Data;

namespace BusinessLogic
{
    public interface IUserActivityService
    {
        EntityDataPage<UserInfo> GetUserActivitiesPage(int pageNumber, int pageSize);

        void RegisterActivity(UserActivity userActivity);

    }
}
