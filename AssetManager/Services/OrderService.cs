using AssetManager.Models;
using AssetManager.Repos;
using AssetManager.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Services;

public class OrderService : IOrderService
{
    private IOrderRepository _orderRepository;
    private IAssetRepository _assetRepository;
    private IDictRepository _dictRepository;

    public OrderService(IOrderRepository orderRepository, IAssetRepository assetRepository, IDictRepository dictRepository)
    {
        _orderRepository = orderRepository;
        _assetRepository = assetRepository;
        _dictRepository = dictRepository;
    }

    public List<Order> GetAllOrders()
    {
        return _orderRepository.GetAllOrders();
    }

    public OrderGridViewModel GetPageOfOrders(int pageNumber)
    {
        int pageSize = 15;

        return _orderRepository.GetPageOfOrders(pageSize,pageNumber);
    }

    public Order GetOrderById(int id)
    {
        var order = _orderRepository.GetOrderById(id);

        if (order == null)
        {
            throw new InvalidOperationException();
        }

        return order;
    }

    public int Create(Order submission)
    {
        List<ProductOrder> products = new ();
        
        foreach (var item in submission.Products)
        {
            if (item?.Count != null && item.Count > 0 && item.ProductId != 0)
            {
                products.Add(item);
            }
        }

        submission.Products = products;
        
        return _orderRepository.Create(submission);
    }

    public void Update(Order updatedOrder)
    {
        List<ProductOrder> products = new();

        foreach (var item in updatedOrder.Products)
        {
            if (item != null && item.Count != -1)
            {
                products.Add(item);
            }
        }

        updatedOrder.Products = products;

        _orderRepository.Update(updatedOrder);
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
                List<Asset> assets = new();

                for (var i = 0; i < p.Count; i++)
                {
                    assets.Add(new Asset()
                    {
                        // disgusting--will fix after implementing dict cacheing. 
                        AssetTypeId = p.ProductId switch
                        {
                            201 => 201,
                            202 => 201,
                            203 => 201,
                            204 => 201,
                            205 => 201,
                            301 => 201,
                            302 => 201,
                            303 => 201,
                            304 => 201,
                            305 => 201,
                            306 => 201,
                            307 => 201,
                            308 => 201,
                            309 => 201,
                            401 => 201,
                            402 => 201,
                            403 => 201,
                            404 => 201,
                            501 => 201,
                            502 => 201,
                            503 => 201,
                            504 => 201,
                            505 => 201,
                            _ => 201
                        },
                        ModelId = p.ProductId,
                        SiteId = 401,
                        PersonId = null
                    });
                }

                _assetRepository.CreateAssets(assets);
            }

            _orderRepository.ReceiveOrder(id);
        }
    }
}
