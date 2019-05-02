using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keylobby.API.Configs
{
    public static class NonWwwConfigServices
    {
        public static IApplicationBuilder RegisterNonWwwRedirect(this IApplicationBuilder applicationBuilder)
        {

            var options = new RewriteOptions()
                .AddRedirectToWwwPermanent();

            applicationBuilder.UseRewriter(options);

            return applicationBuilder;
        }
    }
}
