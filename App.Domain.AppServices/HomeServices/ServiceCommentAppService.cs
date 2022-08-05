using App.Domain.Core.HomeService.Contracts.AppServices;
using App.Domain.Core.HomeService.Contracts.Services;
using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.HomeServices
{
    public class ServiceCommentAppService : IServiceCommentAppService
    {
        private readonly IServiceCommentService _serviceCommentService;
        public ServiceCommentAppService(IServiceCommentService serviceCommentService)
        {
            _serviceCommentService = serviceCommentService;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceCommentService.Delete(id, cancellationToken);
        }

        public async Task<List<ServiceCommentDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _serviceCommentService.GetAll(cancellationToken);
        }

        public Task<ServiceCommentDto>? GetByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceCommentDto>? GetByServiceId(int serviceId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceCommentDto>? GetByTitle(string title, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task Set(ServiceCommentDto dto, CancellationToken cancellationToken)
        {
            await _serviceCommentService.Set(dto, cancellationToken);
        }

        public Task Update(ServiceCommentDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
