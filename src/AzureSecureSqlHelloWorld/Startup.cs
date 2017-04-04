using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AzureSecureSqlHelloWorld.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureSecureSqlHelloWorld
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            //if (env.IsEnvironment("Development"))
            //{
            //    builder.AddApplicationInsightsSettings(developerMode: true);
            //}

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.AddApplicationInsightsTelemetry(Configuration);
            
            services.AddMvc();

            var builder = new ContainerBuilder();

            builder.RegisterInstance(this.Configuration).As<IConfigurationRoot>();
            builder.Populate(services);
           
            builder.Register(c =>
            {
                var config = c.Resolve<IConfigurationRoot>();
                var optionsBuilder = new DbContextOptionsBuilder<MyAppContext>();
                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"), options => options.EnableRetryOnFailure());
                return new MyAppContext(optionsBuilder.Options);
            }).As<MyAppContext>();

            this.ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            //app.UseApplicationInsightsRequestTelemetry();
            //app.UseApplicationInsightsExceptionTelemetry();
            app.UseMvc();
        }

        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; set; }
    }
}
