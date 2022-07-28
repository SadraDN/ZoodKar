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
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;
        public OrderAppService(IOrderService OrderService)
        {
            _orderService = OrderService;
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderService.Delete(id, cancellationToken);
        }

        public async Task<List<OrderDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _orderService.GetAll(cancellationToken);
        }

        public Task<List<OrderDto>> GetAllByCustomerId(int customerId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDto>> GetAllByExpertId(int expertId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDto>? GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await _orderService.GetByOrderId(orderId, cancellationToken);
        }

        public async Task Set(OrderDto dto, CancellationToken cancellationToken)
        {
            dto.StatusId = 1;
            await _orderService.Set(dto, cancellationToken);
        }

        public async Task Update(OrderDto dto, CancellationToken cancellationToken)
        {
            await _orderService.Update(dto, cancellationToken);
        }
    }
}
