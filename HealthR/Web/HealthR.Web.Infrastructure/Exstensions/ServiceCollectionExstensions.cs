﻿
namespace HealthR.Web.Infrastructure.Exstensions
{
    using System.Linq;
    using System.Reflection;

    using Microsoft.Extensions.DependencyInjection;


    public static class ServiceCollectionExstensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
                Assembly
               .GetEntryAssembly()
               .GetReferencedAssemblies()
               .Select(Assembly.Load)
               .Where(a=>a.FullName.Contains("HealthR.Service"))
               .SelectMany(x => x.DefinedTypes)
               .Where(t => t.IsClass && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t

                })
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Implementation));

            return services;
        }
    }
}
