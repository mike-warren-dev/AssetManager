using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.Repos;
using AssetManager.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetManager.Controllers
{
    public class AssetsController : Controller
    {
        private IAssetService _assetService;

        public AssetsController(IAssetRepository repository, IAssetService assetService)
        {
            _assetService = assetService;
        }

        public IActionResult Index()
        {
            List<AssetDisplayDto> list = _assetService.GetAllAssets();

            return View(list);
        }

        [HttpGet]
        public IActionResult AddEdit(int id)
        {
            if (id == 0)
            {
                return PartialView("_AddEditAssetModal");
            }
            else
            {
                var asset = _assetService.GetAssetById(id);

                if (asset == null) return PartialView("_AddEditAssetModal");

                AssetAddEditDto dto = new()
                {
                    AssetId = asset.AssetId,
                    AssetType = asset.AssetType,
                    Model = asset.Model,
                    Site = asset.Site,
                    PersonId = asset.PersonId
                };

                return PartialView("_AddEditAssetModal",dto);
            }
        }

        [HttpPost]
        public IActionResult AddEdit(AssetAddEditDto asset)
        {
            if(asset.AssetId > 0)
            {
                _assetService.Update(asset);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _assetService.Create(asset);
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
