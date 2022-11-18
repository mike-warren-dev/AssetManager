using AssetManager.DTOs;
using AssetManager.Models;

namespace AssetManager.ViewModels
{
    public class AssetAddEditViewModel
    {
        public AssetAddEditDto AssetDto { get; set; } = null!;
        public IEnumerable<PersonDisplayDto> People { get; set; } = null!;
    }
}
