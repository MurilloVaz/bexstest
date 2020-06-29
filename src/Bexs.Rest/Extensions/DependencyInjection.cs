using Bexs.Rest.Interfaces;
using Bexs.Rest.Repositories;
using Bexs.Rest.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bexs.Rest.Extensions
{
    public static class DependencyInjection
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAdministrationRepository, AdministrationRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();

            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<IAdministrationService, AdministrationService>();
        }

    }
}
