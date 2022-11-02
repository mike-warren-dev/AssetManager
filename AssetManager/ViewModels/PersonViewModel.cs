using AssetManager.Models;
using AssetManager.DTOs;

namespace AssetManager.ViewModels;

public class PersonViewModel
{
    public IEnumerable<Person> People { get; set; } = null!;
    public PersonCreateDto NewPerson { get; set; } = null!;
}
