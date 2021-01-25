using System;
using BusinessEntities;
using Common;

namespace Core.Services.Category
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
   public class UpdateCategoryService : IUpdateCategoryService
    {
        public void Update(BusinessEntities.Category category,string name, bool active)
        {
            category.SetName(name);
            category.SetActive(active);
        }
    }
}
