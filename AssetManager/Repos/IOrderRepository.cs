using AssetManager.DTOs;

namespace AssetManager.Repos
{
    public interface IOrderRepository
    {
        IEnumerable<OrderDisplayDto> GetAllOrders();
    }
}