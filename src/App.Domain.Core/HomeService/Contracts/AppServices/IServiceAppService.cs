using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Contracts.AppServices
{
    public interface IServiceAppService
    {
        Task<List<ServiceDto>> GetAll(CancellationToken cancellationToken);
        Task<ServiceDto>? GetByOrderId(int orderId, CancellationToken cancellationToken);
        Task<ServiceDto>? GetById(int id, CancellationToken cancellationToken);
        Task Set(ServiceDto dto, CancellationToken cancellationToken);
        Task Update(ServiceDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
