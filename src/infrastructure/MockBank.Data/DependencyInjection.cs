using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MockBank.Application.IRepository;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            if (connectionString == null)
            {
                Console.WriteLine($"[Error]::{DateTime.Now.ToString()}::---> AppSetting Connection string not found");
                throw new SystemException( "Fluent Migration error");
            }

            services.AddFluentMigratorCore()
                .ConfigureRunner(cfg =>
                {
                    Console.WriteLine("Begin to Migrate----");
                    cfg
                        .AddSQLite()
                        .WithGlobalConnectionString(
                            connectionString) //.ScanIn(Assembly.GetExecutingAssembly()).For.All())
                        .ScanIn(typeof(DependencyInjection).Assembly).For.Migrations();
                })
                .AddLogging(cfg => cfg.AddFluentMigratorConsole());
           services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}