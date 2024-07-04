using KhumaloCraft_Part2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KhumaloCraft_Part2.Controllers
{
    // Ensures that all actions in this controller require authorization by default
   // [Authorize]
    public class HomeController : Controller
    {
        // Logger instance to log information, warnings, and errors
        private readonly ILogger<HomeController> _logger;

        // Constructor to initialize the logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: Home/Index
        // Displays the home page
        [AllowAnonymous]
        public IActionResult Index()
        {

            return View();
        }

        // GET: Home/Contact
        // Displays the contact page
        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }

        // GET: Home/About
        // Displays the about page
        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        // GET: Home/Products
        // Displays the products page
        public IActionResult Products()
        {
            return View();
        }

        // GET: Home/Error
        // Displays the error page with detailed information
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Creates an instance of ErrorViewModel with the current request ID or trace identifier
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Home/Login
        // Displays the login page, allowing anonymous access
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
    }
}
