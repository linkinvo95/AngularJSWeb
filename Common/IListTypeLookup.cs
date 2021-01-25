using System.Collections.Generic;

namespace Common
{
    public interface IListTypeLookup<T> : IEnumerable<T>
    {
        void Register(T item);
    }
}