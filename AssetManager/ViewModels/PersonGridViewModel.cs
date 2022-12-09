using AssetManager.DTOs;

namespace AssetManager.ViewModels;

public class PersonGridViewModel
{
    public PersonGridViewModel()
    {
        People = new();
    }
    public List<PersonDisplayDto> People { get; set; }
    public int PageCount { get; set; }
    public int CurrentPage { get; set; }
}
