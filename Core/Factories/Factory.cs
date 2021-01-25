using System;
using Common;

namespace Core.Factories
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class Factory<T> : IFactory<T> where T : class
    {
        public T Create()
        {
            return Activator.CreateInstance(typeof (T)) as T;
        }
    }
}