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
        private IBaseRepository<UserActivity> userActivityRepository;

        private IUserRepository userRepository;

        private static ICollection<UserActivity> localUserActivitiesStorage; 

        private static int _activitiesToRegister;

        public UserActivityService(IBaseRepository<UserActivity> userActivityRepository, IUserRepository userRepository)
        {
            this.userActivityRepository = userActivityRepository;
            this.userRepository = userRepository;
            localUserActivitiesStorage = new List<UserActivity>();
        }
        public EntityDataPage<UserInfo> GetUserActivitiesPage(int pageNumber, int pageSize)
        {
            var query = userRepository.GetAll();

            var count = query.Count();

            var list =
                query.Select(
                    u =>
                        new UserInfo
                        {
                            Login = u.Login,
                            City = u.City,
                            ClientIpAddress =
                                u.UserActivities.FirstOrDefault(a => a.DateTime == u.UserActivities.Max(d => d.DateTime))
                                    .ClientIpAddress,
                            LastActivityDateTime = u.UserActivities.Max(d => d.DateTime),
                            MessageCount = u.UserActivities.Sum(m => m.MessageCount)
                        }).OrderBy(u => u.Login).Skip(pageSize*pageNumber)
                    .Take(pageSize)
                    .ToList();
            ;
            list.ForEach(i => i.AverageMessageCount = GetAverageMessageCount(i.City));
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
            
            var existLocalActivity = localUserActivitiesStorage.FirstOrDefault(a => a.UserId == userActivity.UserId && a.Date == date);

            if (existLocalActivity != null)
            {
                existLocalActivity.DateTime = userActivity.DateTime;
                existLocalActivity.ClientIpAddress = userActivity.ClientIpAddress;
                existLocalActivity.MessageCount += userActivity.MessageCount;
            }
            else
            {
                localUserActivitiesStorage.Add(userActivity);
            }
            _activitiesToRegister += 1;
            if (_activitiesToRegister > 5)
            {
                foreach (var activity in localUserActivitiesStorage)
                {
                    var remoteActivity = userActivityRepository.GetAll().FirstOrDefault(a => a.UserId == activity.UserId && a.Date == date);
                    if (remoteActivity != null)
                    {
                        remoteActivity.DateTime = activity.DateTime;
                        remoteActivity.ClientIpAddress = activity.ClientIpAddress;
                        remoteActivity.MessageCount += activity.MessageCount;
                        userActivityRepository.Update(remoteActivity);
                    }
                    else
                    {
                        userActivityRepository.Add(activity);
                    }
                }
                _activitiesToRegister = 0;
                localUserActivitiesStorage.Clear();
                userActivityRepository.SaveChanges();
            }
        }

        private double? GetAverageMessageCount(string city)
        {
            var query = userActivityRepository.GetAll().Where(a => a.User.City == city && a.Date == DateTime.Today);
            if (!query.Any())
                return 0;
            double? avg = query.Average(a => a.MessageCount);
            return avg;

        }
    }
}
