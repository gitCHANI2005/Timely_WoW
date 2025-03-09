using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public static class ExtensionService
    {
        public static IServiceCollection AddServiceExtension(this IServiceCollection services)
        {

            services.AddRepository();
            services.AddScoped<IService<CategoryDto>, CategoryService>();
            //....
            //services.AddScoped<IService<UserDto>, UserService>();
            //....
            services.AddAutoMapper(typeof(MyMapper));
            return services;
        }
    }
}
