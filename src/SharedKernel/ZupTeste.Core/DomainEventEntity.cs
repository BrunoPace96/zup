using MediatR;

namespace ZupTeste.Core
{
    public abstract partial class EntityBase
    {
        private readonly List<INotification> _domainEvents = new();

        public IReadOnlyCollection<INotification> DomainEvents =>
            _domainEvents?.AsReadOnly();
        
        public bool HasDomainEvents() =>
            _domainEvents.Count > 0;

        public void AddDomainEvent(INotification eventItem) =>
            _domainEvents.Add(eventItem);

        public void RemoveDomainEvent(INotification eventItem) =>
            _domainEvents?.Remove(eventItem);

        public void ClearDomainEvents() =>
            _domainEvents?.Clear();
    }
}