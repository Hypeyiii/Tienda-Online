using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tienda_Online.Controllers;
using Tienda_Online.Models;
using Tienda_Online.Services;
using Tienda_Online.ViewModels;

namespace Tienda.Controllers;

public class AccountController : BaseController
{
    public AccountController(ApplicationDbContext context) : base(context)
    {
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task <IActionResult> Register(User User)
    {
        try
        {
            if(User != null)
            {
                if(await _context.User.AnyAsync(u => u.UserName == User.UserName))
                {
                    ModelState.AddModelError("UserName", "Username already exists");
                    return View(User);
                }
                _context.User.Add(User);
                await _context.SaveChangesAsync();

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, User.UserName));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier , User.UserID.ToString()));


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return RedirectToAction("Index", "Home");
            }
            return View(User);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(User);
        }
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        if(User.Identity !=null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login (string UserName, string password)
    {
        try
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == UserName && u.Password == password);
            if(user != null)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
}