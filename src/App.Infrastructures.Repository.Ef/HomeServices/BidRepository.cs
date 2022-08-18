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
    public class BidRepository: IBidRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BidRepository> _logger;
        public BidRepository(AppDbContext context, ILogger<BidRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(BidDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.HomeService.Entities.Bid record = new()
            {
                OrderId = dto.OrderId,
                ExpertUserId = dto.ExpertUserId,
                SuggestedPrice = dto.SuggestedPrice,
                IsApproved = dto.IsApproved,
                CreatedAt = dto.CreatedAt,
            };
            if (record.Id != 0)
            {
                _logger.LogInformation("New {Method} added succesfully", "Bid");
            }
            else
            {
                _logger.LogWarning("Add new {Method} failed", "Bid");
            }
            await _context.AddAsync(record, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task Update(BidDto dto, CancellationToken cancellationToken)
        {
            var record = await _context.Bids.Where(p => p.Id == dto.Id).SingleAsync(cancellationToken);
            record.OrderId = dto.OrderId;
            record.ExpertUserId = dto.ExpertUserId;
            record.SuggestedPrice = dto.SuggestedPrice;
            record.IsApproved = dto.IsApproved;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Method} updated succesfully", "Bid");
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var record = await _context.Bids.Where(p => p.Id == id).SingleAsync(cancellationToken);
            if (record == null)
            {
                _logger.LogWarning("No {Method} found by Id {Id} to delete", "Bid", id);
            }
            _context.Remove(record!);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Method} deleted succesfully", "Bid");
        }

        public async Task<List<BidDto>> GetAll(CancellationToken cancellationToken)
        {
            var bids = await _context.Bids.Select(p => new BidDto()
            {
                Id = p.Id,
                OrderId = p.OrderId,
                ExpertUserId = p.ExpertUserId,
                SuggestedPrice=p.SuggestedPrice,
                IsApproved=p.IsApproved,
                CreatedAt = p.CreatedAt,

            }).ToListAsync(cancellationToken);
            if (bids != null)
            {
                _logger.LogInformation("All {Method} get succesfully ", "Bids");
            }
            else
            {
                _logger.LogWarning("Get All {Method} failed", "Bids");
            }
            return bids;
        }

        public async Task<List<BidDto>> GetAllByExpertId(int expertId, CancellationToken cancellationToken)
        {
            var bids = await _context.Bids.Where(x=>x.ExpertUserId==expertId).Select(p => new BidDto()
            {
                Id = p.Id,
                OrderId = p.OrderId,
                ExpertUserId = p.ExpertUserId,
                SuggestedPrice = p.SuggestedPrice,
                IsApproved = p.IsApproved,
                CreatedAt = p.CreatedAt,

            }).ToListAsync(cancellationToken);
            if (bids != null)
            {
                _logger.LogInformation("All {Method} By ExpertId {Id} get succesfully ", "Bids" , expertId);
            }
            else
            {
                _logger.LogWarning("Get All {Method} By ExpertId {Id} failed", "Bids",expertId);
            }
            return bids;
        }

        public async Task<List<BidDto>> GetAllByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var bids = await _context.Bids.Where(x => x.OrderId == orderId).Select(p => new BidDto()
            {
                Id = p.Id,
                OrderId = p.OrderId,
                ExpertUserId = p.ExpertUserId,
                ExpertName = p.AppUser.Name,
                SuggestedPrice = p.SuggestedPrice,
                IsApproved = p.IsApproved,
                CreatedAt = p.CreatedAt,
            }).ToListAsync(cancellationToken);
            if (bids != null)
            {
                _logger.LogInformation("All {Method} By OrderId {Id} get succesfully ", "Bids", orderId);
            }
            else
            {
                _logger.LogWarning("Get All {Method} By OrderId {Id} failed", "Bids", orderId);
            }
            return bids;
        }

        public async Task<BidDto>? GetById(int id, CancellationToken cancellationToken)
        {
            var record = await _context.Bids.Where(p => p.Id == id).Select(p => new BidDto()
            {
                Id = p.Id,
                OrderId= p.OrderId,
                ExpertUserId= p.ExpertUserId,
                ExpertName= p.AppUser.Name,
                SuggestedPrice= p.SuggestedPrice,
                IsApproved= p.IsApproved,
                CreatedAt= p.CreatedAt,
                
            }).SingleOrDefaultAsync();
            if (record != null)
            {
                _logger.LogInformation("{Method} By Id {Title} get succesfully", "Bid", id);
            }
            else
            {
                _logger.LogWarning("{Method} By Id {Title} not found", "Bid", id);
            }
            return record;
        }
    }
}
