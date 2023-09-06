using EcommerceApp.Contracts.Order;
using EcommerceApp.Contracts.Order;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    readonly ILogger<OrderController> _logger;
    private readonly IRequestClient<SubmitOrder> _SubmitOrderRC;

    public OrderController(ILogger<OrderController> logger, IRequestClient<SubmitOrder> SubmitOrderRequestClient)
    {
        _logger = logger;
        _SubmitOrderRC = SubmitOrderRequestClient;
    }


    [HttpPost("submit")]
    public async Task<IActionResult> SubmitOrder(Guid id, string customerNumer)
    {
        var message = new SubmitOrder
        {
            Id = id,
            CustomerNumber = customerNumer,
            Timestamp = InVar.Timestamp
        };
        // Create a request, and return a task for the specified response type
        var response = await _SubmitOrderRC.GetResponse<OrderSubmitted>(message);
        _logger.LogInformation($"-> 🍀 {response.Message.Message}");
        return Ok(response.Message);
    }
}