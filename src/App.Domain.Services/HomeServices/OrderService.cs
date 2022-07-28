﻿using App.Domain.Core.HomeService.Contracts.Repositories;
using App.Domain.Core.HomeService.Contracts.Services;
using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.HomeServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository OrderRepository)
        {
            _orderRepository = OrderRepository;
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderRepository.Delete(id, cancellationToken);
        }

        public async Task<List<OrderDto>> GetAll(CancellationToken cancellationToken)
        {
             return await _orderRepository.GetAll(cancellationToken);
        }

        public async Task<List<OrderDto>> GetAllByCustomerId(int customerId, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetAllByCustomerId(customerId, cancellationToken);
        }

        public async Task<List<OrderDto>> GetAllByExpertId(int expertId, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetAllByExpertId(expertId, cancellationToken);
        }

        public async Task<OrderDto>? GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var record = await _orderRepository.GetByOrderId(orderId, cancellationToken);
            if (record == null)
            {
                throw new Exception($"No Order with Order Id : {orderId}!");
            }
            return record;
        }

        public async Task Set(OrderDto dto, CancellationToken cancellationToken)
        {
            dto.StatusId = 1;
            dto.CreatedAt = DateTime.Now;
            await _orderRepository.Add(dto, cancellationToken);
        }

        public async Task Update(OrderDto dto, CancellationToken cancellationToken)
        {
            await _orderRepository.Update(dto, cancellationToken);
        }
    }
}
