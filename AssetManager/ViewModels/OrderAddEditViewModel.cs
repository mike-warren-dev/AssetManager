using AssetManager.DTOs;
using AssetManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssetManager.ViewModels;

public class OrderAddEditViewModel
{
    public OrderAddEditViewModel()
    {
        OrderDto = new();
    }

    public OrderAddEditDto OrderDto { get; set; }

    public IEnumerable<Person> People { get; set; } = null!;
    public IEnumerable<SelectListItem> VendorOptions { get; set; } = null!;
    public IEnumerable<SelectListItem> ProductOptions { get; set; } = null!;
}
