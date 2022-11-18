using AssetManager.Data;
using AssetManager.DTOs;
using AssetManager.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Repos;

public class AssetRepository : IAssetRepository
{
    private AssetManagerContext _context;

    public AssetRepository(AssetManagerContext repoContext)
    {
        _context = repoContext;
    }

    public List<AssetDisplayDto> GetAllAssets()
    {
        List<AssetDisplayDto> list = _context.Assets.Include(a => a.Person).Select(a => new AssetDisplayDto()
                                    {
                                        AssetId = a.AssetId,
                                        AssetType = a.AssetType.ToString(),
                                        Model = a.Model.ToString(),
                                        Site = a.Site.ToString(),
                                        PersonId = a.PersonId,
                                        PersonName = a.Person != null ? $"{a.Person.FirstName} {a.Person.LastName}" : ""
                                    }).ToList();

        return list;
    }

    public Asset? GetAssetById(int id)
    {
        if (id > 0)
        {
            var asset = _context.Assets.Include(a => a.Person).FirstOrDefault(a => a.AssetId == id);

            return asset;
        }
        else
        {
            return null;
        }
    }

    public AssetDisplayDto? GetAssetDisplayDtoById(int id)
    {
        if (id > 0)
        {
            var asset = _context.Assets.Find(id);

            return new AssetDisplayDto()
            {
                AssetId = asset.AssetId,
                AssetType = asset.AssetType.ToString(),
                Model = asset.Model.ToString(),
                Site = asset.Site.ToString(),
                PersonId = asset.PersonId
            };
        }
        else
        {
            return null;
        }

    }    

    public void Create(AssetAddEditDto dto)
    {
        Asset asset = new() { 
            AssetType = dto.AssetType,
            Model = dto.Model,
            Site = dto.Site,
            PersonId = dto.PersonId,
        };

        _context.Assets.Add(asset);
        Save();
    }

    public void CreateAssets(IEnumerable<AssetAddEditDto> dtos)
    {
        if (dtos.Any())
        {
            _context.AddRange(dtos.Select(d => new Asset()
                                        {
                                            AssetType = d.AssetType,
                                            Model = d.Model,
                                            Site = d.Site,
                                            PersonId = d.PersonId
                                        })) ;
            Save();
        }
    }

    public void Update(AssetAddEditDto dto)
    {
        Asset? asset = _context.Assets.Find(dto.AssetId);

        if (asset != null)
        {
            asset.AssetType = dto.AssetType;
            asset.Model = dto.Model;
            asset.Site = dto.Site;
            asset.PersonId = dto.PersonId;

            Save();
        }
    }

    public void Delete(int id)
    {
        Asset? asset = _context.Assets.Find(id);

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
