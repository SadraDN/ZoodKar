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
    public class BidRepository: IBidsRepository
    {
        private readonly AppDbContext _context;
        public BidRepository(AppDbContext context)
        {
            _context = context;
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
            record.CreatedAt = dto.CreatedAt;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var record = await _context.Bids.Where(p => p.Id == id).SingleAsync(cancellationToken);
            _context.Remove(record!);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<BidDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Bids.Select(p => new BidDto()
            {
                Id = p.Id,
                OrderId = p.OrderId,
                ExpertUserId = p.ExpertUserId,
                SuggestedPrice=p.SuggestedPrice,
                IsApproved=p.IsApproved,
                CreatedAt = p.CreatedAt,

            }).ToListAsync(cancellationToken);
        }

        public async Task<List<BidDto>> GetAllByExpertId(int expertId, CancellationToken cancellationToken)
        {
            return await _context.Bids.Where(x=>x.ExpertUserId==expertId).Select(p => new BidDto()
            {
                Id = p.Id,
                OrderId = p.OrderId,
                ExpertUserId = p.ExpertUserId,
                SuggestedPrice = p.SuggestedPrice,
                IsApproved = p.IsApproved,
                CreatedAt = p.CreatedAt,

            }).ToListAsync(cancellationToken);
        }

        public async Task<List<BidDto>> GetAllByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await _context.Bids.Where(x => x.OrderId == orderId).Select(p => new BidDto()
            {
                Id = p.Id,
                OrderId = p.OrderId,
                ExpertUserId = p.ExpertUserId,
                SuggestedPrice = p.SuggestedPrice,
                IsApproved = p.IsApproved,
                CreatedAt = p.CreatedAt,

            }).ToListAsync(cancellationToken);
        }
    }
}
