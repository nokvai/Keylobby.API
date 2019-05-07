using System.Collections.Generic;
using System.IO.Compression;
using Keylobby.API.Configs;
using Keylobby.API.DataAccessLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Keylobby.API.Identity.BusinessLogicLayer.Interface;
using Keylobby.API.Identity.BusinessLogicLayer.Service;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Keylobby.API
{
    public class Startup
    {
        private readonly string _environment;
        
        public IConfiguration Configuration { get; }

        private readonly IConfigurationRoot _jsonFiles;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
               .AddJsonFile("Manifest\\apiLastUpdated.json", false, true)
               .AddJsonFile("Manifest\\manifest.json", false, true);
            _jsonFiles = builder.Build();

            _environment = _jsonFiles["Info:Environment"];
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailSender, AuthMessageSender>();

           

            services.RegisterGzip();

            services.RegisterMvc();

            if (_environment == Keylobby.API.DataAccessLayer.Models.Environment.Production)  {

            }
            else {
              
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

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = $"KeylobbyApp/dist/Keylobby-UI"; });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<List<UrlRedirects>> urls, IOptions<SubDomains> subDomains)
        {
            app.RegisterUrlRedirects(urls);
            app.UseAuthentication();

            if (_environment == Environment.Production)
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
            app.RegisterSpaStaticFiles();
            app.UseHttpsRedirection();
            app.UseStatusCodePages();

            if (env.IsDevelopment() == false)
            {
                app.RegisterSslRequired();
                app.UseHttpsRedirection();
            }

            app.UseMvc();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = $"KeylobbyApp";
                spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                {
                    FileProvider =
                        new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), $"KeylobbyApp/dist/Keylobby-UI"))
                };

            });


        }
    }
}
