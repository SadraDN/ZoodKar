using App.Domain.Core.HomeService.Contracts.Repositories;
using App.Domain.Core.HomeService.Contracts.Services;
using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.HomeServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository CategoryRepository)
        {
            _categoryRepository = CategoryRepository;
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _categoryRepository.Delete(id, cancellationToken);
        }

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAll(cancellationToken);
        }

        public async Task<CategoryDto>? GetById(int id, CancellationToken cancellationToken)
        {
            var record = await _categoryRepository.GetById(id, cancellationToken);
            if(record == null)
            {
                throw new Exception($"No category with Id : {id}!");
            }
            return record;
        }

        public async Task<CategoryDto>? GetByTitle(string title, CancellationToken cancellationToken)
        {
            var record = await _categoryRepository.GetByTitle(title, cancellationToken);
            if (record == null)
            {
                throw new Exception($"No category with title : {title}!");
            }
            return record;
        }

        public async Task Set(CategoryDto dto, CancellationToken cancellationToken)
        {
            await _categoryRepository.Add(dto,cancellationToken);
        }

        public async Task Update(CategoryDto dto, CancellationToken cancellationToken)
        {
           await _categoryRepository.Update(dto,cancellationToken);
        }
    }
}
