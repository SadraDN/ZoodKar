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
    public class EntityService: IEntityService
    {
        private readonly IEntityRepository _entityRepository;
        public EntityService(IEntityRepository EntityRepository)
        {
            _entityRepository = EntityRepository;
        }
        public async Task<EntityDto>? Get(int id, CancellationToken cancellationToken)
        {
           var record = await _entityRepository.Get(id, cancellationToken);
            if(record == null)
            {
                throw new Exception($"No Entity with Id : {id}!");
            }
            return record;
        }

        public async Task<EntityDto>? Get(string title, CancellationToken cancellationToken)
        {
            var record = await _entityRepository.Get(title, cancellationToken);
            if (record == null)
            {
                throw new Exception($"No Entity with Title : {title}!");
            }
            return record;
        }

        public async Task<List<EntityDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _entityRepository.GetAll(cancellationToken);
        }

        public async Task Set(EntityDto dto, CancellationToken cancellationToken)
        {
            await _entityRepository.Add(dto, cancellationToken);
        }

        public async Task Update(EntityDto dto, CancellationToken cancellationToken)
        {
            await _entityRepository.Update(dto, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _entityRepository.Delete(id, cancellationToken);
        }

    }
}
