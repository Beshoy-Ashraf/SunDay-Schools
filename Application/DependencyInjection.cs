using System.Reflection;
using Application.Common.Authorization;
using Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
      public static IServiceCollection AddApplicationServices(this IServiceCollection services)
      {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // Registration order is pipeline order: logging wraps validation so failures are timed too.
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));
            services.AddScoped<IChurchAuthorizationService, ChurchAuthorizationService>();

            return services;
      }
}