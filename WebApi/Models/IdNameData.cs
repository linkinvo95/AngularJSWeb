using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class IdNameData : IdObjectData
    {
        public IdNameData(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; set;}
    }
}