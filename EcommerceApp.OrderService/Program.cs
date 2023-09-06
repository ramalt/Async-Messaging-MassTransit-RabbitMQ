using EcommerceApp.Components.Consumers.Order;
using EcommerceApp.Contracts.Order;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// MassTransit Configs

builder.Services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
    // x.AddConsumer<SubmitOrderConsumer>();
    x.AddRequestClient<SubmitOrder>(new Uri($"exchange:{KebabCaseEndpointNameFormatter.Instance.Consumer<SubmitOrderConsumer>()}"));
    

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
