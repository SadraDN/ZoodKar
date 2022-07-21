using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Contracts.Services
{
    public interface IEntityService
    {
        Task<EntityDto>? Get(int id, CancellationToken cancellationToken);
        Task<EntityDto>? Get(string title, CancellationToken cancellationToken);
        Task<List<EntityDto>> GetAll(CancellationToken cancellationToken);
        Task Set(EntityDto dto, CancellationToken cancellationToken);
        Task Update(EntityDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
