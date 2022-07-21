using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Contracts.AppServices
{
    public interface IAppFileAppService
    {
        Task<AppFileDto>? Get(int id, CancellationToken cancellationToken);
        Task<AppFileDto>? Get(string fileAddress, CancellationToken cancellationToken);
        Task<List<AppFileDto>> GetAll(CancellationToken cancellationToken);
        Task Set(AppFileDto dto, CancellationToken cancellationToken);
        Task Update(AppFileDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
