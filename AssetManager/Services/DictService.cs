using AssetManager.Repos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssetManager.Services;

public class DictService : IDictService
{
    private readonly IDictRepository _repository;

    public DictService(IDictRepository repository)
    {
        _repository = repository;
    }

    public List<SelectListItem> GetDictItems(int dictId)
    {
        return _repository.GetDictItems(dictId);
    }
}