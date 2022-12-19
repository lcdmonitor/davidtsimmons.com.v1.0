using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System;
using DavidSimmons.Client;
using DavidSimmons.Core.Cloud;
using DavidSimmons.Core.Cloud.Interfaces;
using DavidSimmons.Repository.Interfaces;
using DavidSimmons.Repository;

namespace DavidSimmons
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<UnityContainer> container = new Lazy<UnityContainer>(() =>
        {
            var container = new UnityContainer();
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static UnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        private static void RegisterComponents()
        {
            var container = GetConfiguredContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IBlogClient, BlogClient>();
            container.RegisterType<IWebLogClient, WebLogClient>();
            container.RegisterType<IBusFactory, BusFactory>();
            //todo: not sure if i liek having a refernce to repository here coudl be abused
            container.RegisterType<IWebRepository, WebRepository>();
        }

        public static void RegisterContainer()
        {
            RegisterComponents();
            DependencyResolver.SetResolver(new UnityDependencyResolver(GetConfiguredContainer()));
        }
    }
}