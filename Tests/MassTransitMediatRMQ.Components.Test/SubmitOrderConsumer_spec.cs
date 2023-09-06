using System;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApp.Components.Consumers.Order;
using EcommerceApp.Contracts.Order;
using MassTransit;
using MassTransit.Testing;
using NUnit.Framework;

namespace MassTransitMediatRMQ.Components.Test;

[TestFixture]
public class When_an_order_request_is_consumed
{


    [Test]
    public async Task Should_respond_with_acceptance_if_ok()
    {
        var harmess = new InMemoryTestHarness(); // MassTransit.Testing
        var consumer = harmess.Consumer<SubmitOrderConsumer>();

        await harmess.Start();

        try
        {
            var OrderId = Guid.NewGuid();
            var requestClient = await harmess.ConnectRequestClient<SubmitOrder>();

            var response = await requestClient.GetResponse<OrderSubmitted>(new OrderSubmitted
            {
                Id = OrderId,
                Timestamp = InVar.Timestamp,
                CustomerNumber = "CUSTOMER1234"
            });

            Assert.That(response.Message.Id, Is.EqualTo(OrderId));

            // Consume işlemide SubmitOrder türünde  nesne varsa test başarılıdır.
            Assert.That(consumer.Consumed.Select<SubmitOrder>().Any(), Is.True);

            Assert.That(harmess.Sent.Select<SubmitOrder>().Any(), Is.True);

        }
        finally
        {
            await harmess.Stop();
        }

    }

    [Test]
    public async Task Should_publish_order_submitted_event()
    {
        var harmess = new InMemoryTestHarness(); // MassTransit.Testing
        var consumer = harmess.Consumer<SubmitOrderConsumer>();

        await harmess.Start();

        try
        {
            var OrderId = Guid.NewGuid();

            await harmess.InputQueueSendEndpoint.Send<OrderSubmitted>(new OrderSubmitted
            {
                Id = OrderId,
                Timestamp = InVar.Timestamp,
                CustomerNumber = "CUSTOMER1234"
            });

            // Consume işlemide SubmitOrder türünde  nesne varsa test başarılıdır.
            Assert.That(harmess.Published.Select<OrderSubmitted>().Any(), Is.True);

        }
        finally
        {
            await harmess.Stop();
        }

    }



}