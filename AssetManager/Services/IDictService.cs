using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssetManager.Services
{
    public interface IDictService
    {
        List<SelectListItem> GetDictItems(int dictId);
    }
}