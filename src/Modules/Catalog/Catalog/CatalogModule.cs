using Catalog.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Infrastructure;
using Shared.Infrastructure.Interceptors;
using Shared.Infrastructure.Seed;

namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
        {
            //add services to the container (like program) 
            //api endpoint services
            //application use case services
            //infrastructure services

            var connectionString = configuration.GetConnectionString("eshopdb");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<CatalogDbContext>((serviceProvider, options) =>
            {
                options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "catalog");
                });
            });

            services.AddScoped<IDataSeeder, CatalogDataSeeder>();

            return services; 
        }

        public static WebApplication UseCatalogModule(this WebApplication app)
        {
            //configure the http request pipeline 
            //api endpoint services
            //application use case services
            //infrastructure services

            if (app.Environment.IsDevelopment())
                app.UseMigration<CatalogDbContext>();

            return app;
        }

    }
}
