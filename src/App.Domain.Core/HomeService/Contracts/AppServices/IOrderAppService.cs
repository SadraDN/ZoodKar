using App.Domain.Core.HomeService.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Contracts.AppServices
{
    public interface IOrderAppService
    {
        Task<List<OrderDto>> GetAll(CancellationToken cancellationToken);
        Task<List<OrderDto>?> GetAllByCustomerId(int customerId, CancellationToken cancellationToken);
        Task<List<OrderDto>?> GetAllByExpertId(int expertId, CancellationToken cancellationToken);
        Task<OrderDto>? GetByOrderId(int orderId, CancellationToken cancellationToken);
        Task<List<OrderDto>?> GetAllByOrderId(int orderId, CancellationToken cancellationToken);
        Task Set(OrderDto dto, IList<IFormFile>? orderFile, CancellationToken cancellationToken);
        Task Update(OrderDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<List<OrderDto>?> GetAllExpertOrders(CancellationToken cancellationToken);
    }
}
