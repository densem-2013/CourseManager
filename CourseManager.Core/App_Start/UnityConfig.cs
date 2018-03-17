using System;
using CourseManager.Core.Repositories;
using CourseManager.Core.UnitOfWork;
using Unity;
using Unity.AspNet.Mvc;
using Unity.AspNet.WebApi;

namespace CourseManager.Core
{
    public static class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();

            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        public static System.Web.Http.Dependencies.IDependencyResolver GetDependensyResolver()
        {
            var resolver = new UnityHierarchicalDependencyResolver(Container.Value);

            return resolver;
        }

        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<CourseManagerContext>()
            .RegisterType<IUnitOfWork, UnitOfWork.UnitOfWork>(new PerRequestLifetimeManager())
            .RegisterType(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}