using AssetManager.Data;
using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.Repos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetManager.Controllers;

public class PeopleController : Controller
{
    private readonly ILogger<PeopleController> _logger;
    private IPersonRepository _repository;
    private IDataStore _context;

    public PeopleController(ILogger<PeopleController> logger, IDataStore context, IPersonRepository repository)
    {
        _logger = logger;
        _context = context;
        _repository = repository;
    }
    
    // GET: People/
    public ActionResult Index()
    {
        IEnumerable<PersonDisplayDto> list = _repository.GetAll();

        return View(list);
    }

    // GET: People/Create
    public ActionResult AddEdit(int id)
    {
        PersonCreateDto? person = new();

        if (id == 0)
        {
            return PartialView("_AddEditPersonModal");
        }
        else
        {
            Person? p = _repository.GetPersonById(id);
            
            if (p != null)
            {
                person.PersonId = p.PersonId;
                person.FirstName = p.FirstName;
                person.LastName = p.LastName;
                person.Email = p.Email;
                person.RoleId = p.RoleId;
                person.Assets = p.Assets;
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

            return PartialView("_AddEditPersonModal", person);
        }
        
    }

    // POST: People/AddEdit
    [HttpPost]
    //[ValidateAntiForgeryToken]
    public ActionResult AddEdit([Bind("PersonId,FirstName,LastName,Email,RoleId")]PersonCreateDto submission)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (submission.PersonId == null)
                {
                    _repository.Create(submission);

                    //this is reloading the whole grid!
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _repository.Update(submission);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }
        else
        {
            return RedirectToAction(nameof(Index));
        }
    }

    // DELETE: People/Delete/5
    [HttpDelete]
    public ActionResult Delete(int id)
    {
        _repository.Delete(id);

        return RedirectToAction(nameof(Index));
    }

    // DELETE: People/RemoveAsset
    [HttpPost]
    public ActionResult RemoveAsset(int personId, int assetId)
    {
        _repository.RemoveAssetMap(personId, assetId);

        return RedirectToAction(nameof(Index));
    }

    // DELETE: People/MapAsset
    [HttpPost]
    public ActionResult MapAsset(int personId, int assetId)
    {
        Person? person = _repository.GetPersonById(personId);
        Asset? asset = _context.Assets.FirstOrDefault(a => a.AssetId == assetId);
        

        if (person == null) return BadRequest("The Person is not valid.");

        if (asset == null) return BadRequest("Enter a valid Asset ID");

        if (asset.PersonId != null)
        {
            Person? otherPerson = _repository.GetPersonById((int)asset.PersonId);

            if (otherPerson != null)
            {
                return BadRequest($"The Asset is already mapped to {otherPerson.FirstName} {otherPerson.LastName}");
            }
            else
            {
                return BadRequest($"The Asset is already mapped.");
            }   
        }

        if (person.Assets.Contains(asset) == true) return BadRequest($"The Asset is already mapped.");

        _repository.AddAssetMap(personId, assetId);

        var dto = new AssetDisplayDto()
        {
            AssetId = asset.AssetId,
            AssetType = asset.AssetType.ToString(),
            Model = asset.Model.ToString(),
            Site = asset.Site.ToString(),
            PersonId = asset.PersonId
        };

        return Ok(JsonConvert.SerializeObject(dto));
    }
}
