using AssetManager.Data;
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
        var order = _context.Orders.Include(o => o.Products)
                                   .Include(o => o.Purchaser)
                                   .Include(o => o.Approver)
                                   .FirstOrDefault(o => o.OrderId == id);

        if (order == null)
            return new Order();
        
        return order;
    }

    public int Create(Order submission)
    {
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
                order.Cost = submission.Cost;
                order.PurchaserId = submission.PurchaserId;
                order.ApproverId = submission.ApproverId;
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
        if (id == 0) return;

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
