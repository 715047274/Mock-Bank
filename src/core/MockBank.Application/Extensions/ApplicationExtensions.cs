using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MockBank.Application.IRepository;

namespace MockBank.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            
        }

        
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
 }
}