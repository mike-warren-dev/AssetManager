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

    public List<Order> GetAllOrders()
    {
        return _context.Orders.Include(o => o.Approver)
                              .Include(o => o.Purchaser).ToList();
    }

    public OrderGridViewModel GetPageOfOrders(int pageSize, int pageNumber)
    {
        OrderGridViewModel vm = new();

        vm.CurrentPage = pageNumber;

        int orderCount = _context.Orders.Count();
        vm.PageCount = (orderCount + pageSize - 1 )/ pageSize;

        var query = _context.Orders.Include(o => o.Approver)
                                   .Include(o => o.Purchaser)
                                   .Include(o => o.Vendor)
                                   .Include(o => o.Products)
                                    .ThenInclude(p => p.Product).AsNoTracking();

        vm.Orders = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return vm;
    }

    public Order GetOrderById(int id)
    {
        var order = _context.Orders.Include(o => o.Products).Include(o => o.Purchaser).Include(o => o.Approver).FirstOrDefault(o => o.OrderId == id);

        if (order == null)
        {
            return new Order();
        }
        else
        {
            return order;
        }
    }

    public Order GetOrderDisplayDtoById(int id)
    { 
        var order = _context.Orders.Include(o => o.Products)
                                    .ThenInclude(p => p.Product)
                                   .Include(o => o.Purchaser)
                                   .Include(o => o.Approver).FirstOrDefault(o => o.OrderId == id);

        if (order == null)
        {
            return new Order();
        }
        else
        {
            return order;
        }
    }

    public int Create(Order submission)
    {
        //Order order = new()
        //{
        //    ExternalOrderId = submission.ExternalOrderId,
        //    VendorId = submission.VendorId,
        //    Products = submission.Products == null ? new List<ProductOrder>() : submission.Products,
        //    //Cost = submission.Cost == null ? 0 : (decimal)submission.Cost,
        //    //PurchaserId = submission.PurchaserId == null ? 0 : (int)submission.PurchaserId,
        //    //ApproverId = submission.ApproverId == null ? 0 : (int)submission.ApproverId,
        //    Cost = (decimal)submission.Cost,
        //    PurchaserId = (int)submission.PurchaserId,
        //    ApproverId = (int)submission.ApproverId,
        //    ApprovedDttm = submission.ApprovedDttm,
        //    ReceivedDttm = submission.ReceivedDttm
        //};
        
        //do some checking, dude!

        _context.Orders.Add(submission);
        Save();

        return submission.OrderId;
    }

    public void Update(Order submission)
    {
        if (submission != null && submission.OrderId != 0)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == submission.OrderId);

            if (order != null)
            {
                order.ExternalOrderId = submission.ExternalOrderId;
                order.VendorId = submission.VendorId;
                order.Products = submission.Products == null ? new List<ProductOrder>() : submission.Products;
                //order.Cost = submission.Cost == null ? 0 : (decimal)submission.Cost;
                //order.PurchaserId = submission.PurchaserId == null ? 0 : (int)submission.PurchaserId;
                //order.ApproverId = submission.ApproverId == null   ? 0 : (int)submission.ApproverId;
                order.Cost = (decimal)submission.Cost;
                order.PurchaserId = (int)submission.PurchaserId;
                order.ApproverId = (int)submission.ApproverId;
                order.ApprovedDttm = submission.ApprovedDttm;
                order.ReceivedDttm = submission.ReceivedDttm;

                Save();
            }
        }
    }

    public void Delete(int id)
    {
        if (id == 0) return;
        
        var order = _context.Orders.Find(id);

        if (order != null)
        {
            _context.Orders.Remove(order);
            Save();
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
