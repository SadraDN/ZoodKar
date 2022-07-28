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
    public class ServiceAppService : IServiceAppService
    {
        private readonly IServiceService _serviceService;
        public ServiceAppService(IServiceService ServiceService)
        {
            _serviceService = ServiceService;
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ServiceDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _serviceService.GetAll(cancellationToken);
        }

        public Task<ServiceDto>? GetById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceDto>? GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Set(ServiceDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Update(ServiceDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
