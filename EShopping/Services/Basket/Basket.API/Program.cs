using Basket.Application;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Discount.Grpc.Protos;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(AddMediatR).Assembly));
builder.Services.AddApiVersioning();
builder.Services.AddStackExchangeRedisCache(x =>
{
    x.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});
builder.Services.AddScoped<Basket.Application.gRPCServices.DiscountProtoService>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Basket.API",
        Version = "v1"
    });
});
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(x=>x.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]) );
builder.Services.AddHealthChecks()
    .AddRedis(
    builder.Configuration["CacheSettings:ConnectionString"] + "", "Redis Health",
    Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded
    );
var app = builder.Build();

// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
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
//app.MapControllers();

app.Run();
