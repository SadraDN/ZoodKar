using App.Domain.Core.HomeService.Contracts.Repositories;
using App.Domain.Core.HomeService.Dtos;
using App.Domain.Core.HomeService.Entities;
using App.Infrastructures.Database.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Repository.Ef.HomeServices
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;
        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(ServiceDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.HomeService.Entities.Service record = new()
            {
                CategoryId = dto.CategoryId,
                Title = dto.Title,
                ShortDescription = dto.ShortDescription,
                Price = dto.Price,
                
            };
            await _context.AddAsync(record, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            foreach (var file in dto.AppFiles)
            {
                ServiceFile serviceFiles = new ServiceFile
                {
                    ServiceId = record.Id,
                    FileId = file.Id,
                    CreatedUserId=file.CreatedUserId,
                    CreatedAt = DateTime.Now,
                };
                record.ServiceFiles.Add(serviceFiles);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task Update(ServiceDto dto, CancellationToken cancellationToken)
        {
            var record = await _context.Services.Where(p => p.Id == dto.Id).Include(x => x.ServiceFiles).SingleAsync(cancellationToken);
            record.CategoryId = dto.CategoryId;
            record.Title = dto.Title;
            record.ShortDescription = dto.ShortDescription;
            record.Price = dto.Price;   

            var serviceFiles = new List<ServiceFile>();
            foreach (var file in dto.AppFiles)
            {
                ServiceFile serviceFile = new ServiceFile
                {

                    ServiceId = record.Id,
                    FileId = file.Id,
                    CreatedUserId = file.CreatedUserId,
                    CreatedAt = DateTime.Now,
                };
                serviceFiles.Add(serviceFile);
            }
            record.ServiceFiles = serviceFiles;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {

            var record = await _context.Services.Where(p => p.Id == id).SingleAsync(cancellationToken);
            _context.Remove(record!);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ServiceDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Services.Select(p => new ServiceDto()
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                Price = p.Price,
                CategoryName = p.Category.Title
            
            }).ToListAsync(cancellationToken);
        }

        public async Task<ServiceDto>? GetByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            var record = await _context.Services.Where(p => p.CategoryId == categoryId).Select(p => new ServiceDto()
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                Price = p.Price,
            }).SingleOrDefaultAsync(cancellationToken);
            return record;
        }

        public async Task<ServiceDto>? GetByServiceId(int serviceId, CancellationToken cancellationToken)
        {
            var record = await _context.Services.Where(p => p.Id == serviceId).Select(p => new ServiceDto()
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                Price = p.Price,
            }).SingleOrDefaultAsync(cancellationToken);
            return record;
        }

        public async Task<ServiceDto>? GetByTitle(string title, CancellationToken cancellationToken)
        {
            var record = await _context.Services.Where(p => p.Title == title).Select(p => new ServiceDto()
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                Price = p.Price,
            }).SingleOrDefaultAsync(cancellationToken);
            return record;
        }

    }
}
