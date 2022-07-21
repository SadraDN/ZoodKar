using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Contracts.Services
{
    public interface IBidService
    {
        Task<List<BidDto>> GetAll(CancellationToken cancellationToken);
        Task<List<BidDto>> GetAllByExpertId(int expertId, CancellationToken cancellationToken);
        Task<List<BidDto>> GetAllByOrderId(int orderId, CancellationToken cancellationToken);
        Task Set(BidDto dto, CancellationToken cancellationToken);
        Task Update(BidDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
