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

    public async Task<List<Order>> GetAllOrders()
    {
        return await _context.Orders.Include(o => o.Approver)
                                    .Include(o => o.Purchaser).ToListAsync();
    }

    public async Task<OrderGridViewModel> GetPageOfOrders(int pageSize, int pageNumber)
    {
        OrderGridViewModel vm = new();

        vm.CurrentPage = pageNumber;

        int orderCount = await _context.Orders.CountAsync();
        vm.PageCount = (orderCount + pageSize - 1 )/ pageSize;

        vm.Orders = await _context.Orders.Include(o => o.Approver)
                                         .Include(o => o.Purchaser)
                                         .Include(o => o.Vendor)
                                         .Include(o => o.Products).ThenInclude(p => p.Product)
                                         //.AsNoTracking();
                                         .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return vm;
    }

    public async Task<Order> GetOrderById(int orderId)
    {
        var order = await _context.Orders.Include(o => o.Approver)
                                         .Include(o => o.Purchaser)
                                         .Include(o => o.Vendor)
                                         .Include(o => o.Products).ThenInclude(p => p.Product)
                                         .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
            return new Order();
        
        return order;
    }

    public async Task<int> Create(Order submission)
    {
        _context.Orders.Add(submission);

        await _context.SaveChangesAsync();

        return submission.OrderId;
    }

    public async Task Update(Order submission)
    {
        if (submission != null && submission.OrderId != 0)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == submission.OrderId);

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

                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task Delete(int orderId)
    {
        if (orderId == 0) return;
        
        var order = await _context.Orders.FindAsync(orderId);

        if (order != null)
        {
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
        }
    }

    public async Task ReceiveOrder(int id)
    {
        if (id == 0) return;

        var order = await _context.Orders.FindAsync(id);

        if (order != null)
        {
            order.ReceivedDttm = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}     
