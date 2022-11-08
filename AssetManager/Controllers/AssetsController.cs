using AssetManager.DTOs;
using AssetManager.Repos;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers
{
    public class AssetsController : Controller
    {
        private IAssetRepository _repository;

        public AssetsController(IAssetRepository repository)
        {
            _repository = repository;
        }

        //GET: Assets
        public IActionResult Index()
        {
            List<AssetDisplayDto> list = _repository.GetAll();

            return View(list);
        }
        //GET: Assets/AddEdit/{id}
        public IActionResult AddEdit(int id)
        {
            if (id == 0)
            {
                return PartialView("_AddEditModal");
            }
            else
            {
                var asset = _repository.GetAssetById(id);

                if (asset == null) return PartialView("_AddEditModal");

                AssetAddEditDto dto = new()
                {
                    AssetId = asset.AssetId,
                    AssetType = asset.AssetType,
                    Model = asset.Model,
                    Site = asset.Site,
                    PersonId = asset.PersonId
                };

                return PartialView("_AddEditModal",dto);
            }
        }

        // POST: Assets/AddEdit
        [HttpPost]
        public IActionResult AddEdit(AssetAddEditDto asset)
        {
            if(asset.AssetId > 0)
            {
                _repository.Update(asset);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _repository.Create(asset);
                return RedirectToAction(nameof(Index));
            }
        }

        // DELETE: Assets/Delete/{id}
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
