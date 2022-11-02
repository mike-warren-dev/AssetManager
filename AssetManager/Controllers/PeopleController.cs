using AssetManager.Data;
using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;

namespace AssetManager.Controllers;

public class PeopleController : Controller
{
    private readonly ILogger<PeopleController> _logger;
    private IDataStore _context;

    public PeopleController(ILogger<PeopleController> logger, IDataStore context)
    {
        _logger = logger;
        _context = context;
    }
    
    // GET: PeopleController
    public ActionResult Index()
    {
        PersonViewModel vm = new();

        vm.People = _context.People.ToList();
        vm.NewPerson = new PersonCreateDto();

        return View(vm);
    }

    // GET: PeopleController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: PeopleController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: PeopleController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(PersonCreateDto submission)
    {
        try
        {
            Person person = new()
            {
                PersonId = _context.People.Max(p => p.PersonId) + 1,
                FirstName = submission.FirstName,
                LastName = submission.LastName,
                Email = submission.Email,
                RoleId = submission.RoleId
            };

            _context.People.Add(person);

            //should I be doing this? 
            //return CreatedAtRoute();

            //this is reloading the whole grid... 
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: PeopleController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: PeopleController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: PeopleController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: PeopleController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
