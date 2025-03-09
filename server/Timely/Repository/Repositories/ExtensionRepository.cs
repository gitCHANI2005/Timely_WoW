using Microsoft.Extensions.DependencyInjection;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public static class ExtensionRepository
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Category>, CategoryRepository > ();
            services.AddScoped<IRepository<City>, CityRepository>();
            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<IRepository<Deliver>, DeliverRepository>();
            services.AddScoped<IRepository<Extra>, ExtraRepository>();
            services.AddScoped<IRepository<MenuDose>, MenuDoseRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<Owner>, OwnerRepository>();
            services.AddScoped<IRepository<Store>, StoreRepository>();
            return services;
        }
    }
}
