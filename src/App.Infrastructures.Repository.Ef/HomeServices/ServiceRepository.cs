using App.Domain.Core.HomeService.Contracts.Repositories;
using App.Domain.Core.HomeService.Dtos;
using App.Domain.Core.HomeService.Entities;
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
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ServiceRepository> _logger;
        public ServiceRepository(AppDbContext context, ILogger<ServiceRepository> logger)
        {
            _context = context;
            _logger = logger;
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
            if (record.Id != 0)
            {
                _logger.LogInformation("New {Method} added succesfully", "Service");
            }
            else
            {
                _logger.LogWarning("Add new {Method} failed", "Service");
            }
            foreach (var file in dto.AppFiles)
            {
                ServiceFile serviceFiles = new ServiceFile
                {
                    ServiceId = record.Id,
                    FileId = file.Id,
                    CreatedUserId = file.CreatedUserId,
                    CreatedAt = DateTime.Now,
                };
                record.ServiceFiles.Add(serviceFiles);
                if (serviceFiles.Id != 0)
                {
                    _logger.LogInformation("{Method} File added succesfully", "Service");
                }
                else
                {
                    _logger.LogWarning("Add new file for {Method} failed", "Service");
                }

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
            _logger.LogInformation("{Method} updated succesfully", "Service");
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var record = await _context.Services.Where(p => p.Id == id).SingleAsync(cancellationToken);
            if (record == null)
            {
                _logger.LogWarning("No {Method} found by Id {Id} to delete","Service",id);
            }
            _context.Remove(record!);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Method} deleted succesfully", "Service");
        }

        public async Task<List<ServiceDto>> GetAll(CancellationToken cancellationToken)
        {
            var service = await _context.Services.Select(p => new ServiceDto()
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                Price = p.Price,
                CategoryName = p.Category.Title,
                AppFiles = p.ServiceFiles.Select(x => new AppFileDto
                {
                    CreatedAt = x.CreatedAt,
                    CreatedUserId = x.CreatedUserId,
                    Id = x.FileId,
                    EntityId = x.File.EntityId,
                    FileAddress = x.File.FileAddress,
                }).ToList(),
            }).ToListAsync(cancellationToken);
            if(service!= null)
            {
                _logger.LogInformation("All {Method} get succesfully ", "Services");
            }
            else
            {
                _logger.LogWarning("Get All {Method} failed","Services");
            }
            return service;
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
            if (record != null)
            {
                _logger.LogInformation("{Method} By CategoryId {id} get succesfully", "Service", categoryId);
            }
            else
            {
                _logger.LogWarning("{Method} By CategoryId {id} not found", "Service", categoryId);
            }
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
            if (record != null)
            {
                _logger.LogInformation("{Method} By ServiceId {id} get succesfully", "Service", serviceId);
            }
            else
            {
                _logger.LogWarning("{Method} By ServiceId {id} not found", "Service", serviceId);
            }
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
            if (record != null)
            {
                _logger.LogInformation("{Method} By Title {Title} get succesfully", "Service", title);
            }
            else
            {
                _logger.LogWarning("{Method} By Title {Title} not found", "Service", title);
            }
            return record;
        }

    }
}
