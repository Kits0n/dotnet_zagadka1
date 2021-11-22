using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace test6.Tools
{
    public class LocalizationPipeline
    {
        public static void Configure(IApplicationBuilder app, IOptions<RequestLocalizationOptions> options)
        {
            app.UseRequestLocalization(options.Value);
        }
    }
}