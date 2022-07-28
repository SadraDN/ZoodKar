﻿using App.Domain.Core.HomeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.HomeService.Contracts.AppServices
{
    public interface IBidAppService
    {
        Task<List<BidDto>> GetAll(CancellationToken cancellationToken);
        Task<List<BidDto>> GetAllByExpertId(int expertId, CancellationToken cancellationToken);
        Task<List<BidDto>> GetAllByOrderId(int orderId, CancellationToken cancellationToken);
        Task<BidDto>? GetById(int id, CancellationToken cancellationToken);
        Task Approve(int orderId, int bidid, CancellationToken cancellationToken);
        Task Set(BidDto dto, CancellationToken cancellationToken);
        Task Update(BidDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
