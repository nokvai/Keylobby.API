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

            //services.Configure<Api>(configurationRoot.GetSection("Api"));

            services.Configure<Manifest>(configurationRoot.GetSection("Info"));

            services.Configure<Manifest>(configurationRoot.GetSection("Mandrill"));

            services.Configure<SubDomains>(configurationRoot.GetSection("SpaSubDomain"));

            //services.Configure<Plaid>(configurationRoot.GetSection("Plaid"));

            //services.Configure<Stripe>(configurationRoot.GetSection("Stripe"));

            services.Configure<List<UrlRedirects>>(configurationRoot.GetSection("Urls"));

            //services.Configure<S3>(configurationRoot.GetSection("S3"));

            //services.Configure<Emailer>(configurationRoot.GetSection("Emailer"));

            //services.Configure<Transferwise>(configurationRoot.GetSection("Transferwise"));

            return services;
        }
    }
}
