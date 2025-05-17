using System.Web.Http;
using SalesHomes.Repositorys;
using SalesHomes.Services;
using SalesHomes.Services.Ports;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace SalesHomes
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<SaleProfile>();
            //});

            //var mapper = config.CreateMapper();
            //container.RegisterInstance(mapper);

            container.RegisterType<ITorneoRepository, TorneoRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAdministradorITMRepository, AdministradorITMRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<TorneoService>(new HierarchicalLifetimeManager());
            container.RegisterType<AdministradorITMService>(new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
