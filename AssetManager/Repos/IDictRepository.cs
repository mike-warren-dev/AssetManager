using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssetManager.Repos
{
    public interface IDictRepository
    {
        int? GetDictIdFromDictOptionId(int dictOptionId);
        List<SelectListItem> GetDictItems(int dictId);
    }
}