using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Repos
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int orderId);
        Task<Order> Create(Order submission);
        Task Update(Order submission);
        Task Delete(int orderId);
        Task ReceiveOrder(int orderId);
        Task<OrderGridViewModel> GetPageOfOrders(int pageSize, int pageNumber);
    }
}