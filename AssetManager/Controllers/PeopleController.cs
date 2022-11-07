﻿using AssetManager.Data;
using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.Repos;
using Microsoft.AspNetCore.Mvc;


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
    
    // GET: PeopleController
    public ActionResult Index()
    {
        List<PersonDisplayDto> list = _context.People.Select(p => new PersonDisplayDto {
                                                                PersonId = p.PersonId,
                                                                FirstName = p.FirstName,
                                                                LastName = p.LastName,
                                                                Email = p.Email,
                                                                RoleId = p.RoleId}).ToList();

        return View(list);
    }

    // GET: PeopleController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: PeopleController/Create
    public ActionResult AddEdit(int id)
    {
        PersonCreateDto? person = new();

        if (id == 0)
        {
            return PartialView("_CreateEditModal");
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
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

            return PartialView("_CreateEditModal", person);
        }
        
    }

    // POST: PeopleController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddEdit(PersonCreateDto submission)
    {
        if (submission == null) return View();

        try
        {
            if (submission.PersonId == 0) 
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

    // DELETE: PeopleController/Delete/5
    [HttpDelete]
    public ActionResult Delete(int id)
    {
        _repository.Delete(id);

        return RedirectToAction(nameof(Index));
    }

}
