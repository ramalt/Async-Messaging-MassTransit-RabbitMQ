using EcommerceApp.Components.Consumers.Order;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

//formatting-style-configs
builder.Services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
//MassTransit Config
builder.Services.AddMassTransit(x =>
{
    //Queue and Exchange binding
    //Adds all consumers from the assembly containing the specified type that are in the same (or deeper) namespace.
    x.AddConsumersFromNamespaceContaining<SubmitOrderConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

    });

});


var app = builder.Build();

app.Run();
