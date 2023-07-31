namespace Microsoft.eShopOnContainers.Services.Ordering.Domain.Events;

public class OrderCompletedDomainEvent : INotification
{
    public Order Order { get; set; }

    public OrderCompletedDomainEvent(Order order)
    {
        Order = order;
    }
}