using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KhumaloCraft_Part2.Data;
using KhumaloCraft_Part2.Models;
using System.Security.Claims;

namespace KhumaloCraft_Part2.Controllers
{
    public class CartController : Controller
    {
        private readonly KhumaloCraft_Part2Context _context;

        public CartController(KhumaloCraft_Part2Context context)
        {
            _context = context;
        }

        // GET: Cart
        // Display the user's cart items
        public async Task<IActionResult> Index()
        {
            // Retrieve the user's email from the claims
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

           
            var cartItems = await _context.CartItem
                
                .Where(ci => ci.UserEmail == userEmail)
                .ToListAsync();

            return View(cartItems);
        }

        // GET: Cart/Details/5
        // Display details of a specific cart item
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find the cart item by product ID
            var cartItem = await _context.CartItem
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: Cart/Create
        // Display the form to create a new cart item
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        // Handle the creation of a new cart item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Price,Quantity")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cartItem);
        }

        // GET: Cart/Edit/5
        // Display the form to edit an existing cart item
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find the cart item by ID
            var cartItem = await _context.CartItem.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            return View(cartItem);
        }

        // POST: Cart/Edit/5
        // Handle the editing of an existing cart item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, int quantity)
        {
            // Find the cart item by ID
            var cartItem = await _context.CartItem.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            // Update the quantity of the cart item
            cartItem.Quantity = quantity;

            _context.Update(cartItem);
            await _context.SaveChangesAsync();

            // Update corresponding order in order history
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.UserId == userId && o.ProductId == id);

            if (order != null)
            {
                order.Quantity = quantity;
                _context.Update(order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Cart/Delete/5
        // Display the form to confirm deletion of a cart item
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find the cart item by product ID
            var cartItem = await _context.CartItem
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: Cart/Checkout
        // Display the checkout page with a thank you message
        public IActionResult Checkout()
        {
            TempData["Message"] = "Thank you for your purchase!";
            return RedirectToAction("Index", "Home");
        }

        // POST: Cart/Delete/5
        // Handle the deletion of a cart item
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartItem = await _context.CartItem.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItem.Remove(cartItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Check if a cart item exists
        private bool CartItemExists(int id)
        {
            return _context.CartItem.Any(e => e.ProductId == id);
        }

        // POST: Cart/DeliveryMethod
        // Redirect to the delivery method view
        [HttpPost]
        public IActionResult DeliveryMethod()
        {
            return RedirectToAction("ShowDeliveryMethod");
        }

        // GET: Cart/ShowDeliveryMethod
        // Render the delivery method view
        public IActionResult ShowDeliveryMethod()
        {
            return View();
        }

        // POST: Cart/ProcessDeliveryMethod
        // Process the selected delivery method and address
        [HttpPost]
        public IActionResult ProcessDeliveryMethod(string deliveryMethod, string address)
        {
            TempData["DeliveryMethod"] = deliveryMethod;
            TempData["Address"] = address;

            return RedirectToAction("Payment");
        }

        // GET: Cart/Payment
        // Render the payment view
        public IActionResult Payment()
        {
            return View();
        }

        // POST: Cart/ProcessPayment
        // Process the payment method and card number
        [HttpPost]
        public IActionResult ProcessPayment(string paymentMethod, string cardNumber)
        {
            TempData["PaymentMethod"] = paymentMethod;
            TempData["CardNumber"] = cardNumber;

            return RedirectToAction("PaymentConfirmation");
        }

        // GET: Cart/PaymentConfirmation
        // Display the payment confirmation page with order details
        public IActionResult PaymentConfirmation()
        {
            var deliveryMethod = TempData["DeliveryMethod"] as string;
            var address = TempData["Address"] as string;
            var paymentMethod = TempData["PaymentMethod"] as string;
            var cardNumber = TempData["CardNumber"] as string;

            // Retrieve cart items for the user
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var cartItems = _context.CartItem
                .Where(ci => ci.UserEmail == userEmail)
                .ToList();

            // Calculate total price
            decimal totalPrice = cartItems.Sum(ci => ci.Price * ci.Quantity);

            // Pass the necessary data to the view
            ViewBag.DeliveryMethod = deliveryMethod;
            ViewBag.Address = address;
            ViewBag.PaymentMethod = paymentMethod;
            ViewBag.CardNumber = cardNumber;
            ViewBag.TotalPrice = totalPrice;
            ViewBag.CartItems = cartItems;

            return View();
        }
    }
}