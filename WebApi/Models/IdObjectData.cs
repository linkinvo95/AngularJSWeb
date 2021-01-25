using System;
using BusinessEntities;

namespace WebApi.Models
{
    public abstract class IdObjectData
    {
        public IdObjectData(Guid id)
        {
            Id = id;
        }

        public IdObjectData(IdObject entity) : this(entity.Id)
        {
        }

        public Guid Id { get; set; }
    }
}