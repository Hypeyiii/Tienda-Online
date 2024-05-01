using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tienda_Online.Controllers;
using Tienda_Online.Models;
using Tienda_Online.ViewModels;

namespace Tienda.Controllers;

public class AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : Controller
{
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            //login
            var result = await signInManager.PasswordSignInAsync(model.Username!, model.Password!, true, true);

            if (result.Succeeded)
            {
                return RedirectToLocal("/Home/Account");
            }

            ModelState.AddModelError("", "Las credenciales no coinciden");
        }
        return View(model);
    }

    public IActionResult Register(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            ApplicationUser user = new()
            {
                UserName = model.Name,
                FirstName = model.FirstName!,
                LastName = model.LastName!,
                Email = model.Email,
                Address = model.Address!,
                City = model.City!,
                Id = Guid.NewGuid().ToString()
            };

            var result = await userManager.CreateAsync(user, model.Password!);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);

                return RedirectToLocal("/");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        return !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? Redirect(returnUrl)
            : RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
    }
}