using App.Domain.Core.HomeService.Contracts.Repositories;
using App.Domain.Core.HomeService.Contracts.Services;
using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.HomeServices
{
    public class ExpertFavoriteServicesService : IExpertFavoriteCategoryService
    {
        private readonly IExpertFavoriteCategoryRepository _expertFavoriteCategoryRepository;

        public ExpertFavoriteServicesService(IExpertFavoriteCategoryRepository expertFavoriteCategoryRepository)
        {
            _expertFavoriteCategoryRepository = expertFavoriteCategoryRepository;
        }

        public async Task Add(ExpertFavoriteServiceDto dto, CancellationToken cancellationToken)
        {
            await _expertFavoriteCategoryRepository.Add(dto, cancellationToken);
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ExpertFavoriteServiceDto>> GetAllByExpertId(int expertId, CancellationToken cancellationToken)
        {
            return await _expertFavoriteCategoryRepository.GetAllByExpertId(expertId, cancellationToken);
        }

        public async Task Update(ExpertFavoriteServiceDto dto, CancellationToken cancellationToken)
        {
            await _expertFavoriteCategoryRepository.Update(dto, cancellationToken);
        }
    }
}
