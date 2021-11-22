using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using test6.Tools;

namespace test6
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // /Area/{2}/Controllers/{1}/{0}Controller.cs
            // /Area/{2}/Views/{1}/{0}.cshtml
            services.AddControllersWithViews()
                .AddViewLocalization(format: LanguageViewLocationExpanderFormat.Suffix, setupAction: options =>
                {
                    options.ResourcesPath = "Resources"; 
                })
                .AddViewOptions(options =>
                {
                    
                })
                .AddRazorOptions(options =>
                {
                    // options.AreaViewLocationFormats.Add("/CustomFolder/Areas/{2}.cshtml");
                    options.ViewLocationFormats.Add("/CustomViewLocation/{0}.cshtml");
                    options.ViewLocationExpanders.Add(new ThemesViewLocationExpander("CustomLayout"));
                    
                    // dodawania własnych assembly - 
                    // ustawienia metod kompilacji widokow -
                    // dostawcy plików z widokami IPhysicalFileProvider -
                });
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            var enabledCultures = new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("pl")
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture =
                    new RequestCulture(enabledCultures.First().Name, enabledCultures.First().Name);
                options.SupportedCultures = enabledCultures;
                options.SupportedUICultures = enabledCultures;
                options.RequestCultureProviders = new[]
                {
                    new QueryStringRequestCultureProvider { QueryStringKey = "culture" }
                    // new AcceptLanguageHeaderRequestCultureProvider{ Options = options }
                    // new RouteDataRequestCultureProvider{ RouteDataStringKey = "culture" }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
