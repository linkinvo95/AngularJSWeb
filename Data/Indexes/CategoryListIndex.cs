using System.Linq;
using BusinessEntities;
using Raven.Client.Indexes;

namespace Data.Indexes
{
   public  class CategoryListIndex : AbstractIndexCreationTask<Category>
    {
        public CategoryListIndex()
        {
            Map = categorys => from category in categorys
                select new
                {
                    category.Name,
                    category.Active
                };

        }
    }
}
