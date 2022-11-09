using AssetManager.DTOs;

namespace AssetManager.Repos
{
    public interface IOrderRepository
    {
        IEnumerable<OrderDisplayDto> GetAllOrders();
        OrderAddEditDto? GetOrderById(int id);
        void Create(OrderAddEditDto submission);
        void Update(OrderAddEditDto submission);
    }
}