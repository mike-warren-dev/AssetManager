using AssetManager.Data;
using AssetManager.DTOs;
using Microsoft.AspNetCore.Mvc;

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
}
