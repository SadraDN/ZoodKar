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
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;
        private readonly IUploadService _uploadService;
        private readonly IConfiguration _configuration;
        private readonly IAppFileService _fileService;
        private readonly IAppUserService _appUserService;
        public OrderAppService(IOrderService OrderService
            , IUploadService uploadService
            , IConfiguration configuration
            , IAppFileService fileService
            , IAppUserService appUserService)
        {
            _orderService = OrderService;
            _uploadService = uploadService;
            _configuration = configuration;
            _fileService = fileService;
            _appUserService = appUserService;
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderService.Delete(id, cancellationToken);
        }

        public async Task<List<OrderDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _orderService.GetAll(cancellationToken);
        }

        public Task<List<OrderDto>?> GetAllByCustomerId(int customerId, CancellationToken cancellationToken)
        {
            return _orderService.GetAllByCustomerId(customerId, cancellationToken);
        }

        public Task<List<OrderDto>?> GetAllByExpertId(int expertId,int serviceId, CancellationToken cancellationToken)
        {
            return _orderService.GetAllByExpertId(expertId, serviceId, cancellationToken);
        }

        public async Task<List<OrderDto>?> GetAllByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await _orderService.GetAllByOrderId(orderId, cancellationToken);
        }

        public async Task<OrderDto>? GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await _orderService.GetByOrderId(orderId, cancellationToken);
        }

        public async Task Set(OrderDto dto, IList<IFormFile>? orderFile, CancellationToken cancellationToken)
        {
            if (orderFile != null)
            {

                var uploadpath = _configuration.GetSection("FileAddress:OrderPath").Value;
                foreach (IFormFile file in orderFile)
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
            dto.StatusId = 1;
            await _orderService.Set(dto, cancellationToken);
        }

        public async Task Update(OrderDto dto, CancellationToken cancellationToken)
        {
            await _orderService.Update(dto, cancellationToken);
        }
    }
}
