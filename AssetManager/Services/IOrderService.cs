using AssetManager.DTOs;
using AssetManager.ViewModels;

namespace AssetManager.Services
{
    public interface IOrderService
    {
        List<OrderDisplayDto> GetAllOrders();
        int Create(OrderAddEditDto dto);
        void Delete(int id);
        OrderAddEditDto? GetOrderById(int id);
        void ReceiveOrder(int id);
        void Update(OrderAddEditDto dto);
        OrderDisplayDto? GetOrderDisplayDtoById(int id);
        OrderGridViewModel GetPageOfOrders(int pageNumber);
    }
}