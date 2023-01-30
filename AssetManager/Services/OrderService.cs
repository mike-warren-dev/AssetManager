using AssetManager.Models;
using AssetManager.Repos;
using AssetManager.ViewModels;

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

    public async Task<List<Order>> GetAllOrders()
    {
        return await _orderRepository.GetAllOrders();
    }

    public async Task<OrderGridViewModel> GetPageOfOrders(int pageNumber)
    {
        int pageSize = 15;

        return await _orderRepository.GetPageOfOrders(pageSize,pageNumber);
    }

    public async Task<Order> GetOrderById(int orderId)
    {
        var order = await _orderRepository.GetOrderById(orderId);

        return order;
    }

    public async Task<int> Create(Order submission)
    {
        var products = new List<ProductOrder>();
        
        foreach (var item in submission.Products)
        {
            if (item?.Count != null && item.Count > 0 && item.ProductId != 0)
            {
                products.Add(item);
            }
        }

        submission.Products = products;
        
        return await _orderRepository.Create(submission);
    }

    public async Task Update(Order updatedOrder)
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

        await _orderRepository.Update(updatedOrder);
    }

    public async Task Delete(int orderId)
    {
        await _orderRepository.Delete(orderId);
    }

    public async Task ReceiveOrder(int orderId)
    {
        var order = await GetOrderById(orderId);

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

            await _orderRepository.ReceiveOrder(orderId);
        }
    }
}
