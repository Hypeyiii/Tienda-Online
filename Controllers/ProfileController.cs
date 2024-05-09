using Microsoft.AspNetCore.Mvc;
using Tienda_Online.Services;
using Microsoft.EntityFrameworkCore;
using Tienda.Controllers;
using Tienda_Online.Models;

namespace Tienda_Online
{
    public class ProfileController : BaseController
    {
        public ProfileController(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var user = await _context.User.Include(u => u.Location).Include(u => u.Orders).FirstOrDefaultAsync(u => u.UserID == id);

                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.UserID == id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
        }

        public IActionResult AddLocation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLocation(Location location, int id)
        {
            try
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.UserID == id);
                if (user != null)
                {
                    location.User = user;
                    _context.Add(location);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { id });
                }
            }
            catch (System.Exception)
            {
                return View(location);
            }

            return RedirectToAction("Details", new { id });
        }
    }
}