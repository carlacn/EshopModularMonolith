using Carter;
using Shared.CQRS;
using Shared.Exceptions.Handler;
using Shared.Messaging;
using Shared.Presentation;

var builder = WebApplication.CreateBuilder(args);

var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;

// Add services to the container.
builder.AddServiceDefaults();

builder.Services
    .AddCarterWithAssemblies(catalogAssembly, basketAssembly);

builder.Services
    .AddMediatRWithAssemblies(catalogAssembly, basketAssembly)
    .AddMassTransitWithAssemblies(builder.Configuration, catalogAssembly, basketAssembly);

builder.Services
    .AddHttpContextAccessor();

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration);

builder.Services
    .AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapDefaultEndpoints();
app.MapCarter();
app.UseExceptionHandler(options => { });

app.UseHttpsRedirection();

app
    .UseCatalogModule()
    .UseBasketModule();

app.Run();
