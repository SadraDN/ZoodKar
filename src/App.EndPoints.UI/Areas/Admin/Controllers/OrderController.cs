using App.Domain.Core.HomeService.Contracts.AppServices;
using App.Domain.Core.HomeService.Dtos;
using App.EndPoints.UI.Areas.Admin.Models.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IOrderStatusAppService _orderStatusAppService;
        private readonly IBidAppService _bidAppService;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderAppService OrderAppService,
            IOrderStatusAppService OrderStatusAppService
            , IBidAppService BidAppService
            , ILogger<OrderController> logger)
        {
            _orderAppService = OrderAppService;
            _orderStatusAppService = OrderStatusAppService;
            _bidAppService = BidAppService;
            _logger = logger;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            _logger.LogTrace("start {action} logging", "Index");
            var statues = await _orderStatusAppService.GetAll(cancellationToken);
            var result = await _orderAppService.GetAll(cancellationToken);
            ViewBag.Statuses = statues.Select(s => new SelectListItem
            {
                Text = s.Title,
                Value = s.Id.ToString()
            });
            if (result.Count == 0)
            {
                _logger.LogWarning("No Order available");
            }
            if (result == null)
            {
                _logger.LogError("Order Repository is null");
            }
            _logger.LogTrace("finish {action} logging", "Index");
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var statues = await _orderStatusAppService.GetAll(cancellationToken);
            ViewBag.Statuses = statues.Select(s => new SelectListItem
            {
                Text = s.Title,
                Value = s.Id.ToString()
            });
            var order = await _orderAppService.GetByOrderId(id, cancellationToken);
            var dto = new OrderUpdateVM
            {
                ServiceBasePrice = order.ServiceBasePrice,
                StatusId = order.StatusId,
                Id = order.Id,
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderUpdateVM model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var dto = new OrderDto
            {
                ServiceBasePrice = model.ServiceBasePrice,
                StatusId = model.StatusId,
                Id = model.Id,
            };
            var statues = await _orderStatusAppService.GetAll(cancellationToken);
            await _orderAppService.Update(dto, cancellationToken);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BidsList(int orderId, CancellationToken cancellationToken)
        {
            //var bids = await _bidAppService.GetAllByOrderId(orderId, cancellationToken);
            var bids = await _orderAppService.GetAllByOrderId(orderId, cancellationToken);
            return View(bids);
        }

        public async Task<IActionResult> Approve(int expertUserId, int orderId, int bidId, CancellationToken cancellationToken)
        {
            await _bidAppService.Approve(expertUserId, orderId, bidId, cancellationToken);
            return RedirectToAction($"Index");
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _orderAppService.Delete(id, cancellationToken);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderAppService.GetByOrderId(orderId, cancellationToken);
            return View(order);
        }
    }


}
