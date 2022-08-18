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
    public class ServiceCommentRepository : IServiceCommentRepository
    {
        private readonly ILogger<ServiceCommentRepository> _logger;
        private readonly AppDbContext _context;
        public ServiceCommentRepository(AppDbContext context, ILogger<ServiceCommentRepository> logger)
        {
            _context = context;
            _logger = logger;
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
            if (record.Id != 0)
            {
                _logger.LogInformation("New {Method} added succesfully", "ServiceComment");
            }
            else
            {
                _logger.LogWarning("Add new {Method} failed", "ServiceComment");
            }
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
            _logger.LogInformation("{Method} updated succesfully", "ServiceComment");
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var record = await _context.ServiceComments.Where(p => p.Id == id).SingleAsync(cancellationToken);
            if (record == null)
            {
                _logger.LogWarning("No {Method} found by Id {Id} to delete", "ServiceComment", id);
            }
            _context.Remove(record!);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Method} deleted succesfully", "ServiceComment");
        }

        public async Task<List<ServiceCommentDto>> GetAll(CancellationToken cancellationToken)
        {
            var serviceComments = await _context.ServiceComments.Select(p => new ServiceCommentDto()
            {
                Id = p.Id,
                OrderId = p.OrderId,
                ServiceId= p.ServiceId,
                CreatedUserId = p.CreatedUserId,
                CommentText = p.CommentText,
                CreatedAt = p.CreatedAt,
            }).ToListAsync(cancellationToken);
            if (serviceComments != null)
            {
                _logger.LogInformation("All {Method} get succesfully ", "ServiceComments");
            }
            else
            {
                _logger.LogWarning("Get All {Method} failed", "ServiceComments");
            }
            return serviceComments;
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
            if (record != null)
            {
                _logger.LogInformation("{Method} By Id {id} get succesfully", "ServiceComment", id);
            }
            else
            {
                _logger.LogWarning("{Method} By Id {id} not found", "ServiceComment", id);
            }
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
            if (record != null)
            {
                _logger.LogInformation("{Method} By OrderId {id} get succesfully", "ServiceComment", orderId);
            }
            else
            {
                _logger.LogWarning("{Method} By OrderId {id} not found", "ServiceComment", orderId);
            }
            return record;
        }

        public async Task<List<ServiceCommentDto>>? GetAllByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var serviceComments = await _context.ServiceComments.Where(x=>x.OrderId==orderId).Select(p => new ServiceCommentDto()
            {
                Id = p.Id,
                OrderId = p.OrderId,
                ServiceId = p.ServiceId,
                CreatedUserId = p.CreatedUserId,
                CommentText = p.CommentText,
                CreatedAt = p.CreatedAt,

            }).ToListAsync(cancellationToken);
            if (serviceComments != null)
            {
                _logger.LogInformation("All {Method} get succesfully ", "ServiceComments");
            }
            else
            {
                _logger.LogWarning("Get All {Method} failed", "ServiceComments");
            }
            return serviceComments;
        }
    }
}
