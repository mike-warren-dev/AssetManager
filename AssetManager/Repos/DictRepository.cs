using AssetManager.Data;
using AssetManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AssetManager.Repos;

public class DictRepository : IDictRepository
{
    private AssetManagerContext _context;
    private IMemoryCache _cache;

    public DictRepository(AssetManagerContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public List<SelectListItem> GetDictItems(int dictId)
    {
        if (!_cache.TryGetValue(dictId, out List<SelectListItem>? cacheValue))
        {
            Dict? dict = _context.Dicts.Include(d => d.DictOptions).FirstOrDefault(d => d.DictId == dictId);
            List<SelectListItem> resultFromDatabase = new();

            if (dict != null)
            {
                var group = new SelectListGroup() { Disabled = false, Name = dict.DisplayName };

                foreach (var item in dict.DictOptions)
                {
                    resultFromDatabase.Add(new SelectListItem()
                    {
                        Disabled = false,
                        Group = group,
                        Selected = false,
                        Text = item.DisplayName,
                        Value = item.DictOptionId.ToString()
                    });
                }
            }

            return resultFromDatabase;
        }
        else
        {
            return cacheValue ?? new List<SelectListItem>();
        }

        //Dict? dict = _context.Dicts.Include(d => d.DictOptions).FirstOrDefault(d => d.DictId == dictId);
        //List<SelectListItem> result = new();

        //if (dict != null)
        //{
        //    var group = new SelectListGroup() { Disabled = false, Name = dict.DisplayName };

        //    foreach (var item in dict.DictOptions)
        //    {
        //        result.Add(new SelectListItem()
        //        {
        //            Disabled = false,
        //            Group = group,
        //            Selected = false,
        //            Text = item.DisplayName,
        //            Value = item.DictOptionId.ToString()
        //        });
        //    }
        //}

        //return result;
    }

    public int? GetDictIdFromDictOptionId(int dictOptionId)
    {
        return _context.DictOptions.Where(o => o.DictOptionId == dictOptionId).Select(o => o.DictId).FirstOrDefault();
    }
}
