using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.Repos;
using AssetManager.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Services;

public class OrderService : IOrderService
{
    private IOrderRepository _orderRepository;
    private IAssetRepository _assetRepository;

    public OrderService(IOrderRepository orderRepository, IAssetRepository assetRepository)
    {
        _orderRepository = orderRepository;
        _assetRepository = assetRepository;
    }

    public List<OrderDisplayDto> GetAllOrders()
    {
        return _orderRepository.GetAllOrders();
    }

    public OrderGridViewModel GetPageOfOrders(int pageNumber)
    {
        int pageSize = 15;

        return _orderRepository.GetPageOfOrders(pageSize,pageNumber);
    }

    public OrderAddEditDto? GetOrderById(int id)
    {
        return _orderRepository.GetOrderById(id);
    }

    public OrderDisplayDto? GetOrderDisplayDtoById(int id)
    {
        return _orderRepository.GetOrderDisplayDtoById(id);
    }

    public int Create(OrderAddEditDto dto)
    {
        List<ProductOrder> products = new ();
        
        foreach (var item in dto.Products)
        {
            if (item?.Count != null && item.Count > 0 && item.Product != 0)
            {
                products.Add(item);
            }
        }

        dto.Products = products;
        
        return _orderRepository.Create(dto);
    }

    public void Update(OrderAddEditDto dto)
    {
        List<ProductOrder> products = new();

        foreach (var item in dto.Products)
        {
            if (item != null && item.Count != -1)
            {
                products.Add(item);
            }
        }

        dto.Products = products;

        _orderRepository.Update(dto);
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
