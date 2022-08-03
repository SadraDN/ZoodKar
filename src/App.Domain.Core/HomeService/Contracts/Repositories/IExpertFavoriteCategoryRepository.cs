using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Contracts.Repositories
{
    public interface IExpertFavoriteCategoryRepository
    {
        Task Add(ExpertFavoriteServiceDto dto, CancellationToken cancellationToken);
        Task Update(ExpertFavoriteServiceDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);

        Task<List<ExpertFavoriteServiceDto>> GetAllByExpertId(int expertId , CancellationToken cancellationToken);
    }
}
