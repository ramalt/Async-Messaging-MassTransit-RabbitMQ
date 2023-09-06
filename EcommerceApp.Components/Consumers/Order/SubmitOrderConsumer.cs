using EcommerceApp.Contracts.Order;
using EcommerceApp.Contracts.Order;
using MassTransit;

namespace EcommerceApp.Components.Consumers.Order;

public class SubmitOrderConsumer : IConsumer<SubmitOrder>
{
    public async Task Consume(ConsumeContext<SubmitOrder> context)
    {
        var values = new
        {
            InVar.Timestamp,
            Id = context.Message.Id,
            CustomerNumber = context.Message.CustomerNumber,
            Message = "Order submitted"

        };
        await context.RespondAsync<OrderSubmitted>(values);
    }
}