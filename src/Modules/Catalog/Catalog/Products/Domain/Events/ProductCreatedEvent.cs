namespace Catalog.Products.Domain.Events
{
    public record ProductCreatedEvent(Product Product) : IDomainEvent;
}
