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
        private IUserService userService;
        public AdminController(IUserService userService)
        {
            this.userService = userService;
        }
        public ActionResult Index()
        {
            var model = new UsersPageViewModel();
            model.PagedList = userService.GetUsersPage(0, 5);
            return View(model);
        }

        [HttpPost]
        public ActionResult UsersPage(UsersPageViewModel model)
        {
            var page = userService.GetUsersPage(model.PagedList.PageNumber, model.PagedList.PageSize);
            return PartialView("Partial/_UsersListPartial", page);
        }
    }
}