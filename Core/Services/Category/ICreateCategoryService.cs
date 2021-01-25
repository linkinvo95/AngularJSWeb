using System;
using BusinessEntities;

namespace Core.Services.Category
{
   public interface ICreateCategoryService
    {
        BusinessEntities.Category Create(Guid id, string name, bool active);
    }
}
