using AssetManager.Models;
using AssetManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.ContentModel;

namespace AssetManager.Controllers;

[Authorize(Roles = "Admin")]
public class PeopleController : Controller
{
    private readonly IPeopleService _peopleService;
    private readonly IAssetService _assetService;

    public PeopleController(IPeopleService peopleService, IAssetService assetService)
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

        return PartialView("_AddEditPersonModal", await _peopleService.GetPersonById(id));
                   
    }

    [HttpPost] //[ValidateAntiForgeryToken]
    public async Task<ActionResult> AddEdit([Bind("PersonId,FirstName,LastName,Email")]Person person)
    {
        if (person.PersonId == 0)
        {
            person.PersonId = await _peopleService.Create(person);

            return PartialView("_RowPartial", person);
        }
        else
        {
            await _peopleService.Update(person);

            return PartialView("_RowPartial", person);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        await _peopleService.Delete(id);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> RemoveAsset(int personId, int assetId)
    {
        await _peopleService.RemoveAssetMap(personId, assetId);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> MapAsset(int personId, int assetId)
    {
        try
        {
            return Ok(JsonConvert.SerializeObject(await _peopleService.MapAssetToPerson(personId, assetId)));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
