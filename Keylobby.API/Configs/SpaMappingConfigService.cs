using Keylobby.API.DataAccessLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Keylobby.API.Configs
{
    public static class SpaMappingConfigService
    {
        public static IApplicationBuilder RegisterMultipleSpa(this IApplicationBuilder app, SubDomains manifest) {

            var client = manifest.Client;
            var admin = manifest.Admin;

            app.MapWhen(
                    context => client == context.Request.Host.Host,
                    clientConfig => ConfigureSpa(clientConfig, "ClientApp"))
                .MapWhen(
                    context => admin == context.Request.Host.Host,
                    adminConfig => ConfigureSpa(adminConfig, "AdminApp"));

            return app;
        }

        private static void ConfigureSpa(IApplicationBuilder admin, string target)
        {
            admin.UseSpa(spa => {
                spa.Options.SourcePath = target;
                spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                {
                    FileProvider =
                        new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), $"{target}/dist"))
                };
            });
        }

    }
}
