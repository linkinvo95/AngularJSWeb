using BusinessEntities;

namespace WebApi.Models.Categorys
{
    public class CategoryData : IdObjectData
    {
        public CategoryData(Category category) : base(category)
        {
            Name = category.Name;
            Active = category.Active;
        }
        public string Name {  get; set; }
        public bool Active { get; set; }
    }
}