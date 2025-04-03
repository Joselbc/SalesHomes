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

            container.RegisterType<IClothingRepository, ClothingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICustomerRepository, CustomerRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IImageRepository, ImageRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<ClothingService>(new HierarchicalLifetimeManager());
            container.RegisterType<CustomerService>(new HierarchicalLifetimeManager());
            container.RegisterType<ImageService>(new HierarchicalLifetimeManager());


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}