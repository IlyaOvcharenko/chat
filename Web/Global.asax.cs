using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using BusinessLogic;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var httpContext = HttpContext.Current;
            var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                if (authTicket != null)
                {
                    var userService = DependencyResolver.Current.GetService<IUserService>();
                    var userInfo = userService.GetUserByLogin(authTicket.Name);

                    if (userInfo != null)
                    {
                        GenericIdentity userIdentity = new GenericIdentity(authTicket.Name);
                        var userPrincipal = new GenericPrincipal(userIdentity,new string[] {userInfo.Role.ToString()});

                        httpContext.User = userPrincipal;
                        Thread.CurrentPrincipal = userPrincipal;
                    }
                    else
                    {
                        httpContext.User = null;
                        Thread.CurrentPrincipal = null;
                    }
                }
            }
        }
    }
}
