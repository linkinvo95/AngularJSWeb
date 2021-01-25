using System;
using BusinessEntities;

namespace Core.Factories
{
    public interface IIdObjectFactory<out T> : IFactory<T>
    {
        T Create(Guid id);
    }
}