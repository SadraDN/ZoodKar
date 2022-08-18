using App.Domain.Core.HomeService.Contracts.Repositories;
using App.Domain.Core.HomeService.Dtos;
using App.Infrastructures.Database.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Repository.Ef.HomeServices
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;
        public CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(CategoryDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.HomeService.Entities.Category record = new()
            {
                Title = dto.Title,
            };
            if (record.Id != 0)
            {
                _logger.LogInformation("New {Method} added succesfully", "Category");
            }
            else
            {
                _logger.LogWarning("Add new {Method} failed", "Category");
            }
            await _context.AddAsync(record, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(CategoryDto dto, CancellationToken cancellationToken)
        {
            var record = await _context.Categories.Where(p => p.Id == dto.Id).SingleAsync(cancellationToken);
            record.Title = dto.Title;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Method} updated succesfully", "Category");
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var record = await _context.Categories.Where(p => p.Id == id).SingleAsync(cancellationToken);
            if (record == null)
            {
                _logger.LogWarning("No {Method} found by Id {Id} to delete", "Category", id);
            }
            _context.Remove(record!);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Method} deleted succesfully", "Category");
        }

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            var record = await _context.Categories.Select(p => new CategoryDto()
            {
                Id = p.Id,
               Title = p.Title

            }).ToListAsync(cancellationToken);
            if (record != null)
            {
                _logger.LogInformation("All {Method} get succesfully ", "Categories");
            }
            else
            {
                _logger.LogWarning("Get All {Method} failed", "Categories");
            }
            return record;
        }

        public async Task<CategoryDto>? GetById(int id, CancellationToken cancellationToken)
        {
            var record = await _context.Categories.Where(p => p.Id == id).Select(p => new CategoryDto()
            {
                Id = p.Id,
                Title = p.Title
            }).SingleOrDefaultAsync();
            if (record != null)
            {
                _logger.LogInformation("{Method} By Id {id} get succesfully", "Category", id);
            }
            else
            {
                _logger.LogWarning("{Method} By Id {id} not found", "Category", id);
            }
            return record;
        }

        public async Task<CategoryDto>? GetByTitle(string title, CancellationToken cancellationToken)
        {
            var record = await _context.Categories.Where(p => p.Title == title).Select(p => new CategoryDto()
            {
                Id = p.Id,
                Title = p.Title
            }).SingleOrDefaultAsync();
            if (record != null)
            {
                _logger.LogInformation("{Method} By Title {id} get succesfully", "Category", title);
            }
            else
            {
                _logger.LogWarning("{Method} By Title {id} not found", "Category", title);
            }
            return record;
        }

    }
}
