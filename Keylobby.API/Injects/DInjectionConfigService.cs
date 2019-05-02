using Keylobby.API.Identity.BusinessLogicLayer;
using Keylobby.API.Identity.BusinessLogicLayer.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keylobby.API.Injects
{
    public static class DInjectionConfigService
    {
        public static IServiceCollection RegisterDInjection(this IServiceCollection services)
        {
            services.AddTransient<IIdentityRepository, IdentityRepository>();
           
          //  services.AddTransient<IEmailerRepository, EmailerRepository>();

            
            return services;
        }
    }
}
