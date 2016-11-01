using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using Web.Models.Admin;

namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IUserActivityService userActivityService;
        public AdminController(IUserActivityService userActivityService)
        {
            this.userActivityService = userActivityService;
        }
        public ActionResult Index()
        {
            var model = new UsersPageViewModel();
            model.PagedList = userActivityService.GetUserActivitiesPage(0, 5);
            return View(model);
        }

        [HttpPost]
        public ActionResult UsersPage(UsersPageViewModel model)
        {
            var page = userActivityService.GetUserActivitiesPage(model.PagedList.PageNumber, model.PagedList.PageSize);
            return PartialView("Partial/_UsersListPartial", page);
        }
    }
}