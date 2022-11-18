using AssetManager.DTOs;

namespace AssetManager.ViewModels;

public class OrderAddEditViewModel
{
    public OrderAddEditViewModel()
    {
        OrderDto = new();
    }

    public OrderAddEditDto OrderDto { get; set; }

    public IEnumerable<PersonDisplayDto> People { get; set; } = null!;
}
