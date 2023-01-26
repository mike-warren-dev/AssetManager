using AssetManager.Models;
using AssetManager.Repos;
using AssetManager.Services;
using AssetManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.Design;

namespace AssetManager.Controllers
{
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
        public IActionResult Index()
        {
            AssetGridViewModel vm = _assetService.GetPageOfAssets(1);

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Assets/GetPageOfAssets/{pageNumber}")]
        public IActionResult GetPageOfAssets(int pageNumber)
        {
            AssetGridViewModel vm = _assetService.GetPageOfAssets(pageNumber);

            return PartialView("_GridPartial", vm);
        }

        [Authorize(Roles = "Admin, Basic")]
        [HttpGet]
        public IActionResult MyAssets()
        {
            var personId = this.User.Claims.FirstOrDefault(c => c.Type == "PersonId")?.Value;

            if (personId == null)
            {
                return View(new List<Asset>());
            }
            else
            {
                List<Asset> list = _assetService.GetAssetsByPersonId(int.Parse(personId));
                return View(list);
            }
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddEdit(int id)
        {
            AssetAddEditViewModel vm = new();
            vm.People = _peopleService.GetAllPeople();
            vm.AssetTypeOptions = _dictService.GetDictItems(101);
            vm.ModelOptions = _dictService.GetDictItems(102);
            vm.SiteOptions = _dictService.GetDictItems(103);

            if (id == 0)
            {
                return PartialView("_AddEditAssetModal", vm);
            }
            else
            {
                var asset = _assetService.GetAssetById(id);

                if (asset == null)
                {
                    return PartialView("_AddEditAssetModal", vm);
                }

                vm.Asset = asset;

                return PartialView("_AddEditAssetModal", vm);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddEdit(AssetAddEditViewModel vm)
        {
            int assetId;

            if(vm.Asset.AssetId == 0)
            {
                assetId = _assetService.Create(vm.Asset);
            }
            else
            {
                _assetService.Update(vm.Asset);
                assetId = vm.Asset.AssetId;
            }

            var asset = _assetService.GetAssetById(assetId);

            if (asset == null)
                return BadRequest();

            return PartialView("_RowPartial", asset);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _assetService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
