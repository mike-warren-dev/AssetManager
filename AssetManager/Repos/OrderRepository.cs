using AssetManager.Data;
using AssetManager.DTOs;
using AssetManager.Enums;
using AssetManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace AssetManager.Repos;

public class OrderRepository : IOrderRepository
{
    private IDataStore _context;
    public OrderRepository(IDataStore context)
    {
        _context = context;
    }

    public IEnumerable<OrderDisplayDto> GetAllOrders()
    {
        var list = _context.Orders.Select(o => new OrderDisplayDto()
                                               {OrderId = o.OrderId,
                                                ExternalOrderId = o.ExternalOrderId,
                                                VendorId = o.VendorId,
                                                Products = o.Products,
                                                Cost = o.Cost,
                                                PurchaserId = o.PurchaserId,
                                                ApproverId = o.ApproverId,                                    
                                                ApprovedDttm = o.ApprovedDttm});

        return list;
    }

    public OrderAddEditDto? GetOrderById(int id)
    {
        OrderAddEditDto? dto = _context.Orders.Select(o => new OrderAddEditDto()
                                                    {OrderId = o.OrderId,
                                                     ExternalOrderId = o.ExternalOrderId,
                                                     VendorId = o.VendorId,
                                                     Products = o.Products,                                           
                                                     Cost = o.Cost,
                                                     PurchaserId = o.PurchaserId,
                                                     ApproverId = o.ApproverId,
                                                     ApprovedDttm = o.ApprovedDttm
                                                    }).FirstOrDefault(o => o.OrderId == id);

        return dto;
    }

    public void Create(OrderAddEditDto submission)
    {
        Order order = new()
        {
            OrderId = submission.OrderId == null ? _context.Orders.Max(o => o.OrderId) : (int)submission.OrderId,
            ExternalOrderId = submission.ExternalOrderId,
            VendorId = submission.VendorId,
            Products = submission.Products == null ? new List<Product>() : submission.Products,
            Cost = submission.Cost,
            PurchaserId = submission.PurchaserId,
            ApproverId = submission.ApproverId,
            ApprovedDttm = submission.ApprovedDttm
        };
        
        _context.Orders.Add(order);
    }

    public void Update(OrderAddEditDto submission)
    {
        if (submission?.OrderId != null)
        {
            Order? order = _context.Orders.FirstOrDefault(o => o.OrderId == submission.OrderId);

            if (order != null)
            {
                var n = _context.Orders.IndexOf(order);

                order.ExternalOrderId = submission.ExternalOrderId;
                order.VendorId = submission.VendorId;
                order.Products = submission.Products == null ? new List<Product>() : submission.Products;
                order.Cost = submission.Cost;
                order.PurchaserId = submission.PurchaserId;
                order.ApproverId = submission.ApproverId;
                order.ApprovedDttm = submission.ApprovedDttm;

                _context.Orders[n] = order;
            }
        }
    }
}
