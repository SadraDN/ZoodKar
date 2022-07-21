using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Contracts.Services
{
    public interface IOrderStatusService
    {
        Task<List<OrderStatusDto>> GetAll(CancellationToken cancellationToken);
        Task<OrderStatusDto>? GetByTitle(string title, CancellationToken cancellationToken);
        Task<OrderStatusDto>? GetById(int id, CancellationToken cancellationToken);
        Task Set(OrderStatusDto dto, CancellationToken cancellationToken);
        Task Update(OrderStatusDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
