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
    public class AppFileService : IAppFileService
    {
        private readonly IAppFileRepository _appFileRepository;
        public AppFileService(IAppFileRepository AppFileRepository)
        {
            _appFileRepository = AppFileRepository;
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _appFileRepository.Delete(id, cancellationToken);
        }

        public async Task<AppFileDto>? Get(int id, CancellationToken cancellationToken)
        {
            var record = await _appFileRepository.Get(id, cancellationToken);
            if(record == null)
            {
                throw new Exception($"There is no File with Id {id}!");
            }
            return record;
        }

        public async Task<AppFileDto>? Get(string fileAddress, CancellationToken cancellationToken)
        {
            var record = await _appFileRepository.Get(fileAddress, cancellationToken);
            if (record == null)
            {
                throw new Exception($"There is no File with file address {fileAddress}!");
            }
            return record;
        }

        public async Task<List<AppFileDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _appFileRepository.GetAll(cancellationToken);
        }

        public async Task<int> Set(AppFileDto dto, CancellationToken cancellationToken)
        {
           return await _appFileRepository.Add(dto, cancellationToken);
        }

        public async Task Update(AppFileDto dto, CancellationToken cancellationToken)
        {
            await _appFileRepository.Update(dto, cancellationToken);
        }
    }
}
