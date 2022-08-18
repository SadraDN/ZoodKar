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
        private readonly IOrderService _orderService;
        public BidAppService(IBidService BidService,
            IOrderService orderService)
        {
            _bidService = BidService;
            _orderService = orderService;
        }

        public async Task Approve(int expertFinalId, int orderId, int bidId, CancellationToken cancellationToken)
        {
            await _bidService.Approve(expertFinalId, orderId, bidId, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
           await _bidService.Delete(id, cancellationToken);
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

        public async Task Set(BidDto dto, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetByOrderId(dto.OrderId, cancellationToken);
            order.StatusId = 2;
            await _orderService.Update(order,cancellationToken);
            await _bidService.Set(dto, cancellationToken);
        }

        public async Task Update(BidDto dto, CancellationToken cancellationToken)
        {
            await _bidService.Update(dto, cancellationToken);
        }
    }
}
