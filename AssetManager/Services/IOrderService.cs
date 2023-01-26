using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Services;

public interface IOrderService
{
    List<Order> GetAllOrders();
    int Create(Order submission);
    void Delete(int id);
    Order? GetOrderById(int id);
    void ReceiveOrder(int id);
    void Update(Order updatedOrder);
    OrderGridViewModel GetPageOfOrders(int pageNumber);
}