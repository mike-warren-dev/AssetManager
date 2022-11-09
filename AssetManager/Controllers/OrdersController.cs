﻿using AssetManager.DTOs;
using AssetManager.Repos;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers
{
    public class OrdersController : Controller
    {
        private IOrderRepository _repository;

        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        // GET: Orders
        public ActionResult Index()
        {
            var list = _repository.GetAllOrders().ToList();
            
            return View(list);
        }

        // GET: Orders/AddEdit/5
        public ActionResult AddEdit(int id)
        {
            if (id == 0)
            {
                return PartialView("_AddEditOrderModal");
            }
            else
            {
                OrderAddEditDto? dto = _repository.GetOrderById(id);

                if (dto != null)
                {
                    return PartialView("_AddEditOrderModal", dto);
                }
                else
                {
                    return PartialView("_AddEditOrderModal");
                }
                
            }
            
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(OrderAddEditDto submission)
        {
            if (submission != null && submission.OrderId == null)
            {
                _repository.Create(submission);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                _repository.Update(submission);

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

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Orders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}