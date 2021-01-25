using System;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class AutoRegisterAttribute : Attribute
    {
        private readonly AutoRegisterTypes _type = AutoRegisterTypes.Scope;

        public AutoRegisterAttribute()
        {
        }

        public AutoRegisterAttribute(AutoRegisterTypes type)
        {
            _type = type;
        }

        public AutoRegisterTypes Type => _type;
    }
}