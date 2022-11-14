﻿using AssetManager.DTOs;
using AssetManager.Models;

namespace AssetManager.Repos
{
    public interface IPersonRepository
    {
        IEnumerable<PersonDisplayDto> GetAll();
        Person? GetPersonById(int id);
        void Create(PersonCreateDto submission);
        void Update(PersonCreateDto submission);
        void Delete(int id);
        void RemoveAssetMap(int personId, int assetId);
        void AddAssetMap(int personId, int assetId);
    }
}