using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using OraclePrimavera.Models;
using System.Diagnostics;

namespace OraclePrimavera.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProject(Project project)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}