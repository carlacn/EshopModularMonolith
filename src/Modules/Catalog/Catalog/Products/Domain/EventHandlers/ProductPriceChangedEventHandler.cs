using MassTransit;
using Shared.Messaging.Events;

namespace Catalog.Products.Domain.EventHandlers
{
    public class ProductPriceChangedEventHandler(IBus bus, ILogger<ProductPriceChangedEventHandler> logger) : INotificationHandler<ProductPriceChangedEvent>
    {
        public async Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

            //publish product price changed integration event for update basket pices
            var integrationEvent = new ProductPriceChangedIntegrationEvent
            {
                ProductId = notification.product.Id,
                Name = notification.product.Name,
                Category = notification.product.Category,
                Description = notification.product.Description,
                ImageFile = notification.product.ImageFile,
                Price = notification.product.Price
            };

            await bus.Publish(integrationEvent, cancellationToken);
        }
    }
}
