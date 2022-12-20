using AssetManager.Data;
using AssetManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Repos;

public class DictRepository : IDictRepository
{
    private AssetManagerContext _context;

    public DictRepository(AssetManagerContext context)
    {
        _context = context;
    }

    public List<SelectListItem> GetDictItems(int dictId)
    {
        Dict? dict = _context.Dicts.Include(d => d.DictOptions).FirstOrDefault(d => d.DictId == dictId);
        List<SelectListItem> result = new();

        if (dict != null)
        {
            var group = new SelectListGroup() { Disabled = false, Name = dict.DisplayName };

            foreach (var item in dict.DictOptions)
            {
                result.Add(new SelectListItem()
                {
                    Disabled = false,
                    Group = group,
                    Selected = false,
                    Text = item.DisplayName,
                    Value = item.DictOptionId.ToString()
                });
            }
        }

        return result;
    }

    public int? GetDictIdFromDictOptionId(int dictOptionId)
    {
        return _context.DictOptions.Where(o => o.DictOptionId == dictOptionId).Select(o => o.DictId).FirstOrDefault();
    }
}
