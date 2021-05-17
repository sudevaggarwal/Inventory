using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Inventary.Core.Domain.VM;
using Inventary.Data.Repositaries;
using Inventary.Services.Servies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventaryManagement.Extentions
{
    public static class DependancyContainerExtention
    {
        public static void RegistorServices(this IServiceCollection services)
        {
            services.AddTransient<IDBConnectionRepository, DBConnectionRepository>();
            services.AddTransient<IInventaryRepository, InventaryRepository>();
            services.AddTransient<IInventaryService, InventaryService>();

        }
    }

    internal class RegisterModelValidators : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IValidator<InventaryDetail>, InventaryDetailValidator>();
        }

    }
    public static class ServiceRegistrationExtension
    {
        public static void AddServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var appServices = typeof(Startup).Assembly.DefinedTypes
                            .Where(x => typeof(IServiceRegistration)
                            .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                            .Select(Activator.CreateInstance)
                            .Cast<IServiceRegistration>().ToList();

            appServices.ForEach(svc => svc.RegisterAppServices(services, configuration));
        }

    }
    public interface IServiceRegistration
    {
        void RegisterAppServices(IServiceCollection services, IConfiguration configuration);
    }
}

