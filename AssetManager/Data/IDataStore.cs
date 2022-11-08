using AssetManager.Models;

namespace AssetManager.Data;

public interface IDataStore
{
    List<Person> People { get; set; }

    List<Asset> Assets { get; set; }
}