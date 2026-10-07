using CHUSHKA.Data;
using CHUSHKA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CHUSHKA.Controllers
{
    [Authorize] // require authenticated users for product list/details/order
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // User and Admin can see products; we'll allow both authenticated roles
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // Only logged-in users with role "User" (not Admin) place orders.
        [Authorize(Roles = "User")]
        [HttpPost]
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
