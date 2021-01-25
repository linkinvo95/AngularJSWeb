using Common;
using BusinessEntities;

namespace Core.Services.Product
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateProductService : IUpdateProductService
    {
        public void Update(BusinessEntities.Product product, BusinessEntities.Category category, string name, decimal price, int quantity)
        {
            product.Set(name, category, price, quantity);

        }
    }
}