using AssetManager.DTOs;

namespace AssetManager.ViewModels;

public class OrderGridViewModel
{
    public OrderGridViewModel() 
    {
        Orders = new();
    }

    public List<OrderDisplayDto> Orders { get; set; }
    public int PageCount { get; set; }
    public int CurrentPage { get; set; }

}
