using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessEntities
{
    public abstract class IdObject
    {
        private Guid _id = Guid.NewGuid();

        public Guid Id
        {
            get => _id;
            private set => _id = value;
        }

        public bool EqualsById(IdObject other)
        {
            return other != null && Id.Equals(other.Id);
        }

        public static T GetById<T>(IEnumerable<T> items, Guid id) where T : IdObject
        {
            return items.FirstOrDefault(item => item.Id == id);
        }

        public static IList<Guid> Ids<T>(IEnumerable<T> items) where T : IdObject
        {
            return items.Select(q => q.Id).ToList();
        }

        public static bool EqualsById(IdObject a, IdObject b)
        {
            return a?.EqualsById(b) ?? b == null;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == GetType() && Id.Equals(((IdObject) obj).Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}