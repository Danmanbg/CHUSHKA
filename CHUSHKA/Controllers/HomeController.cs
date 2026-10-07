using CHUSHKA.Data;
using CHUSHKA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CHUSHKA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        // POST: Home/Order/5
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Order(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var order = new Order
            {
                ProductId = product.Id,
                ClientId = user.Id,
                OrderedOn = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Product '{product.Name}' ordered.";
            return RedirectToAction(nameof(Index));
        }

    }
}
