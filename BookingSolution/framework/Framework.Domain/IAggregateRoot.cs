using System.Collections.Concurrent;

namespace Framework.Domain;

public interface IAggregateRoot
{
}

public interface IAggregateRoot<out TKey> : IAggregateRoot
{
    public TKey Id { get; }
}