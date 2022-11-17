using AssetManager.DTOs;
using AssetManager.Repos;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Services;

public class OrderService : IOrderService
{
    private IOrderRepository _orderRepository;
    private IAssetRepository _assetRepository;

    public OrderService (IOrderRepository orderRepository, IAssetRepository assetRepository)
    {
        _orderRepository = orderRepository;
        _assetRepository = assetRepository;
    }

    public List<OrderDisplayDto> GetAllOrders()
    {
        return _orderRepository.GetAllOrders();
    }

    public OrderAddEditDto? GetOrderById(int id)
    {
        return _orderRepository.GetOrderById(id);
    }

    public void Create(OrderAddEditDto submission)
    {
        _orderRepository.Create(submission);
    }

    public void Update(OrderAddEditDto submission)
    {
        _orderRepository.Update(submission);
    }

    public void Delete(int id)
    {
        _orderRepository.Delete(id);
    }

    public void ReceiveOrder(int id)
    {
        var order = GetOrderById(id);

        if (order != null)
        {

            foreach (var p in order.Products)
            {
                List<AssetAddEditDto> assets = new();

                for (var i = 0; i < p.Count; i++)
                {
                    assets.Add(new AssetAddEditDto()
                    {
                        AssetType = p.Product switch
                        {
                            Enums.Product.MacbookPro13Inch => Enums.AssetType.Laptop,
                            Enums.Product.MacbookPro15Inch => Enums.AssetType.Laptop,
                            Enums.Product.DellInspiron14 => Enums.AssetType.Laptop,
                            Enums.Product.ThinkpadE14Gen3 => Enums.AssetType.Laptop,
                            Enums.Product.ThinkpadE14Gen4 => Enums.AssetType.Laptop,
                            Enums.Product.ThinkpadX1Gen8 => Enums.AssetType.Laptop,
                            Enums.Product.ThinkpadX1Gen9 => Enums.AssetType.Laptop,
                            _ => Enums.AssetType.Laptop
                        },
                        Model = p.Product,
                        Site = Enums.Site.TheWoodlands,
                        PersonId = null
                    });
                }

                _assetRepository.CreateAssets(assets);
            }

            _orderRepository.ReceiveOrder(id);
        }
    }
}
