using System.Collections.Generic;
using Core;

public abstract class RepositoryBase<T> : IRepository<T>
{
    private readonly List<T> _items;

    public RepositoryBase()
    {
        _items = new List<T>();
    }

    public IReadOnlyList<T> Get()
    {
        return _items;
    }

    public void Register(T item)
    {
        if (!_items.Contains(item))
        {
            _items.Add(item);
        }
    }
}
