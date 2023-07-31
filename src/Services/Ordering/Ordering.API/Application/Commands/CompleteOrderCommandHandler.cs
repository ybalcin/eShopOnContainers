namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.Commands;

public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public CompleteOrderCommandHandler(IOrderRepository repository)
    {
        _orderRepository = repository;
    }

    public async Task<bool> Handle(CompleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);
        if (orderToUpdate == null)
        {
            return false;
        }

        orderToUpdate.SetCompletedStatus();
        return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}

public class CompleteOrderIdentifiedCommandHandler : IdentifiedCommandHandler<CompleteOrderCommand, bool>
{
    public CompleteOrderIdentifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<CompleteOrderCommand, bool>> logger
        ) : base(mediator, requestManager, logger)
    {
    }

    protected override bool CreateResultForDuplicateRequest()
    {
        return true;
    }
}