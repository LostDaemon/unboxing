using System.Collections.Generic;

namespace Core
{
    public interface IRepository<T>
    {
        public void Register(T item);
        public IReadOnlyList<T> Get();
    }
}
