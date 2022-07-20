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
    public class ServiceCommentRepository : IServiceCommentRepository
    {

        private readonly AppDbContext _context;
        public ServiceCommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(ServiceCommentDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.HomeService.Entities.ServiceComment record = new()
            {
                ServiceId = dto.ServiceId,
                OrderId = dto.OrderId,
                CreatedAt = dto.CreatedAt,
                CommentText = dto.CommentText,
                CreatedUserId = dto.CreatedUserId
            };
            await _context.AddAsync(record, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(ServiceCommentDto dto, CancellationToken cancellationToken)
        {
            var record = await _context.ServiceComments.Where(p => p.Id == dto.Id).SingleAsync(cancellationToken);
            record.ServiceId = dto.ServiceId;
            record.OrderId = dto.OrderId;
            record.CreatedAt = dto.CreatedAt;
            record.CommentText = dto.CommentText;
            record.CreatedUserId = dto.CreatedUserId;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var record = await _context.ServiceComments.Where(p => p.Id == id).SingleAsync(cancellationToken);
            _context.Remove(record!);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ServiceCommentDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.ServiceComments.Select(p => new ServiceCommentDto()
            {
                Id = p.Id,
                OrderId = p.OrderId,
                ServiceId= p.ServiceId,
                CreatedUserId = p.CreatedUserId,
                CommentText = p.CommentText,
                CreatedAt = p.CreatedAt,
 
            }).ToListAsync(cancellationToken);
        }

        public async Task<ServiceCommentDto>? GetById(int id, CancellationToken cancellationToken)
        {
            var record = await _context.ServiceComments.Where(p => p.Id == id).Select(p => new ServiceCommentDto()
            {
                Id = p.Id,
                OrderId = p.OrderId,
                ServiceId = p.ServiceId,
                CreatedUserId = p.CreatedUserId,
                CommentText = p.CommentText,
                CreatedAt = p.CreatedAt,
            }).SingleOrDefaultAsync(cancellationToken);
            return record;
        }

        public async Task<ServiceCommentDto>? GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var record = await _context.ServiceComments.Where(p => p.OrderId ==orderId ).Select(p => new ServiceCommentDto()
            {
                Id = p.Id,
                OrderId = p.OrderId,
                ServiceId = p.ServiceId,
                CreatedUserId = p.CreatedUserId,
                CommentText = p.CommentText,
                CreatedAt = p.CreatedAt,
            }).SingleOrDefaultAsync(cancellationToken);
            return record;
        }


    }
}
