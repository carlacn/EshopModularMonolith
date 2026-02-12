using Scalar.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

var dbUser = builder.AddParameter("dbUser", "postgres");
var dbPassword = builder.AddParameter("dbPassword", secret: true);

var databaseProvider = builder.AddPostgres(
        name: "postgres",
        userName: dbUser,
        password: dbPassword,
        port: 5432)
    .WithDataVolume()
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);

var database = databaseProvider.AddDatabase("eshopdb");

var webApi = builder.AddProject<Projects.WebAPI>("webapi")
    .WithReference(database)
    .WaitFor(database);

var scalar = builder.AddScalarApiReference(options =>
{
    options.PreferHttpsEndpoint()
        .AllowSelfSignedCertificates();
})
    .WithApiReference(webApi);

builder.Build().Run();
