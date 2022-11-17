using AssetManager.Data;
using AssetManager.DTOs;
using AssetManager.Enums;
using AssetManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NuGet.ContentModel;
using Asset = AssetManager.Models.Asset;

namespace AssetManager.Repos;

public class OrderRepository : IOrderRepository
{
    private AssetManagerContext _context;

    public OrderRepository(AssetManagerContext context)
    {
        _context = context;
    }

    public List<OrderDisplayDto> GetAllOrders()
    {
        return _context.Orders.Select(o => new OrderDisplayDto() {
                                                OrderId = o.OrderId,
                                                ExternalOrderId = o.ExternalOrderId,
                                                VendorId = o.VendorId,
                                                Products = o.Products,
                                                Cost = o.Cost,
                                                PurchaserId = o.PurchaserId,
                                                ApproverId = o.ApproverId,
                                                ApprovedDttm = o.ApprovedDttm,
                                                ReceivedDttm = o.ReceivedDttm}).AsNoTracking().ToList();
    }

    public OrderAddEditDto? GetOrderById(int id)
    {
        OrderAddEditDto? dto = _context.Orders.Include(o => o.Products).Include(o => o.Purchaser).Include(o => o.Approver).Select(o => new OrderAddEditDto()
                                                    {OrderId = o.OrderId,
                                                     ExternalOrderId = o.ExternalOrderId,
                                                     VendorId = o.VendorId,
                                                     Products = o.Products,                                           
                                                     Cost = o.Cost,
                                                     PurchaserId = o.PurchaserId,
                                                     ApproverId = o.ApproverId,
                                                     ApprovedDttm = o.ApprovedDttm,
                                                     ReceivedDttm= o.ReceivedDttm
                                                    }).AsNoTracking().FirstOrDefault(o => o.OrderId == id);

        return dto;
    }

    public void Create(OrderAddEditDto submission)
    {
        Order order = new()
        {
            ExternalOrderId = submission.ExternalOrderId,
            VendorId = submission.VendorId,
            Products = submission.Products == null ? new List<ProductOrder>() : submission.Products,
            Cost = submission.Cost == null ? 0 : (decimal)submission.Cost,
            PurchaserId = submission.PurchaserId == null ? 0 : (int)submission.PurchaserId,
            ApproverId = submission.ApproverId == null ? 0 : (int)submission.ApproverId,
            ApprovedDttm = submission.ApprovedDttm,
            ReceivedDttm = submission.ReceivedDttm
        };
        
        _context.Orders.Add(order);
        Save();
    }

    public void Update(OrderAddEditDto submission)
    {
        if (submission?.OrderId != null)
        {
            Order? order = _context.Orders.FirstOrDefault(o => o.OrderId == submission.OrderId);

            if (order != null)
            {
                order.ExternalOrderId = submission.ExternalOrderId;
                order.VendorId = submission.VendorId;
                order.Products = submission.Products == null ? new List<ProductOrder>() : submission.Products;
                order.Cost = submission.Cost == null ? 0 : (decimal)submission.Cost;
                order.PurchaserId = submission.PurchaserId == null ? 0 : (int)submission.PurchaserId;
                order.ApproverId = submission.ApproverId == null   ? 0 : (int)submission.ApproverId;
                order.ApprovedDttm = submission.ApprovedDttm;
                order.ReceivedDttm = submission.ReceivedDttm;

                Save();
            }
        }
    }

    public void Delete(int id)
    {
        if (id > 0)
        {
            Order? order = _context.Orders.Find(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                Save();
            }
        }
    }

    public void ReceiveOrder(int id)
    {
        var order = _context.Orders.Find(id);

        if (order != null)
        {
            order.ReceivedDttm = DateTime.Now;
            Save();
        }
        

        //var order = _context.Orders.Find(id);

        //if (order != null)
        //{
        //    //var n = _context.Orders.IndexOf(order);
        //    //var nextIndex = _context.Assets.Max(a => a.AssetId) + 1;

        //    order.ReceivedDttm = DateTime.Now;

        //    //should implement a service layer to avoid having to implement the Assets stuff in the order repo or rely on other repo...
        //    foreach (var p in order.Products)
        //    {
        //        List<Asset> assets = new();

        //        for (var i = 0; i < p.Count; i++)
        //        {
        //            assets.Add( new Asset()
        //            {
        //                AssetId = nextIndex,
        //                AssetType = p.Product switch
        //                {
        //                    Enums.Product.MacbookPro13Inch => Enums.AssetType.Laptop,
        //                    Enums.Product.MacbookPro15Inch => Enums.AssetType.Laptop,
        //                    Enums.Product.DellInspiron14 => Enums.AssetType.Laptop,
        //                    Enums.Product.ThinkpadE14Gen3 => Enums.AssetType.Laptop,
        //                    Enums.Product.ThinkpadE14Gen4 => Enums.AssetType.Laptop,
        //                    Enums.Product.ThinkpadX1Gen8 => Enums.AssetType.Laptop,
        //                    Enums.Product.ThinkpadX1Gen9 => Enums.AssetType.Laptop,
        //                    _ => Enums.AssetType.Laptop
        //                },
        //                Model = p.Product,
        //                Site = Enums.Site.TheWoodlands,
        //                PersonId = null
        //            });

        //            nextIndex++;
        //        }

        //        _context.Assets.AddRange(assets);
        //    }

        //    _context.Orders[n] = order;
        //}
    }

    private void Save()
    {
        _context.SaveChanges();
    }
}     
