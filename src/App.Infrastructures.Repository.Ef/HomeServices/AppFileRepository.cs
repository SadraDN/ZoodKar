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
    public class AppFileRepository : IAppFileRepository
    {
        private readonly AppDbContext _context;
        public AppFileRepository(AppDbContext Context)
        {
            _context = Context;
        }
        public async Task Add(AppFileDto dto, CancellationToken cancellationToken)
        {
            AppFile record = new()
            {
                EntityId = dto.EntityId,
                FileAddress = dto.FileAddress,
                CreatedUserId = dto.CreatedUserId,
                CreatedAt = dto.CreatedAt,
            };
            await _context.AddAsync(record, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(AppFileDto dto, CancellationToken cancellationToken)
        {
            var record = await _context.Files.Where(p => p.Id == dto.Id).SingleAsync(cancellationToken);
            record.FileAddress = dto.FileAddress;
            record.EntityId = dto.EntityId;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var record = await _context.Files.Where(p => p.Id == id).SingleAsync(cancellationToken);
            _context.Remove(record!);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<AppFileDto>? Get(int id, CancellationToken cancellationToken)
        {
            var record = await _context.Files.Where(p => p.Id == id).Select(p => new AppFileDto()
            {
                Id = p.Id,
                EntityId = p.EntityId,
                FileAddress = p.FileAddress,
                CreatedUserId = p.CreatedUserId,
                CreatedAt = p.CreatedAt
            }).SingleOrDefaultAsync(cancellationToken);
            return record;
        }

        public async Task<AppFileDto>? Get(string fileAddress, CancellationToken cancellationToken)
        {
            var record = await _context.Files.Where(p => p.FileAddress == fileAddress).Select(p => new AppFileDto()
            {
                Id = p.Id,
                EntityId = p.EntityId,
                FileAddress = p.FileAddress,
                CreatedUserId = p.CreatedUserId,
                CreatedAt = p.CreatedAt
            }).SingleOrDefaultAsync(cancellationToken);
            return record;
        }

        public async Task<List<AppFileDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Files.Select(p => new AppFileDto()
            {
                Id = p.Id,
                EntityId = p.EntityId,
                FileAddress = p.FileAddress,
                CreatedUserId = p.CreatedUserId,
                CreatedAt = p.CreatedAt
            }).ToListAsync(cancellationToken);
        }


    }
}
