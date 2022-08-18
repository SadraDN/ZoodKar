using App.Domain.Core.HomeService.Contracts.AppServices;
using App.Domain.Core.HomeService.Contracts.Services;
using App.Domain.Core.HomeService.Dtos;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Domain.AppServices.HomeServices
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryService _categoryService;
        private readonly IDistributedCache _distributedCache;
        public CategoryAppService(ICategoryService CategoryService, 
            IDistributedCache distributedCache)
        {
            _categoryService = CategoryService;
            _distributedCache = distributedCache;
        }
        public Task Delete(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            List<CategoryDto> categories = new List<CategoryDto>();

            if (_distributedCache.Get("Categories") != null)
            {
                var categoryBytes = await _distributedCache.GetAsync("Categories");
                var categoryString = Encoding.UTF8.GetString(categoryBytes);
                categories = JsonSerializer.Deserialize<List<CategoryDto>>(categoryString);
            }
            else
            {
                categories = await _categoryService.GetAll(cancellationToken);
                var categoryString = JsonSerializer.Serialize(categories);
                var categoryBytes = Encoding.UTF8.GetBytes(categoryString);
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(120),
                };
                _distributedCache.Set("Categories", categoryBytes, options);
            }
            return categories;
        }

        public Task<CategoryDto>? GetById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto>? GetByTitle(string title, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task Set(CategoryDto dto, CancellationToken cancellationToken)
        {
            await _categoryService.Set(dto,cancellationToken);
        }

        public Task Update(CategoryDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
