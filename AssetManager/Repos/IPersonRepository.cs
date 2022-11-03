using AssetManager.DTOs;
using AssetManager.Models;

namespace AssetManager.Repos
{
    public interface IPersonRepository
    {
        Person? GetPersonById(int id);
        void Create(PersonCreateDto submission);
        void Update(PersonCreateDto submission);
        void Delete(int id);
    }
}