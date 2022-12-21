using AssetManager.Data;
using AssetManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;

namespace AssetManager.BackgroundServices;

public class CacheService : BackgroundService
{
    private readonly IMemoryCache _cache;

    private readonly int _refreshIntervalMinutes;
    private List<Dict> _dicts;

    public IServiceProvider Services { get; }

    public CacheService(IMemoryCache cache, IServiceProvider services)
    {
        Services = services;
        _cache = cache;
        _refreshIntervalMinutes = 5;
        _dicts = new List<Dict>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var _getCacheDataTimer = new Timer(UpdateCache, null, TimeSpan.Zero, TimeSpan.FromMinutes(_refreshIntervalMinutes));        
    }

    private void UpdateCache(object? state)
    {
        using (var scope = Services.CreateScope())
        {
            var _context = scope.ServiceProvider.GetService<AssetManagerContext>();

            if (_context == null) return;

            var dicts = _context.Dicts.Include(d => d.DictOptions).ToList();

            foreach (var dict in dicts)
            {
                List<SelectListItem> dictOptionList = new();

                if (dict == null) return;

                var group = new SelectListGroup() { Disabled = false, Name = dict.DisplayName };

                foreach (var item in dict.DictOptions)
                {
                    dictOptionList.Add(new SelectListItem()
                    {
                        Disabled = false,
                        Group = group,
                        Selected = false,
                        Text = item.DisplayName,
                        Value = item.DictOptionId.ToString()
                    });
                }

                _cache.Set(dict.DictId, dictOptionList);
            }
        }
    }
}
