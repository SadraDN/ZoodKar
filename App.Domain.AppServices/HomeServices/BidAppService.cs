using App.Domain.Core.HomeService.Contracts.AppServices;
using App.Domain.Core.HomeService.Contracts.Services;
using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.HomeServices
{
    public class BidAppService : IBidAppService
    {
        private readonly IBidService _bidService;
        public BidAppService(IBidService BidService)
        {
            _bidService = BidService;
        }

        public async Task Approve(int orderId,int bidId, CancellationToken cancellationToken)
        {
            await _bidService.Approve(orderId, bidId, cancellationToken);
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BidDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _bidService.GetAll(cancellationToken);
        }

        public Task<List<BidDto>> GetAllByExpertId(int expertId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BidDto>> GetAllByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await _bidService.GetAllByOrderId(orderId, cancellationToken);
        }

        public async Task<BidDto>? GetById(int id, CancellationToken cancellationToken)
        {
            return await _bidService.GetById(id, cancellationToken);
        }

        public Task Set(BidDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task Update(BidDto dto, CancellationToken cancellationToken)
        {
            await _bidService.Update(dto, cancellationToken);
        }
    }
}
