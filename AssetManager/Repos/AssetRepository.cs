using AssetManager.Data;
using AssetManager.DTOs;
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

    public List<AssetDisplayDto> GetAllAssets()
    {
        List<AssetDisplayDto> list = _context.Assets.Include(a => a.Person).Select(a => new AssetDisplayDto()
                                    {
                                        AssetId = a.AssetId,
                                        AssetTypeId = a.AssetTypeId,
                                        AssetType = a.AssetType.DisplayName,
                                        Model = a.Model.DisplayName,
                                        Site = a.Site.DisplayName,
                                        PersonId = a.PersonId,
                                        PersonName = a.Person != null ? $"{a.Person.FirstName} {a.Person.LastName}" : ""
                                    }).ToList();

        return list;
    }

    public AssetGridViewModel GetPageOfAssets(int pageSize, int pageNumber)
    {        
        AssetGridViewModel vm = new();

        vm.CurrentPage = pageNumber;

        int assetCount = _context.Assets.Count();
        vm.PageCount = (assetCount + pageSize -1) / pageSize;

        var assetQuery = _context.Assets.Include(a => a.Person).Select(a => new AssetDisplayDto()
        {
            AssetId = a.AssetId,
            AssetTypeId = a.AssetTypeId,
            AssetType = a.AssetType.DisplayName,
            Model = a.Model.DisplayName,
            Site = a.Site.DisplayName,
            PersonId = a.PersonId,
            PersonName = a.Person != null ? $"{a.Person.FirstName} {a.Person.LastName}" : ""
        });//.ToList()

        vm.Assets = assetQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return vm;
    }

    public Asset? GetAssetById(int id)
    {
        if (id > 0)
        {
            var asset = _context.Assets.Include(a => a.Person)
                                        .Include(a => a.AssetType)
                                        .Include(a => a.Model)
                                        .Include(a => a.Site)
                                        .FirstOrDefault(a => a.AssetId == id);

            return asset;
        }
        else
        {
            return null;
        }
    }

    public List<AssetDisplayDto> GetAssetsByPersonId(int personId)
    {
        return _context.Assets.Where(a => a.PersonId == personId).Select(asset => new AssetDisplayDto()
        {
            AssetId = asset.AssetId,
            AssetTypeId = asset.AssetTypeId,
            AssetType = asset.AssetType.DisplayName,
            Model = asset.Model.DisplayName,
            Site = asset.Site.DisplayName,
            PersonId = asset.PersonId
        }).ToList();
    }
    public AssetDisplayDto? GetAssetDisplayDtoById(int id)
    {
        if (id > 0)
        {
            var dto = _context.Assets.Include(a => a.Person).Select(a => new AssetDisplayDto()
            {
                AssetId = a.AssetId,
                AssetTypeId = a.AssetTypeId,
                AssetType = a.AssetType.DisplayName,
                Model = a.Model.DisplayName,
                Site = a.Site.DisplayName,
                PersonId = a.PersonId,
                PersonName = a.Person != null ? $"{a.Person.FirstName} {a.Person.LastName}" : ""
            }).FirstOrDefault(a => a.AssetId == id);

            return dto;
        }
        else
        {
            return null;
        }

    }    

    public int Create(AssetAddEditDto dto)
    {
        Asset asset = new() { 
            AssetTypeId = dto.AssetTypeId,
            ModelId = dto.ModelId,
            SiteId = dto.SiteId,
            PersonId = dto.PersonId,
        };

        _context.Assets.Add(asset);
        Save();

        return asset.AssetId;
    }

    public void CreateAssets(IEnumerable<AssetAddEditDto> dtos)
    {
        if (dtos.Any())
        {
            _context.AddRange(dtos.Select(d => new Asset()
                                    {
                                        AssetTypeId = d.AssetTypeId,
                                        ModelId = d.ModelId,
                                        SiteId = d.SiteId,
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
            asset.AssetTypeId = dto.AssetTypeId;
            asset.ModelId = dto.ModelId;
            asset.SiteId = dto.SiteId;
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
