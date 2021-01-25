using System;
using BusinessEntities;

namespace Core.Services.Product
{
   public interface IUpdateProductService
   {
       void Update(BusinessEntities.Product product, BusinessEntities.Category category, string name, decimal price, int quantity);
   }
}
