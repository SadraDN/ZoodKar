using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Contracts.Services
{
    public interface IServiceService
    {
        Task<List<ServiceDto>> GetAll(CancellationToken cancellationToken);
        Task<ServiceDto>? GetByTitle(string title, CancellationToken cancellationToken);
        Task<ServiceDto>? GetByServiceId(int serviceId, CancellationToken cancellationToken);
        Task<ServiceDto>? GetByCategoryId(int categoryId, CancellationToken cancellationToken);
        Task Set(ServiceDto dto, CancellationToken cancellationToken);
        Task Update(ServiceDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
