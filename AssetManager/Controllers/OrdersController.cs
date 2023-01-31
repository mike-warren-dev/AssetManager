using AssetManager.Models;
using Microsoft.AspNetCore.Mvc;
using AssetManager.Services;
using AssetManager.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace AssetManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {

        private readonly IOrderService _orderService;
        private readonly IPeopleService _peopleService;
        private readonly IDictService _dictService;

        public OrdersController(IOrderService orderService, IPeopleService peopleService, IDictService dictService)
        {
            _orderService = orderService;
            _peopleService = peopleService;
            _dictService = dictService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _orderService.GetPageOfOrders(1));
        }

        [HttpGet("Orders/GetPageOfOrders/{pageNumber}")]
        public async Task<ActionResult> GetPageOfOrders(int pageNumber)
        {
            return PartialView("_GridPartial", await _orderService.GetPageOfOrders(pageNumber));
        }

        public async Task<ActionResult> AddEdit(int id)
        {   
            OrderAddEditViewModel vm = new()
            {
                People = await _peopleService.GetAllPeople(),
                VendorOptions = _dictService.GetDictItems(104),
                ProductOptions = _dictService.GetDictItems(102)
            };

            vm.Order.Products.Add(new ProductOrder());

            if (id == 0)
                return PartialView("_AddEditOrderModal", vm);

            vm.Order = await _orderService.GetOrderById(id);
    
            return PartialView("_AddEditOrderModal", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEdit(OrderAddEditViewModel vm)
        {
            if (vm.Order == null) 
                return RedirectToAction(nameof(Index));

            int orderId;

            if (vm.Order.OrderId == 0)
            {
                var order = await _orderService.Create(vm.Order);

                orderId = order.OrderId;
            }
            else
            {
                await _orderService.Update(vm.Order);

                orderId = vm.Order.OrderId;
            }

            return PartialView("_RowPartial", await _orderService.GetOrderById(orderId));
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await _orderService.Delete(id);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> ReceiveOrder(int id)
        {
            try
            {
                await _orderService.ReceiveOrder(id);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
