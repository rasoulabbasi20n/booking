namespace Framework.Domain;

public interface IRepository<T, in TKey> where T : IAggregateRoot<TKey>
{
}