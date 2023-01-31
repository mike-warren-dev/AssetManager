using AssetManager.Data;
using AssetManager.Models;
using AssetManager.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Repos;

public class AssetRepository : IAssetRepository
{
    private AssetManagerContext _context;

    public AssetRepository(AssetManagerContext repoContext)
    {
        _context = repoContext;
    }

    public async Task<List<Asset>> GetAllAssets()
    {
        return await _context.Assets.Include(a => a.Person).ToListAsync();
    }

    public async Task<AssetGridViewModel> GetPageOfAssets(int pageSize, int pageNumber)
    {
        AssetGridViewModel vm = new();

        vm.CurrentPage = pageNumber;

        int assetCount = _context.Assets.Count();
        vm.PageCount = (assetCount + pageSize -1) / pageSize;

        vm.Assets = await _context.Assets.Include(a => a.Person)
                                         .Include(a => a.AssetType)
                                         .Include(a => a.Model)
                                         .Include(a => a.Site)
                                         .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                                         .ToListAsync();
        
        return vm;
    }

    public async Task<Asset> GetAssetById(int id)
    {
        if (id == 0) 
            throw new ArgumentException();
        
        var asset = await _context.Assets.Include(a => a.Person)
                                         .Include(a => a.AssetType)
                                         .Include(a => a.Model)
                                         .Include(a => a.Site)
                                         .FirstOrDefaultAsync(a => a.AssetId == id);

        if (asset == null) 
            throw new ArgumentException();

        return asset;
    }

    public async Task<List<Asset>> GetAssetsByPersonId(int personId)
    {
        return await _context.Assets.Include(a => a.Person)
                                    .Include(a => a.AssetType)
                                    .Include(a => a.Model)
                                    .Include(a => a.Site).Where(a => a.PersonId == personId)
                                    .ToListAsync();
    }   

    public async Task<int> Create(Asset asset)
    {
        if (asset.AssetId != 0)
            throw new InvalidOperationException();

        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();

        return asset.AssetId;
    }

    public async Task CreateAssets(IEnumerable<Asset> assets)
    {
        if (assets.Any())
        {
            _context.AddRange(assets) ;
            await _context.SaveChangesAsync();
        }
    }

    public async Task Update(Asset updatedAsset)
    {
        var asset = await _context.Assets.FindAsync(updatedAsset.AssetId);

        if (asset != null)
        {
            asset.AssetTypeId = updatedAsset.AssetTypeId;
            asset.ModelId = updatedAsset.ModelId;
            asset.SiteId = updatedAsset.SiteId;
            asset.PersonId = updatedAsset.PersonId;

            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var asset = await _context.Assets.FindAsync(id);

        if (asset != null)
        {
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
        }
    }
}
