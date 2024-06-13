using Ecomm.Application.FileStorage;
using Ecomm.Application.Interfaces;
using Ecomm.Application.Services;
using Ecomm.Infraestructure.Commons.Ordering;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecomm.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IOrderingQuery, OrderingQuery>();
            services.AddTransient<IFileStorageLocal, FileStorageLocal>();
            services.AddTransient<IFileStorageLocalApplication, FileStorageLocalApplication>();
            services.AddScoped<ICategoryApplication, CategoryApplication>();
            services.AddScoped<IOrderApplication, OrderApplication>();
            services.AddScoped<IPaymentMethodApplication, PaymentMethodApplication>();
            services.AddScoped<IProductApplication, ProductApplication>();
            services.AddScoped<IRoleApplication, RoleApplication>();
            services.AddScoped<IUserApplication, UserApplication>();
            
            return services;
        }
    }
}
