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
    public class ServiceCommentService : IServiceCommentService
    {
        private readonly IServiceCommentRepository _serviceCommentRepository;
        public ServiceCommentService(IServiceCommentRepository ServiceCommentRepository)
        {
            _serviceCommentRepository = ServiceCommentRepository;
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceCommentRepository.Delete(id, cancellationToken);
        }

        public async Task<List<ServiceCommentDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _serviceCommentRepository.GetAll(cancellationToken);
        }

        public async Task<List<ServiceCommentDto>>? GetAllByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await _serviceCommentRepository.GetAllByOrderId(orderId, cancellationToken);
        }

        public async Task<ServiceCommentDto>? GetById(int id, CancellationToken cancellationToken)
        {
           var record = await GetById(id, cancellationToken);
            if (record == null)
            {
                throw new Exception($"No Service with Id : {id}!");
            }
            return record;
        }

        public async Task<ServiceCommentDto>? GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var record = await GetByOrderId(orderId, cancellationToken);
            if (record == null)
            {
                throw new Exception($"No Service with Order Id : {orderId}!");
            }
            return record;
        }

        public async Task Set(ServiceCommentDto dto, CancellationToken cancellationToken)
        {
            await _serviceCommentRepository.Add(dto, cancellationToken);
        }

        public async Task Update(ServiceCommentDto dto, CancellationToken cancellationToken)
        {
           await _serviceCommentRepository.Update(dto, cancellationToken);
        }
    }
}
