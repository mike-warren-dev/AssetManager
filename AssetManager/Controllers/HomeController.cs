using AssetManager.Data;
using AssetManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssetManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IDataStore _context;

    public HomeController(ILogger<HomeController> logger, IDataStore context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

}