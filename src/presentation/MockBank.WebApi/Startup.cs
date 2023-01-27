using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MockBank.Application;
using MockBank.Data;
using MockBank.WebApi.Extensions;
using MockBank.WebApi.Filters;
using MockBank.WebApi.Helpers;
using MockBank.WebApi.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using WireMock.Settings;

namespace MockBank.WebApi
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
            services.AddApplication(); // application layer
            services.AddInfrastructure(Configuration); // infrastructure layer
            services.AddWepApi();// api layer
            // system config
            services.AddControllers(); 
            services.AddControllersWithViews(options =>
                options.Filters.Add(new ApiExceptionFilter()));
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true
            );
            
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IMigrationRunner migrationRunner,  IApiVersionDescriptionProvider provider)
        {
            migrationRunner.MigrateUp();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerExtension(provider);
            }

            app.UseCors(b =>
            {
                b.AllowAnyMethod();
                b.AllowAnyHeader();
                b.AllowAnyOrigin();
            });
           
            // app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            // html
            app.UseDefaultFiles();
            app.UseStaticFiles();
           
            // default api configuration
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
