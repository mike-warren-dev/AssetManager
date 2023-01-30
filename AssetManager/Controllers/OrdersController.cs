using AssetManager.Repos;
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

        private IOrderService _orderService;
        private IPeopleService _peopleService;
        private IDictService _dictService;

        public OrdersController(IOrderRepository repository, IOrderService orderService, IPeopleService peopleService, IDictService dictService)
        {
            _orderService = orderService;
            _peopleService = peopleService;
            _dictService = dictService;
        }

        public ActionResult Index()
        {
            return View(_orderService.GetPageOfOrders(1));
        }

        [HttpGet("Orders/GetPageOfOrders/{pageNumber}")]
        public ActionResult GetPageOfOrders(int pageNumber)
        {
            return PartialView("_GridPartial", _orderService.GetPageOfOrders(pageNumber));
        }

        public async Task<ActionResult> AddEdit(int id)
        {
            OrderAddEditViewModel vm = new();
            vm.Order.Products.Add(new ProductOrder());
            vm.People = await _peopleService.GetAllPeople();
            vm.VendorOptions = _dictService.GetDictItems(104);
            vm.ProductOptions = _dictService.GetDictItems(102);

            if (id == 0)
            {
                return PartialView("_AddEditOrderModal", vm);
            }
            else
            {
                var order = _orderService.GetOrderById(id);

                if (order != null)
                {
                    vm.Order = order;
                    return PartialView("_AddEditOrderModal", vm);
                }
                else
                {
                    return PartialView("_AddEditOrderModal", vm);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(OrderAddEditViewModel vm)
        {
            if (vm.Order == null) return RedirectToAction(nameof(Index));

            int orderId;

            try
            {
                if (vm.Order.OrderId == 0)
                {
                    orderId = _orderService.Create(vm.Order);
                }
                else
                {
                    _orderService.Update(vm.Order);

                    orderId = (int)vm.Order.OrderId;
                }

                var order = _orderService.GetOrderById(orderId);
                return PartialView("_RowPartial", order);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            _orderService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult ReceiveOrder(int id)
        {
            if (id > 0)
            {
                _orderService.ReceiveOrder(id);
                return RedirectToAction(nameof(Index));
            }
            
            return RedirectToAction(nameof(Index));
            
        }
    }
}
