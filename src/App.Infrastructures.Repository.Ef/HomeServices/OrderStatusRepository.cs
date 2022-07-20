using App.Domain.Core.HomeService.Contracts.Repositories;
using App.Domain.Core.HomeService.Dtos;
using App.Infrastructures.Database.SqlServer;
using Microsoft.EntityFrameworkCore;
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
        public OrderStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(OrderStatusDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.HomeService.Entities.OrderStatus record = new()
            {
              Title = dto.Title,
            };
            await _context.AddAsync(record, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(OrderStatusDto dto, CancellationToken cancellationToken)
        {
            var record = await _context.OrderStatuses.Where(p => p.Id == dto.Id).SingleAsync(cancellationToken);
            record.Title = dto.Title;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var record = await _context.OrderStatuses.Where(p => p.Id == id).SingleAsync(cancellationToken);
            _context.Remove(record!);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<OrderStatusDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.OrderStatuses.Select(p => new OrderStatusDto()
            {
                Id = p.Id,
                Title = p.Title,

            }).ToListAsync(cancellationToken);
        }

          public async Task<OrderStatusDto>? GetByTitle(string title, CancellationToken cancellationToken)
        {
            var record = await _context.OrderStatuses.Where(p => p.Title == title).Select(p => new OrderStatusDto()
            {
                Id = p.Id,
               Title = p.Title
            }).SingleOrDefaultAsync();
            return record;
        }

        public async Task<OrderStatusDto>? GetById(int id, CancellationToken cancellationToken)
        {
            var record = await _context.OrderStatuses.Where(p => p.Id == id).Select(p => new OrderStatusDto()
            {
                Id = p.Id,
                Title = p.Title
            }).SingleOrDefaultAsync();
            return record;
        }


    }
}
