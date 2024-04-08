using EventBus.Messages.Common;
using EventBus.Messages.Events;
using HealthChecks.UI.Client;
using MassTransit;
using Ordering.API.EventBusConsumer;
using Ordering.Application.Extensions;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddInfraServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddApiVersioning();
builder.Services.AddSwaggerGen(x => x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
{
    Title = "Order.API",
    Version = "v1"
}));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<BasketOrderingConsumer>();
builder.Services.AddScoped<BasketOrderingConsumerV2>();
builder.Services.AddHealthChecks().Services.AddDbContext<OrderContext>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BasketOrderingConsumer>();
    x.AddConsumer<BasketOrderingConsumerV2>();
    x.UsingRabbitMq((ct, cf) =>
    {
        cf.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cf.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, x =>
        {
            x.ConfigureConsumer<BasketOrderingConsumer>(ct);
        });

        cf.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueueV2, x =>
        {
            x.ConfigureConsumer<BasketOrderingConsumerV2>(ct);
        });
    });
});
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering.API v1"));
}
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(x =>
{
    x.MapControllers();
    x.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        Predicate = x => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

app.Run();
