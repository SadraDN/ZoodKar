using App.Domain.Core.BaseData.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseData.Contracts.Repositories
{
    public interface IAppFileRepository
    {
        #region "Queries"

        Task<AppFileDto>? Get(int id, CancellationToken cancellationToken);
        Task<AppFileDto>? Get(string fileAddress, CancellationToken cancellationToken);
        Task<List<AppFileDto>> GetAll(CancellationToken cancellationToken);

        #endregion



        #region "Commands"

        Task Add(AppFileDto dto, CancellationToken cancellationToken);
        Task Update(AppFileDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);

        #endregion
    }
}
