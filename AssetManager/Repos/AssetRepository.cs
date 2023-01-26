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

    public List<Asset> GetAllAssets()
    {
        return _context.Assets.Include(a => a.Person).ToList();
    }

    public AssetGridViewModel GetPageOfAssets(int pageSize, int pageNumber)
    {        
        var vm = new AssetGridViewModel();

        vm.CurrentPage = pageNumber;

        int assetCount = _context.Assets.Count();
        vm.PageCount = (assetCount + pageSize -1) / pageSize;

        var assetQuery = _context.Assets.Include(a => a.Person)
                                        .Include(a => a.AssetType)
                                        .Include(a => a.Model)
                                        .Include(a => a.Site);

        vm.Assets = assetQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return vm;
    }

    public Asset GetAssetById(int id)
    {
        if (id > 0)
        {
            var asset = _context.Assets.Include(a => a.Person)
                                        .Include(a => a.AssetType)
                                        .Include(a => a.Model)
                                        .Include(a => a.Site)
                                        .FirstOrDefault(a => a.AssetId == id);

            if (asset != null)
            {
                return asset;
            }    
        }
        
        throw new InvalidOperationException();
        
    }

    public List<Asset> GetAssetsByPersonId(int personId)
    {
        return _context.Assets.Include(a => a.Person)
                              .Include(a => a.AssetType)
                              .Include(a => a.Model)
                              .Include(a => a.Site).Where(a => a.PersonId == personId).ToList();
    }   

    public int Create(Asset asset)
    {
        if (asset.AssetId != 0)
            throw new InvalidOperationException();

        _context.Assets.Add(asset);
        Save();

        return asset.AssetId;
    }

    public void CreateAssets(IEnumerable<Asset> assets)
    {
        if (assets.Any())
        {
            _context.AddRange(assets) ;
            Save();
        }
    }

    public void Update(Asset updatedAsset)
    {
        var asset = _context.Assets.Find(updatedAsset.AssetId);

        if (asset != null)
        {
            asset.AssetTypeId = updatedAsset.AssetTypeId;
            asset.ModelId = updatedAsset.ModelId;
            asset.SiteId = updatedAsset.SiteId;
            asset.PersonId = updatedAsset.PersonId;

            Save();
        }
    }

    public void Delete(int id)
    {
        var asset = _context.Assets.Find(id);

        if (asset != null)
        {
            _context.Assets.Remove(asset);
            Save();
        }
            
    }

    private void Save()
    {
        _context.SaveChanges();
    }

}
