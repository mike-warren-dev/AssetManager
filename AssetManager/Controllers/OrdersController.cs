﻿using AssetManager.Repos;
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
                var order = await _orderService.GetOrderById(id);

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
        public async Task<ActionResult> AddEdit(OrderAddEditViewModel vm)
        {
            if (vm.Order == null) return RedirectToAction(nameof(Index));

            int orderId;

            try
            {
                if (vm.Order.OrderId == 0)
                {
                    orderId = await _orderService.Create(vm.Order);
                }
                else
                {
                    await _orderService.Update(vm.Order);

                    orderId = vm.Order.OrderId;
                }

                return PartialView("_RowPartial", await _orderService.GetOrderById(orderId));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await _orderService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> ReceiveOrder(int id)
        {
            if (id > 0)
            {
                await _orderService.ReceiveOrder(id);
                
                return RedirectToAction(nameof(Index));
            }
            
            return RedirectToAction(nameof(Index));
            
        }
    }
}
