using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Keylobby.API.Configs
{
    public static class GzipConfigService
    {
        public static IServiceCollection RegisterGzip(this IServiceCollection services)
        {

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

            services.AddResponseCompression(options => {

                options.EnableForHttps = true;

                options.Providers.Add<GzipCompressionProvider>();
            });

            return services;
        }
    }
}
