﻿using Microsoft.AspNetCore.Mvc;
using AssetManager.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Identity;

namespace AssetManager.Controllers;

public class AccountsController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<LoginModel> _logger;

    public AccountsController(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("Login");
    }

    public IActionResult Login()
    
    {
        return View(new LoginModel(_signInManager, _logger));
    }

    public IActionResult Register()
    {
        return View();
    }
}
