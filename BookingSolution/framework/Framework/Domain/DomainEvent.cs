using System;

namespace Framework.Domain;
public record DomainEvent : IDomainEvent
{
    public Guid EventId => Guid.NewGuid();
    public DateTime PublishedAt => DateTime.Now;
    public string UserId { get; set; }
}