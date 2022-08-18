using App.Domain.Core.HomeService.Contracts.Repositories;
using App.Domain.Core.HomeService.Dtos;
using App.Domain.Core.HomeService.Entities;
using App.Domain.Core.User.Dtos;
using App.Infrastructures.Database.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace App.Infrastructures.Repository.Ef.HomeServices
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderRepository> _logger;
        public OrderRepository(AppDbContext context,
            ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(OrderDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.HomeService.Entities.Order record = new()
            {
                StatusId = dto.StatusId,
                ServiceId = dto.ServiceId,
                ServiceBasePrice = dto.ServiceBasePrice,
                CustomerUserId = dto.CustomerUserId,
                FinalExpertUserId = dto.FinalExpertUserId,
                CreatedAt = dto.CreatedAt,
                ServiceDate = dto.ServiceDate,
                SerivceAddress = dto.SerivceAddress,
            };
            if (record.Id != 0)
            {
                _logger.LogInformation("New {Method} added succesfully", "Order");
            }
            else
            {
                _logger.LogWarning("Add new {Method} failed", "Order");
            }
            await _context.AddAsync(record, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            foreach (var file in dto.AppFiles)
            {
                OrderFile orderFiles = new OrderFile
                {
                    OrderId = record.Id,
                    FileId = file.Id,
                    CreatedUserId = file.CreatedUserId,
                    CreatedAt = DateTime.Now,
                };
                record.OrderFiles.Add(orderFiles);
                if (file.Id != 0)
                {
                    _logger.LogInformation("{Method} File added succesfully", "Order");
                }
                else
                {
                    _logger.LogWarning("Add new file for {Method} failed", "Order");
                }
            }
            await _context.SaveChangesAsync(cancellationToken);

        }

        public async Task Update(OrderDto dto, CancellationToken cancellationToken)
        {
            var record = await _context.Orders.Where(p => p.Id == dto.Id).Include(x => x.OrderFiles).SingleAsync(cancellationToken);
            record.StatusId = dto.StatusId;
            record.ServiceBasePrice = dto.ServiceBasePrice;
            record.FinalExpertUserId = dto.FinalExpertUserId;

            //var orderFiles = new List<OrderFile>();
            //foreach (var file in dto.AppFiles)
            //{
            //    OrderFile orderFile = new OrderFile
            //    {
            //        OrderId = record.Id,
            //        FileId = file.Id,
            //        CreatedUserId = file.CreatedUserId,
            //        CreatedAt = DateTime.Now,
            //    };
            //    orderFiles.Add(orderFile);
            //}

            //record.OrderFiles = orderFiles;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{Method} updated succesfully", "Order");
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var record = await _context.Orders.Where(p => p.Id == id).SingleAsync(cancellationToken);
            if (record == null)
            {
                _logger.LogWarning("No {Method} found by Id {Id} to delete", "Order", id);
            }
            _context.Remove(record!);
            _logger.LogInformation("{Method} deleted succesfully", "Order");
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<OrderDto>> GetAll(CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Select(p => new OrderDto()
            {
                Id = p.Id,
                ServiceId = p.ServiceId,
                ServiceName = p.Service.Title,
                StatusId = p.StatusId,
                StatusName = p.Status.Title,
                CustomerUserId = p.CustomerUserId,
                CustomerUserName = p.Customer.Name,
                FinalExpertUserId = p.FinalExpertUserId,
                FinalExpertUserName = p.Expert.Name,
                ServiceBasePrice = p.Service.Price,
                CreatedAt = p.CreatedAt,
                SerivceAddress = p.SerivceAddress,
                ServiceDate = p.ServiceDate,
                AppFiles = p.OrderFiles.Select(x => new AppFileDto
                {
                    CreatedAt = x.CreatedAt,
                    CreatedUserId = x.CreatedUserId,
                    Id = x.FileId,
                    EntityId = x.File.EntityId,
                    FileAddress = x.File.FileAddress,
                }).ToList(),
                Bids = p.Bids.Select(b => new BidDto()
                {
                    CreatedAt = p.CreatedAt,
                    ExpertName = p.Expert.Name,
                    ExpertUserId = b.ExpertUserId,
                    Id=b.Id,
                    IsApproved = b.IsApproved,
                    OrderId = b.OrderId,
                    SuggestedPrice = b.SuggestedPrice,

                }).ToList(),
                
            }).ToListAsync(cancellationToken);
            if (orders != null)
            {
                _logger.LogInformation("All {Method} get succesfully ", "Orders");
            }
            else
            {
                _logger.LogWarning("Get All {Method} failed", "Orders");
            }
            return orders;
        }

        public async Task<List<OrderDto>?> GetAllByCustomerId(int customerId, CancellationToken cancellationToken)
        {
            var record = await _context.Orders.Where(p => p.CustomerUserId == customerId).Select(p => new OrderDto()
            {
                Id = p.Id,
                ServiceId = p.ServiceId,
                StatusId = p.StatusId,
                StatusName = p.Status.Title,
                ServiceDate = p.ServiceDate,
                ServiceName = p.Service.Title,
                CustomerUserId = p.CustomerUserId,
                CustomerUserName = p.Customer.Name,
                FinalExpertUserId = p.FinalExpertUserId,
                FinalExpertUserName = p.Expert.Name,
                ServiceBasePrice = p.Service.Price,
                CreatedAt = p.CreatedAt,
                SerivceAddress = p.SerivceAddress,
                AppFiles = p.OrderFiles.Select(x => new AppFileDto
                {
                    CreatedAt = x.CreatedAt,
                    CreatedUserId = x.CreatedUserId,
                    Id = x.FileId,
                    EntityId = x.File.EntityId,
                    FileAddress = x.File.FileAddress,
                }).ToList(),
                Bids = p.Bids.Select(b => new BidDto()
                {
                    CreatedAt = b.CreatedAt,
                    ExpertName = b.AppUser.Name,
                    ExpertUserId = b.ExpertUserId,
                    Id = b.Id,
                    IsApproved = b.IsApproved,
                    OrderId = b.OrderId,
                    SuggestedPrice = b.SuggestedPrice,

                }).ToList(),
                ServiceComments = p.ServiceComments.Select(x => new ServiceCommentDto()
                {
                    CommentText = x.CommentText,
                    CreatedAt = x.CreatedAt,
                    CreatedUserId =x.CreatedUserId,
                    Id = x.Id,
                    OrderId = x.OrderId,
                    ServiceId = x.ServiceId,
                }).ToList()
            }).ToListAsync(cancellationToken);
            if (record != null)
            {
                _logger.LogInformation("{Method} By CustomerId {id} get succesfully", "Order", customerId);
            }
            else
            {
                _logger.LogWarning("{Method} By CustomerID {id} not found", "Order", customerId);
            }
            return record;
        }

        public async Task<List<OrderDto>?> GetAllByExpertId(AppUserDto expert, CancellationToken cancellationToken)
        {
            
            var result = await _context.Orders.Where(x =>x.FinalExpertUserId == expert.Id && expert.Services.Select(x => x.Id).Contains(x.Service.Id) && x.StatusId == 6 || x.StatusId == 5 || x.StatusId == 4).Select(x => new OrderDto
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                CustomerUserId = x.CustomerUserId,
                CustomerUserName = x.Customer.Name,
                FinalExpertUserId = x.FinalExpertUserId,
                FinalExpertUserName = x.Expert.Name,
                SerivceAddress = x.SerivceAddress,
                ServiceBasePrice = x.Service.Price,
                ServiceDate = x.ServiceDate,
                ServiceName = x.Service.Title,
                ServiceId = x.Service.Id,
                StatusId = x.StatusId,
                StatusName = x.Status.Title,
                ServiceComments = x.ServiceComments.Select(x => new ServiceCommentDto
                {
                    CommentText = x.CommentText,
                    CreatedAt = x.CreatedAt,
                    CreatedUserId = x.CreatedUserId,
                    Id = x.Id,
                    OrderId = x.OrderId,
                    ServiceId = x.ServiceId
                }).ToList(),

            }).ToListAsync(cancellationToken);
            if (result != null)
            {
                _logger.LogInformation("{Method} By CustomerId {id} get succesfully", "Order", expert);
            }
            else
            {
                _logger.LogWarning("{Method} By CustomerId {id} not found", "Order", expert);
            }
            return result;
        }

        public async Task<OrderDto>? GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var record = await _context.Orders.Where(p => p.Id == orderId).Select(p => new OrderDto()
            {
                Id = p.Id,
                ServiceId = p.ServiceId,
                StatusId = p.StatusId,
                StatusName = p.Status.Title,
                ServiceDate = p.ServiceDate,
                ServiceName = p.Service.Title,
                CustomerUserId = p.CustomerUserId,
                CustomerUserName=p.Customer.Name,
                FinalExpertUserId = p.FinalExpertUserId,
                FinalExpertUserName=p.Expert.Name,
                ServiceBasePrice = p.Service.Price,
                CreatedAt = p.CreatedAt,
                SerivceAddress = p.SerivceAddress,
                AppFiles = p.OrderFiles.Select(x => new AppFileDto
                {
                    CreatedAt = x.CreatedAt,
                    CreatedUserId = x.CreatedUserId,
                    Id = x.FileId,
                    EntityId = x.File.EntityId,
                    FileAddress = x.File.FileAddress,
                }).ToList(),
                Bids = p.Bids.Select(b => new BidDto()
                {
                    CreatedAt = b.CreatedAt,
                    ExpertName = b.AppUser.Name,
                    ExpertUserId = b.ExpertUserId,
                    Id = b.Id,
                    IsApproved = b.IsApproved,
                    OrderId = b.OrderId,
                    SuggestedPrice = b.SuggestedPrice,


                }).ToList(),
            }).SingleOrDefaultAsync(cancellationToken);
            if (record != null)
            {
                _logger.LogInformation("{Method} By OrderId {id} get succesfully", "Order", orderId);
            }
            else
            {
                _logger.LogWarning("{Method} By OrderId {id} not found", "Order", orderId);
            }
            return record;
        }

        public async Task<List<OrderDto>?> GetAllByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var record = await _context.Orders.Where(p => p.Id == orderId).Select(p => new OrderDto()
            {
                Id = p.Id,
                ServiceId = p.ServiceId,
                StatusId = p.StatusId,
                StatusName = p.Status.Title,
                ServiceDate = p.ServiceDate,
                ServiceName = p.Service.Title,
                CustomerUserId = p.CustomerUserId,
                CustomerUserName = p.Customer.Name,
                FinalExpertUserId = p.FinalExpertUserId,
                FinalExpertUserName = p.Expert.Name,
                ServiceBasePrice = p.Service.Price,
                CreatedAt = p.CreatedAt,
                SerivceAddress = p.SerivceAddress,
                AppFiles = p.OrderFiles.Select(x => new AppFileDto
                {
                    CreatedAt = x.CreatedAt,
                    CreatedUserId = x.CreatedUserId,
                    Id = x.FileId,
                    EntityId = x.File.EntityId,
                    FileAddress = x.File.FileAddress,
                }).ToList(),
                Bids = p.Bids.Select(b => new BidDto()
                {
                    CreatedAt = b.CreatedAt,
                    ExpertName = b.AppUser.Name,
                    ExpertUserId = b.ExpertUserId,
                    Id = b.Id,
                    IsApproved = b.IsApproved,
                    OrderId = b.OrderId,
                    SuggestedPrice = b.SuggestedPrice,

                }).ToList(),
            }).ToListAsync(cancellationToken);
            if (record != null)
            {
                _logger.LogInformation("{Method} By OrderId {id} get succesfully", "Order", orderId);
            }
            else
            {
                _logger.LogWarning("{Method} By OrderId {id} not found", "Order", orderId);
            }
            return record;
        }

        public async Task<List<OrderDto>?> GetAllExpertOrders(AppUserDto expert, CancellationToken cancellationToken)
        {

           var result =await _context.Orders.Where(x => expert.Services.Select(x => x.Id).Contains(x.Service.Id) && x.StatusId == 1 || x.StatusId==2 ).Select(x=>new OrderDto
           {
               Id = x.Id,
               CreatedAt = x.CreatedAt,
               CustomerUserId = x.CustomerUserId,
               CustomerUserName = x.Customer.Name,
               FinalExpertUserId = x.FinalExpertUserId,
               FinalExpertUserName=x.Expert.Name,
               SerivceAddress = x.SerivceAddress,
               ServiceBasePrice = x.Service.Price,
               ServiceDate = x.ServiceDate,
               ServiceName = x.Service.Title,
               ServiceId = x.Service.Id,
               StatusId = x.StatusId,
               StatusName = x.Status.Title,
               Bids= x.Bids.Select(b=> new BidDto
               {
                   SuggestedPrice = b.SuggestedPrice,
                   CreatedAt = b.CreatedAt,
                   ExpertName = b.AppUser.Name,
                   ExpertUserId = b.ExpertUserId,
                   Id = b.Id,
                   IsApproved = b.IsApproved,
                   OrderId = b.OrderId,
               }).ToList(),
               ServiceComments = x.ServiceComments.Select(x=>new ServiceCommentDto
               {
                   CommentText = x.CommentText,
                   CreatedAt = x.CreatedAt,
                   CreatedUserId = x.CreatedUserId,
                   Id = x.Id,
                   OrderId = x.OrderId,
                   ServiceId = x.ServiceId
               }).ToList(),
           }).ToListAsync(cancellationToken);
            if (result != null)
            {
                _logger.LogInformation("{Method} By ExpertId {id} get succesfully", "Order", expert.Id);
            }
            else
            {
                _logger.LogWarning("{Method} By ExpertId {id} not found", "Order", expert.Id);
            }
            return result;
        }
    }
}
