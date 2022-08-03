using App.Domain.Core.HomeService.Contracts.Repositories;
using App.Domain.Core.HomeService.Contracts.Services;
using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.HomeServices
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        private readonly IOrderRepository _orderRepository;
        public BidService(IBidRepository BidRepository, IOrderRepository orderRepository)
        {
            _bidRepository = BidRepository;
            _orderRepository = orderRepository;
        }

        public async Task Approve(int expertFinalId, int orderId,int bidId, CancellationToken cancellationToken)
        {
            var bids = await _bidRepository.GetAllByOrderId(orderId,cancellationToken);
            foreach (var bid in bids)
            {
                bid.IsApproved=false;
               await _bidRepository.Update(bid,cancellationToken);
            }
            var recordBid = await _bidRepository.GetById(bidId, cancellationToken);
            recordBid.IsApproved = true;
            var recordOrder= await _orderRepository.GetByOrderId(orderId, cancellationToken);
            recordOrder.StatusId = 3;
            recordOrder.FinalExpertUserId = expertFinalId;
            await _bidRepository.Update(recordBid, cancellationToken);
            await _orderRepository.Update(recordOrder, cancellationToken);

        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _bidRepository.Delete(id, cancellationToken);
        }

        public async Task<List<BidDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _bidRepository.GetAll(cancellationToken);
        }

        public async Task<List<BidDto>> GetAllByExpertId(int expertId, CancellationToken cancellationToken)
        {
            return await _bidRepository.GetAllByExpertId(expertId, cancellationToken);
        }

        public async Task<List<BidDto>> GetAllByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await _bidRepository.GetAllByOrderId(orderId, cancellationToken);
        }

        public async Task<BidDto>? GetById(int id, CancellationToken cancellationToken)
        {
            return await _bidRepository.GetById(id, cancellationToken);
        }

        public async Task Set(BidDto dto, CancellationToken cancellationToken)
        {
            await _bidRepository.Add(dto, cancellationToken);
        }

        public async Task Update(BidDto dto, CancellationToken cancellationToken)
        {
            await _bidRepository.Update(dto, cancellationToken);
        }
    }
}
