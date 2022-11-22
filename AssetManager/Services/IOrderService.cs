using AssetManager.DTOs;

namespace AssetManager.Services
{
    public interface IOrderService
    {
        List<OrderDisplayDto> GetAllOrders();
        void Create(OrderAddEditDto dto);
        void Delete(int id);
        OrderAddEditDto? GetOrderById(int id);
        void ReceiveOrder(int id);
        void Update(OrderAddEditDto dto);
    }
}