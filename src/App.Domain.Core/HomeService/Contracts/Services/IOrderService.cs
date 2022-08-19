using App.Domain.Core.HomeService.Dtos;
using App.Domain.Core.User.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Contracts.Services
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAll(CancellationToken cancellationToken);
        Task<List<OrderDto>?> GetAllByCustomerId(int customerId, CancellationToken cancellationToken);
        Task<List<OrderDto>?> GetAllByExpertId(AppUserDto expert, CancellationToken cancellationToken);
        Task<OrderDto>? GetByOrderId(int orderId, CancellationToken cancellationToken);
        Task<List<OrderDto>?> GetAllByOrderId(int orderId, CancellationToken cancellationToken);
        Task<List<OrderDto>?> GetAllExpertOrders(AppUserDto expert, CancellationToken cancellationToken);
        Task<List<OrderDto>?> GetAllProcceingOrders(AppUserDto expert, CancellationToken cancellationToken);
        Task Set(OrderDto dto, CancellationToken cancellationToken);
        Task Update(OrderDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);


    }
}
