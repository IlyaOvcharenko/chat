using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogic.Dto;
using BusinessLogic.Paging;
using Data;

namespace Web.Models.Admin
{
    public class UsersPageViewModel
    {
        public EntityDataPage<UserInfo> PagedList { get; set; }
    }
}