using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Net.Http;
using WebApp.Services;


namespace WebApp
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IConferenceService, ConferenceMemoryService>();
            services.AddSingleton<IProposalService, ProposalMemoryService>();
            services.AddSingleton(x =>
                new HttpClient { BaseAddress = new Uri("http://localhost:5000") });

            services.Configure<GlobomanticsOptions>(_configuration.GetSection("Globomantics"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> log)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("First");
            //    await next();
            //});
            //app.Use(next =>
            //{
            //    return async context =>
            //    {
            //        await context.Response.WriteAsync("-");
            //        await next(context);
            //    };
            //});
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("secod");
            //});

            //    log.LogError("ddddd");
            //app.Use(next =>
            //{
            //    return async context =>
            //    {
            //        log.LogInformation("Incomming Request");
            //        if (context.Request.Path.StartsWithSegments("/mym"))
            //        {
            //            await context.Response.WriteAsync("Hit Custom Middleware");
            //            log.LogInformation("Handled Request");

            //        }
            //        else
            //        {
            //            await next(context);
            //            log.LogInformation("outgoing Response");

            //        }
            //    };
            //});
            app.UseMvc(routes =>
            {
                log.LogInformation("ddd");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Conference}/{action=Index}/{id?}");
            });
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
        }

    }

}

