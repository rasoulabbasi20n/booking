using System;
using System.Collections.Concurrent;

namespace Framework.Domain;

public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>
{
    protected AggregateRoot() { }
    protected AggregateRoot(TKey id) : base(id)
    {
    }
     
}

public abstract class AggregateRootWithGuid : AggregateRoot<Guid>
{
    protected AggregateRootWithGuid()
    {
        Id = Guid.NewGuid();
    }
}