using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keylobby.API.Configs
{
    public static class SslRequiredConfigService
    {

        public static IApplicationBuilder RegisterSslRequired(this IApplicationBuilder applicationBuilder) {

            var options = new RewriteOptions().AddRedirectToHttpsPermanent();
            applicationBuilder.UseRewriter(options);

            return applicationBuilder;
        }
    }
}
