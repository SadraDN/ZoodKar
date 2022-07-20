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
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(OrderDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.HomeService.Entities.Order record = new()
            {
                StatusId = dto.StatusId,
                ServiceId = dto.ServiceId,
                ServiceBasePrice = dto.ServiceBasePrice,
                CustomerUserId = dto.CustomerUserId,
                FinalExpertUserId = dto.FinalExpertUserId,
                CreatedAt = dto.CreatedAt,
            };
            await _context.AddAsync(record, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            foreach (var file in dto.AppFiles)
            {
                OrderFile orderFiles = new OrderFile
                {
                    OrderId = record.Id,
                    FileId = file.Id,
                    CreatedUserId = file.CreatedUserId,
                    CreatedAt = DateTime.Now,
                };
                record.OrderFiles.Add(orderFiles);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(OrderDto dto, CancellationToken cancellationToken)
        {
            var record = await _context.Orders.Where(p => p.Id == dto.Id).Include(x => x.OrderFiles).SingleAsync(cancellationToken);
            record.ServiceId = dto.ServiceId;
            record.StatusId = dto.StatusId;
            record.ServiceBasePrice = dto.ServiceBasePrice;
            record.CustomerUserId = dto.CustomerUserId;
            record.FinalExpertUserId = dto.FinalExpertUserId;

            var orderFiles = new List<OrderFile>();
            foreach (var file in dto.AppFiles)
            {
                OrderFile orderFile = new OrderFile
                {
                    OrderId = record.Id,
                    FileId = file.Id,
                    CreatedUserId = file.CreatedUserId,
                    CreatedAt = DateTime.Now,
                };
                orderFiles.Add(orderFile);
            }

            record.OrderFiles = orderFiles;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var record = await _context.Orders.Where(p => p.Id == id).SingleAsync(cancellationToken);
            _context.Remove(record!);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<OrderDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Orders.Select(p => new OrderDto()
            {
                Id = p.Id,
                ServiceId = p.ServiceId,
                StatusId = p.StatusId,
                CustomerUserId = p.CustomerUserId,
                FinalExpertUserId = p.FinalExpertUserId,
                ServiceBasePrice = p.ServiceBasePrice,
                CreatedAt = p.CreatedAt,
            }).ToListAsync(cancellationToken);
        }

        public async Task<List<OrderDto>> GetAllByCustomerId(int customerId, CancellationToken cancellationToken)
        {
            return await _context.Orders.Where(x => x.CustomerUserId == customerId).Select(p => new OrderDto()
            {
                Id = p.Id,
                ServiceId = p.ServiceId,
                StatusId = p.StatusId,
                CustomerUserId = p.CustomerUserId,
                FinalExpertUserId = p.FinalExpertUserId,
                ServiceBasePrice = p.ServiceBasePrice,
                CreatedAt = p.CreatedAt,
            }).ToListAsync(cancellationToken);
        }

        public async Task<List<OrderDto>> GetAllByExpertId(int expertId, CancellationToken cancellationToken)
        {
            return await _context.Orders.Where(x=>x.FinalExpertUserId == expertId).Select(p => new OrderDto()
            {
                Id = p.Id,
                ServiceId = p.ServiceId,
                StatusId = p.StatusId,
                CustomerUserId = p.CustomerUserId,
                FinalExpertUserId = p.FinalExpertUserId,
                ServiceBasePrice = p.ServiceBasePrice,
                CreatedAt = p.CreatedAt,
            }).ToListAsync(cancellationToken);
        }

        public async Task<OrderDto>? GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var record = await _context.Orders.Where(p => p.Id == orderId).Select(p => new OrderDto()
            {
                Id = p.Id,
                ServiceId = p.ServiceId,
                StatusId = p.StatusId,
                CustomerUserId = p.CustomerUserId,
                FinalExpertUserId = p.FinalExpertUserId,
                ServiceBasePrice = p.ServiceBasePrice,
                CreatedAt = p.CreatedAt
            }).SingleOrDefaultAsync(cancellationToken);
            return record;
        }

    }
}
