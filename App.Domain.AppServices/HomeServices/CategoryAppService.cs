using App.Domain.Core.HomeService.Contracts.AppServices;
using App.Domain.Core.HomeService.Contracts.Services;
using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.HomeServices
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryService _categoryService;
        public CategoryAppService(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }
        public Task Delete(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _categoryService.GetAll(cancellationToken);
        }

        public Task<CategoryDto>? GetById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto>? GetByTitle(string title, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Set(CategoryDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Update(CategoryDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
