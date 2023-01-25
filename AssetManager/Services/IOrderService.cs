using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Services
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        int Create(Order dto);
        void Delete(int id);
        Order? GetOrderById(int id);
        void ReceiveOrder(int id);
        void Update(Order dto);
        Order? GetOrderDisplayDtoById(int id);
        OrderGridViewModel GetPageOfOrders(int pageNumber);
    }
}