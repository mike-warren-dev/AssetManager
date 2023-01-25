using AssetManager.Models;

namespace AssetManager.ViewModels;

public class PersonGridViewModel
{
    public PersonGridViewModel()
    {
        People = new();
    }
    public List<Person> People { get; set; }
    public int PageCount { get; set; }
    public int CurrentPage { get; set; }
}
