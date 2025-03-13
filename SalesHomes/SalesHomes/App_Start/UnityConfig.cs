using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;
using SalesHomes.Repositorys;
using SalesHomes.Services.Ports;
using SalesHomes.Services;
using SalesHomes.Models;
using AutoMapper;
using SalesHomes.Profiles;
using SaleRepository = SalesHomes.Repositorys.SaleRepository;

namespace SalesHomes
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SaleProfile>();
            });

            var mapper = config.CreateMapper();
            container.RegisterInstance(mapper);

            container.RegisterType<ISaleManager, SaleRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAgencyManager, AgencyRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IClientManager, ClientRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IHousingManager, HousingRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<SaleService>(new HierarchicalLifetimeManager());
            container.RegisterType<HousingService>(new HierarchicalLifetimeManager());
            container.RegisterType<AgencyService>(new HierarchicalLifetimeManager());
            container.RegisterType<ClientService>(new HierarchicalLifetimeManager());

            container.RegisterType<ITMHousingSalesEntities>(new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}