
using Keylobby.API.DataAccessLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keylobby.API.Configs
{
    public static class UrlRedirectConfigService
    {
        public static IApplicationBuilder RegisterUrlRedirects(this IApplicationBuilder app, IOptions<List<UrlRedirects>> urls) {

            var rewriteOptions = new RewriteOptions();

            foreach (var url in urls.Value)
            {
                rewriteOptions.AddRedirect(url.From, url.To);
            }

            app.UseRewriter(rewriteOptions);

            return app;
        }
    }
}
