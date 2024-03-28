using Catalog.Application.Handlers;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using HealthChecks.UI.Client;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddHealthChecks().AddMongoDb(builder.Configuration["DatabaseSettings:ConnectionString"], "Catalog Mongo Db Health Check", Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded);
builder.Services.AddSwaggerGen(_ => _.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Catalog.API", Version = "v1" }));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITypeRepository, ProductRepository>();
builder.Services.AddScoped<IBrandRepository, ProductRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(_ => _.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseEndpoints(_ =>
{
    _.MapControllers();
    _.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});
app.MapControllers();

app.Run();
