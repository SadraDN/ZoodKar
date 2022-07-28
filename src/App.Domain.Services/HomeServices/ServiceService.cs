using App.Domain.Core.HomeService.Contracts.Repositories;
using App.Domain.Core.HomeService.Contracts.Services;
using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.HomeServices
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceService(IServiceRepository ServiceRepository )
        {
            _serviceRepository = ServiceRepository;
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceRepository.Delete(id,cancellationToken);
        }

        public async Task<List<ServiceDto>> GetAll(CancellationToken cancellationToken)
        {
           return await _serviceRepository.GetAll(cancellationToken);
        }

        public async Task<ServiceDto>? GetByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            return await GetByCategoryId(categoryId,cancellationToken);
        }

        public async Task<ServiceDto>? GetByServiceId(int serviceId, CancellationToken cancellationToken)
        {
            return await _serviceRepository.GetByServiceId(serviceId,cancellationToken);
        }

        public async Task<ServiceDto>? GetByTitle(string title, CancellationToken cancellationToken)
        {
            return await _serviceRepository.GetByTitle(title,cancellationToken);
        }

        public async Task Set(ServiceDto dto, CancellationToken cancellationToken)
        {
            await _serviceRepository.Add(dto,cancellationToken);
        }

        public async Task Update(ServiceDto dto, CancellationToken cancellationToken)
        {
            await _serviceRepository.Update(dto, cancellationToken);
        }
    }
}
