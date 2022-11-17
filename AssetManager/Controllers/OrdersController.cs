using AssetManager.DTOs;
using AssetManager.Repos;
using AssetManager.Models;
using Microsoft.AspNetCore.Mvc;
using AssetManager.Services;

namespace AssetManager.Controllers
{
    public class OrdersController : Controller
    {
        private IOrderService _orderService;

        public OrdersController(IOrderRepository repository, IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: Orders
        public ActionResult Index()
        {
            var list = _orderService.GetAllOrders();
            
            return View(list);
        }

        // GET: Orders/AddEdit/5
        public ActionResult AddEdit(int id)
        {
            
            
            if (id == 0)
            {
                OrderAddEditDto emptyDto = new();
                emptyDto.Products.Add(new ProductOrder());

                return PartialView("_AddEditOrderModal", emptyDto);
            }
            else
            {
                OrderAddEditDto? dto = _orderService.GetOrderById(id);

                if (dto != null)
                {
                    if (!dto.Products.Any())
                        dto.Products.Add(new ProductOrder());

                    return PartialView("_AddEditOrderModal", dto);
                }
                else
                {
                    return PartialView("_AddEditOrderModal");
                }
                
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(OrderAddEditDto submission)
        {
            if (submission != null && submission.OrderId == null)
            {
                _orderService.Create(submission);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                _orderService.Update(submission);

                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orders/Edit/5
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

        // DELETE: Orders/Delete/{id}
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            _orderService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        // POST: Orders/Receive/{id}
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
