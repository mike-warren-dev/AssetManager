using AssetManager.DTOs;
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
            //List<AssetDisplayDto> list = _assetService.GetAllAssets();
            AssetGridViewModel vm = _assetService.GetPageOfAssets(1);

            //return View(list);
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Assets/GetPageOfAssets/{pageNumber}")]
        public IActionResult GetPageOfAssets(int pageNumber)
        {
            //List<AssetDisplayDto> list = _assetService.GetAllAssets();
            AssetGridViewModel vm = _assetService.GetPageOfAssets(pageNumber);

            //return View(list);
            return PartialView("_GridPartial", vm);
        }

        [Authorize(Roles = "Admin, Basic")]
        [HttpGet]
        public IActionResult MyAssets()
        {
            var personId = this.User.Claims.FirstOrDefault(c => c.Type == "PersonId")?.Value;

            if (personId == null)
            {
                return View(new List<AssetDisplayDto>());
            }
            else
            {
                List<AssetDisplayDto> list = _assetService.GetAssetsByPersonId(int.Parse(personId));
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

                if (asset == null) return PartialView("_AddEditAssetModal", vm);                

                vm.AssetDto = new AssetAddEditDto()
                {
                    AssetId = asset.AssetId,
                    AssetTypeId = asset.AssetTypeId,
                    //AssetType = asset.AssetType.DisplayName,
                    ModelId = asset.ModelId,
                    SiteId = asset.SiteId,
                    PersonId = asset.PersonId,
                    PersonName = asset.Person != null ? $"{asset.Person.FirstName} {asset.Person.LastName}" : ""
                };

                return PartialView("_AddEditAssetModal", vm);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddEdit(AssetAddEditViewModel vm)
        {
            int assetId;

            if(vm.AssetDto.AssetId == 0)
            {
                assetId = _assetService.Create(vm.AssetDto);
            }
            else
            {
                _assetService.Update(vm.AssetDto);
                assetId = vm.AssetDto.AssetId;
            }

            var dto = _assetService.GetAssetDisplayDtoById(assetId);
            return PartialView("_RowPartial", dto);
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
