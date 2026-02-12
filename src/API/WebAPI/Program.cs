using Shared.Messaging;

var builder = WebApplication.CreateBuilder(args);

var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;

// Add services to the container.
builder.AddServiceDefaults();

builder.Services
    .AddMassTransitWithAssemblies(builder.Configuration, catalogAssembly);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app
    .UseCatalogModule();

app.Run();
