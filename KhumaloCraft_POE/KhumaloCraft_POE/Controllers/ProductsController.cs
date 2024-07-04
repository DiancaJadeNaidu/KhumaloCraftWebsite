using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KhumaloCraft_Part2.Data;
using KhumaloCraft_Part2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KhumaloCraft_Part2.Controllers
{
    // Ensures that all actions in this controller require authorization by default
    [Authorize]
    public class ProductsController : Controller
    {
        // Dependency injection for the database context
        private readonly KhumaloCraft_Part2Context _context;

        // Constructor to initialize the database context
        public ProductsController(KhumaloCraft_Part2Context context)
        {
            _context = context;
        }

        // GET: Products
        // Displays a list of all products
        public IActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            ViewData["PriceSortParm"] = sortOrder == "PriceAsc" ? "PriceDesc" : "PriceAsc";

            if (searchString != null)
            {
                
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var products = from p in _context.Products
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString)
                                       || s.Category.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "NameDesc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "PriceAsc":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "PriceDesc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }

            return View(products.ToList());
        }

        // GET: Products/Details/5
        // Displays the details of a specific product
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        // Displays the product creation form
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // Handles the form submission for creating a new product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Name,Price,Category,Availability,ImageUrl")] Products products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Products/Edit/5
        // Displays the product editing form
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // POST: Products/Edit/5
        // Handles the form submission for editing an existing product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,Name,Price,Category,Availability,ImageUrl")] Products products)
        {
            if (id != products.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductID))
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
            return View(products);
        }

        // GET: Products/Delete/5
        // Displays the product deletion confirmation page
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        // Handles the product deletion confirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Checks if a product exists by its ID
        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }

        // GET: Products/OrderHistory
        // Displays the order history for the current user
        [Authorize]
        public async Task<IActionResult> OrderHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            IEnumerable<KhumaloCraft_Part2.Models.Orders> orders;

            if (userEmail == "admin@gmail.com")
            {
                orders = (from o in _context.Orders
                          join p in _context.Products on o.ProductId equals p.ProductID
                          select new KhumaloCraft_Part2.Models.Orders
                          {
                              OrderId = o.OrderId,
                              ProductId = o.ProductId,
                              Quantity = o.Quantity,
                              OrderDate = o.OrderDate,
                              Status = o.Status,
                              UserEmail = o.UserEmail,
                              ProductName = p.Name
                          });
            }
            else
            {
                orders = (from o in _context.Orders
                          join p in _context.Products on o.ProductId equals p.ProductID
                          where o.UserId == userId
                          select new KhumaloCraft_Part2.Models.Orders
                          {
                              OrderId = o.OrderId,
                              ProductId = o.ProductId,
                              Quantity = o.Quantity,
                              OrderDate = o.OrderDate,
                              Status = o.Status,
                              UserEmail = o.UserEmail,
                              ProductName = p.Name
                          });
            }
         
            return View(orders);
        }

        // POST: Products/UpdateStatus
        // Updates the status of an order
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int orderId, string status)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail != "admin@gmail.com")
            {
                return Forbid();
            }

            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = status;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(OrderHistory));
        }

        // POST: Products/PlaceOrder
        // Places an order for a product
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                TempData["ErrorMessage"] = "Quantity must be greater than 0";
                return RedirectToAction("Index");
            }

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

          

            decimal totalAmount = product.Price * quantity;
            bool freeDelivery = totalAmount >= 100;
            bool applyDiscount = quantity >= 3;
            decimal discountedAmount = applyDiscount ? totalAmount * 0.8m : totalAmount;

            var order = new KhumaloCraft_Part2.Models.Orders
            {
                ProductId = productId,
                Quantity = quantity,
                UserId = userId,
                UserEmail = userEmail,
                OrderDate = DateTime.Now,
                Status = "Pending"
            };

            _context.Orders.Add(order);

            var cartItem = new CartItem
            {
                ProductName = product.Name,
                Price = product.Price,
                Quantity = quantity,
                UserEmail = userEmail
            };
            _context.CartItem.Add(cartItem);

            await _context.SaveChangesAsync();

            TempData["CartMessage"] = "Order added to cart!";
            return RedirectToAction("Index");
        }
    }
}