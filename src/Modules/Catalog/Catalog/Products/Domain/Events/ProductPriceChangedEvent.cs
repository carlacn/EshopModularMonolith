namespace Catalog.Products.Domain.Events
{
    public record ProductPriceChangedEvent(Product product) : IDomainEvent;
}
