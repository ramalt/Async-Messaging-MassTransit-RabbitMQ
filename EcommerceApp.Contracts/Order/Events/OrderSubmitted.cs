namespace EcommerceApp.Contracts.Order.Events;

public record OrderSubmitted
{
    public Guid Id { get; init; }
    public DateTime Timestamp { get; init; }
    public string CustomerNumber { get; init; }
    public string Message { get; init; }
}