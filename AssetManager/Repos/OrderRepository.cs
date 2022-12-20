using AssetManager.Data;
using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Repos;

public class OrderRepository : IOrderRepository
{
    private AssetManagerContext _context;

    public OrderRepository(AssetManagerContext context)
    {
        _context = context;
    }

    public List<OrderDisplayDto> GetAllOrders()
    {
        return _context.Orders.Include(o => o.Approver)
                              .Include(o => o.Purchaser)
                              .Select(o => new OrderDisplayDto() 
                              {
                                OrderId = o.OrderId,
                                ExternalOrderId = o.ExternalOrderId,
                                Vendor = o.Vendor.DisplayName,
                                Products = o.Products,
                                Cost = o.Cost,
                                PurchaserId = o.PurchaserId,
                                PurchaserName = o.Purchaser != null ? $"{o.Purchaser.FirstName} {o.Purchaser.LastName}" : "",
                                ApproverId = o.ApproverId,
                                ApproverName = o.Approver  != null ? $"{o.Approver.FirstName} {o.Approver.LastName}" : "",
                                ApprovedDttm = o.ApprovedDttm,
                                ReceivedDttm = o.ReceivedDttm
                              }).AsNoTracking().ToList();
    }

    public OrderGridViewModel GetPageOfOrders(int pageSize, int pageNumber)
    {
        OrderGridViewModel vm = new();

        vm.CurrentPage = pageNumber;

        int orderCount = _context.Orders.Count();
        vm.PageCount = (orderCount + pageSize - 1 )/ pageSize;

        var query = _context.Orders.Include(o => o.Approver)
                      .Include(o => o.Purchaser)
                      .Include(o => o.Products)
                        .ThenInclude(p => p.Product)
                      .Select(o => new OrderDisplayDto()
                      {
                          OrderId = o.OrderId,
                          ExternalOrderId = o.ExternalOrderId,
                          Vendor = o.Vendor.DisplayName,
                          Products = o.Products,
                          Cost = o.Cost,
                          PurchaserId = o.PurchaserId,
                          PurchaserName = o.Purchaser != null ? $"{o.Purchaser.FirstName} {o.Purchaser.LastName}" : "",
                          ApproverId = o.ApproverId,
                          ApproverName = o.Approver != null ? $"{o.Approver.FirstName} {o.Approver.LastName}" : "",
                          ApprovedDttm = o.ApprovedDttm,
                          ReceivedDttm = o.ReceivedDttm
                      }).AsNoTracking();

        vm.Orders = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return vm;
    }

    public OrderAddEditDto? GetOrderById(int id)
    {
        OrderAddEditDto? dto = _context.Orders.Include(o => o.Products).Include(o => o.Purchaser).Include(o => o.Approver).Select(o => new OrderAddEditDto()
                                                    {OrderId = o.OrderId,
                                                     ExternalOrderId = o.ExternalOrderId,
                                                     VendorId = o.VendorId,
                                                     Products = o.Products,                                           
                                                     Cost = o.Cost,
                                                     PurchaserId = o.PurchaserId,
                                                     ApproverId = o.ApproverId,
                                                     ApprovedDttm = o.ApprovedDttm,
                                                     ReceivedDttm= o.ReceivedDttm
                                                    }).AsNoTracking().FirstOrDefault(o => o.OrderId == id);

        return dto;
    }

    public OrderDisplayDto? GetOrderDisplayDtoById(int id)
    { 
        OrderDisplayDto? dto = _context.Orders.Include(o => o.Products).ThenInclude(p => p.Product).Include(o => o.Purchaser).Include(o => o.Approver).Select(o => new OrderDisplayDto()
        {
            OrderId = o.OrderId,
            ExternalOrderId = o.ExternalOrderId,
            Vendor = o.Vendor.DisplayName,
            Products = o.Products,
            Cost = o.Cost,
            PurchaserId = o.PurchaserId,
            PurchaserName = o.Purchaser != null ? $"{o.Purchaser.FirstName} {o.Purchaser.LastName}" : "",
            ApproverId = o.ApproverId,
            ApproverName = o.Approver != null ? $"{o.Approver.FirstName} {o.Approver.LastName}" : "",
            ApprovedDttm = o.ApprovedDttm,
            ReceivedDttm = o.ReceivedDttm
        }).AsNoTracking().FirstOrDefault(o => o.OrderId == id);

        return dto;
    }

    public int Create(OrderAddEditDto submission)
    {
        Order order = new()
        {
            ExternalOrderId = submission.ExternalOrderId,
            VendorId = submission.VendorId,
            Products = submission.Products == null ? new List<ProductOrder>() : submission.Products,
            Cost = submission.Cost == null ? 0 : (decimal)submission.Cost,
            PurchaserId = submission.PurchaserId == null ? 0 : (int)submission.PurchaserId,
            ApproverId = submission.ApproverId == null ? 0 : (int)submission.ApproverId,
            ApprovedDttm = submission.ApprovedDttm,
            ReceivedDttm = submission.ReceivedDttm
        };
        
        _context.Orders.Add(order);
        Save();

        return order.OrderId;
    }

    public void Update(OrderAddEditDto submission)
    {
        if (submission?.OrderId != null)
        {
            Order? order = _context.Orders.FirstOrDefault(o => o.OrderId == submission.OrderId);

            if (order != null)
            {
                order.ExternalOrderId = submission.ExternalOrderId;
                order.VendorId = submission.VendorId;
                order.Products = submission.Products == null ? new List<ProductOrder>() : submission.Products;
                order.Cost = submission.Cost == null ? 0 : (decimal)submission.Cost;
                order.PurchaserId = submission.PurchaserId == null ? 0 : (int)submission.PurchaserId;
                order.ApproverId = submission.ApproverId == null   ? 0 : (int)submission.ApproverId;
                order.ApprovedDttm = submission.ApprovedDttm;
                order.ReceivedDttm = submission.ReceivedDttm;

                Save();
            }
        }
    }

    public void Delete(int id)
    {
        if (id > 0)
        {
            Order? order = _context.Orders.Find(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                Save();
            }
        }
    }

    public void ReceiveOrder(int id)
    {
        var order = _context.Orders.Find(id);

        if (order != null)
        {
            order.ReceivedDttm = DateTime.Now;
            Save();
        }
    }

    private void Save()
    {
        _context.SaveChanges();
    }
}     
