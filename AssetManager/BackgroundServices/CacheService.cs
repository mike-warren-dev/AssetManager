using AssetManager.Data;
using AssetManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AssetManager.BackgroundServices;

//public class CacheService : BackgroundService
//{
//    private Task? _executeTask;
//    private CancellationTokenSource? _stoppingCts;
//    private AssetManagerContext _context;

//    private readonly ILogger<CacheService> _logger;
//    private readonly IMemoryCache _cache;

//    private readonly int _minutesToCache;
//    private readonly int _refreshIntervalInSeconds;
//    private List<Dict> _dicts;


//    public CacheService(ILogger<CacheService> logger, IMemoryCache cache, AssetManagerContext context)
//    {
//        _context = context;
//        _logger = logger;
//        _cache = cache;
//        _minutesToCache = 5;
//        _refreshIntervalInSeconds = (_minutesToCache * 60) - 10;
//        _dicts = new List<Dict>();
//    }

//    //public Task StartAsync(CancellationToken cancellationToken)
//    //{
//    //    // Create linked token to allow cancelling executing task from provided token
//    //    _stoppingCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

//    //    // get cache data from db
//    //    var _getCacheDataTimer = new Timer(GetDataToCache, null, TimeSpan.Zero, TimeSpan.FromSeconds(300));

//    //    // set cache variables
//    //    var _updateCacheTimer = new Timer(CacheData, null, new TimeSpan(0,0,5), TimeSpan.FromSeconds(300));


//    //    // Otherwise it's running
//    //    return Task.CompletedTask;
//    //}

//    //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    //{
//    //    // get cache data from db
//    //    var _getCacheDataTimer = new Timer(GetDataToCache, null, TimeSpan.Zero, TimeSpan.FromSeconds(300));

//    //    // set cache variables
//    //    var _updateCacheTimer = new Timer(CacheData, null, new TimeSpan(0, 0, 5), TimeSpan.FromSeconds(300));

//    //    _dicts = _context.Dicts.Include(d => d.DictOptions).ToList();

//    //    await _cache.Set("dicts", _dicts, _minutesToCache);

//    //}


//    //private void GetDataToCache(object? state)
//    //{
//    //    _dicts = _context.Dicts.Include(d => d.DictOptions).ToList();
//    //}

//    //private void CacheData(object? state)
//    //{
//    //    await _cache.SetAsync("dicts",_dicts, _minutesToCache);
//    //}
//}
