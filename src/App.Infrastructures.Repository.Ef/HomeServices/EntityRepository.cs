using App.Domain.Core.HomeService.Contracts.Repositories;
using App.Domain.Core.HomeService.Dtos;
using App.Domain.Core.HomeService.Entities;
using App.Infrastructures.Database.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Repository.Ef.HomeServices
{
    public class EntityRepository : IEntityRepository
    {
        private readonly AppDbContext _context;
        public EntityRepository(AppDbContext Context)
        {
            _context = Context;
        }
        public async Task Add(EntityDto dto, CancellationToken cancellationToken)
        {
            Entity record = new()
            {
                Title = dto.Title,
            };
            await _context.AddAsync(record, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task Update(EntityDto dto, CancellationToken cancellationToken)
        {
            var record = await _context.Entities.Where(p => p.Id == dto.Id).SingleAsync(cancellationToken);
            record.Title = dto.Title;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var record = await _context.Entities.Where(p => p.Id == id).SingleAsync(cancellationToken);
            _context.Remove(record!);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<EntityDto>? Get(int id, CancellationToken cancellationToken)
        {
            var record = await _context.Entities.Where(p => p.Id == id).Select(p => new EntityDto()
            {
                Id = p.Id,
                Title = p.Title,
            }).SingleOrDefaultAsync(cancellationToken);
            return record;
        }

        public async Task<EntityDto>? Get(string title, CancellationToken cancellationToken)
        {
            var record = await _context.Entities.Where(p => p.Title == title).Select(p => new EntityDto()
            {
                Id = p.Id,
                Title = p.Title,
            }).SingleOrDefaultAsync(cancellationToken);
            return record;
        }

        public async Task<List<EntityDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Entities.Select(p => new EntityDto()
            {
                Id = p.Id,
                Title = p.Title,
            }).ToListAsync(cancellationToken);
        }


    }
}
