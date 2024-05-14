using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ecomm.Infraestructure.Persistences.Contexts;
using Ecomm.Infraestructure.Persistences.Interfaces;
using Ecomm.Infraestructure.Persistences.Repositories;

namespace Ecomm.Infraestructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(EcommContext).Assembly.FullName;
            services.AddDbContext<EcommContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("EcommConnection"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
