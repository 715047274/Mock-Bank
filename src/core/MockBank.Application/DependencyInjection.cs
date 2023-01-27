using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MockBank.Application.Configurations.Common.Behaviours;
using FluentValidation;

namespace MockBank.Application
{
    public static class DependencyInjection
    {
        public  static IServiceCollection  AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

          // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
          services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<>)); 
          services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MockScenarioBehaviour<,>));
        
            return services;
        }
    }
}