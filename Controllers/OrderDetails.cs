using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tienda_Online.Models;
using Tienda_Online.Services;

namespace Tienda_Online.Controllers
{
    public class OrderDetails : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderDetails(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderDetails.Include(o => o.Order).Include(o => o.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order_Details = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.IdOrder == id);
            if (order_Details == null)
            {
                return NotFound();
            }

            return View(order_Details);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "State");
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "ProductDescription");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrder,IdProduct,Quantity,Price")] Order_Details order_Details)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order_Details);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "State", order_Details.IdOrder);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "ProductDescription", order_Details.IdProduct);
            return View(order_Details);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order_Details = await _context.OrderDetails.FindAsync(id);
            if (order_Details == null)
            {
                return NotFound();
            }
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "State", order_Details.IdOrder);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "ProductDescription", order_Details.IdProduct);
            return View(order_Details);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrder,IdProduct,Quantity,Price")] Order_Details order_Details)
        {
            if (id != order_Details.IdOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order_Details);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Order_DetailsExists(order_Details.IdOrder))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "State", order_Details.IdOrder);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "ProductDescription", order_Details.IdProduct);
            return View(order_Details);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order_Details = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.IdOrder == id);
            if (order_Details == null)
            {
                return NotFound();
            }

            return View(order_Details);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order_Details = await _context.OrderDetails.FindAsync(id);
            if (order_Details != null)
            {
                _context.OrderDetails.Remove(order_Details);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Order_DetailsExists(int id)
        {
            return _context.OrderDetails.Any(e => e.IdOrder == id);
        }
    }
}
