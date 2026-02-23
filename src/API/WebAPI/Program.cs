using Carter;
using Shared.CQRS;
using Shared.Messaging;
using Shared.Presentation;

var builder = WebApplication.CreateBuilder(args);

var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;

// Add services to the container.
builder.AddServiceDefaults();

builder.Services
    .AddCarterWithAssemblies(catalogAssembly);

builder.Services
    .AddMediatRWithAssemblies(catalogAssembly)
    .AddMassTransitWithAssemblies(builder.Configuration, catalogAssembly);

builder.Services
    .AddHttpContextAccessor();

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapDefaultEndpoints();
app.MapCarter();

app.UseHttpsRedirection();

app
    .UseCatalogModule();

app.Run();
