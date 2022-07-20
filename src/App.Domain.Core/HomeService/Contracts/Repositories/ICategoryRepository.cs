using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Contracts.Repositories
{
    public interface ICategoryRepository
    {
        #region "Queries"

        Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken);
        Task<CategoryDto>? GetByTitle(string title, CancellationToken cancellationToken);
        Task<CategoryDto>? GetById(int id, CancellationToken cancellationToken);

        #endregion


        #region "Commands"

        Task Add(CategoryDto dto, CancellationToken cancellationToken);
        Task Update(CategoryDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);

        #endregion
    }
}
