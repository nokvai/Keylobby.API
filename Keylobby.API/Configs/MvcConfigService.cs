using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Keylobby.API.Configs
{
    public static class MvcConfigService
    {
        public static IServiceCollection RegisterMvc(this IServiceCollection services)
        {
            //If it insist to use: 'AddMvcCore': Install-Package Microsoft.AspNetCore.Mvc 
            services.AddMvc()
                .AddMvcOptions(options => options.MvcOptions())
                .AddJsonOptions(options => options.MvcJsonOptions())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2); ;

            return services;
        }

        private static MvcJsonOptions MvcJsonOptions(this MvcJsonOptions option)
        {
            option.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            option.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            //This will stop the Circular Reference.
            option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return option;
        }

        private static MvcOptions MvcOptions(this MvcOptions option)
        {
            option.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            return option;
        }
    }
}
