using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Dto;
using BusinessLogic.Paging;
using Data;
using DataAccess.Repositories;

namespace BusinessLogic
{
    public class UserActivityService : IUserActivityService
    {
        private IUserActivityRepository userActivityRepository;

        private static int _activitiesToRegister;
        public UserActivityService(IUserActivityRepository userActivityRepository)
        {
            this.userActivityRepository = userActivityRepository;
        }
        public EntityDataPage<UserInfo> GetUserActivitiesPage(int pageNumber, int pageSize)
        {
            var dtoQuery = from a in userActivityRepository.GetAll()
                group a by a.UserId
                into grouped
                select new UserInfo {Login = grouped.First().User.Login, City = grouped.First().User.City,
                    MessageCount = grouped.Sum(g => g.MessageCount),
                    ClientIpAddress = grouped.FirstOrDefault(g=>g.DateTime == grouped.Max(d=>d.DateTime)).ClientIpAddress, LastActivityDateTime = grouped.Max(d => d.DateTime),
                    AverageMessageCount = userActivityRepository.GetAll().Where(ac=>ac.User.City == grouped.FirstOrDefault().User.City).Average(act=>act.MessageCount)
                };
            var count = dtoQuery.Count();

            var list = dtoQuery.Skip(pageSize * pageNumber)
                   .Take(pageSize)
                   .ToList();
            return new EntityDataPage<UserInfo>
            {
                EntityCount = count,
                List = list,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

        }

        public void RegisterActivity(UserActivity userActivity)
        {
            var date = DateTime.Today;
            var existActivity = userActivityRepository.GetAll().FirstOrDefault(a => a.UserId == userActivity.UserId && a.Date == date);
            var existLocalActivity = userActivityRepository.GetLocals().FirstOrDefault(a => a.UserId == userActivity.UserId && a.Date == date);
            if (existActivity != null)
            {
                existActivity.DateTime = userActivity.DateTime;
                existActivity.ClientIpAddress = userActivity.ClientIpAddress;
                existActivity.MessageCount += userActivity.MessageCount;
                userActivityRepository.Update(existActivity);
            }
            else if (existLocalActivity != null)
            {
                existLocalActivity.DateTime = userActivity.DateTime;
                existLocalActivity.ClientIpAddress = userActivity.ClientIpAddress;
                existLocalActivity.MessageCount += userActivity.MessageCount;
            }
            else
            {
                userActivityRepository.Create(userActivity);
            }
            _activitiesToRegister += 1;
            if (_activitiesToRegister > 5)
            {
                userActivityRepository.Flush();
            }
        }
    }
}
