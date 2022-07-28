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
    public class OrderStatusAppService : IOrderStatusAppService
    {
        private readonly IOrderStatusService _orderStatusService;
        public OrderStatusAppService(IOrderStatusService OrderStatusService)
        {
            _orderStatusService= OrderStatusService;
        }
        public Task Delete(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderStatusDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _orderStatusService.GetAll(cancellationToken);
        }

        public Task<OrderStatusDto>? GetById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OrderStatusDto>? GetByTitle(string title, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Set(OrderStatusDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Update(OrderStatusDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
