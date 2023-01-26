using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Repos
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        int Create(Order submission);
        void Update(Order submission);
        void Delete(int id);
        void ReceiveOrder(int id);
        OrderGridViewModel GetPageOfOrders(int pageSize, int pageNumber);
    }
}