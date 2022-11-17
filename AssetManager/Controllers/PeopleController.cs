﻿using AssetManager.Data;
using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.Repos;
using AssetManager.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetManager.Controllers;

public class PeopleController : Controller
{
    private IPeopleService _peopleService;
    private IAssetService _assetService;

    public PeopleController(ILogger<PeopleController> logger, IPersonRepository repository, IPeopleService peopleService, IAssetService assetService)
    {
        _peopleService = peopleService;
        _assetService = assetService;
    }
    
    public ActionResult Index()
    {
        IEnumerable<PersonDisplayDto> list = _peopleService.GetAllPeople();

        return View(list);
    }

    public ActionResult AddEdit(int id)
    {
        PersonCreateDto dto = new();

        if (id == 0)
        {
            return PartialView("_AddEditPersonModal");
        }
        else
        {
            try
            {
                dto = _peopleService.GetPersonCreateDtoById(id);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

            return PartialView("_AddEditPersonModal", dto);
        }
    }

    [HttpPost] //[ValidateAntiForgeryToken]
    public ActionResult AddEdit([Bind("PersonId,FirstName,LastName,Email,RoleId")]PersonCreateDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (dto.PersonId == null)
                {
                    _peopleService.Create(dto);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _peopleService.Update(dto);

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

    [HttpDelete]
    public ActionResult Delete(int id)
    {
        _peopleService.Delete(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public ActionResult RemoveAsset(int personId, int assetId)
    {
        
        _peopleService.RemoveAssetMap(personId, assetId);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public ActionResult MapAsset(int personId, int assetId)
    {
        Person? person = _peopleService.GetPersonById(personId);
        Asset? asset = _assetService.GetAssetById(assetId);

        if (person == null) return BadRequest("The Person is not valid.");

        if (asset == null) return BadRequest("Enter a valid Asset ID");

        if (asset.PersonId != null)
        { 
            Person? otherPerson = _peopleService.GetPersonById((int)asset.PersonId);

            return BadRequest($"The Asset is already mapped to {otherPerson.FirstName} {otherPerson.LastName}");
        }

        if (person.Assets.Contains(asset) == true) return BadRequest($"The Asset is already mapped.");

        _peopleService.AddAssetMap(personId, assetId);

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
