﻿using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.Repos;

namespace AssetManager.Services;

public class AssetService : IAssetService
{
    private IAssetRepository _assetRepository;
    
    public AssetService(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }
    
    public List<AssetDisplayDto> GetAllAssets()
    {
        return _assetRepository.GetAllAssets();
    }
    
    public Asset? GetAssetById(int id)
    {
        return _assetRepository.GetAssetById(id);
    }

    public void Create(AssetAddEditDto asset)
    {
        _assetRepository.Create(asset);
    }
    
    public void Delete(int id)
    {
        _assetRepository.Delete(id);
    }

    public void Update(AssetAddEditDto asset)
    {
        _assetRepository.Update(asset);
    }
    
    public AssetDisplayDto? GetAssetDisplayDtoById(int id)
    {
        return null;
    }
}