using System;
using BusinessEntities;
using Common;

namespace Core.Factories
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class IdObjectFactory<T> :Factory<T>, IIdObjectFactory<T> where T : IdObject
    {
       public T Create(Guid id)
        {
            var instance = Create();
            ReflectionHelper.SetProperty(instance, typeof (T).GetProperty("Id"), id);
            return instance;
        }
    }
}