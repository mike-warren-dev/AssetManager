using AssetManager.Models;
using AssetManager.Repos;
using AssetManager.Services;
using AssetManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers;

[Authorize(Roles = "Admin, Basic")]
public class AssetsController : Controller
{
    private IAssetService _assetService;
    private IPeopleService _peopleService;
    private IDictService _dictService;

    public AssetsController(IAssetRepository repository, IAssetService assetService, IPeopleService peopleService, IDictService dictService)
    {
        _assetService = assetService;
        _peopleService = peopleService;
        _dictService = dictService;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        AssetGridViewModel vm = await _assetService.GetPageOfAssets(1);

        return View(vm);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("Assets/GetPageOfAssets/{pageNumber}")]
    public async Task<IActionResult> GetPageOfAssets(int pageNumber)
    {
        AssetGridViewModel vm = await _assetService.GetPageOfAssets(pageNumber);

        return PartialView("_GridPartial", vm);
    }

    [Authorize(Roles = "Admin, Basic")]
    [HttpGet]
    public async Task<IActionResult> MyAssets()
    {
        var personId = this.User.Claims.FirstOrDefault(c => c.Type == "PersonId")?.Value;

        if (personId == null)
        {
            return View(new List<Asset>());
        }
        else
        {
            List<Asset> list = await _assetService.GetAssetsByPersonId(int.Parse(personId));
            return View(list);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> AddEdit(int id)
    {
        AssetAddEditViewModel vm = new();

        vm.People = await _peopleService.GetAllPeople();
        vm.AssetTypeOptions = _dictService.GetDictItems(101);
        vm.ModelOptions = _dictService.GetDictItems(102);
        vm.SiteOptions = _dictService.GetDictItems(103);

        if (id == 0)
            return PartialView("_AddEditAssetModal", vm);
        
        try
        {
            vm.Asset = await _assetService.GetAssetById(id);
        }
        catch (ArgumentException)
        {
            return BadRequest("The Person is not valid.");
        }
            
            
        return PartialView("_AddEditAssetModal", vm);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddEdit(AssetAddEditViewModel vm)
    {
        int assetId;

        if(vm.Asset.AssetId == 0)
        {
            assetId = await _assetService.Create(vm.Asset);
        }
        else
        {
            await _assetService.Update(vm.Asset);

            assetId = vm.Asset.AssetId;
        }

        var asset = await _assetService.GetAssetById(assetId);

        if (asset == null)
            return BadRequest();

        return PartialView("_RowPartial", asset);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _assetService.Delete(id);

        return RedirectToAction(nameof(Index));
    }

}
