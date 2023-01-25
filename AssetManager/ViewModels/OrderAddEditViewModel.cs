using AssetManager.DTOs;
using AssetManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssetManager.ViewModels;

public class OrderAddEditViewModel
{
    public OrderAddEditViewModel()
    {
        Order = new();
    }

    public Order Order { get; set; }

    public IEnumerable<Person> People { get; set; } = null!;
    public IEnumerable<SelectListItem> VendorOptions { get; set; } = null!;
    public IEnumerable<SelectListItem> ProductOptions { get; set; } = null!;
}
