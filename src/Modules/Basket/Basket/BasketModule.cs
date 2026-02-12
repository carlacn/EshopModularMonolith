using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            return services;
        }

        public static WebApplication UseBasketModule(this WebApplication app)
        {
            //configure the http request pipeline 
            //api endpoint services
            //application use case services
            //infrastructure services
            return app;
        }
    }
}
