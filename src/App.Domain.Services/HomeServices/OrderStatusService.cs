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
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        public OrderStatusService(IOrderStatusRepository OrderStatusRepository)
        {
            _orderStatusRepository = OrderStatusRepository;
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderStatusRepository.Delete(id,cancellationToken);
        }

        public async Task<List<OrderStatusDto>> GetAll(CancellationToken cancellationToken)
        {
           return await _orderStatusRepository.GetAll(cancellationToken);
        }

        public async Task<OrderStatusDto>? GetById(int id, CancellationToken cancellationToken)
        {
            var record = await _orderStatusRepository.GetById(id,cancellationToken);
            if (record == null)
            {
                throw new Exception($"No Order Statues with Id : {id}!");
            }
            return record;
        }

        public async Task<OrderStatusDto>? GetByTitle(string title, CancellationToken cancellationToken)
        {
            var record = await _orderStatusRepository.GetByTitle(title, cancellationToken);
            if (record == null)
            {
                throw new Exception($"No Order status with Title : {title}!");
            }
            return record;
        }

        public async Task Set(OrderStatusDto dto, CancellationToken cancellationToken)
        {
            await _orderStatusRepository.Add(dto,cancellationToken);
        }

        public async Task Update(OrderStatusDto dto, CancellationToken cancellationToken)
        {
            await _orderStatusRepository.Update(dto,cancellationToken);
        }
    }
}
