using Erfa.ProductionManagement.Persistance.Repositories;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Erfa.ProductionManagement.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ErfaDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnString")));
            services.AddDbContext<ErfaProductionDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnString")));
            services.AddDbContext<ErfaArchiveDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnString")));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IProductionItemRepository, ProductionItemRepository>();
            services.AddScoped<IProductionGroupRepository, ProductionGroupRepository>();
            services.AddScoped<IArchiveProductionGroupRepository, ArchiveProductionGroupRepository>();

            return services;
        }
    }
}
