using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            return services; 
        }

        public static WebApplication UseCatalogModule(this WebApplication app)
        {
            //configure the http request pipeline 
            //api endpoint services
            //application use case services
            //infrastructure services
            return app;
        }
    }
}
