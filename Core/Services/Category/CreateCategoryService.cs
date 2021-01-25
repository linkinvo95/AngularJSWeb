using System;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Category
{
    [AutoRegister]
    public class CreateCategoryService : ICreateCategoryService
    {
        private readonly IIdObjectFactory<BusinessEntities.Category> _categoryFactory;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUpdateCategoryService _updateCategoryService;

        public CreateCategoryService(IIdObjectFactory<BusinessEntities.Category> categoryFactory, ICategoryRepository categoryRepository, IUpdateCategoryService updateCategoryService)
        {
            _categoryFactory = categoryFactory;
            _categoryRepository = categoryRepository;
            _updateCategoryService = updateCategoryService;
        }

        public BusinessEntities.Category Create(Guid id, string name, bool active)
        {
            var category = _categoryFactory.Create(id);
            _updateCategoryService.Update(category, name, active);
            _categoryRepository.Save(category);
            return category;
        }
    }
}
