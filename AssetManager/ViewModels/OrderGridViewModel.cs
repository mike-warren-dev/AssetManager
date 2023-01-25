using AssetManager.Models;

namespace AssetManager.ViewModels;

public class OrderGridViewModel
{
    public OrderGridViewModel() 
    {
        Orders = new();
    }

    public List<Order> Orders { get; set; }
    public int PageCount { get; set; }
    public int CurrentPage { get; set; }

}
