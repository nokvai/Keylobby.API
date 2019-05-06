using Keylobby.API.DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keylobby.API.Configs
{
    public static class JsonFilesConfigService
    {

        public static IServiceCollection RegisterJsonFiles(this IServiceCollection services,
            IConfigurationRoot configurationRoot)
        {
            services.Configure<SubDomains>(configurationRoot.GetSection("SpaSubDomain"));
            services.Configure<List<UrlRedirects>>(configurationRoot.GetSection("Urls"));

            return services;
        }
    }
}
