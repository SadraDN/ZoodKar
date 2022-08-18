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
    public class OrderStatusRepository: IOrderStatusRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderStatusRepository> _logger;
        public OrderStatusRepository(AppDbContext context, ILogger<OrderStatusRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(OrderStatusDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.HomeService.Entities.OrderStatus record = new()
            {
              Title = dto.Title,
            };
            if (record.Id != 0)
            {
                _logger.LogInformation("New {Method} added succesfully", "OrderStatus");
            }
            else
            {
                _logger.LogWarning("Add new {Method} failed", "OrderStatus");
            }
            await _context.AddAsync(record, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(OrderStatusDto dto, CancellationToken cancellationToken)
        {
            var record = await _context.OrderStatuses.Where(p => p.Id == dto.Id).SingleAsync(cancellationToken);
            record.Title = dto.Title;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Method} updated succesfully", "OrderStatus");
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var record = await _context.OrderStatuses.Where(p => p.Id == id).SingleAsync(cancellationToken);
            if (record == null)
            {
                _logger.LogWarning("No {Method} found by Id {Id} to delete", "OrderStatus", id);
            }
            _context.Remove(record!);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Method} deleted succesfully", "OrderStatus");
        }

        public async Task<List<OrderStatusDto>> GetAll(CancellationToken cancellationToken)
        {
            var status = await _context.OrderStatuses.Select(p => new OrderStatusDto()
            {
                Id = p.Id,
                Title = p.Title,

            }).ToListAsync(cancellationToken);
            if (status != null)
            {
                _logger.LogInformation("All {Method} get succesfully ", "OrderStatuses");
            }
            else
            {
                _logger.LogWarning("Get All {Method} failed", "OrderStatuses");
            }
            return status;
        }

          public async Task<OrderStatusDto>? GetByTitle(string title, CancellationToken cancellationToken)
        {
            var record = await _context.OrderStatuses.Where(p => p.Title == title).Select(p => new OrderStatusDto()
            {
                Id = p.Id,
               Title = p.Title
            }).SingleOrDefaultAsync();
            if (record != null)
            {
                _logger.LogInformation("{Method} By Title {title} get succesfully", "OrderStatus", title);
            }
            else
            {
                _logger.LogWarning("{Method} By Title {title} not found", "OrderStatus", title);
            }
            return record;
        }

        public async Task<OrderStatusDto>? GetById(int id, CancellationToken cancellationToken)
        {
            var record = await _context.OrderStatuses.Where(p => p.Id == id).Select(p => new OrderStatusDto()
            {
                Id = p.Id,
                Title = p.Title
            }).SingleOrDefaultAsync();
            if (record != null)
            {
                _logger.LogInformation("{Method} By Id {Id} get succesfully", "OrderStatus", id);
            }
            else
            {
                _logger.LogWarning("{Method} By Id {Id} not found", "OrderStatus", id);
            }
            return record;
        }


    }
}
