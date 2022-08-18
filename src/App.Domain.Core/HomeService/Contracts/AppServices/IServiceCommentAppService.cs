using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Contracts.AppServices
{
    public interface IServiceCommentAppService
    {
        Task<List<ServiceCommentDto>> GetAll(CancellationToken cancellationToken);
        Task<ServiceCommentDto>? GetByTitle(string title, CancellationToken cancellationToken);
        Task<ServiceCommentDto>? GetByServiceId(int serviceId, CancellationToken cancellationToken);
        Task<ServiceCommentDto>? GetByCategoryId(int categoryId, CancellationToken cancellationToken);
        Task<List<ServiceCommentDto>>? GetAllByOrderId(int orderId, CancellationToken cancellationToken);
        Task Set(ServiceCommentDto dto, CancellationToken cancellationToken);
        Task Update(ServiceCommentDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
