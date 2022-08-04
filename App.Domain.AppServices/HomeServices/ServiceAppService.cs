using App.Domain.Core.HomeService.Contracts.AppServices;
using App.Domain.Core.HomeService.Contracts.Services;
using App.Domain.Core.HomeService.Dtos;
using App.Domain.Core.User.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly IUploadService _uploadService;
        private readonly IAppFileService _fileService;
        private readonly IAppUserService _appUserService;
        public ServiceAppService(IServiceService ServiceService
            , IConfiguration configuration,
            IUploadService uploadService, IAppFileService fileService,
            IAppUserService appUserService)
        {
            _serviceService = ServiceService;
            _configuration = configuration;
            _uploadService = uploadService;
            _fileService = fileService;
            _appUserService = appUserService;
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

        public async Task Set(ServiceDto dto, List<IFormFile>? serviceFile, CancellationToken cancellationToken)
        {
            if (serviceFile != null)
            {

                var uploadpath = _configuration.GetSection("FileAddress:ServicePath").Value;
                foreach (IFormFile file in serviceFile)
                {
                    var fileName = await _uploadService.AddFile(file, uploadpath);
                    var fileId = await _fileService.Set(new AppFileDto
                    {
                        CreatedAt = DateTime.Now,
                        CreatedUserId = await _appUserService.GetLoggedUserId(),
                        FileAddress = fileName,
                        EntityId = 1
                    }, cancellationToken);
                    var oFile = await _fileService.Get(fileId, cancellationToken);
                    dto.AppFiles.Add(oFile);
                }
            }
            await _serviceService.Set(dto, cancellationToken);
        }

        public Task Update(ServiceDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
