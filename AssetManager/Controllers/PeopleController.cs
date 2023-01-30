using AssetManager.Models;
using AssetManager.Repos;
using AssetManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetManager.Controllers;

[Authorize(Roles = "Admin")]
public class PeopleController : Controller
{
    private IPeopleService _peopleService;
    private IAssetService _assetService;

    public PeopleController(ILogger<PeopleController> logger, IPersonRepository repository, IPeopleService peopleService, IAssetService assetService)
    {
        _peopleService = peopleService;
        _assetService = assetService;
    }

    public async Task<ActionResult> Index()
    {
        return View(await _peopleService.GetPageOfPeople(1));
    }

    [HttpGet("People/GetPageOfPeople/{pageNumber}")]
    public async Task<ActionResult> GetPageOfPeople(int pageNumber)
    {
        return PartialView("_GridPartial", await _peopleService.GetPageOfPeople(pageNumber));
    }

    [HttpGet]
    public async Task<ActionResult> GetPersonOptions()
    {
        return PartialView("_PersonOptions", await _peopleService.GetAllPeople());
    }

    [HttpGet]
    public async Task<ActionResult> AddEdit(int id)
    {

        if (id == 0)
            return PartialView("_AddEditPersonModal");

        Person person;

        try
        {
            person = await _peopleService.GetPersonById(id);
        }
        catch (ArgumentException) 
        {
            return BadRequest("Something went wrong. Please refresh and try again.");
        }

        return PartialView("_AddEditPersonModal", person);
        
    }

    [HttpPost] //[ValidateAntiForgeryToken]
    public async Task<ActionResult> AddEdit([Bind("PersonId,FirstName,LastName,Email")]Person person)
    {
        if (person.PersonId == 0)
        {
            person.PersonId = await _peopleService.Create(person);

            return PartialView("_RowPartial", person);
        }
        
        await _peopleService.Update(person);

        return PartialView("_RowPartial", person);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        await _peopleService.Delete(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<ActionResult> RemoveAsset(int personId, int assetId)
    {
        
        await _peopleService.RemoveAssetMap(personId, assetId);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<ActionResult> MapAsset(int personId, int assetId)
    {
        if (assetId == 0)
            return BadRequest("Enter a valid Asset ID");

        Person person;

        try
        {
            person = await _peopleService.GetPersonById(personId);                
        }
        catch (ArgumentException)
        {
            return BadRequest("The Person is not valid.");
        }
        
        var asset = _assetService.GetAssetById(assetId);

        if (asset == null) 
            return BadRequest("Enter a valid Asset ID");

        if (asset.PersonId != null)
        {
            Person otherPerson;

            try
            {
                otherPerson = await _peopleService.GetPersonById((int)asset.PersonId);
            }
            catch
            {
                return BadRequest($"The Asset is already mapped.");
            }

            return BadRequest($"The Asset is already mapped to {otherPerson.FirstName} {otherPerson.LastName}");    
        }

        if (person.Assets.Contains(asset) == true) 
            return BadRequest($"The Asset is already mapped.");

        try
        {
            await _peopleService.AddAssetMap(personId, assetId);
        }
        catch (ArgumentException)
        {
            return BadRequest($"Something went wrong. Please refresh and try again.");
        }
        
        return Ok(JsonConvert.SerializeObject(asset));
    }
}
