using MediatR;

namespace Shared.DDD
{
    public interface IDomainEvent : INotification
    {
        Guid EventID => Guid.NewGuid();
        public DateTime OcurredOn => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName!;
    }
}
