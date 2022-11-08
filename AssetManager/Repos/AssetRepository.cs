using AssetManager.Data;
using AssetManager.DTOs;
using AssetManager.Models;

namespace AssetManager.Repos;

public class AssetRepository : IAssetRepository
{
    private IDataStore _context { get; }

    public AssetRepository(IDataStore dataStore)
    {
        _context = dataStore;
    }

    public List<AssetDisplayDto> GetAll()
    {
        List<AssetDisplayDto> list = _context.Assets.Select(a => new AssetDisplayDto()
                                                    {
                                                        AssetId = a.AssetId,
                                                        AssetType = a.AssetType,
                                                        Model = a.Model,
                                                        Site = a.Site,
                                                        PersonId = a.PersonId
                                                    }).ToList();

        return list;
    }

    public Asset? GetAssetById(int id)
    {
        if (id > 0)
        {
            var asset = _context.Assets.Where(a => a.AssetId == id).FirstOrDefault();

            return asset;
        }
        else
        {
            return null;
        }
    }

    public void Create(AssetAddEditDto asset)
    {
        Asset a = new() { 
            AssetId = _context.Assets.Max(a => a.AssetId) + 1,
            AssetType = asset.AssetType,
            Model = asset.Model,
            Site = asset.Site,
            PersonId = asset.PersonId,
        };

        _context.Assets.Add(a);
    }

    public void Update(AssetAddEditDto asset)
    {
        Asset? a = _context.Assets.FirstOrDefault(a => a.AssetId == asset.AssetId);

        if (a != null)
        {
            int n = _context.Assets.IndexOf(a);

            a.AssetType = asset.AssetType;
            a.Model = asset.Model;
            a.Site = asset.Site;
            a.PersonId = asset.PersonId;

            _context.Assets[n] = a;
        }
    }

    public void Delete(int id)
    {
        Asset? asset = _context.Assets.FirstOrDefault(a => a.AssetId == id);

        if (asset != null)
            _context.Assets.Remove(asset);
    }

}
