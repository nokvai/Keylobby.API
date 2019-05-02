using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Keylobby.API.Configs;
using Keylobby.API.DataAccessLayer.Models;
using Keylobby.API.Injects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Keylobby.API
{
    public class Startup
    {
        private readonly string _environment;

        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        private readonly IConfigurationRoot _jsonFiles;

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(@"Manifest/apiLastUpdated.json", false, true)
                .AddJsonFile(@"Manifest/manifest.json", false, true)
                .AddJsonFile(@"Manifest/emailSettings.json", false, true)
                .AddJsonFile(@"Manifest/subDomains.json", false, true);


            _jsonFiles = builder.Build();

            _environment = _jsonFiles["Info:Environment"];
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.RegisterJwtAuthentication();

            //services.RegistryJwtPolicy();

            services.RegisterGzip();

            services.RegisterMvc();

            //services.RegisterMapper();

            services.RegisterJsonFiles(_jsonFiles);

            //services.RegisterPartBnbDbContext(_jsonFiles);

            //services.RegisterIdentityDbContext(_jsonFiles);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.RegisterDInjection();

            //services.RegisterMemoryCache();

            if (_environment == Keylobby.API.DataAccessLayer.Models.Environment.Production)  {

            }
            else {
                //services.RegisterSwagger();
                services.AddCors(options => {
                    options.AddPolicy("AllowAllHeaders",
                        builder => {
                            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                });
            }

            services.Configure<GzipCompressionProviderOptions> (options => options.Level = CompressionLevel.Fastest);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<List<UrlRedirects>> urls, IOptions<SubDomains> subDomains)
        {

            app.RegisterUrlRedirects(urls);

            app.UseAuthentication();

                if (_environment == Keylobby.API.DataAccessLayer.Models.Environment.Production)
                {

                    app.RegisterNonWwwRedirect();
                }
                else
                {

                    //INFO: Don't enable this on production for security purposes:

                    // app.UseSwaggerAuthentication();

                    // app.RegisterSwagger();

                    app.UseCors("AllowAllHeaders");

                    app.UseDeveloperExceptionPage();
                }

            app.UseHsts();
            app.UseResponseCompression();
            app.UseStaticFiles();
            //app.RegisterSpaStaticFiles();
            app.UseHttpsRedirection();
            app.UseStatusCodePages();
            //app.RegisterStaticFiles();

            if (env.IsDevelopment() == false) {
                app.RegisterSslRequired();
                app.UseHttpsRedirection();
            }

            //app.UseMvcWithDefaultRoute();
            app.UseMvc();
            //app.RegisterMultipleSpa(subDomains.Value);
        }
    }
}
