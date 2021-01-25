namespace Core.Services.Category
{
    public interface IUpdateCategoryService
    {
        void Update(BusinessEntities.Category category, string name, bool active);
    }
}
