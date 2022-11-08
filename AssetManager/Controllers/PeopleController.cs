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
    
    // GET: People/
    public ActionResult Index()
    {
        IEnumerable<PersonDisplayDto> list = _repository.GetAll();

        return View(list);
    }

    // GET: People/Details/5
    public ActionResult Details(int id)
    {
        return View();
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

}
