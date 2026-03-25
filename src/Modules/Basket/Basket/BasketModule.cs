using Basket.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Infrastructure;
using Shared.Infrastructure.Interceptors;

namespace Basket
{
    public static class BasketModule
    {
        public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration configuration)
        {
            //add services to the container (like program) 
            //api endpoint services
            //application use case services
            //infrastructure services

            var connectionString = configuration.GetConnectionString("eshopdb");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<BasketDbContext>((serviceProvider, options) =>
            {
                options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "basket");
                });
            });

            return services;
        }

        public static WebApplication UseBasketModule(this WebApplication app)
        {
            //configure the http request pipeline 
            //api endpoint services
            //application use case services
            //infrastructure services

            if (app.Environment.IsDevelopment())
                app.UseMigration<BasketDbContext>();

            return app;
        }
    }
}
