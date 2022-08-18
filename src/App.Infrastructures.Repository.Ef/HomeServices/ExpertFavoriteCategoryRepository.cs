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
    public class ExpertFavoriteCategoryRepository : IExpertFavoriteCategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ExpertFavoriteCategoryRepository> _logger;
        public ExpertFavoriteCategoryRepository(AppDbContext context, ILogger<ExpertFavoriteCategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(ExpertFavoriteServiceDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.HomeService.Entities.ExpertFavoriteService record = new()
            {
                Id = dto.Id,
                ExpertUserId = dto.ExpertUserId,
                ServiceId = dto.ServiceId,
                CreatedAt = dto.CreatedAt,
            };
            if (record.Id != 0)
            {
                _logger.LogInformation("New {Method} added succesfully", "ExpertFavoriteCategory");
            }
            else
            {
                _logger.LogWarning("Add new {Method} failed", "ExpertFavoriteCategory");
            }
            await _context.AddAsync(record, cancellationToken);
            
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ExpertFavoriteServiceDto>> GetAllByExpertId(int expertId, CancellationToken cancellationToken)
        {
            var favorites = await _context.ExpertFavoriteCategories.Where(x=>x.ExpertUserId == expertId).Select(p => new ExpertFavoriteServiceDto()
            {
                Id = p.Id,
                ServiceId = p.ServiceId,
                ServiceTitle = p.Service.Title,
                ExpertUserId = p.ExpertUserId,
                ExpertName = p.AppUser.Name,
                CreatedAt = p.CreatedAt,

            }).ToListAsync(cancellationToken);
            if (favorites != null)
            {
                _logger.LogInformation("All {Method} get succesfully ", "ExpertFavoriteCategories");
            }
            else
            {
                _logger.LogWarning("Get All {Method} failed", "ExpertFavoriteCategories");
            }
            return favorites;
        }

        public async Task Update(ExpertFavoriteServiceDto dto, CancellationToken cancellationToken)
        {
            var record = await _context.ExpertFavoriteCategories.Where(p => p.Id == dto.Id).SingleAsync(cancellationToken);
            record.ServiceId = dto.ServiceId;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Method} updated succesfully", "ExpertFavoriteCategory");
        }
    }
}
