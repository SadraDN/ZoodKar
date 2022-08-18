using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Contracts.Services
{
    public interface IServiceCommentService
    {
        Task<List<ServiceCommentDto>> GetAll(CancellationToken cancellationToken);
        Task<ServiceCommentDto>? GetByOrderId(int orderId, CancellationToken cancellationToken);
        Task<ServiceCommentDto>? GetById(int id, CancellationToken cancellationToken);
        Task<List<ServiceCommentDto>>? GetAllByOrderId(int orderId, CancellationToken cancellationToken);
        Task Set(ServiceCommentDto dto, CancellationToken cancellationToken);
        Task Update(ServiceCommentDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
