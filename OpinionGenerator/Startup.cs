using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpinionGenerator.Core.Profiles;
using OpinionGenerator.Core.Services;
using OpinionGenerator.Data;
using System;

namespace OpinionGenerator
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
            services.AddDbContext<OpinionGeneratorDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("OpinionGenerator"));
            })
            .AddAutoMapper(typeof(ArticlesProfile), typeof(AzTextAnalyticsResultProfile))
            .AddScoped<IOpinionGeneratorData, OpinionGeneratorSqlData>()
            .Configure<NewsAPIOptions>(Configuration.GetSection("NewsAPI"))
            .AddScoped<IArticleService, NewsAPIArticleService>()
            .Configure<AzureTextAnalyticsOptions>(Configuration.GetSection("AzTextAnalytics"))
            .AddScoped<ITextAnalyticsService, AzureTextAnalyticsService>();
                        
            //services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddRazorPages();

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Map("/razor", razor =>
            {
                razor.UseRouting().UseEndpoints(endpoints =>
                {
                    endpoints.MapRazorPages();
                });
            }).Map("/ng", ng => {
                ng.UseRouting().UseSpa(spa =>
                {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501

                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        //spa.UseAngularCliServer(npmScript: "start");
                        //instead of starting AngularCliServer from startup of ASP.NET CORE web server, we should run it independently:
                        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200/ng/");
                    }
                });
            });


            //if necessary revert to the following to have the angular app at the root route
            //app.UseSpa(spa =>
            //{
            //    // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //    // see https://go.microsoft.com/fwlink/?linkid=864501

            //    spa.Options.SourcePath = "ClientApp";

            //    if (env.IsDevelopment())
            //    {
            //        //spa.UseAngularCliServer(npmScript: "start");
            //        //instead of starting AngularCliServer from startup of ASP.NET CORE web server, we should run it independently:
            //        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
            //    }
            //});
        }
    }
}
