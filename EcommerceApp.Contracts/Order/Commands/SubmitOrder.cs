namespace EcommerceApp.Contracts.Order.Commands;

public record SubmitOrder
{
    public Guid Id { get; init; }
    public DateTime Timestamp { get; init; }
    public string CustomerNumber { get; init; }
}