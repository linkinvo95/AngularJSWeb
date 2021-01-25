using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Core.Services.Category;
using Data.Repositories;
using WebApi.Models.Categorys;


namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
  [RoutePrefix("categories")]
    public class CategoryController : BaseApiController
    {
        private readonly ICreateCategoryService _createCategoryService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUpdateCategoryService _updateCategoryService;

        public CategoryController(ICreateCategoryService createCategoryService, ICategoryRepository categoryRepository, IUpdateCategoryService updateCategoryService)
        {
            _createCategoryService = createCategoryService;
            _categoryRepository = categoryRepository;
            _updateCategoryService = updateCategoryService;
        }

        [Route("create/{categoryId:guid}")]
        [HttpPost]
        public HttpResponseMessage CreateCategory(Guid categoryId, [FromBody] CategoryModel model)
        {
            var category = _createCategoryService.Create(categoryId, model.Name, model.Active);
            return Found(new CategoryData(category));
        }

        [Route("delete/{categoryId:guid}")]
        [HttpDelete]

        public HttpResponseMessage DeleteCategory(Guid categoryId)
        {
            var category = _categoryRepository.Get(categoryId);
            if (category == null)
            {
                return DoesNotExist();
            }
            _categoryRepository.Delete(category);
            return Found();
        }

        [Route("update/{categoryId:guid}")]
        [HttpPost]

        public HttpResponseMessage UpdateCategory(Guid categoryId, [FromBody] CategoryModel model)
        {
            var category = _categoryRepository.Get(categoryId);
            if (category == null)
            {
                return DoesNotExist();
            }
            _updateCategoryService.Update(category, model.Name, model.Active);
            return Found(new CategoryData(category));
        }

        [Route("{categoryId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetCategory(Guid categoryId)
        {
            var category = _categoryRepository.Get(categoryId);
            return category == null
                ? DoesNotExist()
                : Found(new CategoryData(category));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetCategory(int skip, int take, string name = null, bool? active = null)
        {
            var categorys = _categoryRepository.Get(name, active, skip, take);
            var data = categorys.Select(category => new CategoryData(category));
            return Found(data);
        }
    }
}
