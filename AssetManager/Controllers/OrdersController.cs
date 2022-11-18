using AssetManager.DTOs;
using AssetManager.Repos;
using AssetManager.Models;
using Microsoft.AspNetCore.Mvc;
using AssetManager.Services;
using AssetManager.ViewModels;

namespace AssetManager.Controllers
{
    public class OrdersController : Controller
    {
        private IOrderService _orderService;
        private IPeopleService _peopleService;

        public OrdersController(IOrderRepository repository, IOrderService orderService, IPeopleService peopleService)
        {
            _orderService = orderService;
            _peopleService = peopleService;
        }

        public ActionResult Index()
        {
            var list = _orderService.GetAllOrders();
            
            return View(list);
        }

        public ActionResult AddEdit(int id)
        {
            OrderAddEditViewModel vm = new();
            vm.OrderDto.Products.Add(new ProductOrder());
            vm.People = _peopleService.GetAllPeople();
            
            if (id == 0)
            {
                return PartialView("_AddEditOrderModal", vm);
            }
            else
            {
                var order = _orderService.GetOrderById(id);

                if (order != null)
                {
                    vm.OrderDto = order;
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
            if (vm.OrderDto == null) return RedirectToAction(nameof(Index));
            
            if (vm.OrderDto.OrderId == null)
            {
                _orderService.Create(vm.OrderDto);
            }
            else
            {
                _orderService.Update(vm.OrderDto);
            }

            return RedirectToAction(nameof(Index));
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
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
