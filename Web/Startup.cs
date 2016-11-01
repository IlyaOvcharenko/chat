using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLogic;
using Data;
using DataAccess.Repositories;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using Web.Hubs;

[assembly: OwinStartup(typeof(Web.Startup))]

namespace Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            GlobalHost.DependencyResolver.Register(
           typeof(ChatHub),
           () => new ChatHub(DependencyResolver.Current.GetService<IUserActivityService>(), DependencyResolver.Current.GetService<IUserService>()));

            app.MapSignalR();
        }
    }
}
