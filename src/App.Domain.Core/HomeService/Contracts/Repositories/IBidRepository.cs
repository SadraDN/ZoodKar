using App.Domain.Core.HomeService.Dtos;

namespace App.Domain.Core.HomeService.Contracts.Repositories
{
    public interface IBidRepository
    {
        #region "Queries"

        Task<List<BidDto>> GetAll(CancellationToken cancellationToken);
        Task<List<BidDto>> GetAllByExpertId(int expertId, CancellationToken cancellationToken);
        Task<List<BidDto>> GetAllByOrderId(int orderId, CancellationToken cancellationToken);
        Task<BidDto>? GetById(int id,CancellationToken cancellationToken);

        #endregion


        #region "Commands"

        Task Add(BidDto dto, CancellationToken cancellationToken);
        Task Update(BidDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);

        #endregion

    }
}