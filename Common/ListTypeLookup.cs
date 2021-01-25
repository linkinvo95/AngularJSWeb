using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public class ListTypeLookup<T> : IListTypeLookup<T> where T : class
    {
        private readonly List<T> _list = new List<T>();

        public void Register(T item)
        {
            _list.Add(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}