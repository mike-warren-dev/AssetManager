using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.Repos;
using AssetManager.Services;
using AssetManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AssetManager.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AssetsController : Controller
    {
        private IAssetService _assetService;
        private IPeopleService _peopleService;

        public AssetsController(IAssetRepository repository, IAssetService assetService, IPeopleService peopleService)
        {
            _assetService = assetService;
            _peopleService = peopleService;
        }

        public IActionResult Index()
        {
            List<AssetDisplayDto> list = _assetService.GetAllAssets();

            return View(list);
        }

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

        [HttpGet]
        public IActionResult AddEdit(int id)
        {
            AssetAddEditViewModel vm = new();
            vm.People = _peopleService.GetAllPeople();

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
                    AssetType = asset.AssetType,
                    Model = asset.Model,
                    Site = asset.Site,
                    PersonId = asset.PersonId,
                    PersonName = asset.Person != null ? $"{asset.Person.FirstName} {asset.Person.LastName}" : ""
                };

                return PartialView("_AddEditAssetModal", vm);
            }
        }

        [HttpPost]
        public IActionResult AddEdit(AssetAddEditViewModel vm)
        {
            if(vm.AssetDto.AssetId > 0)
            {
                _assetService.Update(vm.AssetDto);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _assetService.Create(vm.AssetDto);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _assetService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
