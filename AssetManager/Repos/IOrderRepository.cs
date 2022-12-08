using AssetManager.DTOs;

namespace AssetManager.Repos
{
    public interface IOrderRepository
    {
        List<OrderDisplayDto> GetAllOrders();
        OrderAddEditDto? GetOrderById(int id);
        int Create(OrderAddEditDto submission);
        void Update(OrderAddEditDto submission);
        void Delete(int id);
        void ReceiveOrder(int id);
        OrderDisplayDto? GetOrderDisplayDtoById(int id);
    }
}