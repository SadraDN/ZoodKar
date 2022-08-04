using App.Domain.Core.HomeService.Contracts.AppServices;
using App.Domain.Core.HomeService.Dtos;
using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Entities;
using App.EndPoints.UI.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.UI.Controllers
{

    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IServiceAppService _serviceAppService;
        private readonly ICategoryAppService _categoryAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAppUserAppService _appUserAppService;
        private readonly IBidAppService _bidAppService;
        public OrderController(IOrderAppService OrderAppService
            , IServiceAppService ServiceAppService
             , ICategoryAppService CategoryAppService
            , IHttpContextAccessor HttpContextAccessor
            , IAppUserAppService AppUserAppService
            , IBidAppService BidAppService)
        {
            _orderAppService = OrderAppService;
            _serviceAppService = ServiceAppService;
            _categoryAppService = CategoryAppService;
            _httpContextAccessor = HttpContextAccessor;
            _appUserAppService = AppUserAppService;
            _bidAppService = BidAppService;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var record = await _categoryAppService.GetAll(cancellationToken);
            var categories = record.Select(c => new CategoryVM
            {
                Id = c.Id,
                Title = c.Title,
            }).ToList();
            return View(categories);
        }


        public async Task<IActionResult> ServiceList(int categoryId, CancellationToken cancellationToken)
        {
            var record = await _serviceAppService.GetAll(cancellationToken);
            var services = record.Where(c => c.CategoryId == categoryId).Select(c => new ServiceVM
            {
                CategoryName = c.CategoryName,
                Title = c.Title,
                CategoryId = c.CategoryId,
                Price = c.Price,
                Id = c.Id,
                ShortDescription = c.ShortDescription,
            }).ToList();
            return View(services);
        }

        [Authorize(Roles = "CustomerRole")]
        [HttpGet]
        public IActionResult Create(int serviceId, CancellationToken cancellationToken)
        {
            ViewBag.ServiceId = Convert.ToInt32(serviceId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderInputVM model, IList<IFormFile>? orderFile, int id, CancellationToken cancellationToken)
        {
            var userId = await _appUserAppService.GetLoggedUserId();
            if (ModelState.IsValid)
            {
                var order = new OrderDto
                {
                    ServiceDate = model.ServiceDate,
                    SerivceAddress = model.Address,
                    ServiceId = id,
                    CustomerUserId = userId,
                };
                await _orderAppService.Set(order, orderFile, cancellationToken);
                return RedirectToAction("Index");
            }
            return View(model);

        }

        [Authorize(Roles = "CustomerRole")]
        public async Task<IActionResult> OrdersList(CancellationToken cancellationToken)
        {
            var customerId = await _appUserAppService.GetLoggedUserId();
            var orders = await _orderAppService.GetAllByCustomerId(customerId, cancellationToken);
            return View(orders);
        }

        [Authorize(Roles = "CustomerRole")]
        public async Task<IActionResult> BidsList(int orderId, CancellationToken cancellationToken)
        {
            var bids = await _orderAppService.GetAllByOrderId(orderId, cancellationToken);
            return View(bids);
        }

        [Authorize(Roles = "CustomerRole")]
        public async Task<IActionResult> Approve(int expertUserId, int orderId, int bidId, CancellationToken cancellationToken)
        {
            await _bidAppService.Approve(expertUserId, orderId, bidId, cancellationToken);
            return RedirectToAction("BidsList");
        }

        [Authorize(Roles = "ExpertRole")]
        public async Task<IActionResult> ExpertRequest(CancellationToken cancellationToken)
        {
            var expertOrders = await _orderAppService.GetAllExpertOrders(cancellationToken);
            return View(expertOrders);
        }

        [Authorize(Roles = "ExpertRole")]
        [HttpGet]
        public IActionResult ExpertBid(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ExpertBid(BidDto model, int orderId, CancellationToken cancellationToken)
        {
            var expertId = await _appUserAppService.GetLoggedUserId();

            if (ModelState.IsValid)
            {
                var bid = new BidDto()
                {
                    ExpertUserId = expertId,
                    OrderId = orderId,
                    SuggestedPrice = model.SuggestedPrice,
                };
                await _bidAppService.Set(bid, cancellationToken);
                return RedirectToAction("ExpertRequest");
            }
            return View(model);
        }
    }
}
