using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Contracts.Repositories
{
    public interface IServiceRepository
    {
        #region "Queries"

        Task<List<ServiceDto>> GetAll(CancellationToken cancellationToken);
        Task<ServiceDto>? GetByTitle(string title, CancellationToken cancellationToken);
        Task<ServiceDto>? GetByServiceId(int serviceId, CancellationToken cancellationToken);
        Task<ServiceDto>? GetByCategoryId(int categoryId, CancellationToken cancellationToken);

        #endregion


        #region "Commands"

        Task Add(ServiceDto dto, CancellationToken cancellationToken);
        Task Update(ServiceDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);

        #endregion
    }
}
