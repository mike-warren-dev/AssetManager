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
    //[ValidateAntiForgeryToken]
    public ActionResult Create(PersonViewModel submission)
    {
        try
        {
            //return RedirectToAction(nameof(Index));

            Person person = new()
            {
                PersonId = _context.People.Max(p => p.PersonId) + 1,
                ExternalId = submission.NewPerson.ExternalId == null ? Guid.NewGuid().ToString() : submission.ExternalId,
                FirstName = submission.NewPerson.FirstName,
                LastName = submission.NewPerson.LastName,
                Email = submission.NewPerson.Email,
                RoleId = submission.NewPerson.RoleId
            };
            Debug.WriteLine("before:");
            Debug.WriteLine(_context.People.Count);
            _context.People.Add(person);
            Debug.WriteLine("after:");
            Debug.WriteLine(_context.People.Count);

            //return CreatedAtRoute(); //how do I actually do this, though? 
            return Ok();
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
