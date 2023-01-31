using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Services;

public interface IOrderService
{
    Task<List<Order>> GetAllOrders();
    Task<Order> Create(Order submission);
    Task Delete(int orderId);
    Task<Order> GetOrderById(int orderId);
    Task ReceiveOrder(int orderId);
    Task Update(Order updatedOrder);
    Task<OrderGridViewModel> GetPageOfOrders(int pageNumber);
}