using System;
using System.Data.Entity;
using BusinessLogic;
using Data;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Utility;

namespace Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here            
            container.RegisterType<DbContext, DataContext>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserActivityService, UserActivityService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IBaseRepository<UserActivity>, UserActivityRepository>();
            container.RegisterType<IBaseRepository<User>, BaseRepository<User>>();
            container.RegisterType<ICryptoManager, CryptoManager>();
        }
    }
}
